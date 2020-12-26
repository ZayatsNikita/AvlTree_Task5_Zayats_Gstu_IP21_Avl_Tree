using System;

namespace AvlTree
{

    public class Tree<T> where T: IComparable
    {
        private Node<T> root;
        public void Add(T data)
        {
            Node<T> newItem = new Node<T>(data);

            root = RecursiveInsert(root, newItem);
        }
        private Node<T> RecursiveInsert(Node<T> current, Node<T> node)
        {
            if (current == null)
            {
                current = node;
            }
            else
            {
                int compareRes = node.Data.CompareTo(current.Data);
                if (compareRes > 0)
                {
                    current.Right = RecursiveInsert(current.Right, node);
                }
                else
                {
                    if(compareRes < 0)
                    {
                        current.Left = RecursiveInsert(current.Left, node);
                    }
                }
                current = balance_tree(current);
            }
            return current;
        }
        private Node<T> balance_tree(Node<T> current)
        {
            int b_factor = balance_factor(current);
            if (b_factor > 1)
            {
                if (balance_factor(current.Left) > 0)
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
                if (balance_factor(current.Right) > 0)
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
        public void Delete(T target)
        {//and here
            root = Delete(root, target);
        }
        private Node<T> Delete(Node<T> current, T target)
        {
            Node<T> parent;
            if (current == null)
            {
                return null;
            }
            else
            {
                int compareRes = target.CompareTo(current.Data);
                //left subtree
                if (compareRes < 0)
                {
                    current.Left = Delete(current.Left, target);
                    if (balance_factor(current) == -2)//here
                    {
                        if (balance_factor(current.Right) <= 0)
                        {
                            current = RotateRR(current);
                        }
                        else
                        {
                            current = RotateRL(current);
                        }
                    }
                }
                //right subtree
                else if (compareRes > 0)
                {
                    current.Right = Delete(current.Right, target);
                    if (balance_factor(current) == 2)
                    {
                        if (balance_factor(current.Left) >= 0)
                        {
                            current = RotateLL(current);
                        }
                        else
                        {
                            current = RotateLR(current);
                        }
                    }
                }
                //if target is found
                else
                {
                    if (current.Right != null)
                    {
                        //delete its inorder successor
                        parent = current.Right;
                        while (parent.Left != null)
                        {
                            parent = parent.Left;
                        }
                        current.Data = parent.Data;
                        current.Right = Delete(current.Right, parent.Data);
                        if (balance_factor(current) == 2)//rebalancing
                        {
                            if (balance_factor(current.Left) >= 0)
                            {
                                current = RotateLL(current);
                            }
                            else { current = RotateLR(current); }
                        }
                    }
                    else
                    {   //if current.left != null
                        return current.Left;
                    }
                }
            }
            return current;
        }
        public void Find(T key)
        {
            if (Find(key, root).Data.Equals(key))
            {
                Console.WriteLine("{0} was found!", key);
            }
            else
            {
                Console.WriteLine("Nothing found!");
            }
        }
        private Node<T> Find(T target, Node<T> current)
        {
            int compareRes = target.CompareTo(current.Data);
            if (compareRes < 0)
            {
                if (target.Equals(current.Data))
                {
                    return current;
                }
                else
                    return Find(target, current.Left);
            }
            else
            {
                if (target.Equals(current.Data))
                {
                    return current;
                }
                else
                    return Find(target, current.Right);
            }

        }
        public void DisplayTree()
        {
            if (root == null)
            {
                Console.WriteLine("Tree is empty");
                return;
            }
            InOrderDisplayTree(root);
            Console.WriteLine();
        }
        private void InOrderDisplayTree(Node<T> current)
        {
            if (current != null)
            {
                InOrderDisplayTree(current.Left);
                Console.Write("({0}) ", current.Data);
                InOrderDisplayTree(current.Right);
            }
        }
        private int max(int l, int r)
        {
            return l > r ? l : r;
        }
        private int getHeight(Node<T> current)
        {
            int height = 0;
            if (current != null)
            {
                int l = getHeight(current.Left);
                int r = getHeight(current.Right);
                int m = max(l, r);
                height = m + 1;
            }
            return height;
        }
        private int balance_factor(Node<T> current)
        {
            int l = getHeight(current.Left);
            int r = getHeight(current.Right);
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
    }
}
