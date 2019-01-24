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
                try
                {
                    File.Delete(toDelete);
                }
                catch (Exception)
                {

                }
            }
        }

        [TestMethod]
        public void TestInitialization_InitializationSuccessful()
        {
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void GetAbsolutePath_ValidFileRelativePath_ReturnsAbsolutePath()
        {
            string path = GetAbsolutePath(filePath1);
            Assert.AreEqual<string>(filePath1, GiftImporter.GetAbsolutePath(filePath1));
        }

        [TestMethod]
        public void GetAbsolutePath_NullPath_ThrowArgumentException()
        {

        }
    }
}
