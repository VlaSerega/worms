using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Worms.GameModel;

namespace Worms.Services
{
    public class WorldSimulatorService : IHostedService
    {
        private readonly WorldSimulator _simulator;
        private readonly IHostApplicationLifetime _lifetime;

        public WorldSimulatorService(IServiceProvider serviceProvider, IHostApplicationLifetime lifetime)
        {
            _simulator = new WorldSimulator(serviceProvider.GetService<IFoodGenerator>(),
                serviceProvider.GetService<IFileLogger>());

            WorldState state = new WorldState(serviceProvider.GetService<INameGenerator>());

            Worm worm = new Worm(serviceProvider.GetService<INameGenerator>().NextName(), 0, 0);
            state.AddWorm(worm);

            _simulator.InitState(state);

            _lifetime = lifetime;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(Run);

            return Task.CompletedTask;
        }

        private void Run()
        {
            int currencyStep = 0;

            while (currencyStep != Const.MaxMoveNumber)
            {
                _simulator.MakeStep();

                currencyStep++;
            }

            _lifetime.StopApplication();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}