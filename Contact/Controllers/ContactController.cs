using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataLayer;
using Entities;
using Services.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace Contact.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        // GET: Contact
        public IActionResult Index()
        {
            var list = _contactService.ContactList();
            return View(list);
        }  
        public IActionResult Index2()
        {
            var list = _contactService.ContactList();
            return View(list);
        } 



        // GET: Contact/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = _contactService.GetContact(id.Value);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        //[Authorize("Admin")]
        [Authorize(Policy = "Admin")]
        // GET: Contact/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,PhoneNumber,HomePhone")] Entities.Contact contact)
        {
            if (ModelState.IsValid)
            {
                _contactService.AddContact(contact);
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult SearchContact(string name)
        {
          var listOfFindedContact=  _contactService.SearchContact(name);
            return PartialView(listOfFindedContact);
            
        }

        // GET: Contact/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = _contactService.GetContact(id.Value);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,PhoneNumber,HomePhone")] Entities.Contact contact)
        {
            if (id != contact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _contactService.UpdateContact(contact);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExists(contact.Id))
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
            return View(contact);
        }

        // GET: Contact/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = _contactService.GetContact(id.Value);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: Contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _contactService.DeleteContact(id);

            return RedirectToAction(nameof(Index));
        }

        private bool ContactExists(int id)
        {
            var contact = _contactService.GetContact(id);
            if (contact != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
