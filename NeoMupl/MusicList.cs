﻿#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NeoMupl.Player;

namespace NeoMupl
{
    public class MusicList : Dictionary<string, MusicData>
    {
        #region プロパティ

        #endregion

        #region コンストラクタ

        public MusicList() { }

        #endregion

        #region メソッド

        /// <summary>曲データを追加する</summary>
        /// <param name="data">追加するデータ</param>
        public void Add(MusicData data) { try { Add(data.FileName, data); } catch (Exception) { } }

        /// <summary>曲データを追加する</summary>
        /// <param name="fileName">追加するファイル</param>
        public void Add(string fileName, List<ExtensionRule> extensionRules)
        {
            if (fileName.EndsWith("NeoMupl.nmp"))
            {
                ImportMPVB(fileName);
            }
            else
            {
                try { Add(fileName, new MusicData(fileName, extensionRules)); }
                catch (Exception) { }
            }
        }

        /// <summary>曲データを追加する</summary>
        /// <param name="files">追加するファイル群</param>
        public void Add(string[] files, List<ExtensionRule> extensionRules) { foreach (string file in files) Add(file, extensionRules); }

        /// <summary>曲データを置換する</summary>
        /// <param name="oldData">古いデータ</param>
        /// <param name="newData">新しいデータ</param>
        public void Set(string oldFileName, MusicData newData)
        {
            if (oldFileName != newData.FileName) Remove(oldFileName);
            this[newData.FileName] = newData;
        }

        public void SetPort(MusicData data, string port)
        {
            if (!(data.Option is DMOption dm)) data.Option = dm = new DMOption();
            dm.port = port;
        }

        // TODO: 実際にリセットするべきなのでは？(#13)
#pragma warning disable IDE0060 // 未使用のパラメーターを削除します
        public void SetReset(MusicData data, bool reset)
#pragma warning restore IDE0060 // 未使用のパラメーターを削除します
        {
#pragma warning disable IDE0059 // 値の不必要な代入
            if (!(data.Option is DMOption dm)) data.Option = dm = new DMOption();
#pragma warning restore IDE0059 // 値の不必要な代入
            // TODO: そしてここでリセットをかけたい
        }

        public void Load(string listFile)
        {
            try
            {
                using StreamReader sr = new StreamReader(listFile);
                string line = sr.ReadLine().Trim();
                string[] elem;
                MusicData? data = null;
                if (line == "NeoMupl015")
                {
                    // C#版データじゃー！
                    Clear();
                    while ((line = sr.ReadLine()) != null)
                    {
                        elem = line.Trim().Split('\t');
                        if (elem.Length == 0) continue;
                        if (elem[0] == "File")
                        {
                            Add(data = new MusicData(elem[1]));
                            continue;
                        }
                        if (data == null)
                        {
                            continue;
                        }
                        switch (elem[0])
                        {
                            case "Title": data.Title = elem[1]; break;
                            case "Volume": data.Volume = double.Parse(elem[1]); break;
                            case "Loop": data.LoopStart = double.Parse(elem[1]); data.LoopEnd = double.Parse(elem[2]); break;
                            case "SkipRate": data.SkipRate = double.Parse(elem[1]); break;
                            case "PlayMethod": data.PlayMethod = (PlayMethod)int.Parse(elem[1]); break;
                            case "MIDIPort": SetPort(data, elem[1]); break;
                            case "MIDIReset": SetReset(data, Boolean.Parse(elem[1])); break;
                            case "LastPlayed": data.LastPlayedTicks = long.Parse(elem[1]); break;
                            default: break;
                        }
                    }
                }
                // VB版データは別口で読む
            }
            catch (Exception) { }
        }

        public void Save(string listFile)
        {
            using StreamWriter sw = new StreamWriter(listFile);
            sw.WriteLine("NeoMupl015");
            MusicData def = new MusicData("");
            foreach (MusicData data in this.Values)
            {
                // デフォルト値が確定しているものについてはわざわざ記録しない
                sw.WriteLine("File\t" + data.FileName);
                sw.WriteLine("Title\t" + data.Title);   // ←デフォルトを設定で変更可能
                if (data.Volume != def.Volume) sw.WriteLine("Volume\t" + data.Volume.ToString());
                if (data.LoopStart > 0 || data.LoopEnd > 0) sw.WriteLine("Loop\t" + data.LoopStart.ToString() + "\t" + data.LoopEnd.ToString());
                if (data.SkipRate != def.SkipRate) sw.WriteLine("SkipRate\t" + data.SkipRate.ToString());
                sw.WriteLine("PlayMethod\t" + ((int)data.PlayMethod).ToString());   // ←デフォルトがMIDIとそれ以外で異なる
                try
                {
                    if (data.Option is DMOption dm && dm.port != "") sw.WriteLine("MIDIPort\t" + dm.port);
                }
                catch (Exception) { }
                if (data.LastPlayedTicks != def.LastPlayedTicks) sw.WriteLine("LastPlayed\t" + data.LastPlayedTicks.ToString());
            }
        }

        private void ImportMPVB(string fileName)
        {
            try
            {
                using StreamReader sr = new StreamReader(fileName, Encoding.GetEncoding("shift_jis"));
                string line = sr.ReadLine().Trim();
                string[] elem;
                MusicData? data = null;
                if (line != null && line.StartsWith("Count="))
                {
                    // VB版データじゃー！
                    elem = line.Trim().Split('=');
                    MusicData[] buf = new MusicData[int.Parse(elem[1])];
                    int offset = 0;
                    while ((line = sr.ReadLine()) != null)
                    {
                        elem = line.Trim().Split('=');
                        if (elem.Length == 0) continue;
                        if (elem[0].StartsWith("FileName")) data = buf[offset++] = new MusicData(elem[1]);
                        if (data == null) continue;
                        else if (elem[0].StartsWith("Title")) data.Title = elem[1];
                        else if (elem[0].StartsWith("Volume")) data.Volume = double.Parse(elem[1]);
                        else if (elem[0].StartsWith("LoopStart")) data.LoopStart = double.Parse(elem[1]);
                        else if (elem[0].StartsWith("LoopEnd")) data.LoopEnd = double.Parse(elem[1]);
                        else if (elem[0].StartsWith("SkipRate")) data.SkipRate = double.Parse(elem[1]);
                        else if (elem[0].StartsWith("LastPlayed")) data.LastPlayedDateTime = new DateTime(1899, 12, 30).AddDays(double.Parse(elem[1]));
                        else if (elem[0].StartsWith("<0>")) foreach (MusicData md in buf) md.FileName = md.FileName.Replace("<0>", elem[1]);
                        else if (elem[0].StartsWith("<1>")) foreach (MusicData md in buf) md.FileName = md.FileName.Replace("<1>", elem[1]);
                        else if (elem[0].StartsWith("<2>")) foreach (MusicData md in buf) md.FileName = md.FileName.Replace("<2>", elem[1]);
                        else if (elem[0].StartsWith("<3>")) foreach (MusicData md in buf) md.FileName = md.FileName.Replace("<3>", elem[1]);
                        else if (elem[0].StartsWith("<4>")) foreach (MusicData md in buf) md.FileName = md.FileName.Replace("<4>", elem[1]);
                        else if (elem[0].StartsWith("<5>")) foreach (MusicData md in buf) md.FileName = md.FileName.Replace("<5>", elem[1]);
                        else if (elem[0].StartsWith("<6>")) foreach (MusicData md in buf) md.FileName = md.FileName.Replace("<6>", elem[1]);
                        else if (elem[0].StartsWith("<7>")) foreach (MusicData md in buf) md.FileName = md.FileName.Replace("<7>", elem[1]);
                        else if (elem[0].StartsWith("<8>")) foreach (MusicData md in buf) md.FileName = md.FileName.Replace("<8>", elem[1]);
                        else if (elem[0].StartsWith("<9>")) foreach (MusicData md in buf) md.FileName = md.FileName.Replace("<9>", elem[1]);
                    }
                    foreach (MusicData md in buf) Add(md);
                }
            }
            catch (Exception) { }
        }

        #endregion

    }
}
