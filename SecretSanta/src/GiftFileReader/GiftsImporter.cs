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
            else if (File.Exists(path))
            {
                return path;
            }
            else
            {
                string combinedPath = Path.Combine(System.Environment.CurrentDirectory, path);
                if (File.Exists(combinedPath))
                {
                    return combinedPath;
                }
                else
                {
                    throw new FileNotFoundException("The specified file does not exist.");
                }
            }
        }
    }
}
