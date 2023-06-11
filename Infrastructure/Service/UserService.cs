using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Entities;
using Infrastructure.Repository;
namespace Infrastructure.Service
{
    public interface IUserService
    {
        IQueryable<User> GetUsers();
        User GetUser(string id);
        void InsertUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);

        User GetUserByName(string name);

    }
    public class UserService : IUserService
    {
        private IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public IQueryable<User> GetUsers()
        {
            return userRepository.GetAll();
        }
        public User GetUser(string id)
        {
            return userRepository.GetById(id);
        }
        public void InsertUser(User user)
        {
            userRepository.Insert(user);
        }
        public void UpdateUser(User user)
        {
            userRepository.Update(user);
        }
        public void DeleteUser(User user)
        {
            userRepository.Delete(user);
        }
        public User GetUserByName(string name)
        {
            // Call the repository to get the user by name
            var user = userRepository.GetAll().FirstOrDefault(u => u.Name == name);

            return user;
        }

     
    }
}
