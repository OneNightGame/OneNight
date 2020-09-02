using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneNight.Data
{
    public class UsernameService
    {
        private List<string> onlineUsers = new List<string>();

        public bool IsOnline(string user)
        {
            return onlineUsers.Contains(user);
        }

        public void AddUser(string user)
        {
            onlineUsers.Add(user);
        }
    }
}
