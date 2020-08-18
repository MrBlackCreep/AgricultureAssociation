using System;
using System.Runtime.CompilerServices;
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
            helper.Events.Display.RenderingActiveMenu += MenuHandler.OnRenderingActiveMenu;


        }

        private void OnSaveLoaded(object sender, SaveLoadedEventArgs e)
        {
            MenuHandler.Init();
        }

        private static void OnButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            // ignore if player hasn't loaded a save yet
            if (!Context.IsWorldReady)
                return;

            if (e.Button == SButton.R)
            {
                MenuHandler.OpenBoardMainMenu();
            }


        }



        public void Log(string str)
        {
            this.Monitor.Log(str, LogLevel.Debug);
        }
    }
}