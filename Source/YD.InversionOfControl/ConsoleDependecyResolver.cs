using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            //// Logging
            //container.Bind<ILoggingService>().To<ConsoleLoggingService>();

            //// Services
            //container.Bind<IConversationService>().To<ConversationService>();
            //container.Bind<IConversationConfiguration>().To<ConversationConfiguration>();
            //container.Bind<IEmotionAnalysisService>().To<EmotionAnalysisService>();

            //// Data
            //container.Bind<IAIDataUnitOfWork>().To<FakeDataUnitOfWork>().InSingletonScope();
            //container.Bind<IConversationRepository>().To<TempConversationRepository>().InSingletonScope();
            //container.Bind<IUtteranceRepository>().To<TempUtteranceRepository>().InSingletonScope();
            //container.Bind<IParticipantRepository>().To<TempParticipantRepository>().InSingletonScope();
        }
    }
}
