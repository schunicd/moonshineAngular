using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheMoonshineCafe.Data
{
    public class DbInitializer
    {
        public static void Initialize(MoonshineCafeContext context)
        {
            context.Database.EnsureCreated();

            if (context.Admins.Any())
            {
                return;
            }
            //TODO
        }
    }
}
