using BadSnowstorm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NullQuest.DependencyInjection
{
    public class MyjectControllerFactory : IControllerFactory
    {
        private readonly MyjectContainer _container;

        public MyjectControllerFactory(MyjectContainer container)
        {
            _container = container;
        }

        public Controller Create<TController>()
            where TController : Controller
        {
            return _container.Get<TController>();
        }
    }
}
