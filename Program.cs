namespace Egen_länkad_lista_lab
{
    /// <summary>
    /// Oscar Gillberg
    /// BUV23
    /// Uppgift egen länkad lista
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            
            DubbelLinkedList<int> list = new DubbelLinkedList<int>();
            list.AddLast(1); 
            list.AddLast(2); 
            list.AddLast(3); 
            list.AddLast(4);
            list.AddLast(7);
            list.AddLast(6);
            list.AddLast(9);
            list.AddLast(8);

            Console.WriteLine($"Count: {list.Count}");

            Console.WriteLine("---------------");

            list.PrintFromHead();

            //Console.WriteLine("---------------");

            //list.PrintFromTail();
            
            Console.WriteLine("---------------");
            
            list.RemoveLast();

            list.PrintFromHead();

            //Console.WriteLine("---------------");

            list.RemoveFirst();

            //list.PrintFromTail();

            Console.WriteLine("---------------");

            Console.WriteLine(list[1]);

            Console.WriteLine("---------------");

            list[1] = 10;
            Console.WriteLine(list[1]);

            Console.WriteLine("---------------");

            foreach (var item in list)
            {
                Console.WriteLine(item);

            }

            list.SortList(ascending: true);

            Console.WriteLine("---------------");

            foreach (var item in list)
            {
                Console.WriteLine(item);

            }

            Console.ReadLine();
        }
    }
}
