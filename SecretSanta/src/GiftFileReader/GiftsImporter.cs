using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace GiftFileReader
{
    public class GiftsImporter
    {
        public int DetermineUserID(string filePath)
        {
            if (File.Exists(filePath) || 
                File.Exists(Path.Combine(System.Environment.CurrentDirectory, filePath)))
            {
                string header;
                
            }

            return 0;
        }

        public string GetAbsolutePath(string path)
        {
            if (File.Exists(path))
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
