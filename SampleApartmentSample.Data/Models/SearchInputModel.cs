using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SampleApartmentSample.Data.Models
{
    public class SearchInputModel
    {
        [Required]
        public string searchkey { get; set; }
        public string market { get; set; }
        public int? limit { get; set; }
    }
}
