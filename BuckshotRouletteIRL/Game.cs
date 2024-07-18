using BuckshotRouletteIRL.Enums;
using BuckshotRouletteIRL.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BuckshotRouletteIRL
{
    public class Game
    {
        private readonly int MAX_SHELLS = 8;
        private readonly int MIN_SHELLS = 2;
        private readonly int MAX_HEALTH = 4;
        private readonly int MIN_HEALTH = 2;

        public int ReturnCode = 0;
        private GameStateEnum State = GameStateEnum.Running;
        private Shotgun Shotgun = new();

        private readonly Player RedPlayer = new("Red");
        private readonly Player BluePlayer = new("Blue");

        public int Start()
        {
            Console.WriteLine("GAME STARTED\n");
            Console.WriteLine("--CONTROLS--\n's' : pick up shotgun \n'i' : open inventory\n");
            return Loop();
        }

        public int Loop()
        {
            while (State == GameStateEnum.Running)
            {
                ShellEnum chambered = ShellEnum.None, ejected;
                Player wielder = RedPlayer;
                Player opponent = BluePlayer;
                Player target = BluePlayer;
                
                for (int i = 0; i < 3; i++)
                {
                    // Load shotgun with random amount of shells
                    Console.WriteLine("Loading shells...");
                    Shotgun.LoadMagazine(RandomNumberGenerator.GetInt32(MIN_SHELLS, MAX_SHELLS+1));

                    DisplayShells(true);
                    DisplayHealth();

                    while (RedPlayer.Health > 0 && BluePlayer.Health > 0 && Shotgun.Magazine.Count > 0)
                    {
                        bool isChoosing = true;

                        Console.WriteLine($"=== PLAYER {wielder.Name}'S TURN ===");

                        while (isChoosing)
                        {
                            // Get user input
                            string input = Console.ReadLine()?.ToLower().Trim() ?? "";

                            switch (input)
                            {
                                case ("r"):
                                    ejected = Shotgun.Rack();
                                    Console.WriteLine($"Shotgun racked: a {(ejected == ShellEnum.Live ? "live" : "blank")} shell pops out.");
                                    isChoosing = false;
                                    break;

                                case ("i"):
                                    Console.WriteLine("-- INVENTORY --");
                                    break;

                                case ("s"):
                                    _ = ChooseTarget() == TargetEnum.Opponent ? target = opponent : target = wielder;

                                    chambered = Shotgun.Fire();
                                    Console.WriteLine($"Shotgun fired: it was {(chambered == ShellEnum.Live ? "live" : "blank")}.");
                                    if (chambered == ShellEnum.Live)
                                    {
                                        target.Health--;
                                        DisplayHealth();
                                    }
                                    isChoosing = false;
                                    break;
                                default:
                                    break;
                            }
                        }

                        // Swap target and wielder
                        if (target.GetHashCode() != wielder.GetHashCode() || chambered != ShellEnum.Blank) (opponent, wielder) = (wielder, opponent);
                    }

                    // Check for a winner
                    if (RedPlayer.Health <= 0 || RedPlayer.Health <= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"PLAYER {(RedPlayer.Health >= 0 ? RedPlayer.Name : BluePlayer.Name)}");
                        Console.ResetColor();

                        // Set random player health
                        int health = RandomNumberGenerator.GetInt32(MIN_HEALTH, MAX_HEALTH + 1);
                        RedPlayer.Health = health;
                        BluePlayer.Health = health;
                    }
                }


            }
            return Stop();
        }

        public int Stop()
        {
            return ReturnCode;
        }

        /// <summary>
        /// Opens a dialogue option and asks which person to target
        /// </summary>
        /// <returns>the option the user chooses</returns>
        private TargetEnum ChooseTarget()
        {
            Console.WriteLine("Please choose a target:\n1) Opponent\n2) Self");
            string input = "";
            string[] options = ["1", "2"];

            while (input == "" && !options.Contains(input))
            {
                input = Console.ReadLine()?.ToLower().Trim() ?? "";
                switch (input)
                {
                    case "1":
                        return TargetEnum.Opponent;
                    case "2":
                        return TargetEnum.Self;
                    default:
                        input = "";
                        break;
                }
            }
            return TargetEnum.Opponent;
        }

        private void DisplayHealth()
        {
            Console.WriteLine($"PLAYER {RedPlayer.Name}: {String.Concat(Enumerable.Repeat(" █", RedPlayer.Health))} \t \t PLAYER {BluePlayer.Name}: {String.Concat(Enumerable.Repeat(" █", BluePlayer.Health))}");
        }

        private void DisplayShells(bool showColoredShells)
        {
            if (showColoredShells)
            {
                // Draw each shell colored
                foreach (ShellEnum shell in Shotgun.Magazine)
                {
                    if (shell == ShellEnum.Live) Console.ForegroundColor = ConsoleColor.DarkRed;
                    else Console.ForegroundColor = ConsoleColor.DarkCyan;

                    Console.Write(" █");
                }
                Console.Write("\n");

                // Reset color
                Console.ResetColor();
            }
            Console.WriteLine($"{Shotgun.LiveShells} live shells, {Shotgun.BlankShells} blank shells");

        }
    }
}
