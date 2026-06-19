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
    public class Island
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Gold { get; set; }
        public int Virus { get; set; }
        public Island(int id, string name, int gold, int virus)
        {
            this.Id = id;
            this.Name = name;
            this.Gold = gold;
            this.Virus=virus;
        }
    }

    public static class Database
    {
        private static Dictionary<string,User> usersTable;
        private static Dictionary<int, Island> islandsTable;
        static Database()
        {
            usersTable = new Dictionary<string, User>();
            islandsTable = new Dictionary<int, Island>();
            usersTable.Add("ojti@csharp.ro", new User("ojti@csharp.ro", "Ojti2025"));
        }
        public static void addIsland(int id,string name, int gold, int virus)
        {
            islandsTable.Add(id, new Island(id, name, gold, virus));
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
