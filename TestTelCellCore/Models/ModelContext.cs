using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestTelCellCore.Models
{
    using Microsoft.EntityFrameworkCore; // use DbContext for EF Core

    //using System.ComponentModel.DataAnnotations.Schema;
   

    public partial class ModelContext : DbContext
    {
        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }


        public virtual DbSet<Paste> Paste { get; set; }
        public virtual DbSet<AccessDatesLog> AccessDatesLog { get; set; }


    }
}