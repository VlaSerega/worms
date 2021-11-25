﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Worms.Services;

namespace Worms.Main
{
    internal static class Program
    {
        public static void Main(string[] args)

        {
            CreateHostBuilder(args).Build().Run();
        }


        private static IHostBuilder CreateHostBuilder(string[] args)

        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>

                {
                    services.AddHostedService<WorldSimulatorService>();
                    services.AddSingleton<IFoodGenerator, FoodGenerator>();
                    services.AddSingleton<ISimpleLogger, SimpleFileLogger>();
                    services.AddSingleton<INameGenerator, NameGenerator>();
                    services.AddSingleton<IBehavior, Behavior>();
                });
        }
    }
}