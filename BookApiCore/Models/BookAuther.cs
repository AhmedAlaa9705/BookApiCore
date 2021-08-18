using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApiCore.Models
{
    public class BookAuther
    {
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int AutherId { get; set; }
        public Auther Auther { get; set; }
    }
}
