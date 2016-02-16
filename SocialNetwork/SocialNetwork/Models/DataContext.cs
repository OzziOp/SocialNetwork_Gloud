using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{
    public class DataContext: DbContext
    {
        public DataContext() : base("SocialDB")
    { }
        public DbSet<User> Users { get; set; }
    }
}