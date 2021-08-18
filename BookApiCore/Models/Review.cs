using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookApiCore.Models
{
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(200,MinimumLength =10,ErrorMessage ="Heading must be betwen 10 and 200 chars ")]
        public string Headine { get; set; }

        [Required]
        [StringLength(2000,MinimumLength =50,ErrorMessage ="ReviewText must be betwen 50 and 200 characters")]
        public string ReviewText { get; set; }
        [Required]
        [Range(1,5,ErrorMessage ="Rating must be betwen 1 and 5 stars")]
        public int Rating { get; set; }
        public virtual Reviewer Reviewer { get; set; }
        public virtual Book Book { get; set; }
    }
}
