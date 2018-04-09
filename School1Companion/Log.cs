using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;

namespace School1Companion
{
    public class Log
    {
        public Log()
        {
        }

        public static bool LogError(string Source, string ErrMsg, string LogPath)
        {
            try
            {
                // Log Error
                FileMode oMode = FileMode.Append;
                // Write to file and return true
                FileInfo oFI = new FileInfo(LogPath + "ErrorLog.txt");

                if (!File.Exists(LogPath + "ErrorLog.txt") || oFI.Length > 999999)
                    oMode = FileMode.Create;

                FileStream fs = new FileStream(LogPath + "ErrorLog.txt", oMode, FileAccess.Write);
                StreamWriter wr = new StreamWriter(fs);
                string sMsg = "ERROR IN " + Source + " on " + DateTime.Now.ToString() + ": " + ErrMsg + 
                    Environment.NewLine;
                wr.Write(sMsg);
                wr.Close();

                return (true);

            }
            catch (Exception)
            {
                return (false);
            }
        }
    }
}
