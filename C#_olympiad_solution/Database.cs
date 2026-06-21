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
        private static Dictionary<string, User> usersTable;
        private static Dictionary<int, Island> islandsTable;
        private static HashSet<int> islandVisitStatus;
        private static Dictionary<string, Dictionary<int,int>> islDistanceTable;
        static Database()
        {
            usersTable = new Dictionary<string, User>();
            islandsTable = new Dictionary<int, Island>();
            islandVisitStatus = new HashSet<int>();
            islDistanceTable = new Dictionary<string, Dictionary<int,int>>();
            usersTable.Add("ojti@csharp.ro", new User("ojti@csharp.ro", "Ojti2025"));
            
        }
        public static void addIsland(int id, string name, int gold, int virus)
        {
            islandsTable.Add(id, new Island(id, name, gold, virus));
        }
        public static void addDistance(string islFrom, int idIslTo, int distance) 
        {
            if(!islDistanceTable.ContainsKey(islFrom))
            { 
                islDistanceTable.Add(islFrom, new Dictionary<int, int>());
                
            }
            islDistanceTable[islFrom].Add(idIslTo, distance);
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
        public static void setIslandVisited(int id)
        {
            islandVisitStatus.Add(id);
        }
        public static bool checkIslandStatus(int id)
        {
            if (islandVisitStatus.Contains(id))
            {
                return false;
            }
            return true;
        }
        public static Dictionary<int, Island> getIslands()
        {
            return islandsTable;
        }
        public static int getDistance(string islFrom, int idIslTo)
        {
            if (islDistanceTable.ContainsKey(islFrom))
            {
                if (islDistanceTable[islFrom].ContainsKey(idIslTo))
                {
                    return islDistanceTable[islFrom][idIslTo];
                }
            }
            return -1; // Return -1 if distance not found
        }
        public static string getIslandNameById(int id)
        {
            if (islandsTable.ContainsKey(id))
            {
                return islandsTable[id].Name;
            }
            return null; // Return null if island not found
        }
    }
}
