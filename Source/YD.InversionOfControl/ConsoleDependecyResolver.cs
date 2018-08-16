using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YD.Common.Contracts;
using YD.Core;
using YD.Services;
using YD.Services.Abstraction;
using YD.Services.Abstraction.CommandProcessing;
using YD.Services.Abstraction.UI;
using YD.Services.CommandProcessing;
using YD.Services.UI;

namespace YD.InversionOfControl
{
    public class ConsoleDependecyResolver
    {
        private readonly IKernel kernel;

        public ConsoleDependecyResolver()
        {
            kernel = new StandardKernel();
            ConfigureContainer(kernel);
        }

        public T Create<T>()
        {
            return kernel.Get<T>();
        }

        public object Create(Type type)
        {
            return kernel.Get(type);
        }

        private void ConfigureContainer(IKernel container)
        {
            //Core
            container.Bind<Engine>().ToSelf().InSingletonScope();

            // Services
            container.Bind<IUIService>().To<ConsoleUIService>().InSingletonScope();
            container.Bind<ICommandProcessingService>().To<CommandProcessingService>();
            container.Bind<ICommandFactoryService>().To<CommandFactoryService>();
        }
    }
}
