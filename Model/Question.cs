using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Question : BaseEntity
    {
        public string QuestionDesc { get; set; }
        public string AnswerA { get; set; }
        public string AnswerB { get; set; }
        public string AnswerC { get; set; }
        public string AnswerD { get; set; }
        public string CorrectAnswer { get; set; }
        public int QuestionOrder { get; set; }
        public int ArticleId { get; set; }
    }
}
