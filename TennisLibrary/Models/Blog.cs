using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisLibrary.Models
{
    public class Blog
    {
        #region Instances
        private int _blogpostid;
        private DateTime _date;
        #endregion

        #region Properties
        public int ID { get { return _blogpostid; } }
        public string Author { get; set; }
        public string? Title { get; set; }
        public string Body { get; set; }
        public DateTime Date { get { return _date; } }
        #endregion

        #region Constructor
        public Blog(string author, string? title, string body)
        {
            _blogpostid++;
            Author = author;
            Title = title;
            Body = body;
        }
        #endregion

        #region Methods

        #endregion
    }
}
