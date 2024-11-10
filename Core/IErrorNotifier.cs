#nullable enable
namespace NeoMupl
{
    public interface IErrorNotifier
    {
        public enum NoticeType
        {
            MessageOnly,
            AskMaybeYes,
            AskMaybeNo,
        }

        public bool Notify(string message, NoticeType type);
    }
}
