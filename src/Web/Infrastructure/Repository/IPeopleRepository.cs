using System.Collections.Generic;
using Web.Models;

namespace Web.Infrastructure.Repository
{
    public interface IPeopleRepository
    {
        IList<Person> GetPeople();
        Person Find(string name);
    }
}