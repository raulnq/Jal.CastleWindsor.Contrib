using System;
using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Jal.CastleWindsor.Contrib.Installer
{
    public class ConventionInstaller: IWindsorInstaller
    {
        private readonly Func<Assembly, BasedOnDescriptor> _installerSetup;

        private readonly Assembly[]  _installerSourceAssemblies;
  
        public ConventionInstaller(Assembly[] installerSourceAssemblies, Func<Assembly, BasedOnDescriptor> installerSetup=null)
        {
            _installerSourceAssemblies = installerSourceAssemblies;
            _installerSetup = installerSetup;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var assemblies = _installerSourceAssemblies;

            if (assemblies!=null)
            {
                foreach (var assembly in assemblies)
                {
                    if (_installerSetup == null)
                    {
                        container.Register(Classes.FromAssembly(assembly).Pick());
                    }
                    else
                    {
                        container.Register(_installerSetup(assembly));
                    }
                }
            }
        }
    }
}