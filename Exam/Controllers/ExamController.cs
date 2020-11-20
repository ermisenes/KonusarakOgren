using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Repository.Common;

namespace Exam.Controllers
{
    public class ExamController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private IGenericRepository<Article> _articleRepo;
        private IGenericRepository<Question> _questionRepo;


        public ExamController(IUnitOfWork unitOfWork, IGenericRepository<Article> articleRepo, IGenericRepository<Question> questionRepo)
        {
            _unitOfWork = unitOfWork;
            _articleRepo = articleRepo;
            _questionRepo = questionRepo;
        }
        public IActionResult Index()
        {
            var articles = _articleRepo.GetAll().ToList();

            return View(articles);
        }

        public IActionResult ViewExam(int? Id)
        {
            ExamModel examModel;
           
            if (Id != null)
            {
                var questions = _questionRepo.FindBy(x => x.ArticleId == Id).ToList();
                var article = _articleRepo.FindBy(x => x.Id == Id).FirstOrDefault();
                examModel = new ExamModel
                {
                    Title = article.ArticleTitle,
                    Desc = article.ArticleDesc,
                    ExamDate = article.ArticleDate,
                    QuestionsList = questions
                };
                var cnt = examModel.QuestionsList.Count;
                for (int i = 0; i < (4 - cnt); i++)
                {

                    examModel.QuestionsList.Add(new Question());

                }

               return View(examModel);

            }

            return View();
        }

        [HttpPost]
        public IActionResult Check(int id)
        {
            var question = _questionRepo.FindAll(y => y.ArticleId == id).ToList();
            Dictionary<string,string> correct = new Dictionary<string, string>();
            int x=1;
            foreach (var item in question)
            {
                switch (item.CorrectAnswer)
                {
                    case "A":
                        correct.Add(x+"_td-1",item.CorrectAnswer);
                        break;
                    case "B":
                        correct.Add(x+"_td-2",item.CorrectAnswer);
                        break;
                    case "C":
                        correct.Add(x+"_td-3",item.CorrectAnswer);
                        break;
                    case "D":
                        correct.Add(x+"_td-4",item.CorrectAnswer);
                        break;
                    default:
                        break;
                }
                
                x++;
            }
                        

            return Json(new { success = true, result=correct.ToList()});
        }
    }
}
