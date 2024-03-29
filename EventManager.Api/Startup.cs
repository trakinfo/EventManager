﻿using EventManager.Core.DataBaseContext;
using EventManager.Core.DataBaseContext.SQL;
using EventManager.Core.Repository;
using EventManager.Infrastructure.DataBaseContext;
using EventManager.Infrastructure.DataBaseContext.MySql.SQL;
using EventManager.Infrastructure.Mapper;
using EventManager.Infrastructure.Repository;
using EventManager.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventManager.Api
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
			//services.Add(new ServiceDescriptor(typeof(IDataBaseContext), new MySqlContext(Configuration.GetConnectionString("MySqlConnection"))));
			services.AddTransient<IDataBaseContext>(s => new MySqlContext(Configuration.GetConnectionString("MySqlConnection")));
			
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IEventRepository, EventRepository>();
			services.AddScoped<ILocationRepository, LocationRepository>();
			services.AddScoped<IAddressRepository, AddressRepository>();
			services.AddScoped<ISectorRepository, SectorRepository>();
			services.AddScoped<ITicketRepository, TicketRepository>();

			services.AddScoped<IEventService, EventService>();
			services.AddScoped<ILocationService, LocationService>();

			services.AddScoped<ILocationSql, LocationSql>();
			services.AddScoped<IAddressSql, AddressSql>();
			services.AddScoped<ISectorSql, SectorSql>();
			services.AddScoped<ITicketSql, TicketSql>();
			services.AddScoped<IEventSql, EventSql>();

			services.AddSingleton(AutoMapperConfig.Initialize());
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
				
            }

            app.UseMvc();
        }
    }
}
