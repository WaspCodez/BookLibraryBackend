using Microsoft.VisualBasic;

namespace AhmedSabirPerraiTechnicalTest.components
{
    public class Book
    {
        public string id { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public DateOnly publishedDate { get; set; }
        public string ISBN { get; set; }

        public Book()
        {

        }
    }
}
