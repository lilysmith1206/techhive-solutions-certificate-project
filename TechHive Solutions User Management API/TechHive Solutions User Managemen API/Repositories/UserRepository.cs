using TechHive_Solutions_User_Management_API.Models;

namespace TechHive_Solutions_User_Management_API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private int _nextId = 1;

        private readonly List<User> _users = [];

        public User? GetUser(int id)
        {
            return _users.SingleOrDefault(user => user.Id == id);
        }

        public int AddUser(string name)
        {
            int userId = _nextId;

            _users.Add(new User() { Id = userId, Name = name });

            _nextId++;

            return userId;
        }

        public void UpdateUser(int id, string newName)
        {
            User? user = _users.SingleOrDefault(user => user.Id == id);

            if (user == null)
            {
                throw new ArgumentException($"Error updating user with id {id}: No user with id {id}.");
            }

            user.Name = newName;
        }

        public void DeleteUser(int id)
        {
            User? user = _users.SingleOrDefault(user => user.Id == id);

            if (user == null)
            {
                throw new ArgumentException($"Error updating user with id: No user with id {id}.");
            }

            _users.Remove(user);
        }
    }
}
