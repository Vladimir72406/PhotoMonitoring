using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhotoMonitoring.Model
{
    public class Photo
    {
        public string file { get; set; }

        public string surname { get; set; }
        public string name { get; set; }
        public string middleName { get; set; }
        public double temperature { get; set; }
        public DateTime datetimeFix { get; set; }
        public string fio_full
        {
            get
            {
                return surname + " " + name + " " + middleName;
            }
        }
        public string color
        {
            get
            {
                if (this.temperature > 36.6)
                    return "Red";
                else
                    return "Green";
            }
        }
    }
}
