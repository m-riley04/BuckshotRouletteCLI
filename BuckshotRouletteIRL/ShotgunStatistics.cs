using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuckshotRouletteIRL
{
    public class ShotgunStatistics
    {
        public int TotalFired { get; set; } = 0;
        public int TotalLoaded { get; set; } = 0;
        public int TotalRacked { get; set; } = 0;

        /// <summary>
        /// Resets all statistics
        /// </summary>
        public void Reset()
        {
            TotalFired = 0;
            TotalLoaded = 0;
            TotalRacked = 0;
        }
    }
}
