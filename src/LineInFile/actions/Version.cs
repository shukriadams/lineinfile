using System.Reflection;
using LineInFile.Porter_Packages.MadScience_ReflectionHelpers;

namespace LineInFile
{
    public class Version
    {
        /// <summary>
        /// 
        /// </summary>
        public void Work()
        {
            string currentVersion = ResourceHelper.ReadStringResourceFromCallingAssembly("LineInFile.currentVersion.txt");
            Console.WriteLine($"version : {currentVersion}");
        }
    }    
}