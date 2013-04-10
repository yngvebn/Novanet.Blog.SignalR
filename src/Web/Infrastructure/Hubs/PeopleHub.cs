using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using Web.Infrastructure.Repository;
using Web.Models;

namespace Web.Infrastructure.Hubs
{
    public class PeopleHub: Hub
    {
        private readonly IPeopleRepository _peopleRepository;

        public PeopleHub(IPeopleRepository peopleRepository)
        {
            _peopleRepository = peopleRepository;
        }

        public PeopleViewModel GetPeople()
        {
            return new PeopleViewModel() { People = _peopleRepository.GetPeople() };
        }

        public Person FindPerson(string name)
        {
            return _peopleRepository.Find(name);
        }

        public PeopleViewModel ByCompany(string companyName)
        {
            return new PeopleViewModel() { People = _peopleRepository.GetPeople().Where(c => c.Company == companyName).ToList() };
        }
    }

    public class PeopleViewModel
    {
        public IList<Person> People { get; set; }
    }
}