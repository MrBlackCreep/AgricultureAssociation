using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using StardewValley.Menus;
using Entoarox.Framework;
using Entoarox.Framework.UI;
using Microsoft.Xna.Framework.Graphics;
using LogLevel = StardewModdingAPI.LogLevel;

namespace AgricultureAssociation

{
    /// <summary>The mod entry point.</summary>
    public class ModEntry : Mod
    {

        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            helper.Events.Input.ButtonPressed += OnButtonPressed;
            helper.Events.GameLoop.SaveLoaded += OnSaveLoaded;
            helper.Events.Display.RenderingActiveMenu += OnRenderingActiveMenu;
            MenuFactory.Mod = this;




        }

        private void OnSaveLoaded(object sender, SaveLoadedEventArgs e)
        {
            MenuFactory.Init();
        }

        private static void OnButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            // ignore if player hasn't loaded a save yet
            if (!Context.IsWorldReady || e.Button != SButton.R)
                return;

            Game1.activeClickableMenu = MenuFactory.TestMenu;


        }

        private static void OnRenderingActiveMenu(object sender, RenderingActiveMenuEventArgs e)
        {
            if (Game1.activeClickableMenu is FrameworkMenu)
            {
                e.SpriteBatch.Draw(Game1.fadeToBlackRect, Game1.graphics.GraphicsDevice.Viewport.Bounds, Color.Black * 0.4f);
            }
        }

        public void Log(string str)
        {
            this.Monitor.Log(str, LogLevel.Debug);
        }
    }
}