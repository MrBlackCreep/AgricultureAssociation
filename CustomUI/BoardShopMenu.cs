using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Entoarox.Framework.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PyTK.Extensions;
using StardewValley;

namespace AgricultureAssociation.CustomUI
{
    class BoardShopMenu
    {
        //TODO Everything
        /*

Neutral
- Shipment evaluation (slight rep increase for hq crops)
- Blessing of Luck (Increases luck by 1-2 levels)


Friendly
- Organise banquet (Minor friendship increase for all villagers)
- (Toggle) Speed boost


Honored
- (Toggle) Care Packages (Disable friendship decay)
- Dousing plane


Revered
- Weather machine II (decide weather for tomorrow)
- 


Exalted
-


         */


        public static FrameworkMenu Menu = new FrameworkMenu(new Point(200, 140));

        public static void Init()
        {
            Menu = new FrameworkMenu(new Point(200, 140));
            int rank = AssociationHandler.Main.Rank;

            Menu.AddComponent(new ButtonFormComponent(new Point(0,0),80,"Shipment Eval", OnShipEvalButton));
            Menu.AddComponent(new ButtonFormComponent(new Point(100, 0), 80, "Blessing of Luck", OnLuckButton));

            Menu.AddComponent(new ButtonFormComponent(new Point(0, 20), 80, "Organize Banquet", OnBanquetButton));
            Menu.AddComponent(new ButtonFormComponent(new Point(100, 20), 80, "Speed Boost", OnSpeedButton));

            Menu.AddComponent(new ButtonFormComponent(new Point(0, 40), 80, "Dousing Plane", OnPlaneButton));
            Menu.AddComponent(new ButtonFormComponent(new Point(100, 40), 80, "Care Packages", OnCareButton));

            Menu.AddComponent(new ButtonFormComponent(new Point(0, 60), 80, "Weather Machine", OnWeatherButton));
            Menu.AddComponent(new ButtonFormComponent(new Point(100, 60), 80, "W.I.P.", OnWip1Button));

            Menu.AddComponent(new ButtonFormComponent(new Point(100, 80), 80, "W.I.P.", OnWip2Button));
        }

        public static void Update()
        {

        }

        private static void OnShipEvalButton(IInteractiveMenuComponent component, IComponentContainer container,
            FrameworkMenu menu)
        {

        }

        private static void OnLuckButton(IInteractiveMenuComponent component, IComponentContainer container,
            FrameworkMenu menu)
        {

        }

        private static void OnBanquetButton(IInteractiveMenuComponent component, IComponentContainer container,
            FrameworkMenu menu)
        {

        }

        private static void OnSpeedButton(IInteractiveMenuComponent component, IComponentContainer container,
            FrameworkMenu menu)
        {

        }

        private static void OnPlaneButton(IInteractiveMenuComponent component, IComponentContainer container,
            FrameworkMenu menu)
        {

        }

        private static void OnCareButton(IInteractiveMenuComponent component, IComponentContainer container,
            FrameworkMenu menu)
        {

        }

        private static void OnWeatherButton(IInteractiveMenuComponent component, IComponentContainer container,
            FrameworkMenu menu)
        {

        }

        private static void OnWip1Button(IInteractiveMenuComponent component, IComponentContainer container,
            FrameworkMenu menu)
        {

        }

        private static void OnWip2Button(IInteractiveMenuComponent component, IComponentContainer container,
            FrameworkMenu menu)
        {

        }

        private static void OnContractButton(IInteractiveMenuComponent component, IComponentContainer container,
            FrameworkMenu menu)
        {

        }

        private static string AwaitingConfirmation;
        private static void OpenConfirmationMenu(string type)
        {
            string label1 = "";
            string label2 = "";
            AwaitingConfirmation = type;
            var str = "Inactive";
            switch (type)
            {
                case "ship":
                    label1 = "Let your shipment for today be evaluated by the Association to earn extra Reputation.";
                    label2 = "Cost: 15x Favor";
                    break;
                case "luck":
                    label1 = "Let a blessing to Yoba slightly increase your luck for today.";
                    label2 = "Cost: 10x Favor";
                    break;
                case "banquet":
                    label1 = "Organize a Banquet for Pelican Town, everyone is gonna be very happy about this!";
                    label2 = "Cost: 25x Favor";
                    break;
                case "speed":
                    if (AssociationHandler.Main.Speed)
                    {
                        str = "Active";
                    }
                    label1 = "A shipment of very strong coffee every morning! Guaranteed to speed up your day!";
                    label2 = "Cost: 5x Favor per day     //     Currently " + str;
                    break;
                case "douse":
                    label1 = "Charter a dousing plane to water all your crops for today.";
                    label2 = "Cost: 8x Favor";
                    break;
                case "care":
                    if (AssociationHandler.Main.Care)
                    {
                        str = "Active";
                    }
                    label1 = "Sends Care Packages to the villagers of Pelican Town every day.";
                    label2 = "Cost: 2x Favor per day     //     Currently " + str;
                    break;
                case "weather":
                    label1 = "Make it rain tomorrow! W.I.P. (adding more weather options)";
                    label2 = "Cost: 8x Favor";
                    break;
            }
            var menu = new FrameworkMenu(new Point(185, 35), false);
            var text1 = new TextComponent(new Point(0, 0), label1);
            var text2 = new TextComponent(new Point(0, 7), label2);
            var accept = new ClickableTextureComponent(new Rectangle(140, 16, 12, 12), Game1.content.Load<Texture2D>("LooseSprites//Cursors").getArea(new Rectangle(128, 256, 64, 64)), OnAccept);
            var decline = new ClickableTextureComponent(new Rectangle(155, 16, 12, 12), Game1.content.Load<Texture2D>("LooseSprites//Cursors").getArea(new Rectangle(192, 256, 64, 64)), OnDecline);

            menu.AddComponent(text1);
            menu.AddComponent(text2);
            menu.AddComponent(accept);
            menu.AddComponent(decline);
            Game1.activeClickableMenu = menu;
        }

        private static void OnAccept(IInteractiveMenuComponent component, IComponentContainer container,
            FrameworkMenu menu)
        {
            switch (AwaitingConfirmation)
            {
                case "ship":
                    AssociationHandler.Main.Eval = true;
                    break;
                case "luck":
                    
                    break;
                case "banquet":

                    break;
                case "speed":

                    break;
                case "douse":

                    break;
                case "care":

                    break;
                case "weather":

                    break;

            }
        }

        private static void OnDecline(IInteractiveMenuComponent component, IComponentContainer container,
            FrameworkMenu menu)
        {
            Game1.activeClickableMenu = Menu;
        }
    }
}

