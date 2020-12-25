using System;
using CallBackBot.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using VkNet;
using VkNet.Abstractions;
using VkNet.Model;

namespace CallBackBot
{
    /// <summary>
    ///      Configures services and the app's request pipeline.
    /// </summary>
    public class Startup
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CallBackBot.Startup"/> class.
        /// </summary>
        /// <param name="configuration">
        ///     Represents a set of key/value application configuration properties.
        /// </param>
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        ///     This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Defines a contract for a collection of service descriptors.</param>
        public void ConfigureServices(IServiceCollection services)
        {

            // configure options
            services.Configure<VkOptions>(_configuration.GetSection("VkOptions"));
            services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<VkOptions>>().Value);

            services.AddSingleton<IVkApi>(resolver =>
            {
                var options = resolver.GetRequiredService<VkOptions>();
                var api = new VkApi();
                api.Authorize(new ApiAuthParams
                {
                    AccessToken = options.AccessToken
                });
                return api;
            });
            services.AddControllers();
        }

        /// <summary>
        ///     This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">
        ///     Provides the mechanisms to configure an application's request pipeline.
        /// </param>
        /// <param name="env">
        ///     Provides information about the web hosting environment an application is running in.
        /// </param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}