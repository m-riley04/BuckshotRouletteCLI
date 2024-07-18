using BuckshotRouletteIRL.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuckshotRouletteIRL
{
    public class Player
    {
        public List<Item> Items = new();
        public readonly string Name = "Player";
        public int Health { get; set; } = 2;
        public int TotalWins { get; set; } = 0;
        public bool IsHandcuffed { get; set; } = false;

        public Player(string name)
        {
            Name = name;
        }
    }
}
