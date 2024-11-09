using System.Collections.Generic;
using System.Linq;

namespace NeoMupl.History
{
    public class RemoveEvent : IEvent
    {
        public string Name { get; }

        private readonly MusicData[] musicDatas;

        public RemoveEvent(IEnumerable<MusicData> musicDatas)
        {
            this.musicDatas = musicDatas.Select(musicData => musicData.Clone() as MusicData).ToArray();
            Name = this.musicDatas.Length == 1 ? $@"{this.musicDatas[0].Title}を削除" : $@"{this.musicDatas.Length}件を削除";
        }

        public void Redo(MusicList musicList)
        {
            foreach (var musicData in musicDatas)
            {
                musicList.Remove(musicData.FileName);
            }
        }

        public void Undo(MusicList musicList)
        {
            foreach (var musicData in musicDatas)
            {
                musicList.Add(musicData);
            }
        }
    }
}
