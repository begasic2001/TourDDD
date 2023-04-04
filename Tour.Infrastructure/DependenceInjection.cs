using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Tour.Application.Common.Interfaces.Authentication;
using Tour.Application.Common.Services;
using Tour.Application.Interfaces;
using Tour.Application.Services.Authentication;
using Tour.Domain.Entities;
using Tour.Infrastructure.Authentication;
using Tour.Infrastructure.Common;
using Tour.Infrastructure.Data;
using Tour.Infrastructure.Repositories;
using Tour.Infrastructure.Services;


namespace Tour.Infrastructure
{
    public static class DependenceInjection
    {
        public static IHostBuilder AddHostBuild(this IHostBuilder hostBuilder)
        {
            hostBuilder.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            hostBuilder.ConfigureContainer<ContainerBuilder>(autofacConfigure =>
            {
                autofacConfigure.
                    RegisterType<AuthenticationApp>().As<IAuthentication>();
                autofacConfigure.
                    RegisterType<JwtTokenGenerator>().As<IJwtTokenGenerator>();
                autofacConfigure.
                    RegisterType<DateTimeProvider>().As<IDateTimeProvider>();

                autofacConfigure
                    .RegisterType<Repository<Country>>().As<IRepository<Country>>();
                autofacConfigure
                    .RegisterType<Repository<City>>().As<IRepository<City>>();
                autofacConfigure
                   .RegisterType<Repository<Sight>>().As<IRepository<Sight>>();
                autofacConfigure
                   .RegisterType<Repository<Transport>>().As<IRepository<Transport>>();
                autofacConfigure
                   .RegisterType<Repository<Tours>>().As<IRepository<Tours>>();
                autofacConfigure
                   .RegisterType<Repository<User>>().As<IRepository<User>>();
                // autofacConfigure
                //    .RegisterType<Repository<Order>>().As<IRepository<Order>>();
                // autofacConfigure
                //.RegisterType<Repository<OrderDetail>>().As<IRepository<OrderDetail>>();
                autofacConfigure
                    .RegisterType<UnitOfWork>().As<IUnitOfWork>();
                autofacConfigure
                  .RegisterType<CountryService>().As<ICountryService>();
                autofacConfigure
                  .RegisterType<CityService>().As<ICityService>();
                autofacConfigure
                   .RegisterType<SightService>().As<ISightService>();
                autofacConfigure
                  .RegisterType<TransportService>().As<ITransportService>();
                autofacConfigure
                  .RegisterType<TourService>().As<ITourService>();
                autofacConfigure
              .RegisterType<UserService>().As<IUserService>();

            });
            return hostBuilder;
        }

        public static IServiceCollection AddServiceCollection(this IServiceCollection services, Microsoft.Extensions.Configuration.ConfigurationManager configuration)
        {
            services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<TourDatabaseContext>().AddDefaultTokenProviders();

            services.AddDbContext<TourDatabaseContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("DefaultString"), b => b.MigrationsAssembly("Tour.Api"));
            });
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:ValidAudience"],
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                };
            });
            return services;
        }
    }
}
