using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entoarox.Framework.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PyTK.Extensions;
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
            for (int i = 0; i < 8; i++)
            {
                if (i <= 3)
                {
                    foreach (var component in GenerateContractComponent(AssociationHandler.Main.AvailableContracts[i],
                        new Point(44 * i, 30)))
                    {
                        Menu.AddComponent(component);

                    }
                }
                else
                {
                    foreach (var component in GenerateContractComponent(AssociationHandler.Main.AvailableContracts[i],
                        new Point(44 * (i - 4), 75)))
                    {
                        Menu.AddComponent(component);

                    }
                }
            }
        }

        private static List<IMenuComponent> GenerateContractComponent(Contract c, Point pos)
        {
            List<IMenuComponent> r = new List<IMenuComponent>();
            r.Add(new TextureComponent(new Rectangle(pos.X + 22, pos.Y + 10, 16,16),c.Item.Sprite));
            r.Add(new TextComponent(new Point(pos.X + 10, pos.Y+ 18),AssociationHandler.StaticDigits(c.AmountNeeded, 3) +"x",true, 1.25f));
            r.Add(new TextComponent(new Point(pos.X +15, pos.Y+30),AssociationHandler.StaticDigits(c.RewardFavor, 3)));
            r.Add(new TextureComponent(new Rectangle(pos.X + 26, pos.Y+29,8,8), Game1.content.Load<Texture2D>("LooseSprites//Cursors").getArea(new Rectangle(294, 392, 16, 16))));
            r[0].Layer = 2;
            r[1].Layer = 3;
            r[2].Layer = 2;
            r[3].Layer = 2;
            return r;
        }

    }
}
