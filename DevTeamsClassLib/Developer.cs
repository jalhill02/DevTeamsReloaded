using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamsClassLib
{
    public class Developer
    {
        public int ID { get; set; }
        public string DevName { get; set; }
        public bool HasPluralsightLicense { get; set; }

        // Constructors
        public Developer()
        {

        }
        public Developer(string devName, bool hasPluralSightLicense)
        {
            DevName = devName;
            HasPluralsightLicense = hasPluralSightLicense;
        }
    }
}
