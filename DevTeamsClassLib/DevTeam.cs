using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamsClassLib
{
    public class DevTeam
    {
        // DevTeamID, this is my key
        public int DevTeamID { get; set; }
        // When making a list you must set it to a new version to allow enablment of adding new items.
        public List<Developer> Developers { get; set; } = new List<Developer>();
        public string DevTeamName { get; set; }

        public DevTeam()
        {

        }
        public DevTeam(string devTeamName)
        {
            DevTeamName = devTeamName;
           
        }
    }
}
