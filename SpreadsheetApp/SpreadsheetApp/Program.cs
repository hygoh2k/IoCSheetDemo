using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Common.Model;
using Spreadsheet.Model;
using Spreadsheet.Model.Spreadsheet.Model;

namespace SpreadsheetApp
{
    class Program
    {
        private static Castle.Windsor.WindsorContainer _container;

        static void RegisterComponents()
        {
            _container.Register(Component.For<SpreadsheetCommand>()
                .ImplementedBy<CreateSpreadsheetCommand>()
                .DependsOn(Dependency.OnValue("cmdKey", "C"))
            );

            _container.Register(Component.For<SpreadsheetCommand>()
                .ImplementedBy<UpdateSpreadsheetCommand>()
                .DependsOn(Dependency.OnValue("cmdKey", "N"))

            );

            _container.Register(Component.For<SpreadsheetCommand>()
                .ImplementedBy<SumSpreadsheetCommand>()
                .DependsOn(Dependency.OnValue("cmdKey", "S"))

            );

            _container.Register(Component.For<SpreadsheetCollection>());

            _container.Register(Component.For<ISheetPrinter>()
                .ImplementedBy<SpreadsheetPrinter>());


        }

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
            //register Dependencies
            RegisterComponents();

            int exitCode = 0x0;
            try
            {
                var sheetManager = _container.Resolve<SpreadsheetCollection>();
                var sheetPrinter = _container.Resolve<ISheetPrinter>();

                Console.WriteLine("Enter command");
                ISheet currentSheet = null;
                while (true)
                {
                    Console.Write(">");
                    string[] input = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    string cmdKey = input.Length > 0 ? input[0] : "";

                    if (cmdKey.Equals("Q"))
                    {
                        break;
                    }

                    string[] cmdArgs = input.Length > 1 ? input.Skip(1).ToArray() : new string[] { };

                    if (sheetManager.CommandCollection.ContainsKey(cmdKey))
                    {
                        var cmd = sheetManager.CommandCollection[cmdKey];
                        var result = cmd.Execute(currentSheet, cmdArgs);
                        if (result.Success)
                        {
                            currentSheet = result.Spreadsheet;
                            sheetPrinter.PrintContent(currentSheet, Console.Out);

                        }
                        else
                        {
                            Console.WriteLine(result.ErrorMessage);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Command");
                    }

                }
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
