#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using MifuminLib;

namespace NeoMupl
{
    public class Setting
    {
        private readonly string saveFile = Path.Combine(Application.StartupPath, "NeoMupl.ini");

        public string ListFile { get; set; } = Path.Combine(Application.StartupPath, "NeoMupl.nmp");
        public double MinPlayTime { get; set; } = 10;
        public FinishAction FinishAction { get; set; } = FinishAction.Stop;
        public Sorting Sorting { get; set; } = Sorting.FileName;
        public bool Reversed { get; set; } = false;
        public string Port { get; set; } = "Default";

        public string TitlePattern
        {
            get { return MusicData.Pattern; }
            set { MusicData.Pattern = value; }
        }

        public double TimeWeight { get; set; } = 1.0;
        public bool ShowStatus { get; set; } = true;
        public List<StatusItem> StatusItems { get; set; } = new List<StatusItem>();
        public bool EraseLogOnExit { get; set; }
        public string LogFile { get; set; } = Path.Combine(Application.StartupPath, "NeoMupl.log");
        public bool PlayLog { get; set; }
        public bool StopLog { get; set; }
        public bool ErrorLog { get; set; }
        public bool IgnoreTimerError { get; set; }
        public bool ReportException { get; set; }
        public int MainLeft { get; set; }
        public int MainTop { get; set; }
        public int MainWidth { get; set; }
        public int MainHeight { get; set; }
        public FormWindowState MainWindowState { get; set; }
        public string WindowTitlePattern { get; set; } = "<StatusJ> <Title> - NeoMupl";

        public Setting()
        {
            Load(saveFile);
        }
        public void Save()
        {
            Save(saveFile);
        }
        public void SaveAnotherName()
        {
            Save(PathMaker.GetAnotherName(saveFile));
        }

        private void Save(string fileName)
        {
            PropertyInfo[] pil = GetType().GetProperties();
            using StreamWriter sw = new StreamWriter(fileName);
            foreach (PropertyInfo pi in pil)
            {
                if (pi.PropertyType.IsEnum)
                {
                    sw.WriteLine($"{pi.Name}\t{(int)pi.GetValue(this, null)}");
                }
                else if (Type.GetTypeCode(pi.PropertyType) != TypeCode.Object)
                {
                    sw.WriteLine($"{pi.Name}\t{pi.GetValue(this, null)}");
                }
                else
                {
                    if (pi.PropertyType == StatusItems.GetType())
                    {
                        foreach (StatusItem si in (List<StatusItem>)pi.GetValue(this, null))
                        {
                            sw.WriteLine($"{pi.Name}\t{si}");
                        }
                    }
                    else
                    {
                        sw.WriteLine($"{pi.Name}\t{pi.GetValue(this, null)}");
                    }
                }
            }
        }

        private void Load(string fileName)
        {
            if (File.Exists(fileName))
            {
                string err = "";
                try
                {
                    using StreamReader sr = new StreamReader(fileName);
                    string line;
                    string[] elem;
                    while ((line = sr.ReadLine()) != null)
                    {
                        elem = line.Split('\t');
                        if (elem.Length != 2) continue;
                        if (elem[0] == "StatusItem") elem[0] = "StatusItems";
                        if (elem[0] == "Logging") elem[0] = "PlayLog";
                        PropertyInfo pi = GetType().GetProperty(elem[0]);
                        if (pi == null)
                        {
                            err += $"存在しない設定項目「{elem[0]}」を読み込もうとしました。\n";
                            continue;
                        }
                        object? value = null;
                        try
                        {
                            switch (Type.GetTypeCode(pi.PropertyType))
                            {
                                case TypeCode.Boolean:
                                    value = bool.Parse(elem[1]);
                                    break;
                                case TypeCode.Byte:
                                    value = byte.Parse(elem[1]);
                                    break;
                                case TypeCode.Char:
                                    value = char.Parse(elem[1]);
                                    break;
                                case TypeCode.DBNull:
                                    err += $"読み込みに対応していない設定「{elem[0]}」を読み込もうとしました。\n";
                                    continue;
                                case TypeCode.DateTime:
                                    value = DateTime.Parse(elem[1]);
                                    break;
                                case TypeCode.Decimal:
                                    value = decimal.Parse(elem[1]);
                                    break;
                                case TypeCode.Double:
                                    value = double.Parse(elem[1]);
                                    break;
                                case TypeCode.Empty:
                                    err += $"読み込みに対応していない設定「{elem[0]}」を読み込もうとしました。\n";
                                    continue;
                                case TypeCode.Int16:
                                    value = short.Parse(elem[1]);
                                    break;
                                case TypeCode.Int32:
                                    value = int.Parse(elem[1]);
                                    break;
                                case TypeCode.Int64:
                                    value = long.Parse(elem[1]);
                                    break;
                                case TypeCode.Object:
                                    //if (pi.PropertyType == StatusItems.GetType())
                                    if (pi.PropertyType == typeof(List<StatusItem>))
                                    {
                                        ((List<StatusItem>)pi.GetValue(this, null)).Add(StatusItem.Parse(elem[1]));
                                    }
                                    else
                                    {
                                        err += $"読み込みに対応していない設定「{elem[0]}」を読み込もうとしました。\n";
                                    }
                                    continue;
                                case TypeCode.SByte:
                                    value = sbyte.Parse(elem[1]);
                                    break;
                                case TypeCode.Single:
                                    value = float.Parse(elem[1]);
                                    break;
                                case TypeCode.String:
                                    value = elem[1];
                                    break;
                                case TypeCode.UInt16:
                                    value = ushort.Parse(elem[1]);
                                    break;
                                case TypeCode.UInt32:
                                    value = uint.Parse(elem[1]);
                                    break;
                                case TypeCode.UInt64:
                                    value = ulong.Parse(elem[1]);
                                    break;
                                default:
                                    err += $"読み込みに対応していない設定「{elem[0]}」を読み込もうとしました。\n";
                                    continue;
                            }
                            pi.SetValue(this, value, null);
                        }
                        catch (Exception e)
                        {
                            err += $"設定「{elem[0]}」の値「{elem[1]}」が以下のエラーのため認識できませんでした。\n　メッセージ：{e.Message}\n";
                        }
                    }
                }
                catch (Exception e)
                {
                    err += $"設定読み込み中に以下のエラーが発生しました。\n　メッセージ：{e.Message}\n";
                }
                if (err.Length > 0) MessageBox.Show(err);
            }
            if (StatusItems.Count == 0)
            {
                StatusItems.Add(new StatusItemFullPathName());
                StatusItems.Add(new StatusItemPlayGauge());
            }
        }
    }
}
