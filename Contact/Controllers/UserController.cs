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
        public  IActionResult Index()
        {
            return View( _userRepository.GetAll());
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

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("UserName,Email,Password,Id,AddedDate,ModifiedDate,IPAddress")] User user)
        {
            if (ModelState.IsValid)
            {
                _userRepository.Insert(user);
                _userRepository.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
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

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
            return _userRepository.GetAll().Where(c=>c.Id==id).Any();
        }
    }
}
