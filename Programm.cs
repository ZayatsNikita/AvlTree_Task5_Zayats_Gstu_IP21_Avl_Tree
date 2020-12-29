using System;
using System.Collections.Generic;
using System.Text;

namespace BalancingTrees
{
    class Programm
    {
        public static void Main()
        {
            AvlTree<int> tree = new AvlTree<int>();
            for (int i = 0; i < 20; i++)
            {
                tree.Add(i);
            }
            Console.Write(tree);
            Console.ReadLine();
        }
    }
}
