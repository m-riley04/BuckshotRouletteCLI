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
        public int Health = 2;
        public bool IsHandcuffed = false;
    }
}
