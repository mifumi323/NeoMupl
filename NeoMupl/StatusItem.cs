using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using NeoMupl.Player;

namespace NeoMupl
{
    public abstract class StatusItem
    {
        public void Add(StatusStrip target)
        {
            target.Items.Add(CreateItem());
        }

        protected abstract ToolStripItem CreateItem();
        public abstract void Update(IMusicController player);

        private static Dictionary<string, StatusItem> items = null;

        private static void AddItemInfo(StatusItem item)
        {
            items.Add(item.ToString(), item);
        }

        private static void InitItemInfo()
        {
            if (items != null) return;
            items = new Dictionary<string, StatusItem>();
            AddItemInfo(new StatusItemFullPathName());
            AddItemInfo(new StatusItemFileName());
            AddItemInfo(new StatusItemDirectory());
            AddItemInfo(new StatusItemPlayGauge());
            AddItemInfo(new StatusItemPlayTime());
            AddItemInfo(new StatusItemPlayPosition());
            AddItemInfo(new StatusItemPlayLength());
            AddItemInfo(new StatusItemTitle());
            AddItemInfo(new StatusItemSkipRate());
            AddItemInfo(new StatusItemVolume());
        }

        public static StatusItem Parse(string text)
        {
            InitItemInfo();
            return items[text];
        }

        public static string[] GetNames()
        {
            InitItemInfo();
            string[] ret = new string[items.Count];
            items.Keys.CopyTo(ret, 0);
            return ret;
        }
    }

    public class StatusItemFullPathName : StatusItem
    {
        private ToolStripStatusLabel item;

        protected override ToolStripItem CreateItem()
        {
            if (item == null)
            {
                item = new ToolStripStatusLabel();
            }
            return item;
        }

        public override void Update(IMusicController player)
        {
            if (item == null) return;
            if (player == null || player.Data == null)
            {
                item.Text = "再生中のファイルはありません。";
                return;
            }
            item.Text = player.Data.FileName;
        }

        public override string ToString()
        {
            return "フルパス名";
        }
    }

    public class StatusItemFileName : StatusItem
    {
        private ToolStripStatusLabel item;

        protected override ToolStripItem CreateItem()
        {
            if (item == null)
            {
                item = new ToolStripStatusLabel();
            }
            return item;
        }

        public override void Update(IMusicController player)
        {
            if (item == null) return;
            if (player == null || player.Data == null)
            {
                item.Text = "-";
                return;
            }
            item.Text = Path.GetFileName(player.Data.FileName);
        }

        public override string ToString()
        {
            return "ファイル名";
        }
    }

    public class StatusItemDirectory : StatusItem
    {
        private ToolStripStatusLabel item;

        protected override ToolStripItem CreateItem()
        {
            if (item == null)
            {
                item = new ToolStripStatusLabel();
            }
            return item;
        }

        public override void Update(IMusicController player)
        {
            if (item == null) return;
            if (player == null || player.Data == null)
            {
                item.Text = "-";
                return;
            }
            item.Text = Path.GetDirectoryName(player.Data.FileName);
        }

        public override string ToString()
        {
            return "フォルダ";
        }
    }

    public class StatusItemPlayGauge : StatusItem
    {
        ToolStripProgressBar item;

        protected override ToolStripItem CreateItem()
        {
            if (item == null)
            {
                item = new ToolStripProgressBar();
            }
            return item;
        }

        public override void Update(IMusicController player)
        {
            if (item == null) return;
            if (player == null || player.Data == null)
            {
                item.Value = 0;
                return;
            }
            item.Minimum = 0;
            item.Maximum = (int)player.Length;
            item.Value = Math.Max(0, Math.Min((int)player.Position, item.Maximum));
        }

        public override string ToString()
        {
            return "再生ゲージ";
        }
    }

    public class StatusItemPlayTime : StatusItem
    {
        ToolStripStatusLabel item;

        protected override ToolStripItem CreateItem()
        {
            if (item == null)
            {
                item = new ToolStripStatusLabel();
            }
            return item;
        }

        public override void Update(IMusicController player)
        {
            if (item == null) return;
            if (player == null || player.Data == null)
            {
                item.Text = "0:00/0:00";
                return;
            }
            item.Text = $"{ToString((int)player.Position)}/{ToString((int)player.Length)}";
        }

        private string ToString(int seconds)
        {
            return $"{seconds / 60:D}:{seconds % 60:D2}";
        }

        public override string ToString()
        {
            return "再生位置/時間";
        }
    }

    public class StatusItemPlayPosition : StatusItem
    {
        ToolStripStatusLabel item;

        protected override ToolStripItem CreateItem()
        {
            if (item == null)
            {
                item = new ToolStripStatusLabel();
            }
            return item;
        }

        public override void Update(IMusicController player)
        {
            if (item == null) return;
            if (player == null || player.Data == null)
            {
                item.Text = "0:00";
                return;
            }
            item.Text = $"位置：{ToString((int)player.Position)}";
        }

        private string ToString(int seconds)
        {
            return $"{seconds / 60:D}:{seconds % 60:D2}";
        }

        public override string ToString()
        {
            return "再生位置";
        }
    }

    public class StatusItemPlayLength : StatusItem
    {
        ToolStripStatusLabel item;

        protected override ToolStripItem CreateItem()
        {
            if (item == null)
            {
                item = new ToolStripStatusLabel();
            }
            return item;
        }

        public override void Update(IMusicController player)
        {
            if (item == null) return;
            if (player == null || player.Data == null)
            {
                item.Text = "長さ：0:00";
                return;
            }
            item.Text = $"長さ：{ToString((int)player.Length)}";
        }

        private string ToString(int seconds)
        {
            return $"{seconds / 60:D}:{seconds % 60:D2}";
        }

        public override string ToString()
        {
            return "再生時間";
        }
    }

    public class StatusItemTitle : StatusItem
    {
        private ToolStripStatusLabel item;

        protected override ToolStripItem CreateItem()
        {
            if (item == null)
            {
                item = new ToolStripStatusLabel();
            }
            return item;
        }

        public override void Update(IMusicController player)
        {
            if (item == null) return;
            if (player == null || player.Data == null)
            {
                item.Text = "-";
                return;
            }
            item.Text = player.Data.Title;
        }

        public override string ToString()
        {
            return "タイトル";
        }
    }

    public class StatusItemSkipRate : StatusItem
    {
        ToolStripStatusLabel item;

        protected override ToolStripItem CreateItem()
        {
            if (item == null)
            {
                item = new ToolStripStatusLabel();
            }
            return item;
        }

        public override void Update(IMusicController player)
        {
            if (item == null) return;
            if (player == null || player.Data == null)
            {
                item.Text = "スキップ率：-";
                return;
            }
            item.Text = $"スキップ率：{player.Data.SkipRate}%";
        }

        public override string ToString()
        {
            return "スキップ率";
        }
    }

    public class StatusItemVolume : StatusItem
    {
        ToolStripStatusLabel item;

        protected override ToolStripItem CreateItem()
        {
            if (item == null)
            {
                item = new ToolStripStatusLabel();
            }
            return item;
        }

        public override void Update(IMusicController player)
        {
            if (item == null) return;
            if (player == null || player.Data == null)
            {
                item.Text = "Vol：-";
                return;
            }
            item.Text = $"Vol：{player.Data.Volume}%";
        }

        public override string ToString()
        {
            return "ボリューム";
        }
    }
}
