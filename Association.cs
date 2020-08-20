using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureAssociation
{
    class Association
    {
        public int Reputation;
        public int Rank;
        public int Favor;
        public List<Contract> ActiveContracts;
        public List<Contract> AvailableContracts;

    }
}
