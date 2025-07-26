#nullable enable

using System;

namespace NeoMupl.Player
{
    public class DMOption : ICloneable
    {
        public string port;
        public static string portdefault = "Default";
        public string reset = "GM";
        public DMOption()
        {
            port = "";
        }
        public DMOption(string port, string reset)
        {
            this.port = port;
            this.reset = reset;
        }

        public object Clone()
        {
            return new DMOption(port, reset);
        }
    }
}
