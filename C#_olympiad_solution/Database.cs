using System;
using System.Collections.Generic;
// Define a User class with properties for username and password
namespace C__olympiad_solution
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public User(string user, string passwd)
        {
            this.Username = user;
            this.Password = passwd;
        }
    }

    public static class Database
    {
        private static Dictionary<string,User> usersTable;
        static Database()
        {
            usersTable = new Dictionary<string, User>();
            usersTable.Add("ojti@csharp.ro", new User("ojti@csharp.ro", "Ojti2025"));
        }
        public static bool validateUser(string user, string passwd)
        {
            if (usersTable.ContainsKey(user))
            {
                if (usersTable[user].Password == passwd)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
