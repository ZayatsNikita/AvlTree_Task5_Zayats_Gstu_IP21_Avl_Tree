using System;
using System.Collections.Generic;
using System.Linq;
namespace BalancingTrees
{
    public class Node<T> where T : IComparable
    {
        protected internal struct Element {
            public T data;
            public int сount;
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

        private List<Element> _data;

        protected internal void AddData(T value)
        {
            for (int i = 0; i < _data.Count; i++)
            {
                if (value.Equals(_data[i].data))
                {
                    _data[i].ChangeCount(1);
                    return;
                }
            }
            _data.Add(new Element(value));
        }

        protected internal int Length{ get => _data.Count; }

        protected internal void RemoveData(T value)
        {
            for (int i = 0; i < _data.Count; i++)
            {
                if (value.Equals(_data[i].data))
                {
                    if (_data[i].сount > 1)
                    {
                        _data[i].ChangeCount(-1);
                        return;
                    }
                    else
                    {
                        _data.RemoveAt(i);
                        return;
                    }
                }
            }
        }

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

        public bool ContainIt(T value)
        {
            foreach (Element element in _data)
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
            foreach (Element element in _data)
            {
                if (element.data.Equals(value))
                {
                    return element.сount;
                }
            }
            throw new ArithmeticException();
        }

        public T Data { get => _data[0].data; }


        protected internal List<Element> DataList { get => _data; set { _data = value; } }

        public Node(T data)
        {
            if(data==null)
            {
                throw new NullReferenceException();
            }

            _data = new List<Element>();
            _data.Add(new Element());
            Left = null;
            Right = null;
        }
    }
}

