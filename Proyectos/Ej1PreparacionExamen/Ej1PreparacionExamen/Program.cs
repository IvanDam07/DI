using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ej1PreparacionExamen
{
    internal class Program
    {
        class Producto
        {
            public int Codigo { get; set; }
            public string Nombre { get; set; }
            public decimal Precio { get; set; }
            public int CantidadEnStock { get; set; }

            public Producto(int codigo, string nombre, decimal precio, int cantidad)
            {
                Codigo = codigo;
                Nombre = nombre;
                Precio = precio;
                CantidadEnStock = cantidad;
            }

            public override string ToString()
            {
                return $"Código: {Codigo}, Nombre: {Nombre}, Precio: {Precio}, Stock: {CantidadEnStock}";
            }
        }
        static void Main(string[] args)
        {
            List<Producto> inventario = new List<Producto>();
            int opcion;

            do
            {
                Console.Clear();
                Console.WriteLine("Gestión de Inventario de Tienda");
                Console.WriteLine("1. Agregar producto");
                Console.WriteLine("2. Buscar producto por código");
                Console.WriteLine("3. Actualizar stock de producto");
                Console.WriteLine("4. Mostrar productos con stock menor a un umbral");
                Console.WriteLine("5. Salir");
                Console.Write("Elige una opción: ");
                opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        AgregarProducto(inventario);
                        break;
                    case 2:
                        BuscarProducto(inventario);
                        break;
                    case 3:
                        ActualizarStock(inventario);
                        break;
                    case 4:
                        MostrarProductosConBajoStock(inventario);
                        break;
                    case 5:
                        Console.WriteLine("Saliendo del programa...");
                        break;
                    default:
                        Console.WriteLine("Opción inválida. Prueba de nuevo.");
                        break;
                }

                if (opcion != 5)
                {
                    Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            } while (opcion != 5);
        }

        static void AgregarProducto(List<Producto> inventario)
        {
            Console.Clear();
            Console.WriteLine("Agregar nuevo producto");

            Console.Write("Código: ");
            int codigo = int.Parse(Console.ReadLine());

            //Verificar si existe el código ya
            if (inventario.Exists(p => p.Codigo == codigo))
            {
                Console.WriteLine("Error: Ya existe un producto con ese código.");
                return;
            }

            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();

            Console.Write("Precio: ");
            decimal precio = decimal.Parse(Console.ReadLine());

            Console.Write("Cantidad: ");
            int cantidad = int.Parse(Console.ReadLine());

            inventario.Add(new Producto(codigo,nombre,precio,cantidad));
            Console.WriteLine("Producto agregado con éxito.");
        }

        static void BuscarProducto(List<Producto> inventario)
        {
            Console.Clear();
            Console.WriteLine("Buscar producto por código.");

            Console.Write("Ingresa el código del producto a buscar: ");
            int codigo = int.Parse(Console.ReadLine());

            Producto producto = inventario.Find(p  => p.Codigo == codigo);
            if (producto != null)
            {
                Console.WriteLine("Producto encontrado: ");
                Console.WriteLine(producto);
            } else
            {
                Console.WriteLine("No se encontró ningún producto por ese código.");
            }
        }

        static void ActualizarStock(List<Producto> inventario)
        {
            Console.Clear();
            Console.WriteLine("Actualizar stock de un producto.");

            Console.Write("Introduce el código del producto: ");
            int codigo = int.Parse(Console.ReadLine());

            Producto producto = inventario.Find(p => p.Codigo ==codigo);
            if (producto != null)
            {
                Console.WriteLine($"Producto encontrado: {producto}");
                Console.Write("Introduce la nueva cantidad en stock: ");
                int nuevaCantidad = int.Parse(Console.ReadLine());

                producto.CantidadEnStock = nuevaCantidad;
                Console.WriteLine("Stock actualizado correctamente.");
            }
            else
            {
                Console.WriteLine("No se encontró ningún producto con ese código.");
            }
        }

        static void MostrarProductosConBajoStock(List<Producto> inventario)
        {
            Console.Clear();
            Console.WriteLine("Mostrar productos con stock menor a un umbral");

            Console.Write("Introduce el umbral de stock: ");
            int umbral = int.Parse(Console.ReadLine());

            List<Producto> productoBajoStock = inventario.FindAll(p => p.CantidadEnStock < umbral);

            if (productoBajoStock.Count > 0)
            {
                Console.WriteLine($"Productos con stock por debajo del umbral({umbral}): ");
                foreach(Producto producto in productoBajoStock)
                {
                    Console.WriteLine(producto);
                }
            }
            else
            {
                Console.WriteLine($"No hay productos con stock por debajo del umbral({umbral}).");
            }
        }
    }
}
