using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.IO;
using UnitaryTestsVideoLessons;
using UnitaryTestsVideoLessons.PersonClasses;

namespace UnitaryTestsVideoLessonsTest
{
    [TestClass]
    public class FileProcessTest
    {

        private const string BAD_FILE_NAME = @"C:\BadFileName.bat";
        private string _GoodFileName;
        private const string File_Name = @"FileToDeploy.txt";
        public TestContext TestContext { get; set; }

        #region Test Inicialize e Cleanup
        
        [TestInitialize]
        public void TestInitialize()
        {
            if (TestContext.TestName.StartsWith("FileNameDoesExists"))
            {
                SetGoodFileName();
                if (!string.IsNullOrEmpty(_GoodFileName))
                {                   
                    TestContext.WriteLine($"Creating File: {_GoodFileName}");
                    File.AppendAllText(_GoodFileName, "Some Text");                   
                }
            }
        }

        [TestCleanup]
        public void TestCleanup()
        {
            if (TestContext.TestName.StartsWith("FileNameDoesExists"))
            {
                if (!string.IsNullOrEmpty(_GoodFileName))
                {
                    TestContext.WriteLine($"Deleting File: {_GoodFileName}");
                    File.Delete(_GoodFileName);
                }
            }
        }

        #endregion

        #region AreSame/AreNotSame Tests

        [TestMethod]
        [Owner("tatianex")]
        public void AreSameTest()
        {
            FileProcess x = new FileProcess();
            FileProcess y = x;

            Assert.AreSame(x, y);
        }

        [TestMethod]
        [Owner("tatianex")]
        public void AreNotSameTest()
        {
            FileProcess x = new FileProcess();
            FileProcess y = new FileProcess();

            Assert.AreNotSame(x, y);
        }

        #endregion

        #region IsIstanceOfType Test

        [TestMethod]
        [Owner("tatianex")]
        public void IsIstanceOfTypeTest()
        {
            PersonManager mgr = new PersonManager();
            Person per;

            per = mgr.CreatePerson("Tatiane", "Pedrelli", true);

            Assert.IsInstanceOfType(per, typeof(Supervisor));
        }

        [TestMethod]
        [Owner("tatianex")]
        public void IsNullTest()
        {
            PersonManager mgr = new PersonManager();
            Person per;

            per = mgr.CreatePerson("", "Pedrelli", true);

            Assert.IsNull(per);
        }

        #endregion

        public void SetGoodFileName()
        {
            _GoodFileName = ConfigurationManager.AppSettings["GoodFileName"];
            if (_GoodFileName.Contains("[AppPath]"))
            {
                _GoodFileName = _GoodFileName.Replace("[AppPath]",
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            }
        }        

        [TestMethod]
        [Timeout(3100)]
        [Priority(1)]
        [Owner("tatianex")]
        public void SimulateTimeOut()
        {
            System.Threading.Thread.Sleep(3000);
        }

        [TestMethod]
        [Description("Checking if a file does exist.")]
        [Priority(0)]
        [TestCategory("Noexception")]
        [Owner("tatianex")]
        public void FileNameDoesExists()
        {           
            FileProcess fp = new FileProcess();
            bool fromCall;

            TestContext.WriteLine($"Testing File: {_GoodFileName}");
            fromCall = fp.FileExists(_GoodFileName);           

            Assert.IsTrue(fromCall);
        }

        [TestMethod]
        [Owner("tatianex")]
        public void FileNameDoesExistsSimpleMessage()
        {
            FileProcess fp = new FileProcess();
            bool fromCall;

            TestContext.WriteLine($"Testing File: {_GoodFileName}");
            fromCall = fp.FileExists(_GoodFileName);

            Assert.IsFalse(fromCall, "File does not exist.");
        }

        [TestMethod]
        [Owner("tatianex")]
        public void FileNameDoesExistsMessageFormatting()
        {
            FileProcess fp = new FileProcess();
            bool fromCall;

            TestContext.WriteLine($"Testing File: {_GoodFileName}");
            fromCall = fp.FileExists(_GoodFileName);

            Assert.IsFalse(fromCall, "File '{0}' does not exist.", _GoodFileName);
        }

        [TestMethod]
        [DeploymentItem(File_Name)]
        [Owner("tatianex")]
        public void FileNameDoesExistsUsingDeploymentItem()
        {
            FileProcess fp = new FileProcess();
            string fileName;
            bool fromCall;

            fileName = $@"{TestContext.DeploymentDirectory}\{File_Name}";
            TestContext.WriteLine($"Checking File: {File_Name}");
            fromCall = fp.FileExists(File_Name);

            Assert.IsTrue(fromCall);
        }

        [TestMethod]
        [Description("Checking if a file does NOT exist.")]
        [Priority(0)]
        [TestCategory("Noexception")]
        [Owner("tatianex")]
        public void FileNameDoesNotExists()
        {
            FileProcess fp = new FileProcess();

            bool fromCall;

            fromCall = fp.FileExists(BAD_FILE_NAME);

            Assert.IsFalse(fromCall);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [Priority(1)]
        [TestCategory("Exception")]
        [Owner("silvanox")]
        public void FileNameNullOrEmpty_ThrowsArgumentNullException()
        {
            FileProcess fp = new FileProcess();

            fp.FileExists("");
        }

        [TestMethod]
        [Priority(1)]
        [TestCategory("Exception")]
        [Owner("silvanox")]
        //[Ignore] Usado para ignorar um teste irrelevante para aquele momento/situação
        public void FileNameNullOrEmpty_ThrowsArgumentNullException_UsingTryCatch()
        {
            FileProcess fp = new FileProcess();

            try
            {
                fp.FileExists("");
            }
            catch (ArgumentException)
            {
                // The test was a success
                return;
            }

            Assert.Fail("Fail expected");
        }

        [TestMethod]
        [ExpectedException(typeof(AssertFailedException))]
        [Owner("silvanox")]
        public void AreEqualCaseSensitiveTest()
        {
            string str1 = "Tatiane";
            string str2 = "tatiane";

            Assert.AreEqual(str1, str2, false);
        }

        [TestMethod]
        [Owner("tatianex")]
        [DataSource("System.Data.SqlClient",
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TesteUnitario;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False",
            "FileProcessTest",
            DataAccessMethod.Sequential)]
        public void FileExistsTestFromDB()
        {
            FileProcess fp = new FileProcess();

            string fileName;
            bool expectedValue, causesException, fromCall;

            fileName = TestContext.DataRow["FileName"].ToString();
            expectedValue = Convert.ToBoolean(TestContext.DataRow["ExpectedValue"]);
            causesException = Convert.ToBoolean(TestContext.DataRow["CausesException"]);

            try
            {
                fromCall = fp.FileExists(fileName);
                Assert.AreEqual(expectedValue, fromCall,
                    $"File: {fileName} has failed. METHOD: FileExistsTestFromDB");
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(causesException);
            }
        }
    }
}
