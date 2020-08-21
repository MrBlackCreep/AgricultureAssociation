using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace AgricultureAssociation.Contracts
{
    class ContractElement
    {
        public string Name;
        public int ItemId;
        public Texture2D Sprite;
        public bool[] Seasons = new bool[4];
        public string SeasonString;
        public double Difficulty;
        public double Yield;

    }
}
