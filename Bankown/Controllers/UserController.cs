using Bankown.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Bankown.Controllers
{
    class UserController
    {
		public static async Task<HttpResponseMessage> LoginRequest(string email,string password)
		{
			var postData = "{\"email\":\"" + email + "\",\"password\":\"" + password + "\"}";
            return await HttpController.Instance.PostAsync("/api/auth/login", postData);
		}
        public static async Task<HttpResponseMessage> RegisterRequest(User user,string password,string passwordRepeat)
        {
            //var postData = JsonConvert.SerializeObject(user);
            JObject json = JObject.FromObject(user);
            json["birth_date"] = user.BirthDate.ToString("dd/MM/yyyy");
            json.Add("password", password);
            json.Add("password_repeat", passwordRepeat);
            Debug.WriteLine(json.ToString());
            return await HttpController.Instance.PostAsync("/api/auth/register", json.ToString());
        }
    }
}
