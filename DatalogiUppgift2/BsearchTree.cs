using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatalogiUppgift2
{
    public class BsearchTree
    {

            // Class containing left and right of the current node and key value
            public class Node
            {
                public int key;
                public Node left, right;

                public Node(int item)
                {
                    key = item;
                    left = right = null;
                }
            }

            // Root of search tree
            public Node root;

            // Constructor
            public BsearchTree() { root = null; }

            public BsearchTree(int value) { root = new Node(value); }

            // Method calls insertRec()
            public void insert(int key) { root = insertRec(root, key); }

            // A recursive function to insert new key in search tree
            private Node insertRec(Node root, int key)
            {

                // If tree is empty, return a new node
                if (root == null)
                {
                    root = new Node(key);
                    return root;
                }

                // Else, checks value and adds to tree
                if (key < root.key)
                    root.left = insertRec(root.left, key);
                else if (key > root.key)
                    root.right = insertRec(root.right, key);

                // Return the (unchanged) root
                return root;
            }

        }
    }
