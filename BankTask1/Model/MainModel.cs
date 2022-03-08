using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Diagnostics;

namespace BankTask1
{
    public class DataForSave
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public ProcessPriorityClass PriorityClass { get;  set; }
    }
 
    public class ParserString
    {
        public static List<string> parse(string text)
        {
            List<string> res = new List<string>();
            Regex regex = new Regex(@"[А-Яа-яёЁA-Za-z]+");
            MatchCollection matches = regex.Matches(text);

            foreach (Match m in matches)
            {
                res.Add(m.Value);
            }
            return res;
        }
    }

}
        

















