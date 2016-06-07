using System;
using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Jal.CastleWindsor.Contrib.Installer
{
    public class ConventionInstaller: IWindsorInstaller
    {
        private readonly Func<FromAssemblyDescriptor, BasedOnDescriptor> _installerSetup;

        private readonly Assembly[]  _installerSourceAssemblies;
  
        public ConventionInstaller(Assembly[] installerSourceAssemblies, Func<FromAssemblyDescriptor, BasedOnDescriptor> installerSetup)
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
                    var fromAssemblyDescriptor = Classes.FromAssembly(assembly);

                    var serviceDescriptor = fromAssemblyDescriptor.Pick();

                    if (_installerSetup != null)
                    {
                        serviceDescriptor = _installerSetup(fromAssemblyDescriptor);
                    }
                    container.Register(serviceDescriptor);
                }
            }
        }
    }
}