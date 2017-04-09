using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThePlayGroundTester.BinaryTree
{
    public class Node
    {
        private static int LastId = 0;
        public int NodeID { get; private set; }

        public int Value { get; private set; }
        public Node Left { get; private set; }
        public Node Right { get; private set; }

        public int Duplicates { get; private set; }

        public Node(int value)
        {
            Value = value;
            NodeID = LastId + 1;
            LastId++;
        }

        public void AddNode(int value)
        {
            if (value == Value)
            {
                Duplicates++;
                return;
            }
            if (value < Value)
            {
                if (Left != null)
                {
                    Left.AddNode(value);
                }
                else
                {
                    Left = new Node(value);
                }
            }
            else if (value > Value)
            {
                if (Right != null)
                {
                    Right.AddNode(value);
                }
                else
                {
                    Right = new Node(value);
                }
            }
        }

        public void Visit()
        {
            //Console.WriteLine("Visiting node: " + NodeID);
            //System.Threading.Thread.Sleep(1000);
            if (Left != null)
            {
                Left.Visit();
            }
            Console.WriteLine("Node ID: " + NodeID + " has value of: " + Value);
            if (Right != null)
            {
                Right.Visit();
            }
        }

        public Node Search(int valueToFind)
        {
            Console.WriteLine("Searching in Node " + NodeID + ", this node has value: " + Value);
            //System.Threading.Thread.Sleep(1);
            //If by magic, this Nodes value is what we're looking for, return the node.
            if (Value == valueToFind)
            {
                return this;
            }

            //if the value is less, we go left. 
            if (valueToFind < Value)
            {
                if (Left != null)
                {
                    return Left.Search(valueToFind);
                }
            }
            //If its more, we go right.
            else if (valueToFind > Value)
            {
                if (Right != null)
                {
                    return Right.Search(valueToFind);
                }
            }

            //If we get this far, we know we diddn't find it, return null.
            return null;
        }
    }
}
