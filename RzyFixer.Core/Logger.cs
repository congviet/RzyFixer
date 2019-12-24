using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Colorful.Console;

namespace RzyFixer.Core
{
    public class Logger
    {
        public enum Type
        {
            Info,
            Error,
            Debug,
            Done,
            MadeByRzy
        }

        public static void Write(string message, Type type = Type.Info)
        {
            switch (type)
            {
                case Type.Info:
                    Console.WriteLine($"[{DateTime.Now}] INFO: {message}", Color.Cyan);
                    break;

                case Type.Error:
                    Console.WriteLine($"[{DateTime.Now}] ERROR: {message}", Color.IndianRed);
                    break;

                case Type.Done:
                    Console.WriteLine($"[{DateTime.Now}] DONE: {message}", Color.LightGreen);
                    break;
            }
        }
    }
}