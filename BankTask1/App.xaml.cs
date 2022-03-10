using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BankTask1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var viewModel = new MainWindowModel();
            viewModel.PropertyChanged += mess;
            var view = new MainWindow { DataContext = viewModel};
            view.Show();
            
        }

        public static void mess(object ob , PropertyChangedEventArgs e )
        {
            Console.WriteLine($"e.PropertyName = {e.PropertyName} ,  e.GetType = {e.GetType()} , e.string {e.ToString()} ");
        }

    }
}

