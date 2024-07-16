using BuckshotRouletteIRL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BuckshotRouletteIRL
{
    public class Shotgun
    {
        public ShotgunStatistics Stats = new();

        public Queue<ShellEnum> magazine = new();
        public bool IsSawedOff = false;

        /// <summary>
        /// The shell that is currently in the chamber
        /// </summary>
        public ShellEnum ChamberedShell 
        {
            get
            {
                return magazine.Peek();
            } 
        }

        /// <summary>
        /// Fires the shotgun
        /// </summary>
        /// <returns>The state of the shell that was fired</returns>
        public ShellEnum Fire()
        {
            Stats.TotalFired++;
            return magazine.Dequeue();
        }

        /// <summary>
        /// Racks the shotgun
        /// </summary>
        /// <returns>The state of the shell that was racked</returns>
        public ShellEnum Rack()
        {
            Stats.TotalRacked++;
            return magazine.Dequeue();
        }

        /// <summary>
        /// Loads a single shell into the chamber
        /// </summary>
        /// <param name="shell"></param>
        public void LoadShell(ShellEnum shell)
        {
            Stats.TotalLoaded++;
            magazine.Enqueue(shell);
        }

        /// <summary>
        /// Loads a number of shells, choosing an equal amount which are live/blank, in a random order
        /// </summary>
        /// <param name="totalShells"></param>
        public void LoadMagazine(int totalShells)
        {
            List<ShellEnum> shells = new List<ShellEnum>();
            int maxBlanks = totalShells / 2;

            // Load the shells evenly
            for (int i = 1; i <= totalShells; i++)
            {
                if (i <= maxBlanks) LoadShell(ShellEnum.Blank);
                else LoadShell(ShellEnum.Live);
            }

            // Randomize the order of shells
            Random rng = new Random();
            magazine = new Queue<ShellEnum>(magazine.OrderBy(_ => rng.Next()));
        }
    }
}
