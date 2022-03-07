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
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;

namespace BankTask1
{
    class MainWindowModel : INotifyPropertyChanged
    {
        private DataForSave _data;
        
        public MainWindowModel()
        {
            Save = new RelayCommand(_ => save());
        }
       
        private string _text = "Задача , процесс 2 13 ";
        
        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged(nameof(Text));
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand Save { get;}
        

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

            
                Process.Start(path);


            }
        }
    }
}






    

   


     

