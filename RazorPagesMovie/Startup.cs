using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;
using Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore;
using System.Globalization;
using System.IO;
using Microsoft.Extensions.ML;
using MlClassification.Model;

namespace RazorPagesMovie
{
    public class Startup
    {
        private readonly string _modelPath;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
            Environment = env;
            Configuration = configuration;
            _modelPath = GetAbsolutePath("MLModel.zip");
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            if (Environment.IsDevelopment())
            {
                services.AddDbContext<RazorPagesMovieContext>(options =>
                options.UseSqlite(
                    Configuration.GetConnectionString("RazorPagesMovieContext")));
            }
            else
            {
                services.AddDbContext<RazorPagesMovieContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("MovieContext")));
            }

            services.AddRazorPages();

            services.AddPredictionEnginePool<ModelInput, ModelOutput>()
            .FromFile(_modelPath);
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }

        public static string GetAbsolutePath(string relativePath)
        {
            FileInfo _dataRoot = new FileInfo(typeof(Program).Assembly.Location);
            string assemblyFolderPath = _dataRoot.Directory.FullName;

            string fullPath = Path.Combine(assemblyFolderPath, relativePath);
            return fullPath;
        }
    }
}
