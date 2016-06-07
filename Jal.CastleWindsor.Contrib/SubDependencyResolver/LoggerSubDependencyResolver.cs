using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Common.Logging;

namespace Jal.CastleWindsor.Contrib.SubDependencyResolver
{
    public class LoggerSubDependencyResolver : ISubDependencyResolver
    {
        public bool CanResolve(CreationContext context, ISubDependencyResolver contextHandlerResolver, ComponentModel model, DependencyModel dependency)
        {
            return dependency.TargetType == typeof(ILog);
        }


        public object Resolve(CreationContext context, ISubDependencyResolver contextHandlerResolver, ComponentModel model, DependencyModel dependency)
        {
            if (CanResolve(context, contextHandlerResolver, model, dependency))
            {
                if (dependency.TargetType == typeof(ILog))
                {
                    return LogManager.GetLogger(model.Implementation);
                }
            }
            return null;
        }
    }
}
