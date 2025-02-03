using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjerciciosBasicos
{
    internal class Matrix
    {
        private double[,] data;
        public int Filas => data.GetLength(0);
        public int Columnas => data.GetLength(1);

        public Matrix(int filas, int columnas)
        {
            data = new double[filas, columnas];
        }

        public double this[int fila, int columna]
        {
            get => data[fila, columna];
            set => data[fila, columna] = value;
        }

        public static Matrix Sumar(Matrix a, Matrix b)
        {
            if (a.Filas != b.Filas || a.Columnas != b.Columnas)
                throw new InvalidOperationException("Las dimensiones de las matrices no coinciden.");

            Matrix resultado = new Matrix(a.Filas, a.Columnas);
            for (int i = 0; i < a.Filas; i++)
                for (int j = 0; j < a.Columnas; j++)
                    resultado[i, j] = a[i, j] + b[i, j];

            return resultado;
        }

        public static Matrix Multiplicar(Matrix a, Matrix b)
        {
            if (a.Columnas != b.Filas)
                throw new InvalidOperationException("El número de columnas de A debe ser igual al número de filas de B.");

            Matrix resultado = new Matrix(a.Filas, b.Columnas);
            for (int i = 0; i < a.Filas; i++)
                for (int j = 0; j < b.Columnas; j++)
                    for (int k = 0; k < a.Columnas; k++)
                        resultado[i, j] += a[i, k] * b[k, j];

            return resultado;
        }
    }
}
