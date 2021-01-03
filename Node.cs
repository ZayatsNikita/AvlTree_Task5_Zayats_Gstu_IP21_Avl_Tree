using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using StudentsLib;
using System.Xml.Serialization;

namespace BalancingTrees
{

    public struct Element<T>
    {
        public T data { get; set; }
        public int сount { get; set; }
        public void ChangeCount(int value)
        {
            сount += value;
        }
        public Element(T data)
        {
            this.data = data;
            сount = 1;
        }
    }

    public class Node<T> : IXmlSerializable where T : IComparable
    {
        public Node()
        {
            serializer = new XmlSerializer(typeof(List<Element<T>>));
            _data = new List<Element<T>>();
            Left = null;
            
            Right = null;
        }
        private XmlSerializer serializer;
       

        private List<Element<T>> _data;

        /////заменить на протектед интернал
        public void AddData(T value)
        {
            for (int i = 0; i < _data.Count; i++)
            {
                if (value.Equals(_data[i].data))
                {
                    _data[i].ChangeCount(1);
                    return;
                }
            }
            _data.Add(new Element<T>(value));
        }

        protected internal int Length{ get => _data.Count; }


        protected internal void RemoveFirstItem()
        {
            if(MaxCount>1)
            {
                _data.Where(x => x.сount == MaxCount).First().ChangeCount(-1);
            }
            else
            {
                _data.RemoveAt(0);
                return;
            }
            _data.RemoveAt(0);
        }

        protected internal bool ContainIt(T value)
        {
            foreach (Element<T> element in _data)
            {
                if(element.data.Equals(value))
                {
                    return true;
                }
            }
            return false;
        }

        protected internal Node<T> Right;

        protected internal Node<T> Left;
        protected internal int MaxCount{ get => _data.Max(x => x.сount); }
        
        protected internal int CountOfSpecificElement(T value)
        {
            foreach (Element<T> element in _data)
            {
                if (element.data.Equals(value))
                {
                    return element.сount;
                }
            }
            throw new ArithmeticException();
        }

        public XmlSchema GetSchema() => null;

        public void ReadXml(XmlReader reader)
        {
            if(reader.NodeType == XmlNodeType.Element)
            {
                if (reader.ReadToFollowing("ArrayOfElementOfStudentInformation"))
                {
                    _data = (List<Element<T>>)serializer.Deserialize(reader);
                }
                else
                _data = null;
                    
                
            }
            
        }

        public void WriteXml(XmlWriter writer)
        {
            serializer.Serialize(writer,_data);
        }

        public T Data { get => _data[0].data; }


        protected internal List<Element<T>> DataList { get => _data; set { _data = value; } }

        public Node(T data)
        {
          
            if(data==null)
            {
                throw new NullReferenceException();
            }

            _data = new List<Element<T>>();
            Element<T> element = new Element<T>(data);
            _data.Add(element);

            //XmlRootAttribute rootAttribute = new XmlRootAttribute();
            //rootAttribute.ElementName = "_data";
            //rootAttribute.IsNullable = true;

            //XmlAttributes atts = new XmlAttributes();
            //atts.Xmlns = false;

            //XmlAttributeOverrides overrides = new XmlAttributeOverrides();
            //overrides.Add(typeof(List<Element<T>>), "ArrayOfElementOfStudentInformation", atts);

            serializer = new XmlSerializer(typeof(List<Element<T>>));
            Left = null;
            Right = null;
        }
    }
}

