using NUnit.Framework;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using BalancingTrees;
using System;
using StudentsLib;
namespace AvlTreeTests
{
    
    public class Tests
    {

        public static bool AreEqual<T>(List<T> one ,List<T> two)
        {
            if(one.Count==two.Count)
            {
                for (int i = 0; i < one.Count; i++)
                {
                    if (!one[i].Equals(two[i]))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        StudentInformation sInfo2 = new StudentInformation("Lesha", "Trud", DateTime.Now, 2);
        StudentInformation sInfo3 = new StudentInformation("lera", "Obj", DateTime.Now, 3);
        StudentInformation sInfo4 = new StudentInformation("Sergey", "Strelba", DateTime.Now, 4);
        StudentInformation sInfo5 = new StudentInformation("", "afdsfasd", DateTime.Now, 5);
        StudentInformation sInfo6 = new StudentInformation("Roma", "MMA", DateTime.Now, 6);
        StudentInformation sInfo7 = new StudentInformation("Liza", "Biologiya", DateTime.Now, 7);
        StudentInformation sInfo8_1 = new StudentInformation("Larisa", "PE", DateTime.Now, 8);
        StudentInformation sInfo8_3 = new StudentInformation("Dinis", "French", DateTime.Now, 8);
        StudentInformation sInfo8_2 = new StudentInformation("Roma", "MMA", DateTime.Now, 8);
        StudentInformation sInfo9 = new StudentInformation("Nikita", "Seti", DateTime.Now, 9);
        StudentInformation sInfo10_1 = new StudentInformation("kiril", "ximiya", DateTime.Now, 10);
        StudentInformation sInfo10_2 = new StudentInformation("masha", "bel", DateTime.Now, 10);
        
        [Test]
        public void RotateRRTest_AllElementsArePassedToTheRightSide_GetBalansedTree()
        {
            AvlTree<StudentInformation> tree = new AvlTree<StudentInformation>();
            tree.Add(sInfo3);
            tree.Add(sInfo6);
            tree.Add(sInfo9);
            bool actual = tree.Root.Left.Data.Equals(sInfo3) && tree.Root.Right.Data.Equals(sInfo9) && tree.Root.Data.Equals(sInfo6);
            Assert.IsTrue(actual);
        }

        [Test]
        public void RotateLLTest_AllElementsArePassedToTheLeftSide_GetBalansedTree()
        {
            AvlTree<StudentInformation> tree = new AvlTree<StudentInformation>();
            tree.Add(sInfo9);
            tree.Add(sInfo6);
            tree.Add(sInfo3);
            bool actual = tree.Root.Left.Data.Equals(sInfo3) && tree.Root.Right.Data.Equals(sInfo9) && tree.Root.Data.Equals(sInfo6);
            Assert.IsTrue(actual);
        }
        [Test]
        public void RotateRLTest_OneAddToLOneAddToRLLOneAddToRR_BalancedTree()
        {
            AvlTree<StudentInformation> tree = new AvlTree<StudentInformation>();
            tree.Add(sInfo3);
            tree.Add(sInfo2);
            tree.Add(sInfo7);
            tree.Add(sInfo5);
            tree.Add(sInfo8_1);
            tree.Add(sInfo4);

            bool actual = tree.Root.Data.Equals(sInfo5) && tree.Root.Left.Data.Equals(sInfo3) && tree.Root.Right.Data.Equals(sInfo7);
            actual = actual && tree.Root.Left.Left.Data.Equals(sInfo2) && tree.Root.Left.Right.Data.Equals(sInfo4) && tree.Root.Right.Right.Data.Equals(sInfo8_1);

            Assert.IsTrue(actual);
        }

        [Test]
        public void RotateLRTest_OneAddToROneAddLOneAddLLOndeAddLROneAddLRL()
        {
            AvlTree<StudentInformation> tree = new AvlTree<StudentInformation>();
            tree.Add(sInfo7);
            tree.Add(sInfo8_1);
            tree.Add(sInfo3);
            tree.Add(sInfo2);
            tree.Add(sInfo5);
            tree.Add(sInfo4);
            bool actual = tree.Root.Data.Equals(sInfo5) && tree.Root.Left.Data.Equals(sInfo3) && tree.Root.Right.Data.Equals(sInfo7);
            actual = actual && tree.Root.Left.Left.Data.Equals(sInfo2) && tree.Root.Left.Right.Data.Equals(sInfo4) && tree.Root.Right.Right.Data.Equals(sInfo8_1);
            Assert.IsTrue(actual);
        }


        [TestCase(0)]
        [TestCase(2)]
        [TestCase(6)]
        [TestCase(8)]
        public void TestForProvidingSequentialAccessToElementsOfTree_CurrectIndexs_ListWithData(int index)
        {
            AvlTree<StudentInformation> tree = new AvlTree<StudentInformation>();
            tree.Add(sInfo2);
            tree.Add(sInfo3);
            tree.Add(sInfo4);
            tree.Add(sInfo5);
            tree.Add(sInfo6);
            tree.Add(sInfo7);
            tree.Add(sInfo8_1);
            tree.Add(sInfo8_2);
            tree.Add(sInfo8_3);
            tree.Add(sInfo9);
            tree.Add(sInfo10_1);
            tree.Add(sInfo10_2);
            List<StudentInformation> actual = tree[index];
            List<StudentInformation> expected = new List<StudentInformation>(); ;
            switch (index)
            {
                case 0:
                    expected.Add(sInfo2);
                    break;
                case 2:
                    expected.Add(sInfo4);
                    break;
                case 6:
                    expected.Add(sInfo8_1);
                    expected.Add(sInfo8_2);
                    expected.Add(sInfo8_3);
                    break;
                case 8:
                    expected.Add(sInfo10_1);
                    expected.Add(sInfo10_2);
                    break;
            }
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestCase(-1)]
        [TestCase(9)]
        public void TestForProvidingSequentialAccessToElementsOfTree_IncourrectIndexs_ListWithData(int index)
        {
            AvlTree<StudentInformation> tree = new AvlTree<StudentInformation>();
            tree.Add(sInfo2);
            tree.Add(sInfo3);
            tree.Add(sInfo4);
            tree.Add(sInfo5);
            tree.Add(sInfo6);
            tree.Add(sInfo7);
            tree.Add(sInfo8_1);
            tree.Add(sInfo8_2);
            tree.Add(sInfo8_3);
            tree.Add(sInfo9);
            tree.Add(sInfo10_1);
            tree.Add(sInfo10_2);
            bool actual=false;
            try
            {
                _ = tree[index];
            }
            catch(ArgumentException)
            {
                actual = true;
            }
            Assert.IsTrue(actual);
        }
        [Test]
        public void XmlSerealizationTest_SerealieTreeWithData_GetEqualTreeWithDesirialize()
        {

            AvlTree<StudentInformation> tree = new AvlTree<StudentInformation>();
            tree.Add(sInfo2);
            tree.Add(sInfo3);
            tree.Add(sInfo4);
            tree.Add(sInfo5);
            tree.Add(sInfo6);
            tree.Add(sInfo7);
            tree.Add(sInfo8_1);
            tree.Add(sInfo8_2);
            tree.Add(sInfo8_3);
            tree.Add(sInfo9);
            tree.Add(sInfo10_1);
            tree.Add(sInfo10_2);


            XmlSerializer xmlSerializer = new XmlSerializer(tree.GetType());

            using (FileStream fileStream = new FileStream(@"..\..\..\TestProg.xml", FileMode.OpenOrCreate))
            {
               xmlSerializer.Serialize(fileStream,tree);
            }
            AvlTree<StudentInformation> res=null;
            using (FileStream fileStream = new FileStream(@"..\..\..\TestProg.xml", FileMode.Open))
            {
                res = (AvlTree<StudentInformation>)xmlSerializer.Deserialize(fileStream);
            }

            bool actual = AreEqual(res[0], tree[0]) && AreEqual(res[1], tree[1]) && AreEqual(res[2], tree[2]);
            actual = actual && AreEqual(res[3], tree[3]) && AreEqual(res[4], tree[4]) && AreEqual(res[5], tree[5])
                && AreEqual(res[6], tree[6]) && AreEqual(res[7], tree[7]) && AreEqual(res[8], tree[8]);

            Assert.IsTrue(actual);

        }
        [Test]
        public void AddTest_NullValue_NullRefernceEceptionThrown()
        {
            AvlTree<string> tree = new AvlTree<string>();
            bool actual = false;
            try
            {
                tree.Add(null);
            }
            catch(NullReferenceException)
            {
                actual = true;
            }
            Assert.IsTrue(actual);

        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void RemoveOneElementWithSameComparedParametersTest_RemoveElementWithSameComapredParameters_ChangedTree(int mode)
        {
            AvlTree<StudentInformation> tree = new AvlTree<StudentInformation>();
            tree.Add(sInfo2);
            tree.Add(sInfo3);
            tree.Add(sInfo4);
            tree.Add(sInfo5);
            tree.Add(sInfo6);
            tree.Add(sInfo7);
            tree.Add(sInfo8_1);
            tree.Add(sInfo8_2);
            tree.Add(sInfo8_3);
            tree.Add(sInfo9);
            tree.Add(sInfo10_1);
            tree.Add(sInfo10_2);
            List<StudentInformation> expected = new List<StudentInformation>();
            List<StudentInformation> actual;
                
            switch(mode)
            {
                case 1:
                    tree.RemoveOneElementWithSameComparedParameters(sInfo8_1);
                    expected.Add(sInfo8_2);
                    expected.Add(sInfo8_3);
                    break;
                case 2:
                    tree.RemoveOneElementWithSameComparedParameters(sInfo8_2);
                    tree.RemoveOneElementWithSameComparedParameters(sInfo8_2);
                    expected.Add(sInfo8_3);
                    break;
                case 3:
                    tree.RemoveOneElementWithSameComparedParameters(sInfo8_3);
                    tree.RemoveOneElementWithSameComparedParameters(sInfo8_1);
                    tree.RemoveOneElementWithSameComparedParameters(sInfo8_1);
                    expected.Add(sInfo9);
                    break;
            }
            actual = tree[6];
            CollectionAssert.AreEqual(expected,actual);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void RemoveOneElementWithSameComparedParametersTest_ThereAreNoSameElementsInTree_TreeNoChanged(int mode)
        {
            AvlTree<StudentInformation> tree = new AvlTree<StudentInformation>();
            AvlTree<StudentInformation> secondTree =  new AvlTree<StudentInformation>();
            #region initialization of both tree and secondTree
            tree.Add(sInfo2);
            tree.Add(sInfo3);
            tree.Add(sInfo4);
            tree.Add(sInfo5);
            tree.Add(sInfo6);
            tree.Add(sInfo8_1);
            tree.Add(sInfo8_2);
            tree.Add(sInfo8_3);
            tree.Add(sInfo9);
            tree.Add(sInfo10_1);
            tree.Add(sInfo10_2);


            secondTree.Add(sInfo2);
            secondTree.Add(sInfo3);
            secondTree.Add(sInfo4);
            secondTree.Add(sInfo5);
            secondTree.Add(sInfo6);
            secondTree.Add(sInfo8_1);
            secondTree.Add(sInfo8_2);
            secondTree.Add(sInfo8_3);
            secondTree.Add(sInfo9);
            secondTree.Add(sInfo10_1);
            secondTree.Add(sInfo10_2);
            #endregion
            List<StudentInformation> expected = new List<StudentInformation>();
            
            bool actual = AreEqual(secondTree[0], tree[0]) && AreEqual(secondTree[1], tree[1]) && AreEqual(secondTree[2], tree[2]);
            actual = actual && AreEqual(secondTree[3], tree[3]) && AreEqual(secondTree[4], tree[4]) && AreEqual(secondTree[5], tree[5])
                && AreEqual(secondTree[6], tree[6]) && AreEqual(secondTree[7], tree[7]);

            tree.RemoveOneElementWithSameComparedParameters(sInfo7);

            actual = actual && AreEqual(secondTree[0], tree[0]) && AreEqual(secondTree[1], tree[1]) && AreEqual(secondTree[2], tree[2]);
            actual = actual && AreEqual(secondTree[3], tree[3]) && AreEqual(secondTree[4], tree[4]) && AreEqual(secondTree[5], tree[5])
                && AreEqual(secondTree[6], tree[6]) && AreEqual(secondTree[7], tree[7]);

            Assert.IsTrue(actual);
        }
        [TestCase("have 2 descending")]
        [TestCase("have 1 Left descending")]
        [TestCase("have 1 Right descending")]
        [TestCase("Root")]
        public void RemoveOneElementWithSameComparedParametersTest_DeleteElement_TreeIsStillBalanced(string mode)
        {
            AvlTree<StudentInformation> forComparing = new AvlTree<StudentInformation>();
            
            forComparing.Add(sInfo2);
            forComparing.Add(sInfo3);
            forComparing.Add(sInfo4);
            forComparing.Add(sInfo5);
            forComparing.Add(sInfo6);
            forComparing.Add(sInfo7);
            forComparing.Add(sInfo8_1);
            forComparing.Add(sInfo8_2);
            forComparing.Add(sInfo8_3);
            forComparing.Add(sInfo9);
            forComparing.Add(sInfo10_1);
            forComparing.Add(sInfo10_2);

            AvlTree<StudentInformation> tree = new AvlTree<StudentInformation>();
            
            tree.Add(sInfo5);
            tree.Add(sInfo7);
            tree.Add(sInfo3);
            tree.Add(sInfo2);
            tree.Add(sInfo4);
            tree.Add(sInfo6);
            tree.Add(sInfo9);

            bool actual = false;

            #region action
            switch (mode)
            {
                case "have 2 descending":
                    tree.Add(sInfo8_1);
                    tree.Add(sInfo8_2);
                    tree.Add(sInfo8_3);
                    tree.Add(sInfo10_1);
                    tree.Add(sInfo10_2);
                    tree.RemoveOneElementWithSameComparedParameters(sInfo7);
                    break;
                case "have 1 Left descending":
                    tree.Add(sInfo8_1);
                    tree.Add(sInfo8_2);
                    tree.RemoveOneElementWithSameComparedParameters(sInfo8_1);
                    tree.RemoveOneElementWithSameComparedParameters(sInfo8_1);
                    
                    break;
                case "have 1 Right descending":
                    tree.Add(sInfo10_1);
                    tree.Add(sInfo10_2);
                    tree.RemoveOneElementWithSameComparedParameters(sInfo10_1);
                    tree.RemoveOneElementWithSameComparedParameters(sInfo10_1);

                    break;
                case "Root":
                    tree.RemoveOneElementWithSameComparedParameters(sInfo5);

                    break;
            }
            #endregion
            #region get actual
            switch (mode)
            {
                case "have 2 descending":
                    actual = AreEqual(forComparing[0], tree[0]) && AreEqual(forComparing[1], tree[1]) && AreEqual(forComparing[2], tree[2])
                        &&  AreEqual(forComparing[3], tree[3]) && AreEqual(forComparing[6], tree[5]) && AreEqual(forComparing[4], tree[4]) && AreEqual(forComparing[7], tree[6])
                        && tree.Root.Right.Right.Right.Data.Equals(sInfo10_1) && tree.Root.Right.Right.Left == null && forComparing.Root.Right.Right.Left.Data.Equals(sInfo8_1);
                    break;
                case "have 1 Left descending":
                case "have 1 Right descending":
                    actual = AreEqual(forComparing[0], tree[0]) && AreEqual(forComparing[1], tree[1]) && AreEqual(forComparing[2], tree[2])
                        && AreEqual(forComparing[3], tree[3]) && AreEqual(forComparing[4], tree[4]) && AreEqual(forComparing[5], tree[5])
                        && AreEqual(forComparing[7], tree[6]) && tree.Root.Right.Right.Right==null && tree.Root.Right.Right.Left == null;
                    break;
                case "Root":
                    actual= AreEqual(forComparing[0], tree[0]) && AreEqual(forComparing[1], tree[1]) && AreEqual(forComparing[2], tree[2])
                        && tree.Root.Data.Equals(sInfo6) && AreEqual(forComparing[5], tree[4]) && AreEqual(forComparing[7], tree[5])
                        && tree.Root.Right.Right.Right == null && tree.Root.Right.Right.Left == null;

                    break;
            }
            #endregion

            Assert.IsTrue(actual);

        }
        [Test]
        public void ReadXmlTest_NullIsUsed_NullReferenceException()
        {
            AvlTree<StudentInformation> tree = new AvlTree<StudentInformation>();
            bool actual = true;
            try
            {
                tree.ReadXml(null);
            }
            catch(NullReferenceException)
            {
                actual = true;
            }
            Assert.IsTrue(actual);
        }
        [Test]
        public void WriteXmlTest_NullIsUsed_NullReferenceException()
        {
            AvlTree<StudentInformation> tree = new AvlTree<StudentInformation>();
            bool actual = true;
            try
            {
                tree.WriteXml(null);
            }
            catch (NullReferenceException)
            {
                actual = true;
            }
            Assert.IsTrue(actual);
        }



    }

}