using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CheckPassword
{
    public class CheckPasswordClass
    {
        public int CP(string str)
        {
            int LvlPassword = 0;

            string[] ListPatern = { @"[A-Z|А-Я]", @"[a-z|а-я]", @"[0-9]", @"\W|_" };

            foreach (string patern in ListPatern)
            {
                if (Regex.IsMatch(str, patern))
                    LvlPassword++;
            }

            if (str.Length >= 8)
                LvlPassword++;

            if (str == " " || Regex.IsMatch(str, @"\s{" + str.Length.ToString() + "}"))
                return 0;

            return LvlPassword;
        }
    }
}
