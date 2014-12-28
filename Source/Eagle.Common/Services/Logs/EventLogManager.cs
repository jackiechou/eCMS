using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Eagle.Common.Services.Logs
{
    public class EventLogManager
    {
        #region WRITE FILE LOG ===============================================================================
        public static void AppendTextToLogFile(string strDataToAppend, string physical_path)
        {
            string currentDate = System.DateTime.Now.ToString("dd-MM-yyyy");
            string logDirName = "LOGS";

            if (Directory.Exists(physical_path))
            {
                string physical_log_path = Path.Combine(physical_path, logDirName);
                if (!Directory.Exists(physical_log_path))
                    Directory.CreateDirectory(physical_log_path);

                string physical_log_file_path = Path.Combine(physical_log_path, currentDate.Replace("/", "-") + ".txt");
                if (!File.Exists(physical_log_file_path))
                    File.Create(physical_log_file_path);

                System.IO.FileInfo info = new System.IO.FileInfo(physical_log_file_path);
                long size = info.Length;

                if (size > 512)
                {
                    physical_log_file_path = Path.Combine(physical_path, System.DateTime.Now.ToString("dd-MM-yyyy") + "-" + System.DateTime.Now.ToString() + ".txt");
                    if (!File.Exists(physical_log_file_path))
                        File.Create(physical_log_file_path);
                }

                string strFileContents = string.Empty, message = string.Empty;
                if (File.Exists(physical_log_file_path))
                {
                    FileStream fileStream = new FileStream(physical_log_file_path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                    using (StreamReader srReader = new StreamReader(fileStream))
                    {
                        strFileContents = srReader.ReadToEnd();
                        if (string.IsNullOrEmpty(strFileContents))
                            message = System.DateTime.Now.ToString("dd-MM-yyyy") + " : " + strDataToAppend;
                        else
                            message = System.DateTime.Now.ToString("dd-MM-yyyy") + " : " + strDataToAppend + Environment.NewLine + strFileContents;
                        srReader.Close();

                        FileStream fs = new FileStream(physical_log_file_path, FileMode.OpenOrCreate, FileAccess.Write);
                        StreamWriter m_streamWriter = new StreamWriter(fs);
                        m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);
                        m_streamWriter.WriteLine(message);
                        m_streamWriter.Flush();
                        m_streamWriter.Close();
                    }

                }
            }
        }

        public static void WriteTextToLogFile(string strDataToAppend, string physical_dir_path)
        {
            string currentDate = System.DateTime.Today.ToString("dd-MM-yyyy");
            string currentTime = System.DateTime.Now.ToString("hh:mm:ss");
            string logDirName = "LOGS";
            string message = currentDate + " " + currentTime + " : " + strDataToAppend;

            if (Directory.Exists(physical_dir_path))
            {
                string physical_log_path = Path.Combine(physical_dir_path, logDirName);
                if (!Directory.Exists(physical_log_path))
                    Directory.CreateDirectory(physical_log_path);

                string logFilePath = Path.Combine(physical_log_path, currentDate.Replace("/", "-") + ".txt");
                if (!File.Exists(logFilePath))
                    File.Create(logFilePath);

                System.IO.FileInfo info = new System.IO.FileInfo(logFilePath);
                long size = info.Length;
                const int maxFileSize = 10 * 1024;
                // In my case it always the case, o/w you can read file line-by-line  
                //byte[] fileBytes = new byte[maxFileSize];
                //int amountOfBytes = StreamReader.Read(fileBytes, 0, maxFileSize);
                //System.Text.UTF8Encoding utf8 = new System.Text.UTF8Encoding();
                //System.String errorFileContext = utf8.GetString(fileBytes, 0, amountOfBytes);

                if (size > maxFileSize)
                {
                    logFilePath = Path.Combine(physical_dir_path, currentDate + "-" + currentTime + ".txt");
                    if (!File.Exists(logFilePath))
                        File.Create(logFilePath);
                }

                if (File.Exists(logFilePath))
                {
                    bool _FileUse = false;
                    while (!_FileUse)
                    {
                        try
                        {
                            using (StreamWriter streamWriter = new StreamWriter(logFilePath, true))
                            {
                                streamWriter.BaseStream.Seek(0, SeekOrigin.End);
                                streamWriter.WriteLine(message);
                                streamWriter.Flush();
                                streamWriter.Close();
                                System.Threading.Thread.Sleep(1000);
                                _FileUse = true;
                                //GC.Collect();
                                //GC.WaitForPendingFinalizers();
                            }
                        }
                        catch (System.IO.IOException ex)
                        {
                            ex.ToString();
                            System.Threading.Thread.Sleep(1000);
                            _FileUse = false;
                        }
                    }
                }
            }
        }
        #endregion ===========================================================================================

        private static void CreateLogFile(string physicalBasePath, string message)
        {
            try
            {
                string basePath = Path.Combine(physicalBasePath, "log");
                if (!Directory.Exists(basePath))
                    Directory.CreateDirectory(basePath);

                string filePath = Path.Combine(basePath, "log.txt");
                if (!File.Exists(filePath))
                    File.Create(filePath);

                WriteLogFile(filePath, message);
            }
            catch (Exception exc)
            {
                exc.ToString();
            }
        }

        //private static void CreateWebLogFile(string message)
        //{
        //    try
        //    {
        //        string file_path = Globals.HostMapPath + "\\logs\\log.txt";
        //        if (!File.Exists(file_path))
        //            File.Create(file_path);

        //        WriteLogFile(file_path, message);
        //    }
        //    catch (Exception exc)
        //    {
        //        exc.ToString();
        //    }
        //}

        public static void WriteLogFile(string filePath, string message)
        {
            if (File.Exists(filePath))
            {
                if (!File.Exists(filePath))
                    File.Create(filePath);

                using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
                {
                    fileStream.Flush();
                    fileStream.Close();
                }

                using (StreamWriter sw = new StreamWriter(filePath, true))
                {
                    string lastRecordText = "# " + System.DateTime.Now.ToString("dd/MM/yyyy") + " # " + Environment.NewLine + "#" + message + " #" + Environment.NewLine;
                    sw.WriteLine(lastRecordText);
                    sw.Close();
                }
            }
        }

        public static void TraceService(string serviceName, string message)
        {
            if (System.Diagnostics.EventLog.SourceExists(message))
            {
                // Create a trace listener for the event log.
                EventLogTraceListener myTraceListener = new EventLogTraceListener(serviceName);
                // Add the event log trace listener to the collection.
                Trace.Listeners.Add(myTraceListener);
                // Write output to the event log.
                string logMessage = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " : " + message;
                Trace.WriteLine(logMessage);
            }
        }

        public static void WriteEntryInEventLog(string serviceName, string message, EventLogEntryType type)
        {
            string strMessage = "ERROR MESSAGE: {0}" + DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToLongTimeString() +
                             Environment.NewLine + Environment.NewLine + message + Environment.NewLine + Environment.NewLine;

            if (!System.Diagnostics.EventLog.SourceExists(serviceName))
                System.Diagnostics.EventLog.CreateEventSource(serviceName, serviceName);

            System.Diagnostics.EventLog eventLog = new System.Diagnostics.EventLog();
            eventLog.Source = serviceName;
            eventLog.Log = serviceName;
            eventLog.WriteEntry(strMessage, type);
        }

        public static void WriteToEventLog(string source, string message, Exception except)
        {
            string ex = except.ToString();
            string EventText = message + Environment.NewLine + Environment.NewLine + ex;
            EventLog elog = new EventLog();
            if (!EventLog.SourceExists(source))
            {
                EventLog.CreateEventSource(source, source);
            }
            elog.Source = source;
            elog.EnableRaisingEvents = true;
            elog.WriteEntry(EventText);
        }

        public static EventLogInstaller CreateEventLogInstaller(string serviceName)
        {
            // Remove Event Source if already there
            if (EventLog.SourceExists(serviceName))
                EventLog.DeleteEventSource(serviceName);

            EventLogInstaller eventLogInstaller = new EventLogInstaller();
            eventLogInstaller.Source = serviceName;
            eventLogInstaller.Log = serviceName;
            return eventLogInstaller;
        }

        public static void RemoveEventLog(string serviceName)
        {
            if (EventLog.SourceExists(serviceName))
            {
                EventLog.DeleteEventSource(serviceName);
            }
        }   
    }
}
