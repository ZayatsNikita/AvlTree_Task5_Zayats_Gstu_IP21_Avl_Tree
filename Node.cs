using System;

namespace BalancingTrees
{
    public class Node<T> where T : IComparable
    {
        protected internal Node<T> Right;

        protected internal Node<T> Left;

        protected internal int Count{get;set;}=1;
        public T Data { get;  set; }
        public Node(T data)
        {
            if(data==null)
            {
                throw new NullReferenceException();
            }
            Data = data;
            Left = null;
            Right = null;
        }
    }
}

