using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThePlayGroundTester.BinaryTree
{
    public class Tree
    {
        private int _Count { get; set; }
        public int Count
        {
            get
            {
                return _Count;
            }
        }

        public Node Root { get; private set; }

        public void AddValue(int value)
        {
            if (Root == null)
            {
                Root = new Node(value);
                _Count++;
            }
            else
            {
                Root.AddNode(value);
                _Count++;
            }
        }

        public void Traverse()
        {
            if (Root != null)
            {
                Root.Visit();
            }
        }

        public Node Search(int valueToFind)
        {
            return Root.Search(valueToFind);
        }
    }
}
