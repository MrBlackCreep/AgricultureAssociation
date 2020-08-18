using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Entoarox.Framework.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PyTK.Extensions;
using StardewValley;

namespace AgricultureAssociation.CustomUI
{
    class BoardMainMenu
    {
        public static FrameworkMenu Menu;
        private static LabelComponent HeaderText;
        public static ProgressbarExtra Reputation;
        public static TextComponent RankDisplay;
        public static TextComponent RepAmount;
        private static TextureComponent ContractBg;

        public static void Init()
        {
            HeaderText = new LabelComponent(new Point(0, -16), "Agricultural Association Board");
            Reputation = new ProgressbarExtra(new Point(8,4),0,200,10,Color.Gray,Color.Blue);
            RankDisplay = new TextComponent(new Point(8,14),"Current Rank: n/a");
            RepAmount = new TextComponent(new Point(130,14),"Reputation: XXXX/XXXX");
            ContractBg = new TextureComponent(new Rectangle(0, 20, 120, 80), Game1.content.Load<Texture2D>("LooseSprites//boardGameBorder"));

            var menu = new FrameworkMenu(new Point(240, 140));
            menu.AddComponent(ContractBg);
            menu.AddComponent(HeaderText);
            menu.AddComponent(Reputation);
            menu.AddComponent(RankDisplay);
            menu.AddComponent(RepAmount);
            
            Menu = menu;
        }

        public static void Update()
        {

        }
    }
}
