using TechHive_Solutions_User_Management_API.Models;

namespace TechHive_Solutions_User_Management_API.Repositories
{
    public interface IUserRepository
    {
        public int AddUser(string name);

        public void DeleteUser(int id);
        
        public User? GetUser(int id);
        
        public void UpdateUser(int id, string newName);
    }
}