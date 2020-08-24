using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using AgricultureAssociation.Contracts;
using StardewValley;

namespace AgricultureAssociation
{
    class Contract
    {
        public readonly ContractElement Item;
        public int AmountNeeded;
        public int AmountReceived = 0;
        public int RewardReputation;
        public int RewardFavor;
        public bool Completed = false;

        public Contract(ContractElement item)
        {
            this.Item = item;
        }
    }
}
