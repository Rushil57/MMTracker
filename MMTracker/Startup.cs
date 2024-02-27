using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MMTracker.Abstract;
using MMTracker.Service;

namespace MMTracker
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSingleton<ICategory>(sp => new CategoryService(Configuration));
            services.AddSingleton<IDepartment>(sp => new DepartmentService(Configuration));
            services.AddSingleton<IMachine>(sp => new MachineService(Configuration));
            services.AddSingleton<IMachine_Maintenance>(sp => new Machine_MaintenanceService(Configuration));
            services.AddSingleton<IMachine_Maintenance_Done>(sp => new Machine_Maintenance_DoneService(Configuration));
            services.AddSingleton<IMachine_Running>(sp => new Machine_RunningService(Configuration));
            services.AddSingleton<IMaintenance>(sp => new MaintenanceService(Configuration));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
