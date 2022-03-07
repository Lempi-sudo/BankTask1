using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BankTask1
{
    public class DataForSave
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int PriorityClass { get; set; }

    }

    public class ParserStringToDataForSave
    {

        public static DataForSave parse(string text)
        {
            Regex regex = new Regex(@"[А-Яа-яёЁ]+");
            MatchCollection matches = regex.Matches(text);
            List<string> nameInit = new List<string>();

            string name = "";

            foreach (Match m in matches)
            {
                name += m.Value + " ";
            }

            Regex regexNum = new Regex(@"\d+");
            MatchCollection matchesNum = regexNum.Matches(text);

            int id = int.Parse(matchesNum[0].Value);
            int priority = int.Parse(matchesNum[1].Value);

            DataForSave res = new DataForSave { Name = name, Id = id, PriorityClass = priority };
            return res;
        }

    }



}
