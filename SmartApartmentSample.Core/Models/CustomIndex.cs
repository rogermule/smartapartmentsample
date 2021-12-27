using System;
using System.Collections.Generic;
using System.Text;

namespace SmartApartmentSample.Core.Models
{
    public class CustomIndex
    {
        public string _index { get; set; }
        public string _id { get; set; }
    }

    public class IndexHolder
    {
        public CustomIndex index { get; set; }

    }
}
