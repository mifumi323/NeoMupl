#nullable enable
using System.Collections.Generic;

namespace NeoMupl.History
{
    public class EditHistory
    {
        private readonly MusicList musicList;
        private readonly List<IEvent> events;
        private int index;

        public EditHistory(MusicList musicList)
        {
            this.musicList = musicList;
            events = new List<IEvent>();
            index = -1;
        }

        public void Add(IEvent e)
        {
            if (index < events.Count - 1)
            {
                events.RemoveRange(index + 1, events.Count - index - 1);
            }
            events.Add(e);
            index++;
        }

        public IEvent? GetNext()
        {
            return index < events.Count - 1 ? events[index + 1] : null;
        }

        public IEvent? GetPrevious()
        {
            return index >= 0 ? events[index] : null;
        }

        public void Undo()
        {
            if (index >= 0)
            {
                events[index].Undo(musicList);
                index--;
            }
        }

        public void Redo()
        {
            if (index < events.Count - 1)
            {
                index++;
                events[index].Redo(musicList);
            }
        }
    }
}
