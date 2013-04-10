using System.Collections.Generic;
using System.Linq;
using Web.Models;

namespace Web.Infrastructure.Repository
{
    public class PeopleRepository: IPeopleRepository
    {
        private List<Person> _people = new List<Person>()
            {
                Person.Create("Yngve B. Nilsen", 32, "Novanet"),
                Person.Create("Steve Ballmer", 66, "Microsoft"),
                Person.Create("Tim Cook", 60, "Apple")
            };

        public IList<Person> GetPeople()
        {
            return _people;
        }

        public Person Find(string name)
        {
            return _people.Single(p => p.Name.Equals(name));
        }
    }
}