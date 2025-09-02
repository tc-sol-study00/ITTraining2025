using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ASPEnshu.Data;
namespace ASPEnshu
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<ASPEnshuContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("ASPEnshuContext") ?? throw new InvalidOperationException("Connection string 'ASPEnshuContext' not found.")));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

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
                pattern: "{controller=Employees}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
