using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exam.Models
{
    public class ExamModel
    {
        public string Title { get; set; }
        public string Desc { get; set; }
        public DateTime? ExamDate { get; set; }
        public List<Question> QuestionsList { get; set; }
    }
}
