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
        public static FrameworkMenu Menu = new FrameworkMenu(new Point(200, 140));

        //Constant menu components
        private static readonly LabelComponent HeaderText = new LabelComponent(new Point(0, -16), "Agricultural Association Board");
        private static readonly TextComponent SeasonalContracts = new TextComponent(new Point(10, 30), "Active Seasonal Contracts:", true, 1F, Color.LightGoldenrodYellow);
        private static readonly TextComponent YearlyContracts = new TextComponent(new Point(10, 70), "Active Year-Long Contracts:", true, 1F, Color.LightGoldenrodYellow);
        private static readonly TextComponent FavorText = new TextComponent(new Point(137, 28), "Current Favor:");

        private static readonly TextureComponent ContractBg = new TextureComponent(new Rectangle(2, 22, 136, 96), Game1.content.Load<Texture2D>("LooseSprites//boardGameBorder"));
        private static readonly TextureComponent FavorStar = new TextureComponent(new Rectangle(165, 38, 16, 16), Game1.content.Load<Texture2D>("LooseSprites//Cursors").getArea(new Rectangle(294, 392, 16, 16)));

        //Clickable
        private static readonly ButtonFormComponent ShopButton = new ButtonFormComponent(new Point(142, 90), 35, "Shop", ButtonClick);
        private static readonly ButtonFormComponent ContractButton = new ButtonFormComponent(new Point(142, 70), 35, "Contracts", ButtonClick);
        private static readonly ButtonFormComponent HelpButton = new ButtonFormComponent(new Point(165, 113), 12, "Help", ButtonClick);

        //Changing menu components
        //TODO Add Containers for individual contract display
        private static readonly ProgressbarExtra Reputation = new ProgressbarExtra(new Point(8, 4), 0, 160, 10, Color.Gray, Color.DeepSkyBlue);
        private static readonly TextComponent RankDisplay = new TextComponent(new Point(8, 15), "Current Rank: Neutral");
        private static readonly TextComponent RepAmount = new TextComponent(new Point(58, 5), "Reputation: 0000/3000", false, 0.25f, Color.BlanchedAlmond);
        private static readonly TextComponent FavorAmount = new TextComponent(new Point(138, 37), "000", true, 2.5f);



        public static void Init()
        {
            Menu.ClearComponents();
            Menu = new FrameworkMenu(new Point(200, 140));

            AddToMenu(ContractBg);
            AddToMenu(FavorStar);
            AddToMenu(HeaderText);
            AddToMenu(FavorText);
            AddToMenu(SeasonalContracts, 2);
            AddToMenu(YearlyContracts, 2);
            AddToMenu(Reputation);
            AddToMenu(RepAmount, 2);
            AddToMenu(RankDisplay);
            AddToMenu(FavorAmount);
            AddToMenu(ContractButton);
            AddToMenu(ShopButton);
            AddToMenu(HelpButton);

            //TODO yearly contracts
            AddToMenu(new TextComponent(new Point(35,80),"W.I.P.",true,4F),3);

            if (AssociationHandler.Main == null)
            {
                return;
            }

            int i = 0;
            foreach (var contract in AssociationHandler.Main.ActiveContracts)
            {
                foreach (var component in CreateContractBlob(i, contract))
                {
                    AddToMenu(component, 3);
                }

                i++;
            }
        }

        public static void Update()
        {
            var a = AssociationHandler.Main;
            if (a.Rank < 4)
            {
                Reputation.Value = (int) (a.Reputation / Association.RepAmounts[a.Rank] * 160);
                RepAmount.Label = "Reputation: "+ a.Reputation + "/" + Association.RepAmounts[a.Rank];
            }
            else
            {
                Reputation.Value = 160;
                RepAmount.Label = "Reputation at maximum";
            }

            RankDisplay.Label = "Current Rank: " + Association.RankNames[a.Rank];
            FavorAmount.Label = AssociationHandler.StaticDigits(a.Favor, 3);




        }

        private static void AddToMenu(IMenuComponent comp, int layer = 1)
        {
            comp.Layer = layer;
            Menu.AddComponent(comp);
        }

        private static void ButtonClick(IInteractiveMenuComponent component, IComponentContainer container, FrameworkMenu menu)
        {
            //TODO DEBUG
            if (component == ContractButton && Game1.dayOfMonth < 60 && AssociationHandler.Main.ActiveContracts.Count < 5)
            {
                Game1.activeClickableMenu = BoardContractMenu.Menu;
            } else if (component == ShopButton)
            {
                Game1.activeClickableMenu = BoardShopMenu.Menu;
            } else if (component == HelpButton)
            {
                AssociationHandler.Main.Reputation += 100;
                Game1.activeClickableMenu = BoardHelpMenu.Menu;
            }

        }

        private static List<IMenuComponent> CreateContractBlob(int count, Contract contract)
        {
            List<IMenuComponent> r = new List<IMenuComponent>();
            r.Add(new TextureComponent(new Rectangle(count*25 + 10, 40 , 16, 16), Game1.content.Load<Texture2D>("Maps//springobjects").getTile(contract.Item.ItemId)));
            r.Add(new TextComponent(new Point(count*25 + 10, 55),AssociationHandler.StaticDigits(contract.AmountReceived, 3)));
            r.Add(new TextComponent(new Point(count*25 + 13, 61), "/"+AssociationHandler.StaticDigits(contract.AmountNeeded,3)));

            if (contract.Completed)
            {
                AddToMenu(new TextureComponent(new Rectangle(count * 25 + 9, 49, 19, 8), Game1.content.Load<Texture2D>("LooseSprites//Cursors").getArea(new Rectangle(340, 409, 25,11))),5);
            }
            return r;
        } 

    }
}
