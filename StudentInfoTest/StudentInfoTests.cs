using NUnit.Framework;
using StudentsLib;
using System;
namespace StudentInfoTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(1)]
        [TestCase(2)]
        public void CreationStudentTest_NullIsSpecifedAsStudentName_NullReferenseException(int mode)
        {
            bool actual = false;
            StudentInformation studentInformation;
            switch (mode)
            {
                case 1:
                    try
                    {
                        studentInformation = new StudentInformation(null, "ABS", DateTime.Now, 8);
                    }
                    catch (NullReferenceException)
                    {
                        actual = true;
                    }
                    break;
                case 2:
                    studentInformation = new StudentInformation();
                    try
                    {
                        studentInformation.StudentName = null;
                    }
                    catch (NullReferenceException)
                    {
                        actual = true;
                    }
                    break;
            }
            Assert.IsTrue(actual);

        }
        [TestCase(1)]
        [TestCase(2)]
        public void CreationStudentTest_NullIsSpecifedAsTestName_NullReferenseException(int mode)
        {

            bool actual = false;
            StudentInformation studentInformation;
            switch (mode)
            {
                case 1:
                    try
                    {
                        studentInformation = new StudentInformation("asd", null, DateTime.Now, 8);
                    }
                    catch (NullReferenceException)
                    {
                        actual = true;
                    }
                    break;
                case 2:
                    studentInformation = new StudentInformation();
                    try
                    {
                        studentInformation.TestName = null;
                    }
                    catch (NullReferenceException)
                    {
                        actual = true;
                    }
                    break;
            }
            Assert.IsTrue(actual);
        }
    }
}