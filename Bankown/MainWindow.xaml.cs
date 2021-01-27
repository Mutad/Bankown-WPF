using Bankown.Controllers;
using Bankown.Models;
using Bankown.Pages;
using Bankown.ViewModels;
using ModernWpf.Controls;
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
        public MainWindow(User user) 
        {
            InitializeComponent();
            DataContext = new ApplicationViewModel() { User = user };

            navigation.SelectedItem = navigation.MenuItems.OfType<NavigationViewItem>().First();
        }

        private void navigation_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                contentFrame.Navigate(typeof(SettingsPage));
            }
            else
            {
                var selectedItem = (ModernWpf.Controls.NavigationViewItem)args.SelectedItem;
                if (selectedItem != null)
                {
                    string selectedItemTag = (string)selectedItem.Tag;
                    string pageName = "Bankown.Pages." + selectedItemTag;
                    Type pageType = Type.GetType(pageName);
                    contentFrame.Navigate(pageType,null, args.RecommendedNavigationTransitionInfo);
                }
            }
        }
    }
}
