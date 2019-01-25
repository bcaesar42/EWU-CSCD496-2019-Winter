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
        private const string _filePath1 = @".\TestGifts1.txt"; //Standard (multiple gifts)
        private const string _filePath2 = @".\TestGifts2.txt"; //Empty file
        private const string _filePath3 = @".\TestGifts3.txt"; //Backwards name
        private const string _filePath4 = @".\TestGifts4.txt"; //Name, but no text
        private const string _filePath5 = @".\TestGifts5.txt"; //One gift
        private const string _filePath6 = @".\TestGifts6.txt"; //Non-existant file
        private const string _filePath7 = @".\TestGifts7.txt"; //Invalid name in header

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
            SetupFile7();
        }

        [TestCleanup]
        public void CleanupTests()
        {
            DeleteTestFiles();
        }

        private void SetupFile1()
        {
            string absolutePath = Path.Combine(GlobalPath, _filePath1);
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
            string absolutePath = Path.Combine(GlobalPath, _filePath2);
            using (StreamWriter writer = File.CreateText(absolutePath))
            {
                //Create empty file.
            }
        }

        private void SetupFile3()
        {
            string absolutePath = Path.Combine(GlobalPath, _filePath3);
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
            string absolutePath = Path.Combine(GlobalPath, _filePath4);
            using (StreamWriter writer = File.CreateText(absolutePath))
            {
                writer.WriteLine("Human McHumanFace");
            }
        }

        private void SetupFile5()
        {
            string absolutePath = Path.Combine(GlobalPath, _filePath5);
            string[] toWrite = {"John Smith",
                                "Cheese Burger_2_A tasty snack._BK.com"};
            using (StreamWriter writer = File.CreateText(absolutePath))
            {
                foreach (string line in toWrite)
                {
                    writer.WriteLine(line);
                }
            }
        }

        private void SetupFile7()
        {
            string absolutePath = Path.Combine(GlobalPath, _filePath7);
            using (StreamWriter writer = File.CreateText(absolutePath))
            {
                writer.WriteLine("InigoMontoya");
            }
        }

        private void DeleteTestFiles()
        {
            string[] filePaths = {_filePath1, _filePath2, _filePath3,
                                  _filePath4, _filePath5, _filePath6, _filePath7};

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
        [DataRow(_filePath1)]
        [DataRow(_filePath2)]
        [DataRow(_filePath3)]
        [DataRow(_filePath4)]
        [DataRow(_filePath5)]
        [DataRow(_filePath6)]
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
        [DataRow(_filePath1)]
        [DataRow(_filePath2)]
        [DataRow(_filePath3)]
        [DataRow(_filePath4)]
        [DataRow(_filePath5)]
        public void FileDoesExist_FileExists_ReturnTrue(string testPath)
        {
            Assert.IsTrue(GiftsImporter.FileDoesExist(testPath));
        }

        [TestMethod]
        public void FileDoesExist_FileDoesntExist_ReturnFalse()
        {
            Assert.IsFalse(GiftsImporter.FileDoesExist(_filePath6));
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
            GiftsImporter.ReadUser(_filePath6);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ReadUser_EmptyFile_ThrowArgumentException()
        {
            GiftsImporter.ReadUser(_filePath2);
        }

        [TestMethod]
        [DataRow(_filePath1, "Bryan", "Caesar")]
        [DataRow(_filePath4, "Human", "McHumanFace")]
        [DataRow(_filePath5, "John", "Smith")]
        public void ReadUser_FirstNameThenLastName_ReturnsName(
                string filePath, string expectedFirst, string expectedLast)
        {
            (string first, string last) result = GiftsImporter.ReadUser(filePath);
            Assert.AreEqual<string>(expectedFirst, result.first);
            Assert.AreEqual<string>(expectedLast, result.last);
        }

        [TestMethod]
        public void ReadUser_LastNameThenFirstName_ReturnsName()
        {
            (string first, string last) result = GiftsImporter.ReadUser(_filePath3);
            Assert.AreEqual<string>("Bryan", result.first);
            Assert.AreEqual<string>("Caesar", result.last);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ReadUser_InvalidNameInHeader_ThrowArgumentException()
        {
            GiftsImporter.ReadUser(_filePath7);
        }
    }
}
