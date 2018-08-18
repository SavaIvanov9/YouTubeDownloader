using Ninject;
using System;
using VA.Services.FileConverting;
using YD.Core;
using YD.Services.Abstraction;
using YD.Services.Abstraction.CommandProcessing;
using YD.Services.Abstraction.UI;
using YD.Services.ConsoleUI;
using YD.Services.Core;
using YD.Services.ErrorLogging;

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
            container.Bind<IErrorLoggingService>().To<ErrorLoggingService>();
            container.Bind<IMp3ConverterService>().To<FFMpegFormatConvertingServices>();
        }
    }
}
