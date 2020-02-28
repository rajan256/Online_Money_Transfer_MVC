using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Online_Money_Transfer_MVC.Models;

namespace Online_Money_Transfer_MVC.Data
{
    public class Online_Money_Transfer_DbContext : DbContext
    {
        public Online_Money_Transfer_DbContext (DbContextOptions<Online_Money_Transfer_DbContext> options)
            : base(options)
        {
        }

        public DbSet<Online_Money_Transfer_MVC.Models.MoneyReceiver> MoneyReceiver { get; set; }

        public DbSet<Online_Money_Transfer_MVC.Models.MoneySender> MoneySender { get; set; }

        public DbSet<Online_Money_Transfer_MVC.Models.MoneyTransfer> MoneyTransfer { get; set; }

        public DbSet<Online_Money_Transfer_MVC.Models.Provider> Provider { get; set; }
    }
}
