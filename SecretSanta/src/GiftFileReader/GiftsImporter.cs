using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace SecretSanta.Import
{
    public static class GiftsImporter
    {
        /*public static int DetermineUserID(string filePath)
        {
            if (File.Exists(filePath) || 
                File.Exists(Path.Combine(System.Environment.CurrentDirectory, filePath)))
            {
                string header;
                
            }

            return 0;
        }*/

        public static string GetAbsolutePath(string path)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ArgumentException("The specified path is null or empty.");
            }
            else if (Path.GetPathRoot(path) == Path.GetPathRoot(System.Environment.CurrentDirectory))
            {
                return path;
            }
            else
            {
                return Path.Combine(System.Environment.CurrentDirectory, path);
            }
        }

        public static bool FileDoesExist(string filePath)
        {
            string absolutePath = GetAbsolutePath(filePath);
            return File.Exists(absolutePath);
        }
    }
}
