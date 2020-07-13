using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel.Design.Serialization;

namespace Simple_Calculator
{
    public class Reader
    {
        string name;
        public Reader(string Name)
        {
            name = Name;
        }
        public string GetLine()
        {
            string path = Directory.GetCurrentDirectory();
            string fullpath = Path.Combine(path, name);
            StreamReader sr1 = new StreamReader(fullpath);
            string line = "";
            line = sr1.ReadLine();
            return line;
        }
    }
    public class Node
    {
        public string Data { get; set; }
        public Node LeftNode { get; set; }
        public Node RightNode { get; set; }
    }
    public class Sym
    {
        public string line { get; set; }
        public int i { get; set; }
        public char sym { get; set; }
        public Sym(char Sym)
        {
            sym = Sym;
        }
        public bool IsDigit()
        {
            switch (sym)
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    return true;
                default:
                    return false;
            }
        }
        public bool IsPlus()
        {
            if (sym == '+')
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsMultiply()
        {
            if (sym == '*')
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsDivide()
        {
            if (sym == '/')
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsMinus()
        {
            if (sym == '-')
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsEqual()
        {
            if (sym == '=')
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsBraceOpen()
        {
            /*if(sym== '(')
            {
                return true;
            }
            else
            {
                return false;
            }*/
            return sym == '(';
        }
        public bool IsBraceClose()
        {
            /*if (sym == ')')
            {
                return true;
            }
            else
            {
                return false;
            }*/
            return sym == ')';
        }
    }
    public class Parser
    {
        /*
        public Node Parse(string line)
        {           
            string state = "null";
            string acc = "";
            Node current = new Node();
            Node root = current;
            Node tmp;

            for (int i = 0; i < line.Length; i++)
            {
                Sym c = new Sym(line[i]);
                if (state == "null" && c.IsDigit())
                //if (state == "null" && c.isDigit())
                    {
                    state = "number";
                    acc = c.sym.ToString();
                    //acc = c.toString();
                    continue;
                }
                else
                if (state == "number" && c.IsDigit())
                {
                    acc = acc + c.sym.ToString();
                    continue;
                }
                else
                if (state == "number" && (c.IsPlus() || c.IsMinus()))
                {
                    state = "operation";
                    //current = root;
                    if (current.LeftNode == null)
                    //if (root.LeftNode == null)
                    {
                        current.LeftNode = new Node();
                        current.LeftNode.Data = acc;
                        current.Data = c.sym.ToString();
                    }
                    else
                    {
                        current.RightNode = new Node();
                        current.RightNode.Data = acc;
                        tmp = root;                 
                        current = new Node();
                        current.Data = c.sym.ToString();
                        current.LeftNode = tmp;
                        tmp = null;
                        root = current;
                    }
                    acc = "";
                    continue;
                }
                if (state == "number" && c.IsMultiply() || c.IsDivide())
                {
                    state = "operation";
                    if (current.LeftNode == null)
                    {
                        current.LeftNode = new Node();
                        current.LeftNode.Data = acc;
                        current.Data = c.sym.ToString();
                    }
                    else
                    {
                        //Console.WriteLine(acc);
                        //tmp = new Node();
                        tmp = current;
                        current = new Node();
                        current.Data = c.sym.ToString();
                        current.LeftNode = new Node();
                        current.LeftNode.Data = acc;
                        tmp.RightNode = current;

                        //Console.WriteLine(current.LeftNode.Data);
                        //Console.WriteLine(current.Data);

                       // tmp = current;
                       // current = new Node();
                       // current.LeftNode = new Node();
                        //current.LeftNode.Data = acc;
                        //current.Data = line[i].ToString();
                        //tmp.RightNode = current;
                        //current = tmp;
                        //tmp = null;
                        ////current = tmp;
                    }
                    acc = "";
                    continue;
                }
                if(state == "operation" && c.IsBraceOpen())
                {
                    state = "brace";
                    continue;
                }
                else
                if(state=="brace" && !c.IsBraceClose())
                {
                    acc = acc + c.sym;
                    continue;
                }
                else
                if(state=="brace" && c.IsBraceClose())
                {
                    Parser brace = new Parser();
                    if ( current.LeftNode==null)
                    {
                        current.LeftNode = brace.Parse(acc);
                    }
                    else
                    {
                        current.RightNode = brace.Parse(acc);
                    }
                    state = "afterbrace";
                    acc = "";
                    //tmp=brace.Parse(acc);
                    //brace.ShowNodes(brace.Parse(acc));
                    continue;
                }
                else
                if(state == "afterbrace" && (c.IsPlus() || c.IsMinus() || c.IsMultiply() || c.IsDivide()))
                {
                    state = "operation";
                    Node a = new Node();
                    a = current;
                    current = new Node();
                    current.LeftNode = new Node();
                    current.LeftNode = a;
                    current.Data = c.sym.ToString();
                    root = current;
                    a = null;
                }
                else
                if (state == "operation" && c.IsDigit())
                {
                    state = "number";
                    acc = c.sym.ToString();
                    continue;
                }               


            }
            
            //if(state== "number" && root.Data.Length>0)
            //{
                if(current.RightNode==null && current.LeftNode != null)
                {
                    current.RightNode = new Node();
                    current.RightNode.Data = acc;
                    //root.RightNode = new Node();
                    //root.RightNode = current;
                    
                    //if (root == null)
                    //{
                        root = current;
                   // }
                    //else
                    //{
                     //   root.RightNode = current;
                    //}
                //}
                //root.RightNode = new Node();
                //root.RightNode.Data = acc;
            //}
            return root;

        }
        */
        public Node Parse(string line)
        {
            string state = "null";
            string acc = "";
            Node current = new Node();
            Node root = current;
            Node tmp;

            for (int i = 0; i < line.Length; i++)
            {
                Sym c = new Sym(line[i]);
                if (state == "null" && c.IsDigit())
                {
                    state = "number";
                    acc = acc + c.sym.ToString();
                    continue;
                }
                else
                if (state == "operation" && c.IsDigit())
                {
                    state = "number";
                    acc = acc + c.sym.ToString();
                    // continue;
                }
                else
                if (state == "number" && c.IsDigit())
                {
                    acc = acc + c.sym.ToString();
                    continue;
                }
                else
                if (state == "number" && (c.IsPlus() || c.IsMinus()))
                {
                    state = "operation";
                    if (current.LeftNode == null)
                    {
                        current.LeftNode = new Node();
                        current.LeftNode.Data = acc;
                        current.Data = c.sym.ToString();
                        acc = "";
                    }
                    else
                    {
                        current.RightNode = new Node();
                        current.RightNode.Data = acc;
                        tmp = root;
                        current = new Node();
                        current.LeftNode = tmp;
                        current.Data = c.sym.ToString();
                        tmp = null;
                        root = current;
                        acc = "";
                    }
                    continue;
                }
                else
                if (state == "number" && (c.IsMultiply() || c.IsDivide()))
                {
                    state = "operation";
                    if (current.LeftNode == null)
                    {
                        current.LeftNode = new Node();
                        current.LeftNode.Data = acc;
                        current.Data = c.sym.ToString();
                        acc = "";
                    }
                    else
                    {
                        tmp = current;
                        current = new Node();
                        current.LeftNode = new Node();
                        current.LeftNode.Data = acc;
                        current.Data = c.sym.ToString();
                        tmp.RightNode = current;
                        //current = tmp;
                        tmp = null;
                        acc = "";
                    }
                    continue;
                }
                else
                if (state == "operation" && c.IsBraceOpen())
                {
                    state = "brace";
                    continue;
                }
                else
                if (state == "brace" && !c.IsBraceClose())
                {
                    acc = acc + c.sym.ToString();
                    continue;
                }
                else
                if(state == "brace" && c.IsBraceClose())
                {
                    Parser brace = new Parser();
                    acc = brace.Calculator(brace.Parse(acc));
                    /*if(current.LeftNode == null)
                    {
                        //current.LeftNode.Data=brace.Calculator(brace.Parse(acc));
                        //Console.WriteLine(brace.Calculator(brace.Parse(acc)));
                        acc = brace.Calculator(brace.Parse(acc));
                    }
                    else
                    {
                        //current.RightNode.Data = brace.Calculator(brace.Parse(acc));
                        //Console.WriteLine(brace.Calculator(brace.Parse(acc)));
                        brace.Calculator(brace.Parse(acc))
                    }*/
                    //acc = "";
                    state="number";
                    continue;
                }
                if (state == "number" && root.Data.Length > 0)
                {
                    if (current.RightNode == null && current.LeftNode != null)
                    {
                        //root.RightNode = new Node();
                        // root.RightNode = current;
                        /*if (root == null)
                        {
                            root = current;
                        }
                        else
                        {
                            root.RightNode = current;
                        }*/
                    }
                    current.RightNode = new Node();
                    current.RightNode.Data = acc;
                }
            }
            return root;
        }
        public void ShowNodes(Node root)
        {
            ShowNode(root);   
        }
        public void ShowNode(Node currentnode)
        {
            if(currentnode.LeftNode!=null)
            {
                ShowNode(currentnode.LeftNode);
            }
            Console.Write(currentnode.Data);
            if (currentnode.RightNode != null)
            {
                ShowNode(currentnode.RightNode);
            }
        }
        public string Calculator(Node root)
        {
            Calculate(root, root);
            return root.Data;
        }
        public void Calculate(Node currentnode, Node root)
        {
            float result=0;
            if (currentnode.LeftNode != null && currentnode.RightNode != null)
            {
                Sym a = new Sym(currentnode.LeftNode.Data.ToCharArray()[0]);
                Sym b = new Sym(currentnode.RightNode.Data.ToCharArray()[0]);
                Sym c = new Sym(currentnode.Data.ToCharArray()[0]);
                if (a.IsDigit() && b.IsDigit())
                {
                    if (c.IsPlus())
                    {
                        result = float.Parse(currentnode.LeftNode.Data) + float.Parse(currentnode.RightNode.Data);
                    }
                    else
                    if (c.IsMinus())
                    {
                        result = float.Parse(currentnode.LeftNode.Data) - float.Parse(currentnode.RightNode.Data);
                    }
                    else
                    if (c.IsMultiply())
                    {
                        result = float.Parse(currentnode.LeftNode.Data) * float.Parse(currentnode.RightNode.Data);
                    }
                    else
                    if (c.IsDivide())
                    {
                        result = float.Parse(currentnode.LeftNode.Data) / float.Parse(currentnode.RightNode.Data);
                    }
                    //currentnode = null;
                    //currentnode = new Node();
                    currentnode.Data = result.ToString();
                    currentnode.LeftNode = null;
                    currentnode.RightNode = null;
                    Calculate(root, root);
                }
                else
                {
                    if (currentnode.LeftNode != null)
                    {
                        Calculate(currentnode.LeftNode, root);
                    }
                    //else
                    if(currentnode.RightNode != null)
                    {
                        Calculate(currentnode.RightNode, root);
                    }
                    //else
                    //if(currentnode.LeftNode == null && currentnode.RightNode == null)
                    //{
                        //Console.WriteLine(root.Data);
                    //}
                }
            }
        }
    }
    
            class Program
            {
                static void Main(string[] args)
                {
                    Parser test = new Parser();
                    Reader red = new Reader("To calculate.txt");
                    string line = red.GetLine();
                    test.ShowNodes(test.Parse(line));
                    Console.ReadKey();
                }
            }
        }
