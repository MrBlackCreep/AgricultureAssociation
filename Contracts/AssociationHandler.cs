using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgricultureAssociation.Contracts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PyTK.Extensions;
using StardewModdingAPI;
using StardewValley;

namespace AgricultureAssociation
{
    class AssociationHandler
    {
        public static List<ContractElement> Crops = new List<ContractElement>();
        public static List<ContractElement> Recipes = new List<ContractElement>();
        public static Mod Mod;



        public static void GenerateCrops()
        {
            Dictionary<int, string> cropsDictionary = Game1.content.Load<Dictionary<int, string>>("Data//Crops");
            var obj = Game1.content.Load<Dictionary<int, string>>("Data//ObjectInformation");

            foreach (var cropEntry in cropsDictionary)
            {
                //generate a dataset for each crop {id,spriteid,seasons,growthtime,regrow,extraloot}
                string[] raw = cropEntry.Value.Split('/');
                
                string[] data = {raw[3],raw[2],raw[1],raw[0],raw[4],raw[6]};

                var crop = new ContractElement();

                crop.ItemId = int.Parse(data[0]);
                if (obj.TryGetValue(crop.ItemId, out string value))
                {
                    crop.Name = value.Split('/')[0];
                }
                else
                {
                    Mod.Monitor.Log("Couldn't find object associated with ID: "+crop.ItemId, LogLevel.Error);

                }

                crop.Sprite = Game1.content.Load<Texture2D>("Maps//springobjects").getTile(crop.ItemId);
                crop.Seasons[0] = data[2].Contains("spring");
                crop.Seasons[1] = data[2].Contains("summer");
                crop.Seasons[2] = data[2].Contains("fall");
                crop.Seasons[3] = data[2].Contains("winter");

                double avrgTime = 0.0;
                double regrowMult = 1;
                double avrgYield = 1;

                foreach (var cycle in data[3].Split(' '))
                {
                    avrgTime += int.Parse(cycle);
                }

                if (int.Parse(data[4]) != -1)
                {
                    regrowMult = 15 / (double)int.Parse(data[4]);
                }

                if (data[5].Contains("true"))
                {
                    var num = data[5].Split(' ');
                    avrgYield = (double) (int.Parse(num[1]) + int.Parse(num[2])) / 2;
                }

                crop.Difficulty = (avrgTime / avrgYield) / regrowMult ;
                Crops.Add(crop);
            }

        }
    }
}
