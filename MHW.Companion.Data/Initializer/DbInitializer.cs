using MHW.Companion.Data.Store;
using System.Linq;

namespace MHW.Companion.Data.Initializer
{
    public class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.MigrateAsync().Wait();

            if (context.Weapons.Any())
                return;

            context.Weapons.Add(new Model.Equipment.Weapon { Name = "Excalibur" });

            context.SaveChanges();
        }
    }
}
