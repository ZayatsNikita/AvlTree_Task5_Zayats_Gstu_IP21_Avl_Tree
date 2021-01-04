using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace BalancingTrees
{
    public class AvlTree<T>: IXmlSerializable  where T: IComparable
    {
        public AvlTree()
        {
            serializer = new XmlSerializer(typeof(Node<T>));
        }

        public Node<T> Root { get; private set; }
        public void Add(T data)
        {
            if(data == null)
            {
                throw new NullReferenceException();
            }
            Root = AddNodeByItem(Root, data);
        }
        private void Add(Node<T> node)
        {
            Root = AddNewNode(Root, node);
        }
        private Node<T> AddNewNode(Node<T> current, Node<T> nodeWithData)
        {
            if (current == null)
            {
                return nodeWithData;
            }
            int compareRes;
            compareRes = nodeWithData.Data.CompareTo(current.Data);
            if (compareRes < 0)
            {
                current.Left = AddNewNode(current.Left, nodeWithData);
            }
            else
            {
                if (compareRes > 0)
                {
                    current.Right = AddNewNode(current.Right, nodeWithData);
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
            int deltaBetwinnLeftAndRightSubTreeHeight = DifferenceInHeightOfLeftAndRightSubTree(current);
            if (deltaBetwinnLeftAndRightSubTreeHeight > 1)
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
            else if (deltaBetwinnLeftAndRightSubTreeHeight < -1)
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
        public void RemoveOneElementWithSameComparedParameters(T value)
        {
           Root = RemoveOneElementWithSameComparedParameters(Root, value,1);
        }
        private Node<T> RemoveOneElementWithSameComparedParameters(Node<T> current, T value, int step)
        {
            Node<T> parentNode;
            if (current == null)
            { 
                return null;
            }
            else
            {
                int compareRes = value.CompareTo(current.Data);
                if (compareRes < 0)
                {
                    current.Left = RemoveOneElementWithSameComparedParameters(current.Left, value, step);
                    if (DifferenceInHeightOfLeftAndRightSubTree(current) == -2)
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
                else
                {
                    if (compareRes > 0)
                    {
                        current.Right = RemoveOneElementWithSameComparedParameters(current.Right, value,step);
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
                        if (step == 1 && (current.Length > 1 || current.MaxCount > 1))
                        {
                            current.RemoveFirstItem();
                            return current;
                        }
                        if (current.Right != null)
                        {
                            step = 0;
                            parentNode = current.Right;
                            while (parentNode.Left != null)
                            {
                                parentNode = parentNode.Left;
                            }
                            current.DataList = parentNode.DataList;
                            current.Right = RemoveOneElementWithSameComparedParameters(current.Right, parentNode.Data, step);
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
            }
            return current;
        }
        public override string ToString()
        {
            if (Root == null)
            {
                return ("Tree is empty");
            }
            stringRepresentationOfTree.Clear();
            ConvertTreeToString(Root, 0);
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
                stringRepresentationOfTree.Append($"{current.Data}\n");
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
                Node<T> node=null;
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        node = (Node<T>)serializer.Deserialize(reader);
                        Add(node);
                    }
                }
            
            }
            catch (XmlException)
            {
                throw new FormatException();
            }
        }

        private XmlSerializer serializer;

        public void WriteXml(XmlWriter writer)
        {
            GetNode(Root, (current)=> { serializer.Serialize(writer, current); });
        }
        
        public List<T> this[int index]
        {
            get
            {
                List<T> res = null;
                int count = 0;
                GetNode(Root, (current) =>
                {
                    if (count == index)
                    {
                        res = current.GetFullInformation();
                        count++;
                    }
                    else
                    {
                        count++;
                    }

                });

                if (res == null)
                {
                    throw new ArgumentOutOfRangeException();
                }
                else
                {
                    return res;
                }

            }
        }

        private void GetNode(Node<T> current, Action<Node<T>> action)
        {
            if (current != null)
            {
                GetNode(current.Left,action);
                    action(current);
                GetNode(current.Right,action);
            }
        }

        
    }
}