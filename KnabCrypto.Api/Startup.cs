using KnabCrypto.Api.Middlewares;
using KnabCrypto.Common.Clients;
using KnabCrypto.Common.Settings;
using KnabCrypto.Transactions.Queries.Handlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace KnabCrypto.Api
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "KnabCrypto.Api", Version = "v1" });
            });

            services.AddHttpClient();
            services.AddScoped<GetCryptoCurrencyQuotesQueryHandler>();
            services.Configure<CryptoCurrencyApiSettings>(Configuration.GetSection("CryptoCurrencyApi"));
            services.Configure<ExchangeRatesApiSettings>(Configuration.GetSection("ExchangeRatesApi"));
            services.AddScoped<ICryptoCurrencyApiClient, CryptoCurrencyApiClient>();
            services.AddScoped<IExchangeRatesApiClient, ExchangeRatesApiClient>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "KnabCrypto.Api v1"));
            }
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}