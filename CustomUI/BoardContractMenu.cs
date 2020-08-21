using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entoarox.Framework.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PyTK.Extensions;
using StardewModdingAPI;
using StardewValley;

namespace AgricultureAssociation.CustomUI
{
    class BoardContractMenu
    {
        public static FrameworkMenu Menu;
        private static ClickableTextureComponent[] Bg = new ClickableTextureComponent[8];
        private static TextureComponent[] Crosses = new TextureComponent[8];

        private static readonly ButtonFormComponent BackButton = new ButtonFormComponent(new Point(163, -3),15,"Back", OnBackButton);
        private static readonly LabelComponent HeaderText = new LabelComponent(new Point(0, -16), "Available Seasonal Contracts");
        private static readonly TextComponent DescText1 = new TextComponent(new Point(0,0),"These are the contracts available for today.");
        private static readonly TextComponent DescText2 = new TextComponent(new Point(0, 8), "Once you accept a contract you cannot abandon it.",true,1F,Color.Red);
        private static readonly TextComponent DescText3 = new TextComponent(new Point(0, 16), "If you fail to complete a contract at the end of the season", true, 1F, Color.Red);
        private static readonly TextComponent DescText4 = new TextComponent(new Point(0, 23), "you will incur penalties to your Reputation and Favor.", true, 1F, Color.Red);


        public static void Init()
        {
            for (int i = 0; i < 8; i++)
            {
                if (i <= 3)
                {
                    Bg[i] = new ClickableTextureComponent(new Rectangle(44 * i, 30, 48, 48), Game1.content.Load<Texture2D>("LooseSprites//DialogBoxGreen"),OnContractButton);
                    var temp = new TextureComponent(new Rectangle(44 * i+8, 30+8, 32, 32), Game1.content.Load<Texture2D>("LooseSprites//Cursors").getArea(new Rectangle(268,470,16,16)));
                    temp.Layer = 3;
                    Crosses[i] = temp;
                }
                else
                {
                    Bg[i] = new ClickableTextureComponent(new Rectangle(44 * (i-4), 75, 48, 48), Game1.content.Load<Texture2D>("LooseSprites//DialogBoxGreen"),OnContractButton);
                    var temp = new TextureComponent(new Rectangle(44 * (i - 4)+8, 75+8, 32, 32), Game1.content.Load<Texture2D>("LooseSprites//Cursors").getArea(new Rectangle(268, 470, 16, 16)));
                    temp.Layer = 3;
                    Crosses[i] = temp;
                }
            }

            if (Menu != null)
            {
                Menu.ClearComponents();
            }
            Menu = new FrameworkMenu(new Point(200, 140),false);
            foreach (var bg in Bg)
            {
                Menu.AddComponent(bg);
            }
            Menu.AddComponent(HeaderText);
            Menu.AddComponent(DescText1);
            Menu.AddComponent(DescText2);
            Menu.AddComponent(DescText3);
            Menu.AddComponent(DescText4);
            Menu.AddComponent(BackButton);
        }

        public static void Update()
        {
            Init();
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

        private static void OnBackButton(IInteractiveMenuComponent component, IComponentContainer container,
            FrameworkMenu menu)
        {
            Game1.activeClickableMenu = BoardMainMenu.Menu;
        }

        private static void OnContractButton(IInteractiveMenuComponent component, IComponentContainer container,
            FrameworkMenu menu)
        {
            for (int i = 0; i < 8; i++)
            {
                if (Bg[i] == component)
                {
                    OpenConfirmationMenu(AssociationHandler.Main.AvailableContracts[i]);
                    AwaitingConfirmationPos = i;
                    break;
                }
            }
        }

        private static Contract AwaitingConfirmation;
        private static int AwaitingConfirmationPos;
        private static void OpenConfirmationMenu(Contract c)
        {
            AwaitingConfirmation = c;
            var menu = new FrameworkMenu(new Point(185, 35), false);
            var text1 = new TextComponent(new Point(0,0),"Delivery request: "+ c.AmountNeeded +"x "+c.Item.Name);
            var text2 = new TextComponent(new Point(0,7),"You cannot abandon a contract once you've accepted it.", true,1F,Color.Red);
            var accept = new ClickableTextureComponent(new Rectangle(140,16,12,12),Game1.content.Load<Texture2D>("LooseSprites//Cursors").getArea(new Rectangle(128, 256, 64, 64)),OnAccept);
            var decline = new ClickableTextureComponent(new Rectangle(155, 16, 12, 12), Game1.content.Load<Texture2D>("LooseSprites//Cursors").getArea(new Rectangle(192, 256, 64, 64)),OnDecline);

            menu.AddComponent(text1);
            menu.AddComponent(text2);
            menu.AddComponent(accept);
            menu.AddComponent(decline);
            Game1.activeClickableMenu = menu;
        }

        private static void OnAccept(IInteractiveMenuComponent component, IComponentContainer container,
            FrameworkMenu menu)
        {
            Menu.AddComponent(Crosses[AwaitingConfirmationPos]);
            AssociationHandler.Main.ActiveContracts.Add(AwaitingConfirmation);
            Bg[AwaitingConfirmationPos].Visible = false;
            AwaitingConfirmation = null;
            AwaitingConfirmationPos = -1;
            Game1.activeClickableMenu = Menu;
        }

        private static void OnDecline(IInteractiveMenuComponent component, IComponentContainer container,
            FrameworkMenu menu)
        {
            Game1.activeClickableMenu = Menu;
        }
    }
}
