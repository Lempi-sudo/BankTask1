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
        public MainWindowModel()
        {
            Save = new RelayCommand(_ => save());
        }
       
        private string _text = "opera ,postgres, StartTime, HasExited,";
        
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
            List<DataForSave> data = new List<DataForSave>();

            var AllProcess = Process.GetProcesses(); //Все процессы запущённые на данный момент 
            var ListProcess = ParserString.parse(Text); // Имена процессов написанные в TextBox
          
            foreach (Process process in AllProcess)
            {
                foreach (string tmpProcess in ListProcess)
                {
                    try
                    {
                        if (tmpProcess == process.ProcessName)
                        {
                            data.Add(new DataForSave { Name = process.ProcessName, Id = process.Id, PriorityClass = process.PriorityClass });
                        }
                    }
                    catch (Win32Exception e)
                    {
                        Trace.WriteLine($"к процессу {tmpProcess} {e.Message}");
                        data.Add(new DataForSave { Name = process.ProcessName + " Отказано в доступе ", Id = 0, PriorityClass = 0 });
                    }
                  
                }
            }
                       
            string path = @"C:\Users\" + Environment.UserName + @"\Documents\Данные о процессе.csv";

            using (var writer = new StreamWriter(path, false, Encoding.GetEncoding("windows-1251")))
            {
                var csvConfig = new CsvConfiguration(CultureInfo.GetCultureInfo("ru-RU"))
                {
                    Delimiter = ";"
                };
                using (var csv = new CsvWriter(writer, csvConfig))
                {
                    csv.WriteRecords(data);
                }
                Process.Start(path);
            }
        }
    }
}














    

   


     

