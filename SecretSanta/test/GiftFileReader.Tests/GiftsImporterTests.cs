using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using SecretSanta.Import;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;
using SecretSanta.Domain.Interface;

namespace SecretSanta.Import.Tests
{
    [TestClass]
    public class GiftsImporterTests
    {
        private const string filePath1 = @".\TestGifts1.txt"; //Standard (multiple gifts)
        private const string filePath2 = @".\TestGifts2.txt"; //Empty file
        private const string filePath3 = @".\TestGifts3.txt"; //Backwards name
        private const string filePath4 = @".\TestGifts4.txt"; //Name, but no text
        private const string filePath5 = @".\TestGifts5.txt"; //One gift
        private const string filePath6 = @".\TestGifts6.txt"; //Non-existant file

        private string GlobalPath
        {
            get
            {
                return System.Environment.CurrentDirectory;
            }
        }

        [TestInitialize]
        public void IntializeTests()
        {
            DeleteTestFiles();
            SetupFile1();
            SetupFile2();
            SetupFile3();
            SetupFile4();
            SetupFile5();
        }

        [TestCleanup]
        public void CleanupTests()
        {
            DeleteTestFiles();
        }

        private void SetupFile1()
        {
            string absolutePath = Path.Combine(GlobalPath, filePath1);
            string[] toWrite = {"Bryan Caesar",
                                "XBox One_12_A box for gaming._Amazon.com",
                                "Tesla Model S_9001_An awesome electric car._Tesla.com"};
            using (StreamWriter writer = File.CreateText(absolutePath))
            {
                foreach (string line in toWrite)
                {
                    writer.WriteLine(line);
                }
            }
        }

        private void SetupFile2()
        {
            string absolutePath = Path.Combine(GlobalPath, filePath2);
            using (StreamWriter writer = File.CreateText(absolutePath))
            {
                //Create empty file.
            }
        }

        private void SetupFile3()
        {
            string absolutePath = Path.Combine(GlobalPath, filePath3);
            string[] toWrite = {"Caesar, Bryan",
                                "XBox One_12_A box for gaming._Amazon.com",
                                "Tesla Model S_9001_An awesome electric car._Tesla.com"};
            using (StreamWriter writer = File.CreateText(absolutePath))
            {
                foreach (string line in toWrite)
                {
                    writer.WriteLine(line);
                }
            }
        }

        private void SetupFile4()
        {
            string absolutePath = Path.Combine(GlobalPath, filePath4);
            using (StreamWriter writer = File.CreateText(absolutePath))
            {
                writer.WriteLine("Human McHumanFace");
            }
        }

        private void SetupFile5()
        {
            string absolutePath = Path.Combine(GlobalPath, filePath5);
            string[] toWrite = {"John SMith",
                                "Cheese Burger_2_A tasty snack._BK.com"};
            using (StreamWriter writer = File.CreateText(absolutePath))
            {
                foreach (string line in toWrite)
                {
                    writer.WriteLine(line);
                }
            }
        }

        private void DeleteTestFiles()
        {
            string[] filePaths = {filePath1, filePath2, filePath3,
                                  filePath4, filePath5, filePath6};

            string toDelete;
            foreach (string path in filePaths)
            {
                toDelete = Path.Combine(GlobalPath, path);
                File.Delete(toDelete);
            }
        }

        [TestMethod]
        public void TestInitialization_InitializationSuccessful()
        {
            Assert.IsTrue(true);
        }

        [TestMethod]
        [DataRow(filePath1)]
        [DataRow(filePath2)]
        [DataRow(filePath3)]
        [DataRow(filePath4)]
        [DataRow(filePath5)]
        [DataRow(filePath6)]
        public void GetAbsolutePath_ValidFileRelativePath_ReturnsAbsolutePath(string testPath)
        {
            string expectedPath = Path.Combine(System.Environment.CurrentDirectory, testPath);
            Assert.AreEqual<string>(expectedPath, GiftsImporter.GetAbsolutePath(testPath));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetAbsolutePath_NullPath_ThrowArgumentException()
        {
            GiftsImporter.GetAbsolutePath(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetAbsolutePath_EmptyPath_ThrowArgumentException()
        {
            GiftsImporter.GetAbsolutePath("");
        }

        [TestMethod]
        [DataRow(filePath1)]
        [DataRow(filePath2)]
        [DataRow(filePath3)]
        [DataRow(filePath4)]
        [DataRow(filePath5)]
        public void FileDoesExist_FileExists_ReturnTrue(string testPath)
        {
            Assert.IsTrue(GiftsImporter.FileDoesExist(testPath));
        }

        [TestMethod]
        public void FileDoesExist_FileDoesntExist_ReturnFalse()
        {
            Assert.IsFalse(GiftsImporter.FileDoesExist(filePath6));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FileDoesExist_NullPath_ThrowArgumentException()
        {
            GiftsImporter.FileDoesExist(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FileDoesExist_EmptyPath_ThrowArgumentException()
        {
            GiftsImporter.FileDoesExist("");
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ReadUser_FileDoesntExist_ThrowFileNotFoundException()
        {
            GiftsImporter.ReadUser(filePath6);
        }
    }
}
