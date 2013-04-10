using System.Linq;
using System.Web.Mvc;
using Web.Infrastructure.Repository;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        IPeopleRepository _repo;
        public HomeController(IPeopleRepository repo)
        {
            _repo = repo;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetPeople()
        {
            return Json(new { People = _repo.GetPeople() }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult FindPerson(string name)
        {
            return Json(_repo.Find(name), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ByCompany(string companyName)
        {
            return Json(_repo.GetPeople().Where(c => c.Company == companyName), JsonRequestBehavior.AllowGet);
        }
    }

}