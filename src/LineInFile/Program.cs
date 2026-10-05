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
                switches.Add(new Argument("line", typeof(string)) { LongName = "line", ShortName = "l" });
                switches.Add(new Argument("insertBefore", typeof(string)) { LongName = "insertbefore", ShortName = "b" });
                switches.Add(new Argument("insertAfter", typeof(string)) { LongName = "insertafter", ShortName = "a" });

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

                if (!switches.IsSet("path"))
                {
                    Console.WriteLine("--path|-p is required");
                    Environment.Exit(1);
                    return;
                }

                if (!switches.IsSet("line"))
                {
                    Console.WriteLine("--line|-l is required");
                    Environment.Exit(1);
                    return;
                }

                if (switches.IsSet("insertBefore") && switches.IsSet("insertAfter"))
                {
                    Console.WriteLine("Cannot set both insertbefore and insertafter");
                    Environment.Exit(1);
                    return;
                }


                Line line = new Line {
                    InsertAfter = switches.Get<string>("insertAfter"),
                    InsertBefore = switches.Get<string>("insertBefore"),
                    Regexp = switches.Get<string>("regexp"),
                };
                Response response = line.Work(
                    path: switches.Get<string>("path"),
                    line: switches.Get<string>("line")
                );
                Console.WriteLine(response.Description);
                if (!response.Succeeded)
                    Environment.Exit(1);
            }
            catch(Exception ex)
            {
                Console.WriteLine("lineInFile exited unexpectedly : ");
                Console.WriteLine(ex);
            }
        }
    }
}