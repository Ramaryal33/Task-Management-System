
using TaskManagementSystem.Web.Hubs;
using TaskManagementSystem.Web.Services;

namespace Task_Management_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

           
            builder.Services.AddControllersWithViews();

            builder.Services.AddSingleton<TaskService>();


            //SignalR service
            builder.Services.AddSignalR();

            var app = builder.Build();

            // ✅ Production error handling
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // ✅ Middleware pipeline
            app.UseHttpsRedirection();
            app.UseStaticFiles();          
            app.UseRouting();
            app.UseAuthorization();

            
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // ✅ SignalR hub endpoint
            app.MapHub<NotificationHub>("/notificationHub");



            app.Run();
        }
    }
}
