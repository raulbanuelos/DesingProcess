using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<Negocio> lista = new List<Negocio>();

            lista.Add(new Negocio { Id = 6, Nombre = "Edgar" });
            lista.Add(new Negocio { Id = 5, Nombre = "Raúl" });
            lista.Add(new Negocio { Id = 3, Nombre = "José" });
            lista.Add(new Negocio { Id = 4, Nombre = "Ramón" });
            lista.Add(new Negocio { Id = 2, Nombre = "Rubén" });
            lista.Add(new Negocio { Id = 1, Nombre = "Artúro" });
            Console.WriteLine("Lista no ordenada");
            foreach (Negocio element in lista)
            {
                Console.WriteLine("ID: {0} ,Nombre: {1}", element.Id, element.Nombre);
            }

            lista = lista.OrderBy(x => x.Nombre).ToList();

            Console.WriteLine("Lista ordenada");
            foreach (Negocio element in lista)
            {
                Console.WriteLine("ID: {0} ,Nombre: {1}", element.Id, element.Nombre);
            }
            Console.ReadKey();
        }
        
    }
}
