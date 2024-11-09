#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NeoMupl.Player;

namespace NeoMupl
{
    public class MusicData : ICloneable
    {
        #region 変数

        private static MusicData? defaultData = null;

        #endregion

        #region プロパティ

        /// <summary>再生するファイル名をフルパスで取得・設定します</summary>
        public string FileName { get; set; }
        public string Directory => Path.GetDirectoryName(FileName);
        public string FileTitle => Path.GetFileNameWithoutExtension(FileName);

        /// <summary>画面上に表示するためのタイトルを取得・設定します</summary>
        public string Title { get; set; }
        /// <summary>ボリュームを%単位で取得・設定します(50%が標準)</summary>
        public double Volume { get; set; }
        /// <summary>ループ開始位置を秒単位で取得・設定します</summary>
        public double LoopStart { get; set; }
        /// <summary>ループ終了位置を秒単位で取得・設定します</summary>
        public double LoopEnd { get; set; }
        /// <summary>ランダム再生でスキップされる確率を%単位で取得・設定します</summary>
        public double SkipRate { get; set; }
        /// <summary>再生方法を取得・設定します</summary>
        public PlayMethod PlayMethod { get; set; }
        public object? Option { get; set; }

        /// <summary>最後に再生された日時をTick単位で取得・設定します</summary>
        public long LastPlayedTicks { get; set; }
        /// <summary>最後に再生された日時をDateTimeで取得・設定します</summary>
        public DateTime LastPlayedDateTime
        {
            get => new DateTime(LastPlayedTicks);
            set => LastPlayedTicks = value.Ticks;
        }

        /// <summary>CreateTitleで使うパターンを取得・設定します</summary>
        public static string Pattern { get; set; } = "<fullpath>";

        #endregion

        #region コンストラクタ

        public MusicData(string fileName, List<ExtensionRule>? extensionRules = null)
        {
            Title = CreateTitle(FileName = fileName);
            Volume = 50;
            LoopStart = LoopEnd = SkipRate = 0;
            LastPlayedTicks = 0;
            PlayMethod = extensionRules?.FirstOrDefault(er => fileName.EndsWith(er.Extension, StringComparison.OrdinalIgnoreCase))?.PlayMethod
                ?? (fileName.EndsWith(".mid", true, null) ? PlayMethod.DirectMusic : PlayMethod.DirectShow);
            Option = null;
        }

        #endregion

        #region メソッド

        public object Clone()
        {
            var clone = (MusicData)MemberwiseClone();
            if (clone.Option is ICloneable option)
            {
                clone.Option = option.Clone();
            }
            return clone;
        }

        public void TimeStamp() { LastPlayedTicks = DateTime.Now.Ticks; }

        public override string ToString() { return Title; }

        public static string CreateTitle(string fileName)
        {
            string[] path = new string[10], rpath = new string[10], p = fileName.Split('\\', '/');
            for (int i = 0; i < 10; i++)
            {
                if (i < p.Length)
                {
                    path[i] = p[i];
                    rpath[i] = p[p.Length - 1 - i];
                }
                else
                {
                    path[i] = "";
                    rpath[i] = "";
                }
            }
            string purefilename = "", filetitle = "", directory = "";
            try { purefilename = Path.GetFileName(fileName); }
            catch (Exception) { }
            try { filetitle = Path.GetFileNameWithoutExtension(fileName); }
            catch (Exception) { }
            try { directory = Path.GetDirectoryName(fileName); }
            catch (Exception) { }
            return Pattern
                .Replace("<fullpath>", fileName)
                .Replace("<filename>", purefilename)
                .Replace("<filetitle>", filetitle)
                .Replace("<directory>", directory)
                .Replace("<0>", path[0]).Replace("<1>", path[1]).Replace("<2>", path[2]).Replace("<3>", path[3])
                .Replace("<4>", path[4]).Replace("<5>", path[5]).Replace("<6>", path[6]).Replace("<7>", path[7])
                .Replace("<8>", path[8]).Replace("<9>", path[9])
                .Replace("<-0>", rpath[0]).Replace("<-1>", rpath[1]).Replace("<-2>", rpath[2]).Replace("<-3>", rpath[3])
                .Replace("<-4>", rpath[4]).Replace("<-5>", rpath[5]).Replace("<-6>", rpath[6]).Replace("<-7>", rpath[7])
                .Replace("<-8>", rpath[8]).Replace("<-9>", rpath[9])
                ;
        }

        public void Dump(StreamWriter sw, MusicData data, bool full)
        {
            if (!full && defaultData == null) defaultData = new MusicData("");
            sw.WriteLine($"File\t{data.FileName}");
            sw.WriteLine($"Title\t{data.Title}");   // ←デフォルトを設定で変更可能
            if (full || data.Volume != defaultData?.Volume) sw.WriteLine($"Volume\t{data.Volume}");
            if (full || data.LoopStart > 0 || data.LoopEnd > 0) sw.WriteLine($"Loop\t{data.LoopStart}\t{data.LoopEnd}");
            if (full || data.SkipRate != defaultData?.SkipRate) sw.WriteLine($"SkipRate\t{data.SkipRate}");
            sw.WriteLine($"PlayMethod\t{(int)data.PlayMethod}");   // ←デフォルトがMIDIとそれ以外で異なる
            if (data.Option is DMOption dm && dm.port != "") sw.WriteLine($"MIDIPort\t{dm.port}");
            if (full || data.LastPlayedTicks != defaultData?.LastPlayedTicks) sw.WriteLine($"LastPlayed\t{data.LastPlayedTicks}");
        }

        #endregion
    }
}
