using System;

namespace StudentsLib
{
    [Serializable]
    public struct StudentInformation : IComparable
    {
        private string _studentName;
        private string _testName;
        public string StudentName {
            
            get=> _studentName;
            
            set
            {
                if(value == null)
                {
                    throw new NullReferenceException();
                }
                _studentName = value;
            }
        }
        public string TestName
        {
            get => _testName;

            set
            {
                if (value == null)
                {
                    throw new NullReferenceException();
                }
                _testName = value;
            }
        }
        
        public DateTime DateOfPassingTheThest { get; set; }
        
        public int MarkforTest { get;  set; }

        public StudentInformation(string name, string testName, DateTime dateOfPassingTheThest, int mark)
        {
            if(name==null || testName == null)
            {
                throw new NullReferenceException();
            }
            _studentName = name;
            _testName = testName;
            DateOfPassingTheThest = dateOfPassingTheThest;
            MarkforTest = mark;
        }

        public override bool Equals(object obj)
        {
            if (obj is StudentInformation)
            {
                StudentInformation fotCompare = (StudentInformation)obj;
                return (fotCompare.StudentName ?? "eroor")== (StudentName ?? "eroor") && (fotCompare.TestName ?? "eroor") == (TestName ?? "eroor") && DateOfPassingTheThest == fotCompare.DateOfPassingTheThest && MarkforTest == fotCompare.MarkforTest;
            }
            return false;
        }

        public int CompareTo(object obj)
        {
            if (obj is StudentInformation)
            {
                return MarkforTest.CompareTo(((StudentInformation)obj).MarkforTest);
            }
            throw new ArgumentException();
        }
    }
     
}
