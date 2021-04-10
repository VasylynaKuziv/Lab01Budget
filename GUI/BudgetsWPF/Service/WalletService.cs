using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab01.Entities;

namespace Budgets.GUI.WPF.Service
{
    class WalletService
    {
        private static List<Wallet> Users = new List<Wallet>()
        {
            new Wallet("wal1", 57), 
            new Wallet("wal2", 157),
            new Wallet("wal3", 257),
            new Wallet("wal4", 24),
            new Wallet("wal5", 15),
        };

        public List<Wallet> GetWallets()
        {
            return Users.ToList();
        }
    }
}

