using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamsClassLib
{
    public class DevTeamRepo
    {
        private readonly DeveloperRepo _developerRepo = new DeveloperRepo(); // this gives you access to the _developerDirectory so you can access existing Developers and add them to a team
        private readonly List<DevTeam> _devTeams = new List<DevTeam>();

        int Count = 0;
        //DevTeam Create

        public void AddDevTeamsToList(DevTeam devTeam)
        {
            Count++;
            devTeam.DevTeamID = Count;

            _devTeams.Add(devTeam);

        }


        //DevTeam Read
        public List<DevTeam> GetDevTeamList()
        {
            return _devTeams;
        }

        //DevTeam Update
        public bool updateDevTeam(int oldDevTeamId, DevTeam newDevTeam)
        {
            DevTeam olDevTeam = GetDevTeamByID(oldDevTeamId); // I believe I need to be using my helper method
            if (olDevTeam != null)
            {
                // We want newDeveloper to equal old developer ID
                newDevTeam.DevTeamID = olDevTeam.DevTeamID;

                //these are the only 'updated' properties
                olDevTeam.Developers = newDevTeam.Developers;
                olDevTeam.DevTeamName = newDevTeam.DevTeamName;
                return true;
            }
            else
            {
                return false;
            }

        }

        //DevTeam Delete
        public bool RemoveDevTeamFromList(int teamName)
        {
            {
                // get developer ID
                DevTeam toBeRemoved = GetDevTeamByID(teamName);
                if (toBeRemoved == null)
                {
                    return false;
                }
                int initialCount = _devTeams.Count();

                _devTeams.Remove(toBeRemoved);

                if (initialCount > _devTeams.Count())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //DevTeam Helper (Get Team by ID)
        public DevTeam GetDevTeamByID(int TeamID) // Not sure why this has an error
        {
            foreach (DevTeam team in _devTeams)
            {
                if (team.DevTeamID == TeamID) // I don't see devTeamName  in DevTeam but it is a property of DevTeam
                {
                    return team;
                }
            }
            return null;
        }
    }
}
