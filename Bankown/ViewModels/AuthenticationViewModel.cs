using Bankown.Controllers;
using Bankown.Models;
using Bankown.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace Bankown.ViewModels
{
    struct AuthResponse
    {
        public string token_type;
        public string access_token;
        public User user;
    }

    class AuthenticationViewModel : INotifyPropertyChanged
    {
        string email = "mutado.nzr@gmail.com";
        string password = "12345678";
        string passwordRepeat = "12345678";
        public ModernWpf.Controls.Frame ContentFrame;

        User user = null;
        public Action CloseAction { get; set; }

        public User User
        {
            get => user;
            private set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }

        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        public string PasswordRepeat
        {
            get => passwordRepeat;
            set
            {
                passwordRepeat = value;
                OnPropertyChanged("PasswordRepeat");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public RelayAsyncCommand LoginCommand { get; private set; }
        public RelayAsyncCommand RegisterCommand { get; private set; }

        public AuthenticationViewModel()
        {
            user = new User();
            LoginCommand = new RelayAsyncCommand(() =>
            {
                Debug.WriteLine("login command execution");
                HttpResponseMessage res = UserController.LoginRequest(Email, Password).Result;
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var json = res.Content.ReadAsStringAsync().Result;
                    AuthResponse loginResponse = JsonConvert.DeserializeObject<AuthResponse>(json);

                    this.User = loginResponse.user;

                    HttpController.Instance.Token = new AccessToken() { Type = loginResponse.token_type, Value = loginResponse.access_token };
                    Application.Current.Dispatcher.Invoke(CloseAction);

                }
                else
                {
                    Trace.WriteLine("login unsuccessful");
                    Trace.WriteLine(res.Content.ReadAsStringAsync().Result);
                    // TODO show error message
                }
            });

            RegisterCommand = new RelayAsyncCommand(() =>
            {
                Debug.WriteLine("register command execution");
                HttpResponseMessage res = UserController.RegisterRequest(User,Password,PasswordRepeat).Result;
                if (res.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    var json = res.Content.ReadAsStringAsync().Result;
                    JObject obj = JObject.Parse(json);
                    this.User = obj["user"].ToObject<User>();
                    Debug.WriteLine(obj["access_token"]);
                    string token = obj["access_token"].ToString();
                    Debug.WriteLine(token[0..5]);
                    HttpController.Instance.Token = new AccessToken() { Type = token[0..6],Value=  token[7..] };
                    Application.Current.Dispatcher.Invoke(CloseAction);
                }
                else
                {
                    Debug.WriteLine(res.Content.ReadAsStringAsync().Result);
                }
            });
        }
    }
}
