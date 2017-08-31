using System;

namespace NeoMupl
{
    /// <summary>音楽の再生情報を保持します。</summary>
    public class MusicInfo
    {
        /// <summary>再生するファイル名をフルパスで取得・設定します。</summary>
        public string FileName { get; set; } = null;

        /// <summary>画面上に表示するためのタイトルを取得・設定します。</summary>
        public string Title { get; set; } = null;

        /// <summary>ボリュームを%単位で取得・設定します。50%が標準です。</summary>
        public double Volume { get; set; } = 50.0;

        /// <summary>ループ開始位置を取得・設定します。単位は再生方法に依存します。</summary>
        public double LoopStart { get; set; } = 0.0;

        /// <summary>ループ終了位置を取得・設定します。単位は再生方法に依存します。</summary>
        public double LoopEnd { get; set; } = 0.0;

        /// <summary>ランダム再生でスキップされる確率を%単位で取得・設定します。</summary>
        public double SkipRate { get; set; } = 0.0;

        /// <summary>再生方法を文字列で取得・設定します。</summary>
        public string PlayMethod { get; set; } = null;

        /// <summary>最後に再生された日時を取得・設定します</summary>
        public DateTime LastPlayed { get; set; }

        /// <summary>再生方法ごとの再生オプションを取得・設定します</summary>
        public object Option { get; set; }
    }
}
