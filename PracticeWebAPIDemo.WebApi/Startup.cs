using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using PracticeWebAPIDemo.Infrastructure.Profiles;
using PracticeWebAPIDemo.Repository.Helpers;
using PracticeWebAPIDemo.Repository.Implement;
using PracticeWebAPIDemo.Repository.Interface;
using PracticeWebAPIDemo.Service.Implement;
using PracticeWebAPIDemo.Service.Infrastructure.Profiles;
using PracticeWebAPIDemo.Service.Interface;
using PracticeWebAPIDemo.WebApi.Infrastructure.ActionFilters;

namespace PracticeWebAPIDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // 建立 ConfigurationBuilder
            var configbuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json"); // 指定 JSON 設定檔的路徑
            // 建立 IConfiguration 物件
            var configuration = configbuilder.Build();
            // 從 IConfiguration 物件中取得連接字串
            string markTsdbConnectionString = configuration.GetConnectionString("MarkTSDB");

            services.AddControllers(options =>
            {
                //加入ActionResultFilter
                options.Filters.Add<ActionResultFilter>();
            });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "PracticeWebAPIDemo",
                    Version = "v1"
                });

                // Set the comments path for the Swagger JSON and UI.
                var basePath = AppContext.BaseDirectory;
                var xmlFiles = Directory.EnumerateFiles(basePath, "*.xml", SearchOption.TopDirectoryOnly);

                foreach (var xmlFile in xmlFiles)
                {
                    options.IncludeXmlComments(xmlFile);
                }
            });
            //FluentValidation
            services.AddFluentValidationAutoValidation(option =>
            {
                option.DisableDataAnnotationsValidation = true;
            });
            // AutoMapper註冊
            services.AddAutoMapper(typeof(ServiceProfile).Assembly);
            services.AddAutoMapper(typeof(CardControllerProfile).Assembly);
            // DI註冊
            services.AddScoped<ICardRepository, CardRepository>();
            services.AddScoped<ICardService, CardService>();
            // 多載DI註冊
            services.AddSingleton<IDatabaseHelper>(serviceProvider =>
            {
                return new DatabaseHelper(markTsdbConnectionString);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
