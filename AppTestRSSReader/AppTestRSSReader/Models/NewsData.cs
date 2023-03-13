using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppTestRSSReader.Models
{
    public partial class NewsData
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string title { get; set; }
        public string link { get; set; }
        public string description { get; set; }
        public string pubdate { get; set; }
        public string guid { get; set; }
        public string imageURL { get; set; }

    }

}
