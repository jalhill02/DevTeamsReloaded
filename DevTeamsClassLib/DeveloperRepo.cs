using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamsClassLib
{
    public class DeveloperRepo
    {
        //we are using the REPOSITORY PATTERN-> scaffolding Methods that will implement C.R.U.D 
        private readonly List<Developer> _developerDirectory = new List<Developer>();

        //starting count of developer
        int Count = 0;

        //Developer Create
        public void AddDeveloperToDirectory(Developer developer)
        {
            //we are going to add one to count every time this METHOD is called
            Count++;

            //assign count to developer.Id-> gives developer Id number
            developer.ID = Count;

            //now add Developer to_developerDirectory
            _developerDirectory.Add(developer);
        }

        //Developer Read
        public List<Developer> GetAllDevelopers()
        {
            return _developerDirectory;
        }

        //Developer Update
        // bring in one developer
        // we can use the helper method to achieve this
        public bool UpdateDeveloper(int oldDevID, Developer newDeveloper)
        {
            // find old developer 
            Developer oldDev = GetDeveloperByID(oldDevID);
            if (oldDev != null)
            {
                // We want newDeveloper to equal old developer ID
                newDeveloper.ID = oldDev.ID;
                oldDev.DevName = newDeveloper.DevName;
                oldDev.HasPluralsightLicense = newDeveloper.HasPluralsightLicense;
                return true;
            }
            else
            {
                return false;
            }
        }

        //Developer Delete
        public bool RemoveDeveloper(int ID)
        {
            // get developer ID
            Developer toBeRemoved = GetDeveloperByID(ID);
            if (toBeRemoved == null)
            {
                return false;
            }
            int initialCount = _developerDirectory.Count();
            // remove developer by ID
             _developerDirectory.Remove(toBeRemoved);

            if (initialCount > _developerDirectory.Count())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Developer Helper (Get Developer by ID)
        public Developer GetDeveloperByID(int developerComparrisonID)
        {
            foreach (var developer in _developerDirectory)
            {
                // we need a conditional that checks the developerID
                if (developer.ID == developerComparrisonID)
                {
                    return developer;
                }
            }
            return null;

        }

        public List<Developer> FindDeveloperswithoutPSL()
        {
            List<Developer> developers = new List<Developer>();
            foreach (var developer in _developerDirectory)
            {
                if (developer.HasPluralsightLicense==false)
                {
                    developers.Add(developer);

                }
            }
            return developers;
        }
    }   
}
