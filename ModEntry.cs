using System;
using System.Runtime.CompilerServices;
using System.Threading;
using AgricultureAssociation.CustomUI;
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
            AssociationHandler.Mod = this;
            helper.Events.Input.ButtonPressed += OnButtonPressed;
            helper.Events.GameLoop.SaveLoaded += OnSaveLoaded;
            helper.Events.Display.RenderingActiveMenu += MenuHandler.OnRenderingActiveMenu;
            helper.Events.GameLoop.DayStarted += OnDayStarted;


        }

        private void OnSaveLoaded(object sender, SaveLoadedEventArgs e)
        {
            MenuHandler.Init();
            AssociationHandler.GenerateCrops();
            AssociationHandler.Main = new Association();

        }

        private static void OnButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            // ignore if player hasn't loaded a save yet
            if (!Context.IsWorldReady)
                return;

            if (Game1.eventUp && !Game1.currentLocation.currentEvent.playerControlSequence
                || Game1.currentBillboard != 0 || Game1.activeClickableMenu != null || Game1.menuUp || Game1.nameSelectUp
                || Game1.IsChatting || Game1.dialogueTyping || Game1.dialogueUp
                || Game1.player.UsingTool || Game1.pickingTool || Game1.numberOfSelectedItems != -1 || Game1.fadeToBlack || e.Button != SButton.MouseRight)
                return;

            if (!Game1.currentLocation.Objects.ContainsKey(e.Cursor.GrabTile))
                return;
            var craftable = Game1.currentLocation.Objects[e.Cursor.GrabTile];
            if (!craftable.bigCraftable.Value && craftable.Name == "Raff.AgricultureAssociationJson")
                return;

            BoardMainMenu.Update();
            Game1.activeClickableMenu = BoardMainMenu.Menu;
        }

        private void OnDayStarted(object sender, DayStartedEventArgs e)
        {
            if (Game1.dayOfMonth < 6)
            {
                AssociationHandler.Main.GenerateSeasonalContracts();
            }

        }




    }
}