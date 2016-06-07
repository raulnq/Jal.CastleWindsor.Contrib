using System.Linq;
using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.ModelBuilder;

namespace Jal.CastleWindsor.Contrib.ContributeComponentModelConstruction
{
    public class DefaultDependencyBasedOnType<T1,T2> : IContributeComponentModelConstruction
    {
        private readonly string _dependencyName;

        public DefaultDependencyBasedOnType(string dependencyName)
        {
            _dependencyName = dependencyName;
        }

        public void ProcessModel(IKernel kernel, ComponentModel model)
        {
            if (model.Implementation  == typeof(T1))
            {
                var parameter = model.Parameters.FirstOrDefault(x => x.Name == typeof(T2).AssemblyQualifiedName);
                if (parameter == null) model.Parameters.Add(typeof(T2).AssemblyQualifiedName, "${" + _dependencyName + "}");
               
            }
        }
    }
}
