using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Api.Middleware;
using WorkoutTracker.Domain;
using WorkoutTracker.Domain.Configuration;
using WorkoutTracker.Domain.Data.Contexts;
using WorkoutTracker.Domain.Data.Repositories.Interfaces;

namespace WorkoutTracker.Api
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
            var connectionString = Configuration.GetConnectionString("workout-tracker");
            services.AddControllers();
            services.AddDbContext<WorkoutTrackerContext>(options =>
            {
                options.UseNpgsql(connectionString, postgresOptions =>
                {
                    postgresOptions.MigrationsAssembly("WorkoutTracker.Api");
                });
            });

            services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IWorkoutUserRepository, WorkoutUserRepository>();
            services.AddScoped<ILoginAttemptRepository, LoginAttemptRepository>();
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
