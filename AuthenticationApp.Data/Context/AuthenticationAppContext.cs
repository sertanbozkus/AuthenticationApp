using AuthenticationApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationApp.Data.Context
{
    public class AuthenticationAppContext :DbContext
    {

        public AuthenticationAppContext(DbContextOptions<AuthenticationAppContext> options) : base(options)
        {
            
        }

        public DbSet<UserEntity> Users { get; set; }

    }
}
