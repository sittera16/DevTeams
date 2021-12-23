using DevTeams_POCOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeams_Repository
{
    public class DeveloperRepository
    {
        private readonly List<Developer> _developerContext = new List<Developer>();
        private int _count = 1000;



        //C
        public bool AddDeveloperToDirectry(Developer developer)
        {
            if (developer is null)
            { 
                return false; 
            }
            else
            {
                _count++;
                developer.ID = _count;
                _developerContext.Add(developer);
                return true;
            }
        }


        //R
        public List<Developer> GetDeveloperList()
        {
            return _developerContext;
        }

        public Developer GetDeveloperByID (int id)
        {
            foreach (Developer developer in _developerContext)
            {
                if (developer.ID == id)
                {
                    return developer;
                }
            }
            return null;
        }

        public List<Developer> GetDeveloperByPluralsight(bool hasPluralSight)
        {
            List<Developer> pluralSightList = new List<Developer>();
            foreach (Developer developer in _developerContext)
            {
                
                if (developer.HasPluralsight == hasPluralSight)
                {
                    pluralSightList.Add(developer);
                }
            }
            return pluralSightList;
        }


        //U
        public bool UpdateExistingDeveloper(int originalDeveloperID, Developer developer)
        {
            Developer oldDeveloper = GetDeveloperByID(originalDeveloperID);

            if (oldDeveloper != null)
            {
                oldDeveloper.ID = developer.ID;
                oldDeveloper.FirstName = developer.FirstName;
                oldDeveloper.LastName = developer.LastName;
                oldDeveloper.HasPluralsight = developer.HasPluralsight;
                return true;
            }
            else
            {
                return false;
            }
        }


        //D
        public bool DeleteExistingDeveloper(Developer existingDeveloper)
        {
            bool deleteDeveloper = _developerContext.Remove(existingDeveloper);
            return deleteDeveloper;
        }
    }
}
