using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c969.Models
{
    public class User
    {
        public int userId {  get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public bool active { get; set; }

        public User()
        {

        }

        public User(string Username, string Password)
        {
            username = Username;
            password = Password;
            active = true;
        }
    }
}
