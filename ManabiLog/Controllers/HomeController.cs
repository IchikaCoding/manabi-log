using ManabiLog.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ManabiLog.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var studyRecord = new StudyRecord();
            studyRecord.Tags = new List<Tag.Name> { "C#", "MVC" };
            return View(studyRecord);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // public IActionResult Check()
        // {
        //     return View();
        // }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
