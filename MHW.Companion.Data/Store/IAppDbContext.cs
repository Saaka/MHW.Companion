using MHW.Companion.Model.Equipment;
using Microsoft.EntityFrameworkCore;

namespace MHW.Companion.Data.Store
{
    public interface IAppDbContext
    {
        DbSet<Weapon> Weapons { get; set; }
    }
}
