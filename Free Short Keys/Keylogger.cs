/*
 *	ArticleKeyLog - Basic Keystroke Mining
 *
 *	Date:	05/12/2005
 *
 *	Author:	Alexander Kent
 *
 *	Description:	Sample Application for the Code Project (www.codeproject.com)
 */

using System;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Free_Short_Keys
{
    /// <summary>
    /// Summary description for Keylogger
    /// 
    /// Timer Intervals are in ms, examples:
    ///	60000ms = 1 minute
    ///	1800000ms = 30 minutes
    /// 36000000ms = 60 minutes
    /// 
    /// </summary>
    /// 	
    public class Keylogger
    {
        /// <summary>
        /// The GetAsyncKeyState function determines whether a key is up or down at the time 
        /// the function is called, and whether the key was pressed after a previous call 
        /// to GetAsyncKeyState.
        /// </summary>
        /// <param name="vKey">Specifies one of 256 possible virtual-key codes. </param>
        /// <returns>If the function succeeds, the return value specifies whether the key 
        /// was pressed since the last call to GetAsyncKeyState, and whether the key is 
        /// currently up or down. If the most significant bit is set, the key is down, 
        /// and if the least significant bit is set, the key was pressed after 
        /// the previous call to GetAsyncKeyState. </returns>
        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(
            System.Windows.Forms.Keys vKey); // Keys enumeration

        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(
            System.Int32 vKey);

        private System.String keyBuffer;
        private System.Timers.Timer timerKeyMine;
        private System.Timers.Timer timerBufferFlush;
        private string keydumpPath;
        private bool saveToFile;

        public Keylogger(string keydumpPath, bool saveToFile = true)
        {
            this.keydumpPath = keydumpPath;
            this.saveToFile = saveToFile;

            //
            // keyBuffer
            //
            keyBuffer = "";

            // 
            // timerKeyMine
            // 
            this.timerKeyMine = new System.Timers.Timer();
            this.timerKeyMine.Enabled = true;
            this.timerKeyMine.Elapsed += new System.Timers.ElapsedEventHandler(this.timerKeyMine_Elapsed);
            this.timerKeyMine.Interval = 10;

            // 
            // timerBufferFlush
            //
            this.timerBufferFlush = new System.Timers.Timer();
            this.timerBufferFlush.Enabled = true;
            this.timerBufferFlush.Elapsed += new System.Timers.ElapsedEventHandler(this.timerBufferFlush_Elapsed);
            this.timerBufferFlush.Interval = 1800000; // 30 minutes
        }

        /// <summary>
        /// Itrerating thru the entire Keys enumeration; downed key names are stored in keyBuffer 
        /// (space delimited).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerKeyMine_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            foreach (System.Int32 i in Enum.GetValues(typeof(Keys)))
            {
                if (GetAsyncKeyState(i) == -32767)
                {
                    string key = Enum.GetName(typeof(Keys), i);
                    switch (key)
                    {
                        case "Oemtilde":
                            key = "`";
                            break;
                        default:
                            break;
                    }
                    keyBuffer += key;

                    foreach (var shortKey in ShortKeyConfiguration.Instance.ShortKeys)
                    {
                        string fullKey = ((shortKey.CustomSuffix.Length > 0 ? shortKey.CustomSuffix : ShortKeyConfiguration.Instance.DefaultSuffix) + shortKey.Key).ToUpperInvariant();
                        if (keyBuffer.EndsWith(fullKey))
                        {
                            string keys = shortKey.ReplacementKey;
                            keys = keys.Replace("\r\n", "\r\r\n");
                            keys = keys.Replace("\r\r", string.Empty);
                            if (shortKey.UseClipboard)
                            {
                                //IDataObject clipboardData = Clipboard.GetDataObject();
                                //Clipboard.SetDataObject(keys);
                                //Clipboard.
                            }
                            else
                            {
                                string backs = string.Empty;
                                for (int j = 0; j < fullKey.Length; j++)
                                {
                                    backs += "{BACKSPACE}";
                                }
                                SendKeys.SendWait($"{backs}{keys}");
                            }
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerBufferFlush_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Flush2File(this.keydumpPath, true);
        }


        /// <summary>
        /// Transfers key stroke data from temporary buffer storage to permanent memory. 
        /// If no exception gets thrown the key stroke buffer resets.
        /// </summary>
        /// <param name="file">The complete file path to write to.</param>
        /// <param name="append">Determines whether data is to be appended to the file. 
        /// If the files exists and append is false, the file is overwritten. 
        /// If the file exists and append is true, the data is appended to the file. 
        /// Otherwise, a new file is created.</param>
        public void Flush2File(string file, bool append)
        {
            try
            {
                if (keyBuffer.Length > 1000)
                {
                    string write = keyBuffer.Remove(1000);
                    keyBuffer = keyBuffer.Remove(0, 1000);
                    if (this.saveToFile)
                    {
                        StreamWriter sw = new StreamWriter(file, append);
                        sw.Write(write);
                        sw.Close();
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        #region Properties
        public bool Enabled
        {
            get
            {
                return timerKeyMine.Enabled && timerBufferFlush.Enabled;
            }
            set
            {
                timerKeyMine.Enabled = timerBufferFlush.Enabled = value;
            }
        }

        public double FlushInterval
        {
            get
            {
                return timerBufferFlush.Interval;
            }
            set
            {
                timerBufferFlush.Interval = value;
            }
        }

        public double MineInterval
        {
            get
            {
                return timerKeyMine.Interval;
            }
            set
            {
                timerKeyMine.Interval = value;
            }
        }
        #endregion

    }
}
