using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace GiftFileReader.Tests
{
    [TestClass]
    public class GiftsImporterTests
    {
        private const string filePath1 = @"\TestFiles\TestGifts1.txt"; //Standard (multiple gifts)
        private const string filePath2 = @"\TestFiles\TestGifts2.txt"; //Empty file
        private const string filePath3 = @"\TestFiles\TestGifts3.txt"; //Backwards name
        private const string filePath4 = @"\TestFiles\TestGifts4.txt"; //Name, but no text
        private const string filePath5 = @"\TestFiles\TestGifts5.txt"; //One gift
        private const string filePath6 = @"\TestFiles\TestGifts6.txt"; //Non-existant file

        public string GlobalPath { get; set; }

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
        public void TestMethod1()
        {
        }
    }
}
