using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using EFRelationships.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace EFRelationships.Web.Controllers
{
    public class QuestionsController : Controller
    {
        private string _connectionString;

        public QuestionsController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ViewForTag(string tagName)
        {
            var repo = new QuestionsRepository(_connectionString);
            return View(repo.GetQuestionsForTag(tagName));
        }

        [HttpPost]
        public IActionResult Add(Question question, List<string> tags)
        {
            question.DatePosted = DateTime.Now;
            var repo = new QuestionsRepository(_connectionString);
            repo.AddQuestion(question, tags);
            return Redirect("/questions");
        }
    }
}