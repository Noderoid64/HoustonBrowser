using System;
using System.Text;
using System.IO;

namespace MercuryLogger.Loggers
{
    public class FileLogger : Logger
    {
        string path;
        string name;
        Encoding encoding;
        bool append;

        public FileLogger(string path) : this(path, DateTime.Now.Hour + "." + DateTime.Now.Minute + "." + DateTime.Now.Second + ".txt")
        {

        }
        public FileLogger(string path, string name) : this(path, name, Encoding.Default, true)
        {

        }
        public FileLogger(string path, string name, Encoding encoding, bool append)
        {
            this.path = path;
            this.name = name;
            this.encoding = encoding;
            this.append = append;
        }

        public override void Log(string value)
        {
            using (StreamWriter sw = new StreamWriter(path + "/" + name, append, encoding))
            {
                sw.WriteLine(value);
            }
        }
    }
}