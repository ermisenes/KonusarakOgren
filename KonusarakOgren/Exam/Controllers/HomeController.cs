using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Exam.Models;
using Repository.Common;
using Model;
using Microsoft.AspNetCore.Http;

namespace Exam.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IUnitOfWork _unitOfWork;
        private IGenericRepository<Article> _articleRepo;
        private IGenericRepository<Question> _questionRepo;


        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, IGenericRepository<Article> articleRepo, IGenericRepository<Question> questionRepo)
        {
            _unitOfWork = unitOfWork;
            _articleRepo = articleRepo;
            _questionRepo = questionRepo;
            _logger = logger;

        }

        public IActionResult Index(int? Id)
        {
            var articles = _articleRepo.GetAll().ToList();
            ExamModel examModel;
            ArticleModel model;
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

                model = new ArticleModel
                {
                    Articles = articles,
                    GetExamModel = examModel
                };

            }
            else
            {
                model = new ArticleModel
                {
                    Articles = articles,
                    GetExamModel = new ExamModel()
                };

            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(IFormCollection model)
        {
            var art = new Article
            {
                ArticleTitle = model["GetExamModel.Title"],
                ArticleDesc = model["GetExamModel.Desc"],
                ArticleDate = Convert.ToDateTime(model["GetExamModel.ExamDate"])
            };
            for (int i = 0; i < 4; i++)
            {
                if (!string.IsNullOrEmpty(model["GetExamModel.QuestionsList[" + i + "].QuestionDesc"].ToString()))
                {
                     
                    int questionId = 0;
                    if (!string.IsNullOrEmpty(model["GetExamModel.QuestionsList[" + i + "].Id"].ToString()))
                    {
                        questionId =Convert.ToInt32(model["GetExamModel.QuestionsList[" + i + "].Id"].ToString());
                    }
                    var question = new Question
                    {
                        AnswerA = model["GetExamModel.QuestionsList[" + i + "].AnswerA"].ToString(),
                        AnswerB = model["GetExamModel.QuestionsList[" + i + "].AnswerB"].ToString(),
                        AnswerC = model["GetExamModel.QuestionsList[" + i + "].AnswerC"].ToString(),
                        AnswerD = model["GetExamModel.QuestionsList[" + i + "].AnswerD"].ToString(),
                        ArticleId = Convert.ToInt32(model["Id"].ToString()) > 0 ? Convert.ToInt32(model["Id"].ToString()) : 0,
                        CorrectAnswer = model["GetExamModel.QuestionsList[" + i + "].CorrectAnswer"].ToString(),
                        QuestionDesc = model["GetExamModel.QuestionsList[" + i + "].QuestionDesc"].ToString(),
                        Id = questionId


                    };
                    if (Convert.ToInt32(model["Id"].ToString()) > 0)
                    {
                        var q = _unitOfWork.Repository<Question>().Update(question);
                    }
                    else
                    {
                        var q = _unitOfWork.Repository<Question>().Insert(question);
                    }

                }
            }
            if (Convert.ToInt32(model["Id"].ToString()) > 0)
            {
                art.Id = Convert.ToInt32(model["Id"].ToString());
                _unitOfWork.Repository<Article>().Update(art);

            }
            else
            {
                _unitOfWork.Repository<Article>().Insert(art);

            }
            _unitOfWork.Commit();
            return Redirect("/Home/Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var article = _articleRepo.FindBy(x => x.Id == id).FirstOrDefault();

            if (article != null)
            {

                _unitOfWork.Repository<Article>().Delete(article);
                var questions = _questionRepo.FindAll(y => y.ArticleId == article.Id).ToList();
                foreach (var item in questions)
                {
                    _unitOfWork.Repository<Question>().Delete(item);
                }

                _unitOfWork.Commit();

            }

            return Redirect("/Home/Index");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
