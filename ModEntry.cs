using System;
using System.IO;
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
using Newtonsoft.Json;

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
            helper.Events.GameLoop.DayEnding += OnDayEnded;
            helper.Events.GameLoop.Saving += OnSave;


        }

        private void OnSave(object sender, SavingEventArgs e)
        {
            string jsonString = JsonConvert.SerializeObject(AssociationHandler.Main);
            File.WriteAllText(Constants.CurrentSavePath+"//AgricultureAssocation.json", jsonString);
        }

        private void OnSaveLoaded(object sender, SaveLoadedEventArgs e)
        {
            AssociationHandler.GenerateCrops();
            string jsonString;
            try
            {
                jsonString = File.ReadAllText(Constants.CurrentSavePath + "//AgricultureAssocation.json");
            }
            catch
            {
                AssociationHandler.Main = new Association();
                return;
            } 

            AssociationHandler.Main = JsonConvert.DeserializeObject<Association>(jsonString);

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

        private static void OnDayStarted(object sender, DayStartedEventArgs e)
        {
            AssociationHandler.CheckContracts();
            MenuHandler.Init();

            //TODO DEBUG
            if (Game1.dayOfMonth < 60)
            {
                AssociationHandler.Main.GenerateSeasonalContracts();
            }

            if (Game1.dayOfMonth == 1)
            {
                //TODO Resolve penalties
            }
        }

        private static void OnDayEnded(object sender, DayEndingEventArgs e)
        {
            var items = Game1.getFarm().getShippingBin(Game1.player);
            foreach (var item in items)
            {
                foreach (var contract in AssociationHandler.Main.ActiveContracts)
                {
                    if (!item.Name.Equals(contract.Item.Name)) continue;
                    AssociationHandler.AddShipment(item);
                    break;
                }
            }
        }
    }
}