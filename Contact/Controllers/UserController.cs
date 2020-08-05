using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataLayer;
using Entities;
using Services;
using Services.Contracts;
using ViewModels.User;

namespace Contact.Controllers
{
    public class UserController : Controller
    {
        private IUserService _userService;
        private IserviceRepository<User> _userRepository;

        public UserController(IUserService userService, IserviceRepository<User> userRepository)
        {
            _userService = userService;
            _userRepository = userRepository;
        }

        // GET: User
        public IActionResult Index()
        {
            var allUsers = _userRepository.GetAll();
            List<ViewModels.User.CreateUserVM> usersviewmodel = new List<CreateUserVM>();
            foreach (var userItem in allUsers)
            {
                UserProfile userProfile = new UserProfile()
                {
                    AddedDate = userItem.UserProfile.AddedDate,
                    Phone = userItem.UserProfile.Phone,
                    Address = userItem.UserProfile.Address,
                    FirstName = userItem.UserProfile.FirstName,
                    Id = userItem.UserProfile.Id,
                    IPAddress = userItem.UserProfile.IPAddress,
                    LastName = userItem.UserProfile.LastName,
                    ModifiedDate = userItem.UserProfile.ModifiedDate != null ? userItem.UserProfile.ModifiedDate : null,

                };
                CreateUserVM tempUser = new CreateUserVM()
                {
                    Address = userProfile.Address,
                    Email = userItem.Email,
                    FirstName = userProfile.FirstName,
                    LastName = userProfile.LastName,
                    Id = userItem.Id,
                    Password = userItem.Password,
                    Phone = userProfile.Phone,
                    UserName = userItem.UserName
                };
                usersviewmodel.Add(tempUser);
            }
            return View(usersviewmodel);
        }

        // GET: User/Details/5
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _userRepository.Get(id.Value);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateUserVM userViewModel)
        {
            if (ModelState.IsValid)
            {
                User user = new User()
                {
                    AddedDate = DateTime.UtcNow,
                    IPAddress = HttpContext.Connection.RemoteIpAddress.ToString(),
                    Password = userViewModel.Password,
                    Email = userViewModel.Email,
                    UserName = userViewModel.UserName,
                };
                UserProfile userProfile = new UserProfile
                {
                    AddedDate = DateTime.UtcNow,
                    IPAddress = HttpContext.Connection.RemoteIpAddress.ToString(),
                    Address = userViewModel.Address,
                    FirstName = userViewModel.FirstName,
                    LastName = userViewModel.LastName,
                    Phone = userViewModel.Phone,
                };
                user.UserProfile = userProfile;
                _userRepository.Insert(user);
                _userRepository.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(userViewModel);
        }

        // GET: User/Edit/5
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _userRepository.Get(id.Value);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, [Bind("UserName,Email,Password,Id,AddedDate,ModifiedDate,IPAddress")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _userRepository.Update(user);
                    _userRepository.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: User/Delete/5
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _userRepository.Get(id.Value);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(long id)
        {
            var user = _userRepository.Get(id);
            _userRepository.Delete(user);
            _userRepository.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(long id)
        {
            return _userRepository.GetAll().Where(c => c.Id == id).Any();
        }
    }
}
