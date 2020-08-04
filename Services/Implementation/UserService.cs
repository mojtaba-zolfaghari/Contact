using Entities;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Implementation
{
    public class UserService : IUserService
    {
        private IserviceRepository<User> _userRepository;
        private IserviceRepository<UserProfile> _userProfileRepository;

        public UserService(IserviceRepository<User> userRepository, IserviceRepository<UserProfile> userProfileRepository)
        {
            _userRepository = userRepository;
            _userProfileRepository = userProfileRepository;
        }
        public void DeleteUser(long id)
        {
            UserProfile userProfile = _userProfileRepository.Get(id);
            User user = _userRepository.Get(id);

            _userProfileRepository.Delete(userProfile);
            _userRepository.Delete(user);

            _userRepository.SaveChanges();
        }

        public User GetUser(long id)
        {
           return _userRepository.Get(id);
        }

        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetAll();
        }

        public void InsertUser(User user)
        {
            _userRepository.Insert(user);
        }

        public void UpdateUser(User user)
        {
            _userRepository.Update(user);
        }
    }
}
