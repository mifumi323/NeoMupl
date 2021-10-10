#nullable enable

namespace NeoMupl.Player
{
    public class DMOption
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
    }
}
