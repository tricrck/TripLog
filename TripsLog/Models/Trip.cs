using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;

namespace TripLog.Models
{
    public class Trip
    {
        public int? TripId { get; set; }

        [Required]
        [Display(Name = "Destination")]
        public string Destination { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Accommodations")]
        public string Accommodation { get; set; }

        [Display(Name = "Accommodation Phone")]
        public string AccommodationPhone { get; set; }

        [Display(Name = "Accommodation Email")]
        public string AccommodationEmail { get; set; }

        [Display(Name = "Thing To Do #1")]
        public string ThingToDo1 { get; set; }

        [Display(Name = "Thing To Do #2")]
        public string ThingToDo2 { get; set; }

        [Display(Name = "Thing To Do #3")]
        public string ThingToDo3 { get; set; }
    }
}