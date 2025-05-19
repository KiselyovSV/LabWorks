using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RazorPagesMovie.Models
{
    public class Movie
    {
        public int ID { get; set; }

        [BindProperty]
        [StringLength(30)]
        [Display(Name = "The name of the movie")]
        public string Title { get; set; } = "";

        private DateTime releaseDate;

        [BindProperty]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate
        {
            get { return releaseDate; }
            set { releaseDate = value.ToUniversalTime(); }
        }

        [StringLength(15)]
        public string Genre { get; set; } = "";

        [Required, Range(100, 3000), DisplayName("The cost of the movie")]
        public decimal Price { get; set; }
        

        

    }
}
