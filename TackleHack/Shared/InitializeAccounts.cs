using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TackleHack.Models;

namespace TackleHack.Shared
{
    public class InitializeAccounts
    {
        public static void Initialize()
        {
            using (var context = new TackleHackSQLContext())
            {
                if (context.Account.Count() == 0)
                {
                    var account = new Account()
                    {
                        Name = "Demo",
                        Description = "Account for demo"
                    };
                    context.Account.Add(account);
                    context.SaveChanges();
                }
            }
        }
    }
}
