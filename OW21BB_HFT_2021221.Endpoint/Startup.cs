using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OW21BB_HFT_2021221.Data;
using OW21BB_HFT_2021221.Logic;
using OW21BB_HFT_2021221.Models;
using OW21BB_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OW21BB_HFT_2021221.Endpoint
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MovieDbApp.Endpoint", Version = "v1" });
            });

            services.AddControllers();

            services.AddTransient<IHospitalLogic, HospitalLogic>();
            services.AddTransient<IDoctorLogic, DoctorLogic>();
            services.AddTransient<IPatientLogic, PatientLogic>();

            services.AddTransient<IRepository<Hospital>, HospitalRepository>();
            services.AddTransient<IRepository<Doctor>, DoctorRepository>();
            services.AddTransient<IRepository<Patient>, PatientRepository>();

            services.AddSingleton<HospitalDbContext, HospitalDbContext>();         
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI(c => 
            c.SwaggerEndpoint("/ swagger / v1 / swagger.json", "MovieDbApp.Endpoint v1"));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
