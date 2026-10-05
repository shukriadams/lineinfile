using System.Reflection;
using System.IO;
using System.Text.RegularExpressions;

namespace LineInFile
{
    public class Line
    {
        public string InsertBefore {get;set;}
        
        public string InsertAfter { get;set; }

        /// <summary>
        /// Regular expression to search for in file. If matched, the affected line will
        /// be updated.
        /// 
        /// Optional. If not set, "line" itself will be matched as a raw string.
        /// </summary>
        public string Regexp  { get;set; }

        public bool DryRun { get;set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path">Path of file to edit</param>
        /// <param name="line">Line to insert/overwrite if regexp matches.</param>
        public Response Work(string path, string line)
        {
            if (!File.Exists(path))
                return new Response{ Description = $"File {path} not found" };

            string fileContent;
            IList<string> lines;
            try
            {
                fileContent = File.ReadAllText(path);
                lines = fileContent.Split(Environment.NewLine).ToList();
            }
            catch(Exception ex)
            {
                return new Response {Description = $"Error reading file {path} : {ex}"};
            }

            int startPosition = 0;
            int endPosition = lines.Count() - 1;
            if (endPosition < 0)
                endPosition = 0;

            LineIndexResponse insertAfterResponse = FindLineInFile.ByRegex(fileContent, lines.ToArray(), this.InsertAfter, startPosition);
            if (insertAfterResponse.Succeeded)
                startPosition = insertAfterResponse.Index;
            else
                return insertAfterResponse;

            LineIndexResponse insertBeforeResponse = FindLineInFile.ByRegex(fileContent, lines.ToArray(), this.InsertAfter, endPosition);
            if (insertBeforeResponse.Succeeded)
                endPosition = insertBeforeResponse.Index;
            else
                return insertBeforeResponse;


            // no regexp given, insert/append line if line not already in file
            if (string.IsNullOrEmpty(this.Regexp))
            {
                int index = FindLineInFile.BySimple(lines.ToArray(), line);
                if (index != -1)
                    return new Response { 
                        Succeeded = true, 
                        Description = $"Line already found in file, no changes made"};

                if (!string.IsNullOrEmpty(this.InsertAfter))
                {
                    lines.Insert(startPosition, line);
                    Console.WriteLine($"Inserting line at line index {startPosition}");
                }
                else if (!string.IsNullOrEmpty(this.InsertBefore))
                {
                    lines.Insert(endPosition, line);
                    Console.WriteLine($"Inserting line at line index {endPosition}");
                }
                else
                {
                    lines = lines.Append(line).ToList();
                    Console.WriteLine($"Inserting line at end of file");
                }
            } 
            else 
            {
                // regexp given, insert/append line base on regexp match
                LineIndexResponse lineMatchResponse = FindLineInFile.ByRegex(fileContent, lines.ToArray(), this.Regexp, -1);
                if (!lineMatchResponse.Succeeded)
                    return lineMatchResponse;

                int insertIndex = lineMatchResponse.Index;
                if (insertIndex == -1)
                {
                    Console.WriteLine($"No match for \"{this.Regexp}\", inserting at end of file");
                    lines = lines.Append(line).ToList();
                } 
                else
                {
                    Console.WriteLine($"Found \"{this.Regexp}\" in at line {(lineMatchResponse.Index + 1)}");
                    lines[insertIndex] = line;
                }

            }

            return this.Write(lines, path);
        }

        private Response Write(IEnumerable<string> lines, string path)
        {
            try 
            {
                string fileContent = String.Join(Environment.NewLine, lines);
                File.WriteAllText(path, fileContent);

                return new Response { 
                    Succeeded =  true, 
                    Description = $"File {path} updated"
                };
            }
            catch(Exception ex)
            {
                return new Response { 
                    Description = $"Error writing file {path} : {ex}"
                };
            }
        }
    }    
}