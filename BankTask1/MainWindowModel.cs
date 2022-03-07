using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using System.Text.RegularExpressions;
using System.IO;
using CsvHelper.Configuration;
using System.Globalization;
using CsvHelper;

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



    class MainWindowModel : INotifyPropertyChanged
    {
        public MainWindowModel()
        {
            Save = new RelayCommand(_ => save());
        }

        private DataForSave _data;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private string _text;
        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged(nameof(Text));
            }

        }

        public ICommand Save { get; }


        private void save()
        {
            _data = ParserStringToDataForSave.parse(_text);

            string path = @"C:\Users\" + Environment.UserName + @"\Documents\Документ Task1.csv";

            using (var writer = new StreamWriter(path, false, Encoding.GetEncoding("windows-1251")))
            {
                var csvConfig = new CsvConfiguration(CultureInfo.GetCultureInfo("ru-RU"))
                {
                    HasHeaderRecord = false,
                    Delimiter = ";"
                };

                using (var csv = new CsvWriter(writer, csvConfig))
                {
                    csv.WriteRecord(_data);
                }

            }
        }
    }
}

     

