using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Egen_länkad_lista_lab
{
    /// <summary>
    /// Oscar Gillberg
    /// BUV23
    /// Uppgift egen länkad lista
    /// </summary>
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

        // denna metoden används för att lägga till en ny nod framför en befintlig nod
        public void AddBefore(Node<T> node, T data)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            Node<T> newNode = new Node<T>(data);
            if (node == head)
            {
                AddFirst(data);
            }
            else
            {
                newNode.Next = node;
                newNode.previous = node.previous;
                node.previous.Next = newNode;
                node.previous = newNode;
                count++;
            }
        }

        // denna metoden används för att lägga till en ny nod efter en befintlig nod
        public void AddAfter(Node<T> node, T data)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            Node<T> newNode = new Node<T>(data);
            if (node == tail)
            {
                AddLast(data);
            }
            else
            {
                newNode.Next = node.Next;
                newNode.previous = node;
                node.Next.previous = newNode;
                node.Next = newNode;
                count++;
            }
        }

        public bool Remove(T data)
        {
            Node<T> current = head;
            while (current != null)
            {
                if (current.Data.Equals(data))
                {
                    if (current == head)
                    {
                        RemoveFirst();
                    }
                    else if (current == tail)
                    {
                        RemoveLast();
                    }
                    else
                    {
                        current.previous.Next = current.Next;
                        current.Next.previous = current.previous;
                        count--;
                    }
                    return true; // Returnar true om noden hittades och togs bort
                }
                current = current.Next;
            }
            return false; // Returnar false om noden inte hittades
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

                Node<T> current;
                if (index < count / 2) // om index är i den första halvan, börja från head
                {
                    current = head;
                    for (int i = 0; i < index; i++)
                    {
                        current = current.Next;
                    }
                }
                else // om index är i andra halvan, börja från tail och gå bakåt
                {
                    current = tail;
                    for (int i = count - 1; i > index; i--)
                    {
                        current = current.previous;
                    }
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
                Node<T> current;
                if (index < count / 2)
                {
                    current = head;
                    for (int i = 0; i < index; i++)
                    {
                        current = current.Next;
                    }
                    
                }
                else
                {
                    current = tail;
                    for (int i = count - 1; i > index; i--)
                    {
                        current = current.previous;
                    }
                   
                }
                current.Data = value;
            }
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

                    int nodesRemoved = 0;
                    Node<T> temp = current.Next;
                    while (temp != null)
                    {
                        nodesRemoved++;
                        temp = temp.Next;
                    }
                    tail = current;
                    current.Next = null;
                    count -= nodesRemoved;
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
        // Snabbare med Merge sort istället för Quick sort eftersom det är en dubbel länkad lista
        public void SortList(bool ascending = true)
        {
            if (head == null || head.Next == null)
            {
                return;
            }

            // Använd MergeSort för att sortera listan
            head = MergeSort(head, ascending);

            // Efter sorteringen uppdaterar vi tail genom att gå från head till slutet av listan
            Node<T> current = head;
            while (current.Next != null)
            {
                current = current.Next;
            }
            tail = current;

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
                    if(result.Next != null) result.Next.previous = result;
                }
                else
                {
                    result = right;
                    result.Next = Merge(left, right.Next, ascending);
                    if (result.Next != null) result.Next.previous = result;
                    
                        
                  
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
                    if (result.Next != null) result.Next.previous = result;
                }
                else
                {
                    result = right;
                    result.Next = Merge(left, right.Next, ascending);
                    if (result.Next != null) result.Next.previous = result;
                }
            }
            return result;
        }

        // Returnerar en nod med data som matchar det angivna värdet
        public Node<T> Find(T data)
        {
            Node<T> current = head;
            while (current != null)
            {
                if (current.Data.Equals(data))
                {
                    return current;
                }
                current = current.Next;
            }
            return null;
        }

        // returnerar true om listan innehåller en nod med data som matchar det angivna värdet
        public bool Contains(T data)
        {
            return Find(data) != null;
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
