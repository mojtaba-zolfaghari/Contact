using DataLayer;
using Entities;
using Microsoft.EntityFrameworkCore;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services
{
    public class ContactService : IContactService
    {
        public readonly DbSet<Entities.Contact> _contacts;
        private readonly ApplicationDbContext _dataContext;

        public ContactService(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
            _contacts = _dataContext.Set<Entities.Contact>();
        }
        public void AddContact(Entities.Contact contact)
        {
            _contacts.Add(contact);

            _dataContext.BeginTransaction();
            _dataContext.Commit();
        }

        public IList<Entities.Contact> ContactList()
        {
            return _contacts.ToList();
        }

        public bool DeleteContact(int ContactId)
        {
            var contact = _contacts.Find(ContactId);
            _contacts.Remove(contact);

            _dataContext.BeginTransaction();
            _dataContext.Commit();
            return true;
        }

        public Entities.Contact GetContact(int ContactId)
        {
            var contact = _contacts.Find(ContactId);
            return contact;
        }

        public IEnumerable<Entities.Contact> SearchContact(string nameToSearch)
        {
            var listOfFinded = _contacts.ToList().Where(c => c.Name.Contains(nameToSearch));
            return listOfFinded;
        }

        public void UpdateContact(Contact contact)
        {
            _contacts.Update(contact);
            _dataContext.SaveChanges();

        }

    }
}
