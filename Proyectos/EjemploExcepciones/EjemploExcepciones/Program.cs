class Program
{
    static void Main(string[] args)
    {
        double[] resultados = new double[10];

        for (int i = 0; i < 10; i++)
        {

            Console.WriteLine($"División {i + 1}");

            try
            {
                
                Console.WriteLine("Ingresa el numerador: ");
                int numerador = Convert.ToInt32(Console.ReadLine());

                int denominador;
                Console.WriteLine("Ingresa el denominador: ");
                denominador = Convert.ToInt32(Console.ReadLine());

                resultados[i] = numerador / denominador;

            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine("No se puede dividir entre 0. " + e.Message);
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("El índice está fuera de los límites del array. " + e.Message);
            }
            finally
            {
                Console.WriteLine("Programa finalizado");
            }

        }
    }
}