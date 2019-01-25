using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace SecretSanta.Import
{
    public static class GiftsImporter
    {
        public static (string firstName, string lastName) ReadUser(string filePath)
        {
            if (!FileDoesExist(filePath))
            {
                throw new FileNotFoundException("The specified file doesn't exist.");
            }

            string[] fileLines = File.ReadAllLines(GetAbsolutePath(filePath));

            if (fileLines.Length < 1)
            {
                throw new ArgumentException("The specified file is empty.");
            }

            return ParseName(fileLines[0]);
        }

        private static (string firstName, string lastName) ParseName(string header)
        {
            string[] splitNames;

            if (header.Contains(","))
            {
                splitNames = header.Split(',');
            }
            else
            {
                splitNames = header.Split();
            }

            if (splitNames.Length < 2)
            {
                throw new ArgumentException("The first line of the file does not contain a first and last name.");
            }

            if (header.Contains(","))
            {
                return (splitNames[1].Trim(), splitNames[0].Trim());
            }
            return (splitNames[0].Trim(), splitNames[1].Trim());
        }

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
