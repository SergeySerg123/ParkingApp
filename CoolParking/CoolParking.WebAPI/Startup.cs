using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CoolParking.WebAPI.Interfaces;
using CoolParking.WebAPI.Models;
using CoolParking.WebAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CoolParking.WebAPI
{
    public class Startup
    {
        private bool isStartedConsole = false;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //InitConsoleApp();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<Parking>();
            services.AddSingleton<Transactions>();
            services.AddTransient<IParkingService, ParkingService>();
            services.AddTransient<ITransactionsService, TransactionService>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if(!isStartedConsole)
            {
                isStartedConsole = !isStartedConsole;
                
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void InitConsoleApp()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo("./bin/Debug/netcoreapp3.1/CoolParking.Bl.exe");
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            Process.Start(startInfo);
            //Process p = new Process();
            //p.StartInfo = new ProcessStartInfo("CoolParking.BL.exe");
            //p.StartInfo.WorkingDirectory = @"E:\WORK\BinaryStudio\CoolParking\CoolParking\CoolParking.WebAPI\bin\Debug\netcoreapp3.1";
            //p.StartInfo.CreateNoWindow = true;
            //p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //p.Start();


            //ProcessStartInfo procInfo = new ProcessStartInfo();
            //procInfo.FileName = "CoolParking.BL.exe";
            ////var path = Directory.GetCurrentDirectory() + "\\bin\\Debug\\netcoreapp3.1\\CoolParking.BL.exe";
            //Process process = Process.Start(procInfo);
        } 
    }
}
