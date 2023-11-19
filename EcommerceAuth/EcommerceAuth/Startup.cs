using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using EcommerceAuth.commons.utils;
using EcommerceAuth.model.context;
using EcommerceAuth.repository.ADirectories;
using EcommerceAuth.repository.Auth;
using EcommerceAuth.repository.Funcionalities;
using EcommerceAuth.repository.Funcionalities_Rols;
using EcommerceAuth.repository.RefreshToke;
using EcommerceAuth.repository.Rol;
using EcommerceAuth.repository.Users_Rols;
using EcommerceAuth.service;
using EcommerceAuth.service.ADirectories;
using EcommerceAuth.service.Auth;
using EcommerceAuth.service.CookieSec;
using EcommerceAuth.service.Funcionalities;
using EcommerceAuth.service.Funcionalities_Rols;
using EcommerceAuth.service.Rol;
using EcommerceAuth.service.Users_Rols;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

namespace EcommerceAuth
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment currentEnvironment)
        {
            Configuration = configuration;
            _currentEnvironment = currentEnvironment;
        }

        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _currentEnvironment;

        private SecretClient _secretClient;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectiondb = this.Configuration.GetConnectionString("DefaultConnection");
            
            if (!_currentEnvironment.IsEnvironment("Development"))
            {
                //var keyVaultEndpoint = new Uri(Configuration.GetSection("KeyVault:VaultUriLogin").Value);
                //_secretClient = new SecretClient(keyVaultEndpoint, new DefaultAzureCredential());

                //connectiondb = _secretClient.GetSecret("ConnectionLogin").Value.Value;
            }

            services.AddCors(options =>
            {
                options.AddPolicy(name: "default",
                                  policy =>
                                  {
                                      policy.WithMethods("GET", "POST", "PUT", "PATCH", "DELETE", "OPTIONS")
                                                .WithHeaders(
                                                HeaderNames.Accept,
                                                HeaderNames.ContentType,
                                                HeaderNames.Authorization)
                                                .AllowCredentials()
                                                .SetIsOriginAllowed(origin =>
                                                {
                                                    if (string.IsNullOrWhiteSpace(origin)) return true;
                                                    //if (_currentEnvironment.IsEnvironment("Development"))
                                                    //{
                                                    //if (origin.ToLower().StartsWith("http://localhost")) return true;
                                                    //}
                                                    //if (origin.ToLower().StartsWith(".azurewebsites.net")) return true;
                                                    return true;
                                                });
                                  });
            });

            services.AddControllers();

            services.AddDbContext<ModelContext>(options =>
                options.UseSqlServer(connectiondb));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EcommerceAuth", Version = "v1" });
            });


            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IAuthRepository, AuthRepository>();
            services.AddTransient<IADirectoryService, ADirectoryService>();
            services.AddTransient<IADirectoryRepository, ADirectoryRepository>();
            services.AddTransient<IKeyVaultUtils, KeyVaultUtils>();
            services.AddTransient<IRefreshToken, RefreshToken>();
            services.AddTransient<ICookieSec, CookieSec>();
            services.AddTransient<IRolService, RolService>();
            services.AddTransient<IRolRepository, RolRepository>();
            services.AddTransient<IFuncionalityService, FuncionalityService>();
            services.AddTransient<IFuncionalityRepository, FuncionalityRepository>();
            services.AddTransient<IUserRolService, UserRolService>();
            services.AddTransient<IUserRolRepository, UserRolRepository>();
            services.AddTransient<IFuncionalityRolService, FuncionalityRolService>();
            services.AddTransient<IFuncionalityRolRepository, FuncionalityRolRepository>();

            services.AddDataProtection();
            services.AddHttpContextAccessor();

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters()
            //    {
            //        ValidateIssuer = false,
            //        ValidateAudience = false,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["clientsId:usersByRol"]))
            //    };
            //});
            services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(config =>
            {
                config.RequireHttpsMetadata = false;
                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["clientsId:usersByRol"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EcommerceAuth v1"));
            }
           
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("default");

            // app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseAuthorization(); 
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
