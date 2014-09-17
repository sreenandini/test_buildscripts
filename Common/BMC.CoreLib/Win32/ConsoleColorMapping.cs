using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BMC.CoreLib.Win32
{
    public static class ConsoleColorMapping
    {
        private static IDictionary<ConsoleColor, Color> _colors = null;

        static ConsoleColorMapping()
        {
            _colors = new SortedDictionary<ConsoleColor, Color>()
            {
                { ConsoleColor.Black, Color.Black },
                { ConsoleColor.Blue, Color.Blue },
                { ConsoleColor.Cyan, Color.Cyan },
                { ConsoleColor.DarkBlue, Color.DarkBlue },
                { ConsoleColor.DarkCyan, Color.DarkCyan },
                { ConsoleColor.DarkGray, Color.DarkGray },
                { ConsoleColor.DarkGreen, Color.DarkGreen },
                { ConsoleColor.DarkMagenta, Color.DarkMagenta },
                { ConsoleColor.DarkRed, Color.DarkRed },
                { ConsoleColor.DarkYellow, Color.Yellow },
                { ConsoleColor.Gray, Color.Gray },
                { ConsoleColor.Green, Color.Green },
                { ConsoleColor.Magenta, Color.Magenta },
                { ConsoleColor.Red, Color.Red },
                { ConsoleColor.White, Color.White },
                { ConsoleColor.Yellow, Color.Yellow },
            };
        }

        public static Color Get(ConsoleColor color)
        {
            return _colors[color];
        }

        public static Color Get()
        {
            return _colors[Console.ForegroundColor];
        }
    }
}
