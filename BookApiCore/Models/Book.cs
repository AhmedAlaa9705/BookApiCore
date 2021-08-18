using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookApiCore.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(10,MinimumLength =3,ErrorMessage ="isbn must be betwen 3 and 20 characters")]
        public string Isbn { get; set; }

        [Required]
        [MaxLength(200,ErrorMessage ="title can not be more than 200 characters")]
        public string Title { get; set; }


        public DateTime? DatePublished { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<BookAuther> BookAuthers { get; set; }
        public virtual ICollection<BookCategory> BookCategories { get; set; }
    }
}
