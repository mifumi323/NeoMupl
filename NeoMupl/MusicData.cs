using System;
using System.IO;

namespace NeoMupl
{
    public class MusicData
    {
        #region 変数

        private string myFileName;
        private string myTitle;
        private double myVolume;
        private double myLoopStart;
        private double myLoopEnd;
        private double mySkipRate;
        private long myLastPlayedTicks;
        private PlayMethod myPlayMethod;
        private object myOption;

        private static MusicData defaultData = null;

        #endregion

        #region プロパティ

        /// <summary>再生するファイル名をフルパスで取得・設定します</summary>
        public string FileName
        {
            get { return myFileName; }
            set { myFileName = value; }
        }
        public string Directory
        {
            get { return Path.GetDirectoryName(FileName); }
            //set { myVar = value; }
        }
        public string FileTitle
        {
            get { return Path.GetFileNameWithoutExtension(FileName); }
            //set { myVar = value; }
        }
	
        /// <summary>画面上に表示するためのタイトルを取得・設定します</summary>
        public string Title
        {
            get { return myTitle; }
            set { myTitle = value; }
        }
        /// <summary>ボリュームを%単位で取得・設定します(50%が標準)</summary>
        public double Volume
        {
            get { return myVolume; }
            set { myVolume = value; }
        }
        /// <summary>ループ開始位置を秒単位で取得・設定します</summary>
        public double LoopStart
        {
            get { return myLoopStart; }
            set { myLoopStart = value; }
        }
        /// <summary>ループ終了位置を秒単位で取得・設定します</summary>
        public double LoopEnd
        {
            get { return myLoopEnd; }
            set { myLoopEnd = value; }
        }
        /// <summary>ランダム再生でスキップされる確率を%単位で取得・設定します</summary>
        public double SkipRate
        {
            get { return mySkipRate; }
            set { mySkipRate = value; }
        }
        /// <summary>再生方法を取得・設定します</summary>
        public PlayMethod PlayMethod
        {
            get { return myPlayMethod; }
            set { myPlayMethod = value; }
        }
        public object Option
        {
            get { return myOption; }
            set { myOption = value; }
        }
	
        /// <summary>最後に再生された日時をTick単位で取得・設定します</summary>
        public long LastPlayedTicks
        {
            get { return myLastPlayedTicks; }
            set { myLastPlayedTicks = value; }
        }
        /// <summary>最後に再生された日時をDateTimeで取得・設定します</summary>
        public DateTime LastPlayedDateTime
        {
            get { return new DateTime(myLastPlayedTicks); }
            set { myLastPlayedTicks = value.Ticks; }
        }

        static private string myPattern = "<fullpath>";
        /// <summary>CreateTitleで使うパターンを取得・設定します</summary>
        static public string Pattern
        {
            get { return myPattern; }
            set { myPattern = value; }
        }
    
        #endregion

        #region コンストラクタ

        public MusicData(string fileName)
        {
            myTitle = CreateTitle(myFileName = fileName);
            myVolume = 50;
            myLoopStart = myLoopEnd = mySkipRate = 0;
            myLastPlayedTicks = 0;
            myPlayMethod = fileName.EndsWith(".mid", true, null) ? PlayMethod.DirectMusic : PlayMethod.DirectShow;
            myOption = null;
        }

        #endregion

        #region メソッド

        public void TimeStamp() { myLastPlayedTicks = DateTime.Now.Ticks; }

        public override string ToString() { return myTitle; }

        static public string CreateTitle(string fileName)
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

        public void Dump(System.IO.StreamWriter sw, MusicData data, bool full)
        {
            if (!full && defaultData == null) defaultData = new MusicData("");
            sw.WriteLine("File\t" + data.FileName);
            sw.WriteLine("Title\t" + data.Title);   // ←デフォルトを設定で変更可能
            if (full || data.Volume != defaultData.Volume) sw.WriteLine("Volume\t" + data.Volume.ToString());
            if (full || data.LoopStart > 0 || data.LoopEnd > 0) sw.WriteLine("Loop\t" + data.LoopStart.ToString() + "\t" + data.LoopEnd.ToString());
            if (full || data.SkipRate != defaultData.SkipRate) sw.WriteLine("SkipRate\t" + data.SkipRate.ToString());
            sw.WriteLine("PlayMethod\t" + ((int)data.PlayMethod).ToString());   // ←デフォルトがMIDIとそれ以外で異なる
            try
            {
                DMOption dm = (DMOption)data.Option;
                if (dm.port != "") sw.WriteLine("MIDIPort\t" + dm.port);
            }
            catch (Exception) { }
            if (full || data.LastPlayedTicks != defaultData.LastPlayedTicks) sw.WriteLine("LastPlayed\t" + data.LastPlayedTicks.ToString());
        }
        
        #endregion
    }
}
