#nullable enable
using System;
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
        public ExtensionRule() : this("", PlayMethod.DirectShow) { }

        public string Extension { get; set; }
        public PlayMethod PlayMethod { get; set; }

        public static ExtensionRule Parse(string extension, string playMethodName)
        {
            return new ExtensionRule(extension, (PlayMethod)Enum.Parse(typeof(PlayMethod), playMethodName));
        }
    }
}
