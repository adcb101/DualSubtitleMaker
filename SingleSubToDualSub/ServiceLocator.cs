using Microsoft.Extensions.DependencyInjection;
using SingleSubToDualSub.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleSubToDualSub
{
    public class ServiceLocator
    {
        private IServiceProvider _serviceProvider;

        public ServiceLocator()
        {
            var serviceCollection = new ServiceCollection();
            //serviceCollection.AddLogging(new NLogAspNetCoreOptions { CaptureMessageTemplates = true, CaptureMessageProperties = true });
            serviceCollection.AddSingleton<IGptHttpClient, GptHttpClient>();
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}
