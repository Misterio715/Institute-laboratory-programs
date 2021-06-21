using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Practice_Task_2
{
    public class Matrix
    {
        //поле со значением массива объкта класса
        public double[,] matrix { get; set; }
        private int N { get; set; }
        //конструктор
        public Matrix(double[,] matrix)
        {
            if (matrix.GetLength(0) == matrix.GetLength(1))
            {
                this.matrix = matrix;
                N = matrix.GetLength(0);
            }
        }
        //матрица со случайными значениями
        public static double[,] RandomMatrix()
        {
            double[,] m = new double[4, 4];
            Random r = new Random();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    m[i, j] = r.Next(0, 51);
                }
            }
            return m;
        }
        private Action<double[,]> Show = m =>
        {
            for (int i = 0; i < m.GetLength(0); i++)
            {
                for (int j = 0; j < m.GetLength(1); j++)
                {
                    Console.Write($"{Math.Round(m[i,j], 1),-6}");
                }
                Console.WriteLine();
            }
            Console.WriteLine("---------------------------");
        };
        //вспомогательный метод складывающий строки для нахождения определителя 
        private void LineAdd(double[,] m, int i)
        {
            if (m[0, i] == 0)
            {
                for (int t = 1; t < N; t++)
                {
                    if (m[t, i] != 0)
                    {
                        for (int p = i; p < N; p++)
                        {
                            m[t - 1, p] += m[t, p];
                        }
                        break;
                    }
                }
                LineAdd(m, i);
            }
            else
                return;
        }
        //определитель
        public double Determinant
        {
            get
            {
                double[,] m = new double[N, N];
                double d = 1;
                Array.Copy(matrix, m, N*N);
                for (int i = 0; i < N - 1; i++)
                {
                    LineAdd(m, i);
                    //Show(m);
                    for (int j = N - 1; j > i; j--)
                    {
                        int r = 1;
                        while (m[j - r, i] == 0)
                            r++;
                        //double q = m[j, i] / m[j - r, i];
                        double q = m[j, i];
                        d *= m[j - r, i];
                        for (int k = i; k < N; k++)
                        {
                            m[j, k] = m[j, k] * m[j - r, i] - m[j - r, k] * q;
                            //m[j, k] = m[j, k] - m[j - r, k] * q;
                        }
                        //Show(m);
                    }
                }
                double D = 1;
                for (int i = 0; i < N; i++)
                    D *= Math.Round(m[i, i], 4);
                return D / d;
            }
        }
        //вспомогательный метод для определителя минора при нахождении обратной матрицы
        private double Minor(int a, int b)
        {
            Matrix M = new Matrix(new double[N - 1, N - 1]);
            for (int i = 0; i < N; i++)
            {
                if (i != a)
                    for (int j = 0; j < N; j++)
                    {
                        if (j != b)
                        {
                            if (i < a & j < b)
                                M[i, j] = matrix[i, j];
                            else if (i < a & j > b)
                                M[i, j - 1] = matrix[i, j];
                            else if (i > a & j < b)
                                M[i - 1, j] = matrix[i, j];
                            else
                                M[i - 1, j - 1] = matrix[i, j];
                        }
                    }
            }
            //Show(M.matrix);
            return M.Determinant;
        }
        //обратная матрица
        public Matrix Inverse()
        {
            double Det = Determinant;
            if (Det == 0)
                return this;
            Matrix A = new Matrix(new double[N, N]);
            Array.Copy(matrix, A.matrix, N*N);
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    //Show(matrix);
                    A[j, i] = Math.Pow(-1, i + j) * Minor(i, j) / Det;
                }
            }
            return A;
        }
        //индекс элемента матрицы
        public double this[int m, int n]
        {
            get
            {
                return matrix[m, n];
            }
            set
            {
                matrix[m, n] = value;
            }
        }
        //перегрузка операторов
        public static Matrix operator +(Matrix A, Matrix B)
        {
            Matrix C = new Matrix(new double[A.N, A.N]);
            for (int i = 0; i < A.N; i++)
            {
                for (int j = 0; j < A.N; j++)
                {
                    C[i, j] = A[i, j] + B[i, j];
                }
            }
            return C;
        }
        public static Matrix operator -(Matrix A, Matrix B)
        {
            Matrix C = new Matrix(new double[A.N, A.N]);
            for (int i = 0; i < A.N; i++)
            {
                for (int j = 0; j < A.N; j++)
                {
                    C[i, j] = A[i, j] - B[i, j];
                }
            }
            return C;
        }
        public static Matrix operator ++(Matrix A)
        {
            Matrix B = new Matrix(new double[,] { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { 0, 0, 0, 1 } } );
            return A + B;
        }
        public static Matrix operator --(Matrix A)
        {
            Matrix B = new Matrix(new double[,] { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { 0, 0, 0, 1 } });
            return A - B;
        }
        public static Matrix operator *(Matrix A, Matrix B)
        {
            Matrix C = new Matrix(new double[A.N, A.N]);
            for (int i = 0; i < A.N; i++)
            {
                for (int j = 0; j < A.N; j++)
                {
                    C.matrix[i, j] = 0;
                    for (int k = 0; k < A.N; k++)
                    {
                        C.matrix[i, j] += A.matrix[i, k] * B.matrix[k, j];
                    }
                }
            }
            return C;
        }
        public static bool operator >(Matrix A, Matrix b) =>
            A.Determinant > b.Determinant;
        public static bool operator <(Matrix A, Matrix b) =>
            A.Determinant < b.Determinant;
        public static bool operator ==(Matrix A, Matrix b) =>
            A.Determinant == b.Determinant;
        public static bool operator !=(Matrix A, Matrix b) =>
            A.Determinant != b.Determinant;
        //перегрузка ToString()
        public override string ToString()
        {
            string text = "Матрица:\n\n";
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    text += $"{Math.Round(matrix[i, j], 3), -7}";
                }
                text += "\n";
            }
            return text + $"\nОпределитель матрицы = {this.Determinant}\n";
            return text;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Matrix Matrix1 = new Matrix(Matrix.RandomMatrix());
            var TestMatrix = new Matrix(new double[,] { { 9, 5, 0, 1 }, { 1, 12, 20, 3 }, { 0, 4, 9, 5 }, { 8, 1, 33, 17 } });
            Console.WriteLine(TestMatrix);
            Console.ReadKey();
        }
    }
}
