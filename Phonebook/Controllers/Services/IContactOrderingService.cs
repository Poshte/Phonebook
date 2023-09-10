using Phonebook.Models;
using System.Collections.Generic;

namespace Phonebook.Controllers.Services
{
    public interface IContactOrderingService
    {
        void OrderContacts(List<Contact> contacts);
    }
}