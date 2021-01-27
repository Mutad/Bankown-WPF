using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace Bankown.Models
{
    public class User : INotifyPropertyChanged
    {

        private uint id;
        private string email;
        private string firstName;
        private string last_name;
        private string country;
        private DateTime birthDate = DateTime.Now;

        [JsonProperty("id")]
        public uint Id { 
            get => id; 
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        [JsonProperty("email")]
        public string Email { 
            get => email;
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }    
        }
        [JsonProperty("first_name")]
        public string FirstName {
            get => firstName; 
            set
            {
                firstName = value;
                OnPropertyChanged("First_name");
            }
        }
        [JsonProperty("last_name")]
        public string LastName {
            get => last_name;
            set
            {
                last_name = value;
                OnPropertyChanged("Last_name");
            }
        }
        [JsonProperty("country")]
        public string Country { 
            get => country;
            set
            {
                country = value;
                OnPropertyChanged("Country");
            }
        }

        [JsonProperty("birth_date")]
        public DateTime BirthDate { 
            get => birthDate;
            set
            {
                Debug.WriteLine(value);
                birthDate = value;
                OnPropertyChanged("Country");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        // TODO Add dates


    }
}
