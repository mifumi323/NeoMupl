#nullable enable
using System.IO;

namespace MifuminLib
{
    public class PathMaker
    {
        public static string GetAnotherName(string path)
        {
            if (!File.Exists(path)) return path;

            string filename = Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path));
            string extension = Path.GetExtension(path);

            for (int i = 1; i < 10000; i++)
            {
                string ret = $"{filename}({i}){extension}";
                if (!File.Exists(ret)) return ret;
            }
            throw new IOException("ファイルの別名が作れませんでした。");
        }
    }
}
