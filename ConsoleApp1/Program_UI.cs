using DevTeams_POCOs;
using DevTeams_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace DevTeams_UI
{
    public class Program_UI
    {
        private readonly DeveloperRepository _devRepo;
        private readonly DevTeamRepository _devTeamRepo;

        public Program_UI()
        {
            _devRepo = new DeveloperRepository();
            _devTeamRepo = new DevTeamRepository(_devRepo);
        }

        public void Run()
        {
            Seed();
            RunApplication();
        }

        private void RunApplication()
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("Welcome to Dev Teams UI\n" +
                    "1. Add a Developer\n" +
                    "2. View All Existing Developers\n" +
                    "3. View a Specific Developer\n" +
                    "4. View a List of Developers with/without Pluralsight\n" +
                    "5. Update an Existing Developer\n" +
                    "6. Remove an Existing Developer\n" +
                    "=======================================================================\n" +
                    "7. Create new Dev Team\n" +
                    "8. View all Existing Dev Teams\n" +
                    "9. View a Specific Dev Team\n" +
                    "10. Add a Developer to a Dev Team\n" +
                    "11. Remove a Developer from a Dev Team\n" +
                    "12. Remove an Existing Dev Team\n" +
                    "=======================================================================\n" +
                    "500. Exit Dev Teams UI\n");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        AddADeveloper();
                        break;
                    case "2":
                        ViewAllExistingDevelopers();
                        break;
                    case "3":
                        ViewAnExistingDeveloper();
                        break;
                    case "4":
                        ViewDevelopersByPluralsight();
                        break;
                    case "5":
                        UpdateAnExistingDeveloper();
                        break;
                    case "6":
                        DeleteAnExistingDevelioer();
                        break;
                    case "7":
                        CreateADevTeam();
                        break;
                    case "8":
                        ViewAllExistingDevTeams();
                        break;
                    case "9":
                        ViewAnExistingDevTeam();
                        break;
                    case "10":
                        AddADevToADevTeam();
                        break;
                    case "11":
                        RemoveADevFromADevTeam();
                        break;
                    case "12":
                        DeleteAnExistingDevTeam();
                        break;
                    case "500":
                        Console.WriteLine("Have a great day!\n" +
                            "=======================================================================\n");
                        Thread.Sleep(1000);
                        Console.WriteLine("Closing Dev Teams UI");
                        Thread.Sleep(1000);
                        isRunning = false;
                        break;
                    default:
                        WriteLine("Invalid Selection");
                        WaitForKey();
                        break;
                }
            }
        }

        private void AddADeveloper()
        {
            Console.Clear();
            Developer developer = new Developer();

            Console.Write("Please enter developer's first name: ");
            string firstName = Console.ReadLine();
            developer.FirstName = firstName;

            Console.Write("Please enter developer's last name: ");
            string lastName = Console.ReadLine();
            developer.LastName = lastName;

            Console.Write("Does the developer have access to Pluralsight? (y/n): ");
            string hasPluralsight = Console.ReadLine();
            switch (hasPluralsight)
            {
                case "y":
                    developer.HasPluralsight = true;
                    break;
                case "n":
                    developer.HasPluralsight = false;
                    break;
                default:
                    break;
            }

            _devRepo.AddDeveloperToDirectry(developer);
        }

        private void ViewAllExistingDevelopers()
        {
            Console.Clear();
            Console.WriteLine("All existing developers: ");
            List<Developer> developers = _devRepo.GetDeveloperList();
            foreach (Developer developer in developers)
            {
                DisplayDeveloperListItem(developer);
            }
            WaitForKey();
        }

        private void ViewAnExistingDeveloper()
        {
            Console.Clear();
            Console.Write("Please enter a Developer ID#: ");
            string inputID = Console.ReadLine();
            int devID = Convert.ToInt32(inputID);
            Developer devInfo = _devRepo.GetDeveloperByID(devID);

            if (devInfo == null)
            {
                Console.WriteLine("Developer not found.");
            }
            else
            {
                DisplayDeveloperListItem(devInfo);
            }

            WaitForKey();
        }

        private void ViewDevelopersByPluralsight()
        {
            Console.Clear();
            Console.Write("Do you want to see developers with or without Pluralsight?\n" +
                "1. With\n" +
                "2. Without\n");
            List<Developer> foundDevelopers = new List<Developer>();
            string hasPluralsight = Console.ReadLine();
            Console.Clear();
            switch (hasPluralsight)
            {
                case "1":
                    foundDevelopers = _devRepo.GetDeveloperByPluralsight(true);
                    break;
                case "2":
                    foundDevelopers = _devRepo.GetDeveloperByPluralsight(false);
                    break;
                default:
                    break;
            }
            foreach (Developer developer in foundDevelopers)
            {
                DisplayDeveloperListItem(developer);
            }
            WaitForKey();
        }

        private void UpdateAnExistingDeveloper()
        {
            Console.Clear();
            Console.WriteLine("List of all current Developers:");
            var developer = new Developer();
            var devs = _devRepo.GetDeveloperList();
            foreach (var dev in devs)
            {
                Console.WriteLine($"{dev.ID} {dev.FullName}");
            }
            Console.WriteLine("=======================================================================");
            Console.WriteLine("Select ID of developer you would like to update: ");
            int userInput = int.Parse(Console.ReadLine());

            Console.Clear();

            Console.WriteLine("What is the correct Developer First Name?");
            string firstName = Console.ReadLine();
            developer.FirstName = firstName;

            Console.WriteLine("What is the correct Developer Last Name?");
            string lastName = Console.ReadLine();
            developer.LastName = lastName;

            Console.Write("Does the developer have access to Pluralsight? (y/n): ");
            string hasPluralsight = Console.ReadLine();
            switch (hasPluralsight)
            {
                case "y":
                    developer.HasPluralsight = true;
                    break;
                case "n":
                    developer.HasPluralsight = false;
                    break;
                default:
                    break;
            }

            _devRepo.UpdateExistingDeveloper(userInput,developer);

            Console.Clear();
            Console.WriteLine("Update completed");
            WaitForKey();

        }

        private void DeleteAnExistingDevelioer()
        {
            Console.Clear();
            Console.WriteLine("Which item would you like to remove?");

            int index = 0;
            List<Developer> developer = _devRepo.GetDeveloperList();
            foreach (Developer item in developer)
            {
                Console.Write($"{index + 1}. ");
                DisplayDeveloperListItem(item);
                index++;
            }
            string optionString = Console.ReadLine();
            int option = Convert.ToInt32(optionString);

            Developer itemToDelete = developer[option - 1];

            Console.WriteLine("Are you sure you want to delete this? (y/n)");
            DisplayDeveloperListItem(itemToDelete);
            if (Console.ReadLine() == "y")
            {
                _devRepo.DeleteExistingDeveloper(itemToDelete);
                Console.WriteLine("Item deleted!");
            }
            else
            {
                Console.WriteLine("Canceled");
            }
            WaitForKey();
        }

        private void CreateADevTeam()
        {
            Console.Clear();
            DevTeam devTeam = new DevTeam();

            Console.Write("Please enter Dev Team's team name: ");
            string teamName = Console.ReadLine();
            devTeam.TeamName = teamName;

            _devTeamRepo.AddDevTeam(devTeam);
        }

        private void ViewAllExistingDevTeams()
        {
            Console.Clear();
            Console.WriteLine("All existing Dev Teams: ");
            List<DevTeam> devTeams = _devTeamRepo.GetDevTeamList();
            foreach (DevTeam devTeam in devTeams)
            {
                DisplayDevTeamListItem(devTeam);
            }
            WaitForKey();
        }

        private void ViewAnExistingDevTeam()
        {
            Console.Clear();
            Console.Write("Please enter a Dev Team ID#: ");
            string inputID = Console.ReadLine();
            int devTeamID = Convert.ToInt32(inputID);
            DevTeam devTeamInfo = _devTeamRepo.GetDevTeamByID(devTeamID);

            if (devTeamInfo == null)
            {
                Console.WriteLine("Dev Team not found.");
            }
            else
            {
                DisplayDevTeamDetails(devTeamInfo);
            }

            WaitForKey();
        }

        private void AddADevToADevTeam()
        {
            Console.Clear();
            Console.WriteLine("Please enter the Dev Team ID# you would like to add a developer to: ");
            string inputTeamID = Console.ReadLine();
            int DevTeamID = Convert.ToInt32(inputTeamID);
            DevTeam devTeamIDName = _devTeamRepo.GetDevTeamByID(DevTeamID);
            Console.WriteLine($"Please enter the Developer ID# you would like to add to {devTeamIDName.TeamName}: ");
            string inputDevId = Console.ReadLine();
            int DevID = Convert.ToInt32(inputDevId);
            _devTeamRepo.AddDevToDevTeam(DevTeamID, DevID);
            string userInput;
            do
            {
                Console.WriteLine($"Would you like to add another Developer to {devTeamIDName.TeamName} (y/n)");
                userInput = Console.ReadLine();
                if (userInput == "y")
                {
                    Console.WriteLine($"Please enter the Developer ID# you would like to add to {devTeamIDName.TeamName}: ");
                    string inputIfDevId = Console.ReadLine();
                    int IfDevID = Convert.ToInt32(inputIfDevId);
                    _devTeamRepo.AddDevToDevTeam(DevTeamID, IfDevID);
                }
                else
                {
                    Console.WriteLine($"Developer(s) added to {devTeamIDName.TeamName}");
                    WaitForKey();
                }
            } while (userInput != "n");
        }

        private void RemoveADevFromADevTeam()
        {
            Console.Clear();
            Console.WriteLine("Please enter the Dev Team ID# you would like to remove a developer from: ");
            string inputTeamID = Console.ReadLine();
            int DevTeamID = Convert.ToInt32(inputTeamID);
            DevTeam devTeamIDName = _devTeamRepo.GetDevTeamByID(DevTeamID);
            Console.WriteLine($"Please enter the Developer ID# you would like to remove from {devTeamIDName.TeamName}: ");
            string inputDevId = Console.ReadLine();
            int DevID = Convert.ToInt32(inputDevId);
            _devTeamRepo.RemoveDevFromDevTeam(DevTeamID, DevID);
        }

        private void DeleteAnExistingDevTeam()
        {
            Console.Clear();
            Console.WriteLine("Which Dev Team would you like to remove?");

            int index = 0;
            List<DevTeam> devTeam = _devTeamRepo.GetDevTeamList();
            foreach (DevTeam item in devTeam)
            {
                Console.Write($"{index + 1}. ");
                DisplayDevTeamListItem(item);
                index++;
            }
            string optionString = Console.ReadLine();
            int option = Convert.ToInt32(optionString);

            DevTeam itemToDelete = devTeam[option - 1];

            Console.WriteLine("Are you sure you want to delete this team? (y/n)");
            DisplayDevTeamListItem(itemToDelete);
            if (Console.ReadLine() == "y")
            {
                _devTeamRepo.DeleteExistingDevTeam(itemToDelete);
                Console.WriteLine("Item deleted!");
            }
            else
            {
                Console.WriteLine("Canceled");
            }
            WaitForKey();
        }

        private void DisplayDeveloperListItem(Developer developer)
        {
            Console.WriteLine($"{developer.FullName} ({developer.ID})\n" +
                $"Developer has Pluralsight: {(developer.HasPluralsight ? "Yes" : "No")} \n" +
                $"=======================================================================");
        }

        private void DisplayDevTeamListItem(DevTeam devTeam)
        {
            Console.WriteLine($"{devTeam.TeamName} ({devTeam.TeamID})\n" +
                $"=======================================================================");
        }

        private void DisplayDevTeamDetails(DevTeam devTeam)
        {
            Console.WriteLine($"{devTeam.TeamID}.) {devTeam.TeamName}\n");
            Console.WriteLine($"Developers currently on {devTeam.TeamName}");
            foreach (Developer developers in devTeam.Developers)
            {
                Console.WriteLine(developers.FullName);
            }
            Console.WriteLine("=======================================================================");
        }

        private void WaitForKey()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        private void Seed()
        {
            Developer andySitter = new Developer();
            andySitter.FirstName = "Andy";
            andySitter.LastName = "Sitter";
            andySitter.HasPluralsight = true;
            _devRepo.AddDeveloperToDirectry(andySitter);

            Developer jordenSitter = new Developer();
            jordenSitter.FirstName = "Jorden";
            jordenSitter.LastName = "Sitter";
            jordenSitter.HasPluralsight = false;
            _devRepo.AddDeveloperToDirectry(jordenSitter);

            Developer johnWick = new Developer();
            johnWick.FirstName = "John";
            johnWick.LastName = "Wick";
            johnWick.HasPluralsight = false;
            _devRepo.AddDeveloperToDirectry(johnWick);

            Developer jonathanTaylor = new Developer();
            jonathanTaylor.FirstName = "Jonathan";
            jonathanTaylor.LastName = "Taylor";
            jonathanTaylor.HasPluralsight = true;
            _devRepo.AddDeveloperToDirectry(jonathanTaylor);

            DevTeam teamOne = new DevTeam();
            teamOne.TeamName = "The A Team";
            _devTeamRepo.AddDevTeam(teamOne);
            _devTeamRepo.AddDevToDevTeam(teamOne.TeamID, johnWick.ID);

            DevTeam teamTwo = new DevTeam();
            teamTwo.TeamName = "The Dumb Devs";
            _devTeamRepo.AddDevTeam(teamTwo);
            _devTeamRepo.AddDevToDevTeam(teamTwo.TeamID, andySitter.ID);
            _devTeamRepo.AddDevToDevTeam(teamTwo.TeamID, jordenSitter.ID);


        }
    }
}
