using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
using System.Collections.Generic;

public class Node
{
    public int Data;
    public Node Next;
    public Node(int data) { Data = data; Next = null; }
}



public class LinkedList
    { private Node head;
        public int? RemoveFromEnd()// 0(n)
        {
            if (head == null)
            {
            return null;





            }
            if (head.Next == null) { 
                int value = head.Data;
                head = null;
                return value;
             
        }
            Node soucasny = head;
            while (soucasny.Next.Next != null) {
                    soucasny = soucasny.Next;
            



            }
            int removedValue = soucasny.Next.Data;
            soucasny.Next = null; // poslední prvek už není odkazován -> bude GC
            return removedValue;
        }
    public bool Exist (int value)//0(n)
    {
        if (head == null)
        {
            return false;





        }
        Node soucasny = head;
        while ((soucasny.Next != null))
        {
            if (soucasny.Data == value)
                {
            return true;
            }
            
    }
        return false;
}
    public void RemoveAll(int value)//O(n)
    {

        while (head.Next != null && head.Data ==value) {
        {
         head = head.Next;   

        }
            if (head == null)
                return;
            Node soucasny = head;
        while (soucasny.Next != null)
        {
            soucasny = soucasny.Next;
                if (soucasny.Next.Data == value)
                {

                    soucasny.Next = soucasny.Next.Next;
                }
                else
                {
                   
                    soucasny = soucasny.Next;
                }



            }
        
    }
    }
    public static LinkedList Intersection(LinkedList a, LinkedList b) //0(n²)
    {
        LinkedList result = new LinkedList();
        Node soucasnaA = a.head;
        Node posledni = null;

        while (soucasnaA != null)
        {

            Node soucasnaB = b.head;
            bool found = false;
            while (soucasnaB != null)
            {
                if (soucasnaB.Data == soucasnaA.Data) { found = true; break; }
                soucasnaB = soucasnaB.Next;
            }

            
            if (found && !Exists(result.head, soucasnaA.Data))
            {
                if (result.head == null) result.head = soucasnaA;
                else posledni.Next = soucasnaA;
                posledni = soucasnaA;
            }

            soucasnaA = soucasnaA.Next;
        }

        if (posledni != null) posledni.Next = null; 
        return result;
    }
    private static bool Exists(Node head, int value)//O(n)
    {
        Node  opera= head;
        while (opera != null)
        {
            if (opera.Data == value) return true;
            opera = opera.Next;
        }
        return false;
    }
    public static LinkedList sjednoceni(LinkedList list1, LinkedList list2)//0(n)
    {
        if (list1.head == null) return list2; 
        if (list2.head == null) return list1; 

        Node soucasna = list1.head;

        
        while (soucasna.Next != null)
        {
            soucasna = soucasna.Next;
        }

        
        soucasna.Next = list2.head;

        return list1; 
    }
    public void RemoveDuplicates()//0(n)
    {
        if (head == null) return;
        Dictionary<int, int> count = new Dictionary<int, int>();
        Node soucasny = head;
        while (soucasny != null)
        {
            if (count.ContainsKey(soucasny.Data))
                count[soucasny.Data]++;
            else
                count[soucasny.Data] = 1;

            soucasny = soucasny.Next;
        }


        while (head != null && count[head.Data] > 1)
        {
            head = head.Next;
        }

        if (head == null) return;

        Node node = head;
        while (node.Next != null)
        {
            if (count[node.Next.Data] > 1)
            {
                node.Next = node.Next.Next; 
            }
            else
            {
                node = node.Next;
            }
        }
    }
    public static LinkedList Union(LinkedList list1, LinkedList list2)// 0(n)
    {

        LinkedList sjednoceny = LinkedList.sjednoceni(list1, list2);


        sjednoceny.RemoveDuplicates();


        return sjednoceny;
    }
}




