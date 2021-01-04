using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace BalancingTrees
{

    [Serializable]
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
            _serializerForList = new XmlSerializer(typeof(List<Element<T>>));
            DataList = new List<Element<T>>();
            Left = null;            
            Right = null;
        }
        private XmlSerializer _serializerForList;

        protected internal void AddData(T value)
        {
            for (int i = 0; i < DataList.Count; i++)
            {
                if (value.Equals(DataList[i].data))
                {
                    DataList[i].ChangeCount(1);
                    return;
                }
            }
            DataList.Add(new Element<T>(value));
        }


        protected internal List<T>  GetFullInformation()
        {
            List<T> list = new List<T>();

            foreach (Element<T> item in DataList)
            {
                for (int i = 0; i < item.сount; i++)
                {
                    list.Add(item.data);
                }
            }
            return list;
        }

        protected internal int Length{ get => DataList.Count; }

        protected internal void RemoveFirstItem()
        {
            if(MaxCount>1)
            {
                DataList.Where(x => x.сount == MaxCount).First().ChangeCount(-1);
            }
            else
            {
                DataList.RemoveAt(0);
                return;
            }
            DataList.RemoveAt(0);
        }



        public Node<T> Right { get; protected internal set; }

        public Node<T> Left { get; protected internal set; }
        protected internal int MaxCount{ get => DataList.Max(x => x.сount); }
        
        public XmlSchema GetSchema() => null;

        public void ReadXml(XmlReader reader)
        {
                if (reader.ReadToFollowing("ArrayOfElementOfStudentInformation"))
                {
                    DataList = (List<Element<T>>)_serializerForList.Deserialize(reader);
                }
        }
            
        

        public void WriteXml(XmlWriter writer)
        {
            _serializerForList.Serialize(writer,DataList);
        }

        public T Data { get => DataList[0].data; }

        /// <summary>
        /// Uses for solving the collision problem.
        /// </summary>
        protected internal List<Element<T>> DataList { get; set; }

        public Node(T data)
        {
          
            if(data==null)
            {
                throw new NullReferenceException();
            }

            DataList = new List<Element<T>>();
            Element<T> element = new Element<T>(data);
            DataList.Add(element);
            _serializerForList = new XmlSerializer(typeof(List<Element<T>>));
            
            Left = null;
            Right = null;
        }
    }
}

