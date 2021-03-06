using System.Collections.Generic;
using System.Text.RegularExpressions;
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
            //собираем все слова из строки и добавляем их в список нужных процессов
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
        

















