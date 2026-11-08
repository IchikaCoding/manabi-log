using ManabiLog.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ManabiLog.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var testStudyRecord = new StudyRecord()
            {
                Id = 1,
                StudyDate = new DateOnly(2026, 11, 8),
                Title = "GitHub Actionsを使ってみる！",
                Tags = new List<Tag>()
                {
                    new Tag(){Id = 1, Name = "GitHub"},
                    // もう一個Tag追加する予定
                    new Tag(){Id = 2, Name = "C#"},
                }

            };
            // studyRecord.Tags = new
            return View(testStudyRecord);
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
