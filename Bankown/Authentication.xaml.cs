using Bankown.Controllers;
using Bankown.Models;
using Bankown.Pages;
using Bankown.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Bankown
{
    /// <summary>
    /// Interaction logic for Authentication.xaml
    /// </summary>
    public partial class AuthenticationWindow : Window
    {
        AuthenticationViewModel authViewModel;
        public User User
        {
            get
            {
                return authViewModel.User;
            }
        }

        public AuthenticationWindow()
        {
            authViewModel = new AuthenticationViewModel();
            authViewModel.CloseAction = new Action(this.Close);
            DataContext = authViewModel;
            InitializeComponent();
            authViewModel.ContentFrame = ContentFrame;

            var loginPage = new LoginPage
            {
                DataContext = authViewModel
            };

            var registerPage = new RegisterPage
            {
                DataContext = authViewModel
            };

            loginPage.navigate = () => { ContentFrame.Navigate(registerPage); };
            registerPage.navigate = () => { ContentFrame.Navigate(loginPage); };

            ContentFrame.Navigate(loginPage);
        }
    }
}
