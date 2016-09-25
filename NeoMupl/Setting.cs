using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using MifuminLib;
using System.Reflection;

namespace NeoMupl
{
    public class Setting
    {
        private string saveFile = Path.Combine(Application.StartupPath, "NeoMupl.ini");

        private string myListFile = Path.Combine(Application.StartupPath, "NeoMupl.nmp");
        public string ListFile
        {
            get { return myListFile; }
            set { myListFile = value; }
        }

        private double myMinPlayTime = 10;
        public double MinPlayTime
        {
            get { return myMinPlayTime; }
            set { myMinPlayTime = value; }
        }

        private FinishAction myFinishAction = FinishAction.Stop;
        public FinishAction FinishAction
        {
            get { return myFinishAction; }
            set { myFinishAction = value; }
        }

        private Sorting mySorting = Sorting.FileName;
        public Sorting Sorting
        {
            get { return mySorting; }
            set { mySorting = value; }
        }

        private bool myReversed = false;
        public bool Reversed
        {
            get { return myReversed; }
            set { myReversed = value; }
        }

        private string myPort = "Default";
        public string Port
        {
            get { return myPort; }
            set { myPort = value; }
        }

        public string TitlePattern
        {
            get { return MusicData.Pattern; }
            set { MusicData.Pattern = value; }
        }

        private double myTimeWeight = 1.0;
        public double TimeWeight
        {
            get { return myTimeWeight; }
            set { myTimeWeight = value; }
        }

        private bool myShowStatus = true;
        public bool ShowStatus
        {
            get { return myShowStatus; }
            set { myShowStatus = value; }
        }

        private List<StatusItem> myStatusItems = new List<StatusItem>();
        public List<StatusItem> StatusItems
        {
            get { return myStatusItems; }
            set { myStatusItems = value; }
        }

        private bool myEraseLogOnExit;
        public bool EraseLogOnExit
        {
            get { return myEraseLogOnExit; }
            set { myEraseLogOnExit = value; }
        }

        private string myLogFile = Path.Combine(Application.StartupPath, "NeoMupl.log");
        public string LogFile
        {
            get { return myLogFile; }
            set { myLogFile = value; }
        }
        
        private bool myPlayLog;
        public bool PlayLog
        {
            get { return myPlayLog; }
            set { myPlayLog = value; }
        }

        private bool myStopLog;
        public bool StopLog
        {
            get { return myStopLog; }
            set { myStopLog = value; }
        }

        private bool myErrorLog;
        public bool ErrorLog
        {
            get { return myErrorLog; }
            set { myErrorLog = value; }
        }

        private bool myIgnoreTimerError;
        public bool IgnoreTimerError
        {
            get { return myIgnoreTimerError; }
            set { myIgnoreTimerError = value; }
        }

        private bool myReportException;
        public bool ReportException
        {
            get { return myReportException; }
            set { myReportException = value; }
        }

        private int myMainLeft;
        public int MainLeft
        {
            get { return myMainLeft; }
            set { myMainLeft = value; }
        }

        private int myMainTop;
        public int MainTop
        {
            get { return myMainTop; }
            set { myMainTop = value; }
        }

        private int myMainWidth;
        public int MainWidth
        {
            get { return myMainWidth; }
            set { myMainWidth = value; }
        }

        private int myMainHeight;
        public int MainHeight
        {
            get { return myMainHeight; }
            set { myMainHeight = value; }
        }

        private FormWindowState myMainWindowState;
        public FormWindowState MainWindowState
        {
            get { return myMainWindowState; }
            set { myMainWindowState = value; }
        }

        private string myWindowTitlePattern = "<StatusJ> <Title> - NeoMupl";
        public string WindowTitlePattern
        {
            get { return myWindowTitlePattern; }
            set { myWindowTitlePattern = value; }
        }
	
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
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                foreach (PropertyInfo pi in pil)
                {
                    if (pi.PropertyType.IsEnum)
                    {
                        sw.WriteLine(pi.Name + "\t" + ((int)pi.GetValue(this, null)).ToString());
                    }
                    else if (Type.GetTypeCode(pi.PropertyType) != TypeCode.Object)
                    {
                        sw.WriteLine(pi.Name + "\t" + pi.GetValue(this, null).ToString());
                    }
                    else
                    {
                        if (pi.PropertyType == StatusItems.GetType())
                        {
                            foreach (StatusItem si in (List<StatusItem>)pi.GetValue(this, null))
                            {
                                sw.WriteLine(pi.Name + "\t" + si.ToString());
                            }
                        }
                        else
                        {
                            sw.WriteLine(pi.Name + "\t" + pi.GetValue(this, null).ToString());
                        }
                    }
                }
            }
        }

        private void Load(string fileName)
        {
            string err = "";
            try
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
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
                            err += "存在しない設定項目「" + elem[0] + "」を読み込もうとしました。\n";
                            continue;
                        }
                        object value = null;
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
                                    err += "読み込みに対応していない設定「" + elem[0] + "」を読み込もうとしました。\n";
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
                                    err += "読み込みに対応していない設定「" + elem[0] + "」を読み込もうとしました。\n";
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
                                        err += "読み込みに対応していない設定「" + elem[0] + "」を読み込もうとしました。\n";
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
                                    err += "読み込みに対応していない設定「" + elem[0] + "」を読み込もうとしました。\n";
                                    continue;
                            }
                            pi.SetValue(this, value, null);
                        }
                        catch (Exception e)
                        {
                            err += "設定「" + elem[0] + "」の値「" + elem[1] + "」が以下のエラーのため認識できませんでした。\n　メッセージ：" + e.Message + "\n";
                        }
                    }
                }
            }
            catch (Exception e)
            {
                err += "設定読み込み中に以下のエラーが発生しました。\n　メッセージ：" + e.Message + "\n";
            }
            if (err.Length > 0) MessageBox.Show(err);
            if (myStatusItems.Count == 0)
            {
                myStatusItems.Add(new StatusItemFullPathName());
                myStatusItems.Add(new StatusItemPlayGauge());
            }
        }
    }
}
