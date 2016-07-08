﻿using System;
using System.Windows.Input;

/* A set of rules involving keys pressed.
Author: Richard Gaynor
*/

namespace InfinitySeriesGenerator
{
    public static class keyRules
    {
        //checks for backspace/tab key
        public static bool IsDelOrBackspaceOrTabKey(Key inKey)
        {
            return inKey == Key.Delete || inKey == Key.Back || inKey == Key.Tab;
        }

        //true if a number, false if not
        public static bool IsNumberKey(Key inKey)
        {
            if (inKey < Key.D0 || inKey > Key.D9)
            {
                if (inKey < Key.NumPad0 || inKey > Key.NumPad9)
                {
                    return false;
                }
            }
            return true;
        }

        //stops people copy/pasting non-numbers in!
        public static string checkForChar(string s)
        {
            var tmp = s;
            foreach (var c in s.ToCharArray())
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(c.ToString(), "^[0-9]*$"))
                {
                    tmp = tmp.Replace(c.ToString(), "");
                }
            }
            return tmp;
        }
    }
}
