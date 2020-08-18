using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entoarox.Framework.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AgricultureAssociation.CustomUI;
using StardewModdingAPI;
using StardewValley;

namespace AgricultureAssociation
{
    class MenuFactory
    {

        public static FrameworkMenu TestMenu = new FrameworkMenu(new Point(150, 100));
        public static ProgressbarExtra ProgBar = new ProgressbarExtra(new Point(70, 38), 5, 50, 13, Color.Gray, Color.Teal);

        public static ModEntry Mod;

        public static void Init()
        {
            TestMenu.AddComponent(new TextComponent(new Point(50, 0), "TestMenu"));
            TestMenu.AddComponent(new ButtonFormComponent(new Point(10, 20), "TestButton1"));
            TestMenu.AddComponent(new ButtonFormComponent(new Point(10, 35), "TestButton2", ClickHandler));
            TestMenu.AddComponent(new ButtonFormComponent(new Point(10, 50), "TestButton3"));
            TestMenu.AddComponent(ProgBar);
        }

        private static void ClickHandler(IInteractiveMenuComponent component, IComponentContainer collection, FrameworkMenu menu)
        {
            MenuFactory.ProgBar.Value++;
            Mod.Log("pressed");
        }
    }
}
