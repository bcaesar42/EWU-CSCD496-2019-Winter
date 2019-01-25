using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;
using System;
using System.Collections.Generic;
using System.IO;

namespace SecretSanta.Import
{
    public static class GiftsImporter
    {
        private static DbContextOptions<ApplicationDbContext> Options { get; set; }

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

        public static List<Gift> ReadGifts(string filePath)
        {
            if (FileDoesExist(filePath))
            {
                string[] lines = File.ReadAllLines(GetAbsolutePath(filePath));
                (string firstName, string lastName) user = ReadUser(filePath);
                List<Gift> toReturn = new List<Gift>();
                string[] giftText;
                Gift gift;

                for (int index = 1; index < lines.Length; index++)
                {
                    giftText = lines[index].Split('_');
                    gift = new Gift
                    {
                        Title = giftText[0],
                        User = new User
                        {
                            FirstName = user.firstName,
                            LastName = user.lastName
                        },
                        Importance = int.Parse(giftText[1]),
                        Description = giftText[2],
                        URL = giftText[3]
                    };
                    toReturn.Add(gift);
                }

                return toReturn;
            }
            return null;
        }

        public static void ReadGiftsFromFile(string filePath)
        {
            if (FileDoesExist(filePath))
            {
                (string firstName, string lastName) userNames = ReadUser(filePath);
                User user = new User
                {
                    FirstName = userNames.firstName,
                    LastName = userNames.lastName
                };

                List<Gift> gifts = ReadGifts(filePath);

                OpenConnection();

                using (ApplicationDbContext context = new ApplicationDbContext(Options))
                {
                    UserService userService = new UserService(context);
                    userService.AddUser(user);

                    GiftService giftService = new GiftService(context);
                    foreach (Gift gift in gifts)
                    {
                        giftService.AddGiftToUser(gift, user.Id);
                    }
                }
            }
        }

        private static void OpenConnection()
        {
            SqliteConnection connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            Options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(connection)
                .Options;

            using (var context = new ApplicationDbContext(Options))
            {
                context.Database.EnsureCreated();
            }
        }
    }
}
