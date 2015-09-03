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
using System.Collections.Generic;
using System.Text.RegularExpressions;

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
            this.timerBufferFlush.Interval = 60000; // 1 minute
        }

        public static List<ShortKey> WatchedShortKeys { get; set; } = new List<ShortKey>();

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
                    keyBuffer += key + " ";

                    string keyBufferCompare = keyBuffer.Replace(" ", string.Empty);
                    foreach (var shortKey in WatchedShortKeys)
                    {
                        string fullKey = (ShortKeyConfiguration.Default.Prefix + shortKey.Key + shortKey.Suffix).ToUpperInvariant();
                        fullKey = TextToShortKey(fullKey);
                        if (keyBufferCompare.EndsWith(fullKey))
                        {
                            SendKeys.SendWait($"{{BACKSPACE {fullKey.Length}}}");
                            string keys = PerformNewLineFix(shortKey.ReplacementKey);
                            keys = ReplaceRegexPatterns(keys);
                            if (shortKey.UseClipboard)
                            {
                                frmMain.Me.Invoke(new Action(() =>
                                {
                                    IDataObject clipboardData = Clipboard.GetDataObject();
                                    Clipboard.SetDataObject(keys);
                                    SendKeys.SendWait($"^(v)");
                                    Clipboard.SetDataObject(clipboardData);
                                }));
                            }
                            else
                            {
                                keys = ReplaceSendKeysSpecialCharacters(keys);
                                SendKeys.SendWait(keys);
                            }
                            if (shortKey.CursorLeftCount > 0)
                            {
                                SendKeys.SendWait($"{{LEFT {shortKey.CursorLeftCount}}}");
                            }
                            break;
                        }
                    }
                }
            }
        }

        private string ReplaceSendKeysSpecialCharacters(string keys)
        {
            keys = ReplaceSendKeysSpecialCharacter(keys, "+");
            keys = ReplaceSendKeysSpecialCharacter(keys, "^");
            keys = ReplaceSendKeysSpecialCharacter(keys, "%");
            keys = ReplaceSendKeysSpecialCharacter(keys, "~");
            keys = ReplaceSendKeysSpecialCharacter(keys, "(");
            keys = ReplaceSendKeysSpecialCharacter(keys, ")");
            return keys;
        }

        private string ReplaceSendKeysSpecialCharacter(string input, string specialCharacter)
        {
            return input.Replace(specialCharacter, "{"+ specialCharacter + "}");
        }

        private string ReplaceRegexPatterns(string shortKeyText)
        {
            string result = shortKeyText;
            result = ReplaceRegexPattern(result, @"\[Date\:(?<regex>.+)\]", (format) => DateTime.Now.ToString(format));
            return result;
        }

        private string ReplaceRegexPattern(string input, string pattern, Func<string, string> getReplaceText)
        {
            string result = input;
            var match = Regex.Match(input, pattern, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
            if (match.Success)
            {
                var replaceText = getReplaceText(match.Groups["regex"].Value);
                result = result.Remove(match.Index, match.Length);
                result = result.Insert(match.Index, replaceText);
            }
            return result;
        }

        public static string PerformNewLineFix(string shortKeyText)
        {
            string result = shortKeyText;
            result = result.Replace("\r\n", "\r\r\n");
            result = result.Replace("\r\r", string.Empty);
            return result;
        }

        private string TextToShortKey(string fullKey)
        {
            string result = fullKey;
            result = result.Replace("`", "Oemtilde");
            result = result.Replace("-", "OemMinus");
            result = result.Replace("+", "Oemplus");
            result = result.Replace("_", "OemMinus");
            result = result.Replace("=", "Oemplus");
            result = result.Replace("{", "Oem4");
            result = result.Replace("}", "Oem5");
            result = result.Replace("[", "Oem4");
            result = result.Replace("[", "Oem5");
            result = result.Replace(";", "OemSemicolon");
            result = result.Replace("'", "OemQuotes");
            result = result.Replace("\\", "OemPipe");
            result = result.Replace(":", "OemSemicolon");
            result = result.Replace("\"", "OemQuotes");
            result = result.Replace("|", "OemPipe");
            result = result.Replace("/", "Divide");
            result = result.Replace("*", "Multiply");
            result = result.Replace("-", "Subtract");
            result = result.Replace("+", "Add");
            result = result.Replace(".", "Decimal");
            result = result.Replace("!", "ShiftKeyD1");
            result = result.Replace("@", "ShiftKeyD2");
            result = result.Replace("#", "ShiftKeyD3");
            result = result.Replace("$", "ShiftKeyD4");
            result = result.Replace("%", "ShiftKeyD5");
            result = result.Replace("^", "ShiftKeyD6");
            result = result.Replace("&", "ShiftKeyD7");
            result = result.Replace("*", "ShiftKeyD8");
            result = result.Replace("(", "ShiftKeyD9");
            result = result.Replace(")", "ShiftKeyD0");
            for (int i = 0; i < 10; i++)
            {
                result = result.Replace($"{i}", $"D{i}");
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerBufferFlush_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Flush2File();
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
        public void Flush2File(bool forced = false)
        {
            try
            {
                if (keyBuffer.Length > 1000 || forced)
                {
                    int length = Math.Min(keyBuffer.Length, 1000);
                    string write = keyBuffer;
                    if (write.Length > length)
                    {
                        write = write.Remove(length);
                    }
                    keyBuffer = keyBuffer.Remove(0, length);
                    if (this.saveToFile && ShortKeyConfiguration.Default.LogKeysDebug)
                    {
                        StreamWriter sw = new StreamWriter(this.keydumpPath, true);
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


        public static List<SpecialKey> SpecialKeys = new List<SpecialKey>
        {
            new SpecialKey { Key= string.Empty, Codes = new List<string> { string.Empty } },
            new SpecialKey { Key= "BACKSPACE", Codes = new List<string> { "{BACKSPACE}", "{BS}", "{BKSP}" } },
            new SpecialKey { Key= "BREAK ", Codes = new List<string> { "{BREAK}" } },
            new SpecialKey { Key= "CAPS LOCK", Codes = new List<string> { "{CAPSLOCK}" } },
            new SpecialKey { Key= "DEL or DELETE", Codes = new List<string> { "{DELETE}", "{DEL}" } },
            new SpecialKey { Key= "DOWN ARROW", Codes = new List<string> { "{DOWN}" } },
            new SpecialKey { Key= "END", Codes = new List<string> { "{END}" } },
            new SpecialKey { Key= "ENTER", Codes = new List<string> { "{ENTER}", "~" } },
            new SpecialKey { Key= "ESC", Codes = new List<string> { "{ESC}" } },
            new SpecialKey { Key= "HELP", Codes = new List<string> { "{HELP}" } },
            new SpecialKey { Key= "HOME", Codes = new List<string> { "{HOME}" } },
            new SpecialKey { Key= "INS or INSERT", Codes = new List<string> { "{INSERT}", "{INS}" } },
            new SpecialKey { Key= "LEFT ARROW", Codes = new List<string> { "{LEFT}" } },
            new SpecialKey { Key= "NUM LOCK",Codes =  new List<string> { "{NUMLOCK}" } },
            new SpecialKey { Key= "PAGE DOWN", Codes = new List<string> { "{PGDN}" } },
            new SpecialKey { Key= "PAGE UP",Codes =  new List<string> { "{PGUP}" } },
            //new SpecialKey { Key= "PRINT SCREEN", new List<string> { "{PRTSC}" } },
            new SpecialKey { Key= "RIGHT ARROW", Codes = new List<string> { "{RIGHT}" } },
            new SpecialKey { Key= "SCROLL LOCK", Codes = new List<string> { "{SCROLLLOCK}" } },
            new SpecialKey { Key= "TAB", Codes = new List<string> { "{TAB}" } },
            new SpecialKey { Key= "UP ARROW", Codes = new List<string> { "{UP}" } },
            new SpecialKey { Key= "F1", Codes = new List<string> { "F1" } },
            new SpecialKey { Key= "F2", Codes = new List<string> { "F2" } },
            new SpecialKey { Key= "F3", Codes = new List<string> { "F3" } },
            new SpecialKey { Key= "F4", Codes = new List<string> { "F4" } },
            new SpecialKey { Key= "F5", Codes = new List<string> { "F5" } },
            new SpecialKey { Key= "F6", Codes = new List<string> { "F6" } },
            new SpecialKey { Key= "F7", Codes = new List<string> { "F7" } },
            new SpecialKey { Key= "F8", Codes = new List<string> { "F8" } },
            new SpecialKey { Key= "F9", Codes = new List<string> { "F9" } },
            new SpecialKey { Key= "F10", Codes = new List<string> { "F10" } },
            new SpecialKey { Key= "F11", Codes = new List<string> { "F11" } },
            new SpecialKey { Key= "F12", Codes = new List<string> { "F12" } },
            new SpecialKey { Key= "F13", Codes = new List<string> { "F13" } },
            new SpecialKey { Key= "F14", Codes = new List<string> { "F14" } },
            new SpecialKey { Key= "F15", Codes = new List<string> { "F15" } },
            new SpecialKey { Key= "F16", Codes = new List<string> { "F16" } },
            new SpecialKey { Key= "Keypad add", Codes = new List<string> { "{ADD}" } },
            new SpecialKey { Key= "Keypad subtract", Codes = new List<string> { "{SUBTRACT}" } },
            new SpecialKey { Key= "Keypad multiply", Codes = new List<string> { "{MULTIPLY}" } },
            new SpecialKey { Key= "Keypad divide", Codes = new List<string> { "{DIVIDE}" } },
        };
    }
}
