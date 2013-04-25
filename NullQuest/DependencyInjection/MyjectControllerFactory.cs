using BadSnowstorm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NullQuest.DependencyInjection
{
    public class MyjectControllerFactory : IControllerFactory
    {
        private readonly IjectContainer _container;

        public MyjectControllerFactory(IjectContainer container)
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
