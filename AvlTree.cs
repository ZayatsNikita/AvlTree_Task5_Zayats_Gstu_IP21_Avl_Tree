using System;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Collections;

namespace BalancingTrees
{
    public class AvlTree<T>: IXmlSerializable, IEnumerable<T>  where T: IComparable
    {
        public AvlTree()
        {
            serializer = new XmlSerializer(typeof(Node<T>));
        }

        Node<T> root;
        public void Add(T data)
        {
            root = AddNodeByItem(root, data);
        }

        private Node<T> AddNodeByNode(Node<T> current, Node<T> nodeWithData)
        {
            if (current == null)
            {
                return nodeWithData;
            }
            int compareRes;
            compareRes = nodeWithData.Data.CompareTo(current.Data);
            if (compareRes < 0)
            {
                current.Left = AddNodeByNode(current.Left, nodeWithData);
            }
            else
            {
                if (compareRes > 0)
                {
                    current.Right = AddNodeByNode(current.Right, nodeWithData);
                }
            }
            current = BalanceTree(current);
            
            return current;
        }



        private Node<T> AddNodeByItem(Node<T> current, T data)
        {
            if (current == null)
            {
                return new Node<T>(data);
            }
            int compareRes;
            compareRes = data.CompareTo(current.Data);
            if (compareRes == 0)
            {
                current.AddData(data);
            }
            else
            {
                if (compareRes < 0)
                {
                    current.Left = AddNodeByItem(current.Left, data);
                }
                else
                {
                    if (compareRes > 0)
                    {
                        current.Right = AddNodeByItem(current.Right, data);
                    }
                }
                current = BalanceTree(current);
            }
            return current;
        }
        private Node<T> BalanceTree(Node<T> current)
        {
            int b_factor = DifferenceInHeightOfLeftAndRightSubTree(current);
            if (b_factor > 1)
            {
                if (DifferenceInHeightOfLeftAndRightSubTree(current.Left) > 0)
                {
                    current = RotateLL(current);
                }
                else
                {
                    current = RotateLR(current);
                }
            }
            else if (b_factor < -1)
            {
                if (DifferenceInHeightOfLeftAndRightSubTree(current.Right) > 0)
                {
                    current = RotateRL(current);
                }
                else
                {
                    current = RotateRR(current);
                }
            }
            return current;
        }
        public void DeleteElementWithSameComparedParameters(T value)
        {
           root = DeleteElementWithSameComparedParameters(root, value);
        }
      

        private Node<T> DeleteElementWithSameComparedParameters(Node<T> current, T value)
        {
            Node<T> parent;
            if (current == null)
            { 
                return null;
            }
            else
            {
                int compareRes = value.CompareTo(current.Data);
                if (compareRes < 0)
                {
                    current.Left = DeleteElementWithSameComparedParameters(current.Left, value);
                    if (DifferenceInHeightOfLeftAndRightSubTree(current) == -2)//here
                    {
                        if (DifferenceInHeightOfLeftAndRightSubTree(current.Right) <= 0)
                        {
                            current = RotateRR(current);
                        }
                        else
                        {
                            current = RotateRL(current);
                        }
                    }
                }
                else if (compareRes > 0)
                {
                    current.Right = DeleteElementWithSameComparedParameters(current.Right, value);
                    if (DifferenceInHeightOfLeftAndRightSubTree(current) == 2)
                    {
                        if (DifferenceInHeightOfLeftAndRightSubTree(current.Left) >= 0)
                        {
                            current = RotateLL(current);
                        }
                        else
                        {
                            current = RotateLR(current);
                        }
                    }
                }
                else
                {
                    if (current.Length > 1 || current.MaxCount>1)
                    {
                        current.RemoveFirstItem();
                        return current;
                    }
                    if (current.Right != null)
                    {
                        parent = current.Right;
                        while (parent.Left != null)
                        {
                            parent = parent.Left;
                        }
                        current.DataList = parent.DataList;
                        current.Right = DeleteElementWithSameComparedParameters(current.Right, parent.Data);
                        if (DifferenceInHeightOfLeftAndRightSubTree(current) == 2)
                        {
                            if (DifferenceInHeightOfLeftAndRightSubTree(current.Left) >= 0)
                            {
                                current = RotateLL(current);
                            }
                            else { current = RotateLR(current); }
                        }
                    }
                    else
                    {  
                        return current.Left;
                    }
                }
            }
            return current;
        }
        public Node<T> Find(T target)
        {
            Node<T> result = root;
            int compareRes;
            while (result != null && (!target.Equals(result.Data)))
            {
                compareRes = result.Data.CompareTo(target);
                if (compareRes > 0)
                    result = result.Left;
                else
                    result = result.Right;
            }
            if (result == null)
            {
                throw new ArgumentException();
            }
            else
            {
                return result;
            }
        }

        public override string ToString()
        {
            if (root == null)
            {
                return ("Tree is empty");
            }
            stringRepresentationOfTree.Clear();
            ConvertTreeToString(root, 0);
            return stringRepresentationOfTree.ToString();
        }

        private readonly StringBuilder stringRepresentationOfTree = new StringBuilder();
        private void ConvertTreeToString(Node<T> current, int level)
        {
            if (current != null)
            {
                ConvertTreeToString(current.Left, level + 1);
                for (int i = 0; i < level; i++)
                {
                    stringRepresentationOfTree.Append("   ");
                }
                stringRepresentationOfTree.Append($"{current.Data}\n"/* + (current.Count > 1 ? $"duplicated {current.Count} times" : null)*/);
                ConvertTreeToString(current.Right, level + 1);
            }
        }
        private int GetHeight(Node<T> current)
        {
            int height = 0;
            if (current != null)
            {
                int l = GetHeight(current.Left);
                int r = GetHeight(current.Right);
                int m = l > r ? l : r;
                height = m + 1;
            }
            return height;
        }
        private int DifferenceInHeightOfLeftAndRightSubTree(Node<T> current)
        {
            int l = GetHeight(current.Left);
            int r = GetHeight(current.Right);
            int b_factor = l - r;
            return b_factor;
        }
       
        private Node<T> RotateRR(Node<T> parent)
        {
            Node<T> pivot = parent.Right;
            parent.Right = pivot.Left;
            pivot.Left = parent;
            return pivot;
        }
        private Node<T> RotateLL(Node<T> parent)
        {
            Node<T> pivot = parent.Left;
            parent.Left = pivot.Right;
            pivot.Right = parent;
            return pivot;
        }
        private Node<T> RotateLR(Node<T> parent)
        {
            Node<T> pivot = parent.Left;
            parent.Left = RotateRR(pivot);
            return RotateLL(parent);
        }
        private Node<T> RotateRL(Node<T> parent)
        {
            Node<T> pivot = parent.Right;
            parent.Right = RotateLL(pivot);
            return RotateRR(parent);
        }




        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            try
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        object t = serializer.Deserialize(reader);
                    }

                }
                reader.Close();
            }
            catch (XmlException)
            {
                throw new FormatException();
            }
        }


        private XmlSerializer serializer;

        public void WriteXml(XmlWriter writer)
        {
            void GetNode(Node<T> current)
            {
                if (current != null)
                {
                    GetNode(current.Left);

                        serializer.Serialize(writer,current);

                    GetNode(current.Right);
                }
            };

           



            //foreach (T data in this)
            //{
            //    writer.WriteStartElement("node");

            //    writer.WriteStartElement($"{data.GetType().Name}");

            //        

            //    writer.WriteEndElement();

            //    writer.WriteEndElement();
            //}
        }


        public IEnumerator<T> GetEnumerator()
        {
            dataOfNodes.Clear();
            GetData(root);
            return dataOfNodes.GetEnumerator();
        }
        List<T> dataOfNodes=new List<T>();

        public void GetData(Node<T> current)
        {
            if (current != null)
            {
                GetData(current.Left);
                dataOfNodes.Add(current.Data);
                GetData(current.Right);
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}