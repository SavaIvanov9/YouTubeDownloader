using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YD.Services.Abstraction;

namespace YD.Core
{
    public class Engine
    {
        private readonly ILoggingService logger;

        public Engine(ILoggingService logger)
        {
            this.logger = logger;
        }

        public void Start()
        {
        }
    }
}
