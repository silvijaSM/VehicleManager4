using Microsoft.EntityFrameworkCore;
using Project.Service.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Data
{
    public class ProjectServiceContext : DbContext
    {
        public ProjectServiceContext(DbContextOptions<ProjectServiceContext> options)
            : base(options)
        {
        }

        public DbSet<VehicleMake> VehicleMake { get; set; } = default!;

        public DbSet<VehicleModel> VehicleModel { get; set; } = default!;

    }
}
