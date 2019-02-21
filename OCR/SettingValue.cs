using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OCR
{
    class SettingValue
    {
        public static string id = "";
        public static string key = "";
        public static string secret = "";
        public static string ReaderLinesFromFile(string filename, int startLine, int linecount)
        {
            int i = 0;
            StringBuilder sb = new StringBuilder();
            StreamReader reader = new StreamReader(filename);
            while (!reader.EndOfStream)
            {
                if (i >= startLine)
                {
                    if (linecount < 1)
                        sb.Append(reader.ReadToEnd());
                    else
                        sb.Append(reader.ReadLine());
                }
                else
                    reader.ReadLine();
                i++;
                if (i >= linecount + startLine) break;
            }
            reader.Close();
            reader.Dispose();
            return sb.ToString().Trim();
        }
    }

}
