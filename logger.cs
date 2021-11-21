using System;
using System.Collections.Generic;
using System.Text;

namespace AutoMihoyoBBS_1
{
    class logger
    {
        public static void println(Type type, object text)
        {
            Console.WriteLine("[" + DateTime.Now.ToString() + "]" + "[" + type.ToString() + "]" + " " + text);
        }
    }

    enum Type
    {
        Info, Warn, Error
    }
}
