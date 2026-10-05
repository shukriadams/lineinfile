using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace LineInFile
{
    public class FindLineInFile
    {
        /// <summary>
        /// Tries to match regex to a line in filecontent, returns position if found, else
        /// returns defaultPosition.
        /// </summary>
        public static LineIndexResponse ByRegex(string fileContent, string[] fileContentLines, string regex, int defaultPosition)
        {
            if (string.IsNullOrEmpty(regex))
                return new LineIndexResponse {
                    Succeeded = true,
                    Index = defaultPosition
                };

            Regex reg = null;
            try
            {
                reg = new Regex(regex);
            }
            catch(Exception ex)
            {
                return new LineIndexResponse {Description = $"Error parsing regex {regex} : {ex}"};
            }

            Match match = reg.Match(fileContent);
            if (match.Success && match.Groups.Count > 0)
            {
                string matchedText = match.Value;
                for (int i = 0; i < fileContentLines.Length ; i ++)
                    if (fileContentLines[i].Contains(matchedText))
                        return new LineIndexResponse {
                            Succeeded = true,
                            Index = i
                        };
            }

            return new LineIndexResponse {
                Succeeded = true,
                Index = defaultPosition
            };
        }

        public static int BySimple(string[] fileContentLines, string line)
        {
            for (int i = 0; i < fileContentLines.Length ; i ++)
                if (line == fileContentLines[i])
                    return i;

            return -1;
        }
    }    
}