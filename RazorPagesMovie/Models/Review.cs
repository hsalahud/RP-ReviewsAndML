using System;
using System.ComponentModel.DataAnnotations;

namespace RazorPagesMovie.Models
{
    public class Review
    {
        [Key]
        public int ID { get; set; }
        public string Comment { get; set; }
        public int MovieID { get; set; }
        public Movie Movie { get; set; }

    }
}
