using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;
using System.IO;
using CsvHelper.Configuration;
using System.Globalization;
using CsvHelper;
using System.Diagnostics;


namespace BankTask1
{
    class MainWindowModel : INotifyPropertyChanged
    {  
        public MainWindowModel()
        {
            Save = new RelayCommand(_ => save());
        }
       
        private string _text = "opera ,postgres, sqlwriter, slack";
        
        //Свойство текст связана с представлением через binding и автоматически обновляется 
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


        // Обработчик события нажатия на копку saveProcess
        private void save()
        {
            List<DataForSave> data = new List<DataForSave>();

            var AllProcess = Process.GetProcesses(); //Все процессы запущённые на данный момент 
            var ListProcess = ParserString.parse(Text); // Имена процессов написанные в TextBox


            //находим в списке всех доступных процессов (AllProcess) интересующие нас процессы (ListProcess)
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
            

            //Cохраняем в csv файл
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

            }
            //открываем в редакторе получившийся csv файл
            Process.Start(path);
        }
    }
}














    

   


     

