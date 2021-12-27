using System;
using System.Collections.Generic;
using System.Text;

namespace SmartApartmentSample.Core.Models
{
    public class Properties
    {
        public int propertyID { get; set; }
        public string name { get; set; }
        public string formerName { get; set; }
        public string streetAddress { get; set; }
        public string city { get; set; }
        public string market { get; set; }
        public string state { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class PropertyHolder
    {
        public Properties property { get; set; }
    }

    public class Holder
    {
        public PropertyHolder[] properties { get; set; }
    }
}
