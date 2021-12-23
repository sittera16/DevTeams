using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeams_POCOs
{
    //Teams need to contain their Team members(Developers) and their Team Name, and Team ID.
    public class DevTeam
    {
        public DevTeam()
        {

        }

        public DevTeam(string teamName, List<Developer> developers)
        {
            TeamName = teamName;
            Developers = developers;
        }

        
        public string TeamName { get; set; }
        public int TeamID { get; set; }
        public List<Developer> Developers { get; set; } = new List<Developer>();
    }
}
