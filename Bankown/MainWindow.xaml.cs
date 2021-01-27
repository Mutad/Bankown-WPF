using Bankown.Controllers;
using Bankown.Models;
using Bankown.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bankown
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public string MainWindowVariable = "variable in main window";
        public MainWindow(User user) 
        {
            InitializeComponent();
            DataContext = new ApplicationViewModel() { User = user };
            //var resp = HttpController.Instance.GetAsync("/api/auth/user").ContinueWith((resp) =>
            //{
            //    Trace.WriteLine(resp.Result.StatusCode);
            //    Trace.WriteLine(resp.Result.Content);
            //});
        }
    }
}
