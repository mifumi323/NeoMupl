using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
