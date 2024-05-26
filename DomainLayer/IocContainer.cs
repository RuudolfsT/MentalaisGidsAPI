using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace DomainLayer
{
    public static class IocContainer
    {
        private static IWindsorContainer _container;

        public static IWindsorContainer Container
        {
            get
            {
                if (_container == null)
                {
                    _container = new WindsorContainer(); // Initialize the container if it's null
                    //_container.Register(Component.For<ILomaRepository>().ImplementedBy<LomaRepository>());
                }
                return _container;
            }
        }

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
    }
}
