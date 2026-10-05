using System;
using LineInFile.Porter_Packages.Madscience_CommandLineSwitches;

namespace LineInFile
{
    class Program
    {
        static void Main(string[] args)
        {
            try 
            {
                CommandLineSwitches switches = new CommandLineSwitches();
                
                switches.Add(new Argument("version", typeof(string)) { LongName = "version", ShortName = "v", IsExclusive = true });
                switches.Add(new Argument("path", typeof(string)) { LongName = "path", ShortName = "p" });
                switches.Add(new Argument("regexp", typeof(string)) { LongName = "regexp", ShortName = "r" });
                switches.Add(new Argument("line", typeof(int?)) { LongName = "line", ShortName = "l" });

                BindResponse bindResponse = switches.Bind(
                    args, 
                    validate : true);

                if (!bindResponse.Succeeded)
                {
                    Console.WriteLine($"Error :\n{bindResponse.Description}");
                    Environment.Exit(1);
                }

                string command = "line";
                if (switches.IsSet("version"))
                    command = "version";

                if (command == "version")
                {
                    Version version = new Version();
                    version.Work();
                    return;
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine("lineInFile exited unexpectedly : ");
                Console.WriteLine(ex);
            }
        }
    }
}