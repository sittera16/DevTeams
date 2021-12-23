using DevTeams_POCOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeams_Repository
{
    public class DevTeamRepository
    {
        private readonly List<DevTeam> _devTeamContext = new List<DevTeam>();
        private DeveloperRepository _developerRepository;
        private int _count = 100;
        public DevTeamRepository(DeveloperRepository developerRepository)
        {
            _developerRepository = developerRepository;
        }



        //C
        public bool AddDevTeam(DevTeam devTeam)
        {
            if (devTeam == null)
            {
                return false;
            }
            else
            {
                _count++;
                devTeam.TeamID = _count;
                _devTeamContext.Add(devTeam);
                return true;
            }
        }


        //R
        public List<DevTeam> GetDevTeamList()
        {
            return _devTeamContext;
        }

        public DevTeam GetDevTeamByID(int id)
        {
            foreach (DevTeam devTeam in _devTeamContext)
            {
                if (devTeam.TeamID == id)
                {
                    return devTeam;
                }
            }
            return null;
        }


        //U
        public bool UpdateExistingDevTeam(int originalDevTeamID, DevTeam devTeam)
        {
            DevTeam oldDevTeam = GetDevTeamByID(originalDevTeamID);

            if (oldDevTeam != null)
            {
                oldDevTeam.TeamID = devTeam.TeamID;
                oldDevTeam.TeamName = devTeam.TeamName;
                oldDevTeam.Developers = devTeam.Developers;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddDevToDevTeam(int teamID, int developerID)
        {
            DevTeam devTeam = GetDevTeamByID(teamID);
            int startingCount = devTeam.Developers.Count;
            Developer devToAdd = _developerRepository.GetDeveloperByID(developerID);

            devTeam.Developers.Add(devToAdd);

            return (startingCount < devTeam.Developers.Count);
        }
        
        public bool RemoveDevFromDevTeam(int teamID, int developerID)
        {
            DevTeam devTeam = GetDevTeamByID(teamID);
            int startingCount = devTeam.Developers.Count;
            Developer devToRemove = _developerRepository.GetDeveloperByID(developerID);

            devTeam.Developers.Remove(devToRemove);

            return (startingCount > devTeam.Developers.Count);
        }

        //D
        public bool DeleteExistingDevTeam(DevTeam existingDevTeam)
        {
            bool deleteDevTeam = _devTeamContext.Remove(existingDevTeam);
            return deleteDevTeam;
        }
    }
}
