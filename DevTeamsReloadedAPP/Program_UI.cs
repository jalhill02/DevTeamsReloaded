using DevTeamsClassLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamsReloadedAPP
{
    class Program_UI
    {
        // Allows you to use the DeveloperRepo
        private readonly DeveloperRepo _developerRepo = new DeveloperRepo();
        private readonly DevTeamRepo _devTeamRepo = new DevTeamRepo();
        public void Run()
        {
            SeedDevelopers();
            Menu();

        }

        private void Menu()
        {
            bool hasStarted = true;
            while (hasStarted)
            {
                Console.WriteLine("Welcome to the DevTeam's App\n" +
                    "1. Create developer\n" +
                    "2. View all developers\n" +
                    "3. View developer by ID\n" +
                    "4. Update developer\n" +
                    "5. Delete developer\n" +
                    "6. Show Developers without PSL\n" +
                    "***********************************\n" +
                    "7. Create DevTeam\n" +
                    "8. View all DevTeams\n" +
                    "9. View DevTeam By ID\n" +
                    "10. Update DevTeam\n" +
                    "11. Delete DevTeam\n" +
                    "0. Close Application\n");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        CreateDeveloper();
                        break;
                    case "2":
                        ViewAllDevelopers();
                        break;
                    case "3":
                        ViewDeveloperById();
                        break;
                    case "4":
                        UpdateDeveloper();
                        break;
                    case "5":
                        DeleteDeveloper();
                        break;
                    case "6":
                        GetDevelopersWithOuthPSL();
                        break;
                    case "7":
                        CreateDevTam();
                        break;
                    case "8":
                        ViewAllDevTeams();
                        break;
                    case "9":
                        ViewDevTeamByID();
                        break;
                    case "10":
                        UpdateDevTeam();
                        break;
                    case "11":
                        DeleteDevTeam();
                        break;

                    case "0":
                        hasStarted = false;
                        break;


                    default:
                        break;
                }
                Console.ReadKey();
                Console.Clear();

            }
        }

        private void DeleteDevTeam()
        {
            Console.Clear();

            Console.WriteLine("Please input a DevTeamId to delete a team");
            int inputDevTeamId = int.Parse(Console.ReadLine());

            bool isSuccessful = _devTeamRepo.RemoveDevTeamFromList(inputDevTeamId);

            if (isSuccessful)
            {
                Console.WriteLine("DevTeam has been deleted.");
            }
            else
            {
                Console.WriteLine("DevTeam has not been deleted.");

            }
        }

        private void UpdateDevTeam()
        {
            Console.Clear();
            bool teamPositionsFilled = false;
            //need Id of old team -> ask the user
            Console.WriteLine("Please enter DevTeam Id:");
            int inputOldDevTeamId = int.Parse(Console.ReadLine());

            // "new up" a DevTeam
            DevTeam newDevTeam = new DevTeam();

            // ask user for the team name
            Console.WriteLine("Please enter a DevTeam Name");
            string inputDevTeamName = Console.ReadLine();
            //assing value to newDeveTeam
            newDevTeam.DevTeamName = inputDevTeamName;


            while (teamPositionsFilled == false)
            {
                // ask user do want to update team members y/n
                Console.WriteLine("Do you want to add developers to a DevTeam y/n");
                string inputMoreDevelopers = Console.ReadLine();

                if (inputMoreDevelopers == "y" || inputMoreDevelopers == "Y")
                {
                    //do the creation of a developers here
                    Developer developer = CreateDeveloperHelper();

                    //i need to add the developer to the team
                    newDevTeam.Developers.Add(developer);
                }
                if (inputMoreDevelopers == "n" || inputMoreDevelopers == "N")
                {
                    //Use the UpdateDevTeam Method
                   bool isSuccessfull= _devTeamRepo.updateDevTeam(inputOldDevTeamId, newDevTeam);
                    if (isSuccessfull)
                    {
                        Console.WriteLine($"{newDevTeam.DevTeamName} has been updated.");
                    }
                    else
                    {
                        Console.WriteLine($"{newDevTeam.DevTeamName} has failed to update.");
                    }
                    
                    //this will terminate the while loop
                    teamPositionsFilled = true;
                }


                //if yes-> use the CreateDevelperHelper() b/c it 
                //returns a developer to add to the list


            }
        }


        private void ViewDevTeamByID()
        {
            Console.Clear();
            Console.WriteLine("Please input the DevTeamI.");
            //retriving developerTeam Id
            int inputDevID = int.Parse(Console.ReadLine());

            //Passing in DeveloperTeam id w/n the _devTeamRepo.Get...(parameters)
            DevTeam devTeam = _devTeamRepo.GetDevTeamByID(inputDevID);

            //info about the team:
            Console.WriteLine($"Team ID:{devTeam.DevTeamID}\n" +
                    $"Team Name:{devTeam.DevTeamName}");

            //info ablout teams developers:
            //devTeam.Developers is a COLLECTION:
            //we have to loop threw
            foreach (var developer in devTeam.Developers)
            {
                DisplayDeveloperInfo(developer);
            }

        }

        private void ViewAllDevTeams()
        {
            Console.Clear();
            List<DevTeam> devTeams = _devTeamRepo.GetDevTeamList();
            //vew the info of the devTeam
            foreach (var team in devTeams)
            {
                Console.WriteLine($"Team ID:{team.DevTeamID}\n" +
                    $"Team Name:{team.DevTeamName}");

                foreach (var developer in team.Developers)
                {
                    DisplayDeveloperInfo(developer);
                    Console.WriteLine("**********************");
                }
            }
        }

        private void CreateDevTam()
        {
            Console.Clear();
            //this controlls our while loop
            bool teamPositionsFilled = false;

            DevTeam devTeam = new DevTeam();
            Console.WriteLine("Please input the DevTeam name.");
            string inputDevTeamName = Console.ReadLine();

            while (teamPositionsFilled == false)
            {
                Console.WriteLine("Do you want to add developers to a DevTeam y/n");
                string inputMoreDevelopers = Console.ReadLine();

                if (inputMoreDevelopers == "y" || inputMoreDevelopers == "Y")
                {
                    //do the creation of a developers here
                    Developer developer = CreateDeveloperHelper();

                    //i need to add the developer to the team
                    devTeam.Developers.Add(developer);
                }
                if (inputMoreDevelopers == "n" || inputMoreDevelopers == "N")
                {
                    //we need to add the DevTeam to the repository
                    _devTeamRepo.AddDevTeamsToList(devTeam);
                    //this will terminate the while loop
                    teamPositionsFilled = true;
                }

            }

        }
        //create Developer helper method
        private Developer CreateDeveloperHelper()
        {
            Console.Clear();
            // make a developer
            Developer developer = new Developer();
            Console.WriteLine("please input a developer name.");
            string inputDeveloperName = Console.ReadLine();
            // Time to user newly created developer.
            developer.DevName = inputDeveloperName;

            //Ask user if they have pluralsight.
            Console.WriteLine("Does the user have pluralsight? y/n");
            string inputHasPluralsight = Console.ReadLine();
            if (inputHasPluralsight == "y" || inputHasPluralsight == "Y")
            {
                // assign bool value to newly created developer
                developer.HasPluralsightLicense = true;

            }
            else if (inputHasPluralsight == "n" || inputHasPluralsight == "N")
            {
                developer.HasPluralsightLicense = false;
            }

            // All properties associated with the Developer Class has been fulfilled
            // Lets add the newly reated developer to the repository
            // This is the "Create"part of the repository
            _developerRepo.AddDeveloperToDirectory(developer);
            return developer;
        }
        private void GetDevelopersWithOuthPSL()
        {
            Console.Clear();
            List<Developer> developers = _developerRepo.FindDeveloperswithoutPSL();
            foreach (var developer in developers)
            {
                DisplayDeveloperInfo(developer);
            }
        }



        private void DeleteDeveloper()
        {
            Console.Clear();
            Console.WriteLine("Please input the user ID to be deleted.");
            int inputDeveloperID = int.Parse(Console.ReadLine());// Taking the uer input and parsing to an int.
            bool isSuccessful = _developerRepo.RemoveDeveloper(inputDeveloperID);
            if (isSuccessful)
            {
                Console.WriteLine($" developer ID:{inputDeveloperID} hasbeen removed");
            }
            else
            {
                Console.WriteLine($" developer ID:{inputDeveloperID} has not been removed");

            }
        }

        private void UpdateDeveloper()
        {
            Console.Clear();
            Console.WriteLine("Please input the ID of te developer to be updated.");
            int inputDeveloperID = int.Parse(Console.ReadLine());// Taking the uer input and parsing to an int.
            Developer newDeveloper = new Developer();

            Console.WriteLine("Please input the developer name");
            string inputDevName = Console.ReadLine();
            newDeveloper.DevName = inputDevName;

            //Ask user if they have pluralsight.
            Console.WriteLine("Does the user have pluralsight? y/n");
            string inputHasPluralsight = Console.ReadLine();
            if (inputHasPluralsight == "y" || inputHasPluralsight == "Y")
            {
                // assign bool value to newly created developer
                newDeveloper.HasPluralsightLicense = true;

            }
            else if (inputHasPluralsight == "n" || inputHasPluralsight == "N")
            {
                newDeveloper.HasPluralsightLicense = false;
            }

            bool isSuccessful = _developerRepo.UpdateDeveloper(inputDeveloperID, newDeveloper);


            if (isSuccessful)
            {
                Console.WriteLine("Update Successful");
            }
            else
            {
                Console.WriteLine("Update failed");
            }



        }

        private void ViewDeveloperById()
        {
            Console.Clear(); // clear console
            Console.WriteLine("Please input a developerID");// Asking for input from the user
            int inputDeveloperID = int.Parse(Console.ReadLine());// Taking the uer input and parsing to an int.
            Developer developer = _developerRepo.GetDeveloperByID(inputDeveloperID); // Compares the ID and returns the developer
            DisplayDeveloperInfo(developer); // returns specific developer
        }

        private void ViewAllDevelopers()
        {
            Console.Clear();
            List<Developer> developers = _developerRepo.GetAllDevelopers();

            foreach (var developer in developers)
            {
                DisplayDeveloperInfo(developer);
            }

        }

        //helper method 
        private void DisplayDeveloperInfo(Developer developer)
        {
            Console.WriteLine($"{developer.ID}\n" +
                $"{developer.DevName}\n" +
                $"{developer.HasPluralsightLicense}\n");
        }

        private void CreateDeveloper()
        {
            Console.Clear();
            // make a developer
            Developer developer = new Developer();
            Console.WriteLine("please input a developer name.");
            string inputDeveloperName = Console.ReadLine();
            // Time to user newly created developer.
            developer.DevName = inputDeveloperName;

            //Ask user if they have pluralsight.
            Console.WriteLine("Does the user have pluralsight? y/n");
            string inputHasPluralsight = Console.ReadLine();
            if (inputHasPluralsight == "y" || inputHasPluralsight == "Y")
            {
                // assign bool value to newly created developer
                developer.HasPluralsightLicense = true;

            }
            else if (inputHasPluralsight == "n" || inputHasPluralsight == "N")
            {
                developer.HasPluralsightLicense = false;
            }

            // All properties associated with the Developer Class has been fulfilled
            // Lets add the newly reated developer to the repository
            // This is the "Create"part of the repository
            _developerRepo.AddDeveloperToDirectory(developer);
        }

        private void SeedDevelopers()
        {
            Developer dev1 = new Developer("Steve", true);
            Developer dev2 = new Developer("Paul", false);
            Developer dev3 = new Developer("Steve", false);
            Developer dev4 = new Developer("Steve", false);

            _developerRepo.AddDeveloperToDirectory(dev1);
            _developerRepo.AddDeveloperToDirectory(dev2);
            _developerRepo.AddDeveloperToDirectory(dev3);
            _developerRepo.AddDeveloperToDirectory(dev4);

            DevTeam devTeam1 = new DevTeam("Andrew");
            devTeam1.Developers.Add(dev1);
            devTeam1.Developers.Add(dev2);

            DevTeam devTeam2 = new DevTeam("Charles");
            devTeam2.Developers.Add(dev3);

            DevTeam devTeam3 = new DevTeam("Larry");
            devTeam3.Developers.Add(dev4);

            // Add DeveTeams to Repository
            _devTeamRepo.AddDevTeamsToList(devTeam1);
            _devTeamRepo.AddDevTeamsToList(devTeam2);
            _devTeamRepo.AddDevTeamsToList(devTeam3);

        }
    }
}
