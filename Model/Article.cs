using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Article: BaseEntity
    {
        public Article()
        {

        }
        public string  ArticleTitle { get; set; }
        public string ArticleDesc { get; set; }
        public DateTime? ArticleDate { get; set; }
    }
}
