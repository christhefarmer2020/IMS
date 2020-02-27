using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IMS.Models
{
    public class DatabaseConnection : DbContext
    {
        public DbSet<EncounterImage> EImage { get; set; }
        public DbSet<Encounter> Encounters { get; set; }
    }
}