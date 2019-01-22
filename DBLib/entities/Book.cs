using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLib.entities
{
    public class Book
    {
        public int BookID { get; private set; }
        public string Title { get; set; }
        public int NumberOfCopies { get; set; }

        public Book(int id, string title, int num) {
            BookID = id;
            Title = title;
            NumberOfCopies = num;
        }

        public Book(string title, int num)
        {
            Title = title;
            NumberOfCopies = num;
        }

    }
}
