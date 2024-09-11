using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egen_länkad_lista_lab
{
    public class Node<T>
    {
        // Denna property används för att skapa en nod
        public T Data { get; set; }
        // Denna property används för att hålla reda på nästa nod
        public Node<T> Next { get; set; }
        // Denna property används för att hålla reda på föregående nod
        public Node<T> previous { get; set; }

        // Denna konstruktor används för att initialisera fälten
        public Node(T data)
        {
            Data = data;
            Next = null;
            previous = null;
        }
    }
}
