using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using AgricultureAssociation.Contracts;
using StardewModdingAPI;
using StardewValley;

namespace AgricultureAssociation
{
    class Association
    {

        public static double[] RepAmounts = { 3000, 6000, 12000, 21000, 0};
        public static String[] RankNames = { "Neutral", "Friendly", "Honored", "Revered", "Exalted" };
        public double Reputation = 0;
        public int Rank = 0;
        public int Favor = 0;
        public List<Contract> ActiveContracts;
        public List<Contract> AvailableContracts;

        public void AddRep(int amount)
        {
            if (amount + Reputation > RepAmounts[Rank])
            {
                Reputation = RepAmounts[Rank];
            }
            else
            {
                Reputation += amount;
            }
        }

        public void AddFavor(int amount)
        {
            if (amount + Reputation > 999)
            {
                Favor = 999;
            }
            else
            {
                Favor += amount;
            }
        }

        public void GenerateSeasonalContracts()
        {
            List<ContractElement> options = new List<ContractElement>();
            foreach (var c in AssociationHandler.Crops)
            {
                if (c.SeasonString.Contains(Game1.currentSeason))
                {
                    options.Add(c);
                }
            }

            List<Contract> contractList = new List<Contract>();

            for (int i = 0; i < 8; i++)
            {
                var newContract = new Contract(options[AssociationHandler.Random.Next(0, options.Count)]);
                newContract.AmountNeeded = (int) ((double) (1 + Rank) / 4 + 1) * AssociationHandler.Random.Next(15, 60);
                if (i > 3)
                {
                    newContract.AmountNeeded = (int) (newContract.AmountNeeded * (AssociationHandler.Random.NextDouble() + 1.5));
                }

                var rand = AssociationHandler.Random.NextDouble()/2;
                newContract.RewardFavor = (int) (newContract.Item.Difficulty * newContract.AmountNeeded *
                                          (rand + 1) / 15);
                newContract.RewardReputation = (int) (newContract.Item.Difficulty * newContract.AmountNeeded *
                                                      ((rand-0.5)*-1 + 1) / 2);
                contractList.Add(newContract);
            }

            AvailableContracts = contractList;

            foreach (var c in AvailableContracts)
            {
                AssociationHandler.Mod.Monitor.Log(c.Item.Name+": "+c.AmountNeeded+ " // "+c.RewardFavor+"/"+c.RewardReputation, LogLevel.Warn);
            }
        }

    }
}
