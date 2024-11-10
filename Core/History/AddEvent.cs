#nullable enable
using System.Collections.Generic;
using System.Linq;

namespace NeoMupl.History
{
    public class AddEvent : IEvent
    {
        public string Name { get; }

        private readonly MusicData[] musicDatas;

        public AddEvent(IEnumerable<MusicData> musicDatas)
        {
            this.musicDatas = musicDatas.Select(musicData => (MusicData)musicData.Clone()).ToArray();
            Name = this.musicDatas.Length == 1 ? $@"{this.musicDatas[0].Title}を追加" : $@"{this.musicDatas.Length}件を追加";
        }

        public void Redo(MusicList musicList)
        {
            foreach (var musicData in musicDatas)
            {
                musicList.Add(musicData);
            }
        }

        public void Undo(MusicList musicList)
        {
            foreach (var musicData in musicDatas)
            {
                musicList.Remove(musicData.FileName);
            }
        }
    }
}
