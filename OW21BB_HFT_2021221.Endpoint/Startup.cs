using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            services.AddControllers();

            services.AddTransient<IHospitalLogic, HospitalLogic>();
            services.AddTransient<IDoctorLogic, DoctorLogic>();
            services.AddTransient<IPatientLogic, PatientLogic>();

            services.AddTransient<IRepository<Hospital>, HospitalRepository>();
            services.AddTransient<IRepository<Doctor>, DoctorRepository>();
            services.AddTransient<IRepository<Patient>, PatientRepository>();

            services.AddTransient<HospitalDbContext, HospitalDbContext>();         
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
