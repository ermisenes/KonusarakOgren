using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exam.Models
{
    public class ArticleModel
    {
        public int Id { get; set; }
        public string  ArticleTitle { get; set; }
        public string ArticleDesc { get; set; }
        public List<Article> Articles { get; set; }
       
        public ExamModel GetExamModel { get; set; }
    }
}
