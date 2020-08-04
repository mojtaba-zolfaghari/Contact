using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Contracts
{
   public interface IContactService
    {
        void AddContact(Entities.Contact contact);
        void UpdateContact(Entities.Contact contact);
        IEnumerable<Entities.Contact> SearchContact(string name);
        bool DeleteContact(int ContactId);
        Entities.Contact GetContact(int ContactId);
        IList<Entities.Contact> ContactList();
    }
}
