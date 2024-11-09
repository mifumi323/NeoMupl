#nullable enable

using System;

namespace NeoMupl.Player
{
    public class DMOption : ICloneable
    {
        public string port;
        public static string portdefault = "Default";
        public DMOption()
        {
            port = "";
        }
        public DMOption(string port)
        {
            this.port = port;
        }

        public object Clone()
        {
            return new DMOption(port);
        }
    }
}
