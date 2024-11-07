#nullable enable
namespace NeoMupl.History
{
    public interface IEvent
    {
        string Name { get; }
        public void Undo(MusicList musicList);
        public void Redo(MusicList musicList);
    }
}
