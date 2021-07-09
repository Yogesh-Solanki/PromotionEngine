using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PromotionEngine.Core.Interfaces;
using PromotionEngine.Infrastructure.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace PromotionEngine
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
            services.AddControllers();
            services.AddTransient(typeof(ICheckoutService), typeof(CheckoutService));
            services.AddSingleton(typeof(IPromotionService), typeof(PromotionService));
            services.AddTransient(typeof(IPricingService), typeof(PricingService));
            services.AddMvc(opts =>
            {
                opts.RespectBrowserAcceptHeader = true;
                opts.InputFormatters.Add(new XmlSerializerInputFormatter(opts));
                opts.OutputFormatters.Add(new XmlSerializerOutputFormatter());
                opts.EnableEndpointRouting = false;
            }).AddFluentValidation(config =>
               {
                   config.RegisterValidatorsFromAssemblyContaining<Startup>();
                   config.ImplicitlyValidateChildProperties = true;
               }).SetCompatibilityVersion(CompatibilityVersion.Latest);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
