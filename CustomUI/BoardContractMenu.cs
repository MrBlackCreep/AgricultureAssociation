using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entoarox.Framework.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;

namespace AgricultureAssociation.CustomUI
{
    class BoardContractMenu
    {
        public static FrameworkMenu Menu = new FrameworkMenu(new Point(200, 140));
        private static ClickableTextureComponent[] Bg = new ClickableTextureComponent[8];


        public static void Init()
        {
            for (int i = 0; i < 8; i++)
            {
                if (i <= 3)
                {
                    Bg[i] = new ClickableTextureComponent(new Rectangle(44 * i, 30, 48, 48), Game1.content.Load<Texture2D>("LooseSprites//DialogBoxGreen"));
                }
                else
                {
                    Bg[i] = new ClickableTextureComponent(new Rectangle(44 * (i-4), 75, 48, 48), Game1.content.Load<Texture2D>("LooseSprites//DialogBoxGreen"));
                }
            }
            Menu = new FrameworkMenu(new Point(200, 140));
            foreach (var bg in Bg)
            {
                Menu.AddComponent(bg);
            }
        }

        public static void Update()
        {

        }


    }
}
