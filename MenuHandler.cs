﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entoarox.Framework.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AgricultureAssociation.CustomUI;
using Newtonsoft.Json.Bson;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace AgricultureAssociation
{
    class MenuHandler
    {


        public static void Init()
        {
            BoardMainMenu.Init();
            BoardShopMenu.Init();
            BoardHelpMenu.Init();
        }

        // Render dim background layer
        public static void OnRenderingActiveMenu(object sender, RenderingActiveMenuEventArgs e)
        {
            if (Game1.activeClickableMenu is FrameworkMenu)
            {
                e.SpriteBatch.Draw(Game1.fadeToBlackRect, Game1.graphics.GraphicsDevice.Viewport.Bounds, Color.Black * 0.4f);
            }
        }


    }
}
