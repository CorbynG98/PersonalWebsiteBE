﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonalWebsiteBE.Core.Settings;

namespace PersonalWebsiteBE.Core.Extensions
{
    public static class ConfigurationExtensions
    {
        public static void AddConfigurationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            ISendGridSettings sendGrid = new SendGridSettings();
            configuration.GetSection("Sendgrid").Bind(sendGrid);
            services.AddSingleton(sendGrid);

            IFireStoreSettings fireStore = new FireStoreSettings();
            configuration.GetSection("GoogleCloud").Bind(fireStore);
            services.AddSingleton(fireStore);
        }
    }
}
