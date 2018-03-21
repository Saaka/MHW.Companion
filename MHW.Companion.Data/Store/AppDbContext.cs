using MHW.Companion.Config;
using MHW.Companion.Model.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace MHW.Companion.Data.Store
{
    public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
    {        
        public AppDbContext(DbContextOptions options) 
            : base(options)
        {
        }
    }
}
