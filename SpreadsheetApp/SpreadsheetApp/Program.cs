using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;

namespace SpreadsheetApp
{
    class Program
    {
        private static Castle.Windsor.WindsorContainer _container;

        static void SetupContainer()
        {
            _container = new WindsorContainer();
            //Allow windsor to resolve constructor that has an ICollection as parameter
            _container.Kernel.Resolver.AddSubResolver(new CollectionResolver(_container.Kernel));
        }

        static void Main(string[] args)
        {
            //setup Dependency Container
            SetupContainer();


            int exitCode = 0x0;
            try
            {
                
            }
            catch (Exception ex)
            {
                //unexpected error goes to here
                exitCode = ex.HResult;
            }
            finally
            {
                Environment.ExitCode = exitCode;
            }
        }
    }
}
