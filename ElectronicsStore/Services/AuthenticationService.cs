using ElectronicsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.Services
{
    public class AuthenticationService
    {
        private readonly List<User> _users;
        public AuthenticationService()
        {
            // создадим нескольких пользователей
            _users = new List<User>
        {
            new User { Username = "admin", Password = "pass3", Role = UserRole.Admin },
            new User { Username = "manager", Password = "pass2", Role = UserRole.Manager },
            new User { Username = "buyer", Password = "pass1", Role = UserRole.Buyer }
        };
        }

        public string Admin { get; }
        public string Buyer { get; }
        public string Manadger { get; }

        public User Authenticate(string username, string password)
        {
            return _users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }
       
    }
}
