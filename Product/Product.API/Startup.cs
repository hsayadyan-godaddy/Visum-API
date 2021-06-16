using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Product.API.Commands.Executor;
using Product.API.Filters;
using Product.API.Filters.Swagger;
using Product.API.WebSocketAPI;
using Product.API.WebSocketAPI.Abstraction;
using Product.API.WSControllers;
using Product.Services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;

namespace Product.API
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
            services.AddSingleton<ITokenValidator, TokenValidator>();
            services.AddTransient<IWebSocketHandler, WebSocketHandler>();
            services.AddTransient<IOperationExecutor, OperationExecutor>();

            RegisterWebSocketOperations(services);
            RegisterCommandExecutors(services);



            var jwtSettings = Configuration.GetSection("JwtSettings");
            var connectionString = Configuration.GetSection("ConnectionString");

            //services.Configure<DBClientSettings>(Configuration.GetSection("ConnectionString"));
            //services.AddSingleton<IDBClientSettings>(sp =>sp.GetRequiredService<IOptions<DBClientSettings>>().Value);
            services.Configure<DBClientSettings>(options =>
            {
                options.MongodbConnection = Configuration.GetSection("ConnectionString:MongodbConnection").Value;
            });
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
                    ValidAudience = jwtSettings.GetSection("validAudience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.GetSection("securityKey").Value))
                };
            });

            services.AddScoped<JwtHandler>();
            services.AddSingleton<DBClient>();

            //TODO AutoMapper
            //services.AddSingleton(mapper);

            services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
            });

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ExceptionFilter));
                options.Filters.Add(typeof(BadRequestFilter));
            }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Formatting = Formatting.None;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.StringEscapeHandling = StringEscapeHandling.EscapeHtml;
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
            });


            services.AddCors(); //todo


            services.AddSwaggerGen(options =>
           {
               options.SwaggerDoc("v1", new OpenApiInfo { Title = "Product.API", Version = "v1" });

               options.OperationFilter<SwaggerExcludeParameterFilter>();
               options.SchemaFilter<SwaggerExcludeSchemaFilter>();
               options.SchemaFilter<SwaggerFormatDateSchemaFilter>();
               options.DescribeAllParametersInCamelCase();


#warning requires to refactor and move to config file. Issue that this file can be missing or path in Linux can be defined in different way as in windows
               try
               {
                   options.IncludeXmlComments("Product.API.xml");
               }
               catch { }
           });

        }

        public class TestISO : JavaScriptDateTimeConverter
        {
            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                return base.ReadJson(reader, objectType, existingValue, serializer);
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                base.WriteJson(writer, value, serializer);
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product.API v1"));
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseWebSockets(new WebSocketOptions
            {
                KeepAliveInterval = TimeSpan.FromSeconds(30),
            });

            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/ws-api")
                {
                    context.Response.GetTypedHeaders().CacheControl =
                    new Microsoft.Net.Http.Headers.CacheControlHeaderValue
                    {
                        Public = true,
                        MaxAge = TimeSpan.FromSeconds(1)
                    };
                    context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Vary] = new string[] { "Accept-Encoding" };



                    var handler = app.ApplicationServices.GetRequiredService<IWebSocketHandler>();

                    await handler.Handle(context);
                }
                else
                {
                    await next();
                }
            });

            EnableWwwRoot(app, env);
        }


        private void EnableWwwRoot(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var CACHE_PERIOD = env.IsDevelopment() ? "1" : "600";

            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Append("Cache-Control", $"public, max-age={CACHE_PERIOD}");
                }
            });
        }


        private void RegisterWebSocketOperations(IServiceCollection services)
        {
            services.AddSingleton<ZoneFlowDataController>();
            services.AddSingleton<PressureDataController>();
            services.AddSingleton<RateDataController>();
        }

        private void RegisterCommandExecutors(IServiceCollection services)
        {
            services.RegisterServicesFactory();

            services.AddScoped<ProductionMonitoringCommandExecutor>();
        }

    }
}
