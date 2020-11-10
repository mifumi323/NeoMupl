#nullable enable
using NeoMupl.Player;

namespace NeoMupl
{
    public class ExtensionRule
    {
        public ExtensionRule(string extension, PlayMethod playMethod)
        {
            Extension = extension;
            PlayMethod = playMethod;
        }

        public string Extension { get; set; }
        public PlayMethod PlayMethod { get; set; }
    }
}
