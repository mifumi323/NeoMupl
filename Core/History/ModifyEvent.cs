namespace NeoMupl.History
{
    public class ModifyEvent
    {
        public string Name { get; }

        private readonly MusicData oldData;
        private readonly MusicData newData;

        public ModifyEvent(MusicData oldData, MusicData newData)
        {
            this.oldData = (MusicData)oldData.Clone();
            this.newData = (MusicData)newData.Clone();
            Name = $@"{this.newData.Title}を編集";
        }

        public void Redo(MusicList musicList)
        {
            musicList.Set(oldData.FileName, newData);
        }

        public void Undo(MusicList musicList)
        {
            musicList.Set(newData.FileName, oldData);
        }
    }
}
