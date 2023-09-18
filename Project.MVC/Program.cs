using Microsoft.EntityFrameworkCore;
using Project.Service.Data;
using Project.Service.Mapping;
using Project.Service.Service;
using Project.Service.Service.VehicleServices;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

builder.Services.AddDbContext<ProjectServiceContext>(options =>
    options.UseSqlite(
        configuration.GetConnectionString("ProjectServiceContext"),
        b => b.MigrationsAssembly("Project.Service")
        ));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IVehicleMakeService, VehicleMakeService>();
builder.Services.AddScoped<IVehicleModelService, VehicleModelService>();
builder.Services.AddScoped<Filters>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();