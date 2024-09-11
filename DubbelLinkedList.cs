using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egen_länkad_lista_lab
{
    public class DubbelLinkedList<T> : IEnumerable<T>
    {
        // detta fält används för att hålla reda på första noden i listan
        private Node<T> head;
        // detta fält används för att hålla reda på sista noden i listan
        private Node<T> tail;
        // detta fält används för att hålla reda på antalet noder i listan
        private int count;

        // denna konstruktor används för att initialisera fälten
        public DubbelLinkedList()
        {
            head = null;
            tail = null;
            count = 0;
        }

        // Denna property är för att räkna antalet noder i listan
        public int Count
        {
            get { return count; }
        }

        // this method is used to add a new node to the list at the beginning
        public void AddFirst(T data)
        {
            Node<T> newNode = new Node<T>(data);
            if (head == null)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                newNode.Next = head;
                head.previous = newNode;
                head = newNode;
            }
            count++;
        }

        // Denna methoden är för att lägga till en ny nod i listan i slutet
        public void AddLast(T data)
        {
            Node<T> newNode = new Node<T>(data);
            if (head == null)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                tail.Next = newNode;
                newNode.previous = tail;
                tail = newNode;
            }
            count++;
        }

        // den här metoden är för att ta bort första noden
        public void RemoveFirst()
        {
            if (head == null)
            {
                throw new InvalidOperationException("List is empty");
            }
            if (head == tail)
            {
                head = null;
                tail = null;
            }
            else
            {
                head = head.Next;
                head.previous = null;
            }
            count--;
        }

        // den här methoden är för att ta bort sista noden i listan
        public void RemoveLast()
        {
            if (head == null)
            {
                throw new InvalidOperationException("List is empty");
            }
            if (head == tail)
            {
                head = null;
                tail = null;
            }
            else
            {
                tail = tail.previous;
                tail.Next = null;
            }
            count--;
        }

        // Denna metoden är för att skriva ut listan från Head
        public void PrintFromHead()
        {
            Node<T> current = head;
            while (current != null)
            {
                Console.WriteLine(current.Data);
                current = current.Next;
            }
        }

        // Denna metoden är för att skriva ut listan från Tail
        public void PrintFromTail()
        {
            Node<T> current = tail;
            while (current != null)
            {
                Console.WriteLine(current.Data);
                current = current.previous;
            }
        }

        public T this[int index]
        {
            // denna indexeringsmetod används för att hämta data i en nod
            get
            {
                if (index < 0 || index >= count)
                {
                    throw new IndexOutOfRangeException("Index out of range");
                }
                Node<T> current = head;
                for (int i = 0; i < index; i++)
                {
                    current = current.Next;
                }
                return current.Data;
            }
            // denna indexeringsmetod används för att ändra data i en nod
            set
            {
                if (index < 0 || index >= count)
                {
                    throw new IndexOutOfRangeException("Index out of range");
                }
                Node<T> current = head;
                for (int i = 0; i < index; i++)
                {
                    current = current.Next;
                }
                current.Data = value;
            }
        }

        // denna metoden används för att lägga till en ny nod framför en befintlig nod
        public void AddBeforeElement(T existingElement, T newElement) 
        {
            Node<T> newNode = new Node<T>(newElement);
            Node<T> current = head;
            while (current != null)
            {
                if (current.Data.Equals(existingElement))
                {
                    if (current == head)
                    {
                        newNode.Next = head;
                        head.previous = newNode;
                        head = newNode;
                    }
                    else
                    {
                        newNode.Next = current;
                        newNode.previous = current.previous;
                        current.previous.Next = newNode;
                        current.previous = newNode;
                    }
                    count++;
                    return;
                }
                current = current.Next;
            }
            throw new InvalidOperationException("Element not found");
        }

        // denna metoden används för att ta bort en nod efter en befintlig nod
        public void RemoveAfterElement(T existingElement)
        {
            Node<T> current = head;
            while (current != null)
            {
                if (current.Data.Equals(existingElement))
                {
                    if (current.Next == null)
                    {
                        throw new InvalidOperationException("No element after the existing element");
                    }
                    if (current.Next == tail)
                    {
                        tail = current;
                        current.Next = null;
                    }
                    else
                    {
                        current.Next = current.Next.Next;
                        current.Next.previous = current;
                    }
                    count--;
                    return;
                }
                current = current.Next;
            }
            throw new InvalidOperationException("Element not found");
        }

        // denna metoden används för att ta bort alla noder efter en befintlig nod
        public void RemoveAllAfterElement(T existingElement)
        {
            Node<T> current = head;
            while (current != null)
            {
                if (current.Data.Equals(existingElement))
                {
                    if (current.Next == null)
                    {
                        throw new InvalidOperationException("No element after the existing element");
                    }
                    tail = current;
                    current.Next = null;
                    count--;
                    return;
                }
                current = current.Next;
            }
            throw new InvalidOperationException("Element not found");
        }

        // Denna metoden används för att returnera värderna i en array
        public T[] ToArray()
        {
            T[] array = new T[count];
            Node<T> current = head;
            for (int i = 0; i < count; i++)
            {
                array[i] = current.Data;
                current = current.Next;
            }
            return array;
        }

        // denna metoden används för att returnera en klonad array
        public T[] ToClonedArray()
        {
            T[] array = ToArray();
            return (T[])array.Clone();
        }


        // Sorterar listan med hjälp av Merge Sort-algoritmen
        //Snabbare med Merge sort istället för Quick sort eftersom det är en dubbel länkad lista
        public void SortList(bool ascending = true)
        {
            if (head == null || head.Next == null)
            {
                return;
            }


            head = MergeSort(head, ascending);

            Node<T> current = head;
            while (current.Next != null)
            {
                current = current.Next;
            }
            tail = current;


            //// gamla sättet att sortera listan
            
            //T[] array = ToArray();
            //Array.Sort(array);
            //Node<T> current = head;
            //for (int i = 0; i < count; i++)
            //{
            //    current.Data = array[i];
            //    current = current.Next;
            //}
        }


        private Node<T> MergeSort(Node<T> headNode, bool ascending)
        {
            // Om den bara har en nod eller ingen nod alls, return.
            if (headNode == null || headNode.Next == null)
            {
                return headNode;
            }

            // Dela listan i två delar.
            Node<T> middle = GetMiddle(headNode);
            Node<T> nextOfMiddle = middle.Next;

            // bryt listan i två delar.
            middle.Next = null;

            // sortera vänstra och högra halvorna rekursivt.
            Node<T> left = MergeSort(headNode, ascending);
            Node<T> right = MergeSort(nextOfMiddle, ascending);

            // Sortera och slå ihop de två sorterna listorna. sedan returnera den sorterade listan.
            Node<T> sortedList = Merge(left, right, ascending);
            return sortedList;
        }

        // Hjälpmetod för att hitta mitten av listan
        private Node<T> GetMiddle(Node<T> headNode)
        {
            if (headNode == null)
            {
                return headNode;
            }
            Node<T> slow = headNode; // förflyttare med hastighet 1
            Node<T> fast = headNode; // förflyttare med hastighet 2

            // Använder två pekare (slow och fast) för att hitta mitten av listan.
            while (fast.Next != null && fast.Next.Next != null)
            {
                slow = slow.Next;
                fast = fast.Next.Next;
            }
            return slow;
        }

        // Hjälpmetod för att slå ihop två sorterade listor
        private Node<T> Merge(Node<T> left, Node<T> right, bool ascending)
        {
            Node<T> result = null;
            if (left == null)
            {
                return right;
            }
            if (right == null)
            {
                return left;
            }
            if (ascending)
            {
                // Om left.Data är mindre än eller lika med right.Data,
                // sätt left som head och slå ihop left.Next med right.
                if (Comparer<T>.Default.Compare(left.Data, right.Data) <= 0)
                {
                    result = left;
                    result.Next = Merge(left.Next, right, ascending);
                }
                else
                {
                    result = right;
                    result.Next = Merge(left, right.Next, ascending);
                }
            }
            else
            {
                // Om left.Data är större än eller lika med right.Data, 
                // sätt right som head och slå ihop left med right.Next.
                if (Comparer<T>.Default.Compare(left.Data, right.Data) >= 0)
                {
                    result = left;
                    result.Next = Merge(left.Next, right, ascending);
                }
                else
                {
                    result = right;
                    result.Next = Merge(left, right.Next, ascending);
                }
            }
            return result;
        }

        // denna metoden används för att returnera en IEnumerator och använda foreach-loop
        public IEnumerator<T> GetEnumerator()
        {
            Node<T> current = head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
