using System;

namespace AvlTree
{
    class Node<T> where T : IComparable
    {
        public int depd;
        public Node<T> Left { get; protected internal set; }
        public Node<T> Right { get; protected internal set; }
        public T Data { get; protected internal set; }
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

