using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCHTCoursework
{
    static class FilePuller
    {
        public static string[] dirs;
        public static List<string> GetFiles(string[] path) 
        {
            System.String[] files;
            List<string> fileList = new List<string>();
            for (int i = 0; i < path.Length; i++)
            {
                files = Directory.GetFiles(path[i]);
                foreach (var file in files)
                {
                    fileList.Add(file);
                }
            }
            return fileList;
        }
        public static string[] GetDirectoriesMe(string rootPath) {
            
                dirs = Directory.GetDirectories(rootPath, "*", SearchOption.TopDirectoryOnly); //get each folder
            
            return dirs;
        }
        
        public static string[] GetASDDirectories(string path)
        {
            string[] files = GetDirectoriesMe(path);
            return files; 
        }
        public static string[] GetTDDirectories(string path)
        {
            string[] files = GetDirectoriesMe(path);
            return files;
        }

        public static List<string> FileFilter(List<string> files, string filter) { 
            List<string> result = new List<string>();
            foreach (string file in files)
            {
                
                if (file.ToLower().Contains(filter)){
                    result.Add(file);
                }
            }
            return result;
        }
    }
}
