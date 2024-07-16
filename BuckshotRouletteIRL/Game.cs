using BuckshotRouletteIRL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuckshotRouletteIRL
{
    public class Game
    {
        public int ReturnCode = 0;
        private GameStateEnum State = GameStateEnum.Running;
        private Shotgun Shotgun = new();

        private Player RedPlayer = new();
        private Player BluePlayer = new();

        public int Start()
        {
            return Loop();
        }

        public int Loop()
        {
            while (State == GameStateEnum.Running)
            {

                for (int i = 0; i < 3; i++)
                {
                    // Check for a winner
                    if (RedPlayer.Health <= 0) break;
                    else if (BluePlayer.Health <= 0) break;

                    // Load shotgun
                    Shotgun.LoadMagazine(6);


                }
                
            }
            return Stop();
        }

        public int Stop()
        {
            return ReturnCode;
        }
    }
}
