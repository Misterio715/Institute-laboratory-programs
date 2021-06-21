using System;
using System.Collections.Generic;

namespace NM_Labs1
{
    public class MatrixApproximationMethod
    {
        public static Matrix LowRankMatrixApproximationMethod(Matrix A)
        {
            int n = A.dim;
            Matrix AAT = A.Multiply(A.Transpose);
            Console.WriteLine($"Матрица A*A^T:\n{AAT}");
            List<Matrix> rotationMethodAAT = EigenvectorsMethod.RotationMethod(AAT, 0.00001f);
            Matrix Lambda = rotationMethodAAT[0];
            Matrix P = rotationMethodAAT[1];

            Matrix LambdaSqrt = new Matrix(n);
            for (int i = 0; i < n; i++)
            {
                LambdaSqrt[i, i] = MathF.Sqrt(Lambda[i, i]);
            }
            
            Console.WriteLine($"Матрица Lambda собственных значений матрицы A*A^T:\n{Lambda}");
            Console.WriteLine($"Корни собственных значений матрицы Labmda:\n{LambdaSqrt}");
            Console.WriteLine($"Матрица P собственных векторов матрицы A*A^T:\n{P}");

            Matrix ATA = A.Transpose.Multiply(A);
            Console.WriteLine($"Матрица A^TA:\n{ATA}");
            List<Matrix> rotationMethodATA = EigenvectorsMethod.RotationMethod(ATA, 0.00001f);
            Matrix Q = rotationMethodATA[1];
            
            Matrix QT = Q.Inversed.Multiply(Lambda.Inversed).Multiply(A.Transpose);
            
            Console.WriteLine($"Матрица Q^T собственных векторов матрицы:\n{Q.Transpose}");
            Console.WriteLine($"Матрица QT собственных векторов матрицы (через матричное уравнение) A^T*A:\n{QT}");

            Matrix approximatedA = P.Multiply(LambdaSqrt).Multiply(QT);
            Matrix L = CheckRows(approximatedA);

            Console.WriteLine($"Аппроксимированная матрица P*LambdaSqrt*Qt:\n{approximatedA}");
            Console.WriteLine($"Проверка на линейную зависимость:\n{L.ToString(5)}");

            Matrix _approximatedA = P.Multiply(LambdaSqrt).Multiply(Q);
            Matrix _L = CheckRows(approximatedA);

            Console.WriteLine($"Аппроксимированная матрица (Q^T) P*LambdaSqrt*Qt:\n{_approximatedA}");
            Console.WriteLine($"Проверка на линейную зависимость:\n{_L.ToString(5)}");
            return approximatedA;
        }

        // private static Matrix ElementaryCheck(Matrix A)
        // {
        //     int n = A.dim;
        //     Matrix U = new Matrix(A);
        //
        //     for (int i = 1; i < n; i++)
        //     {
        //         float p = U[0, 0] / U[i, 0];
        //         if (MathF.Abs(U[i, 0]) > 0)
        //         {
        //             for (int j = 0; j < n; j++)
        //             {
        //                 U[i, j] = U[0, j] - p * U[i, j];
        //             }
        //         }
        //     }
        //     
        //     return U;
        // }
        //
        // public static void Skeletal(Matrix A)
        // {
        //     int n = A.dim;
        //     Matrix S = new Matrix(A);
        //     float a = 0;
        //     for (int p = 0; p < n; p++)
        //     {
        //         int k = 0;
        //         float max = 0;
        //         for (int i = 0; i < n; i++)
        //         {
        //             float tmp = MathF.Abs(S[i, p] - a);
        //             if (tmp > max)
        //             {
        //                 k = i;
        //             }
        //         }
        //
        //         int l = 0;
        //         max = 0;
        //         for (int j = 0; j < n; j++)
        //         {
        //             if (j != p)
        //             {
        //                 float tmp = MathF.Abs(S[k, j] - a);
        //                 if (tmp > max)
        //                 {
        //                     l = j;
        //                 }
        //             }
        //         }
        //     }
        // }
        //
        // public static void Approximation(Matrix A)
        // {
        //     Console.WriteLine($"A:\n{A}");
        //     Matrix AM = new Matrix(A).Transpose;
        //     for (int i = 0; i < AM.dim; i++)
        //     {
        //         for (int j = 0; j < AM.dim; j++)
        //         {
        //             AM[i, j] = AM[i, j] != 0 ? 1 / AM[i, j] : 0;
        //         }
        //     }
        //     Console.WriteLine($"A-:\n{AM}");
        //     Matrix AAM = A.Multiply(AM);
        //     Console.WriteLine($"AA-:\n{AAM}");
        //     List<Matrix> rotationMethodAAM = EigenvectorsMethod.RotationMethod(AAM, 0.0000001f);
        //     Console.WriteLine($"Eigenvalues of AA-:\n{rotationMethodAAM[0]}");
        // }

        private static Matrix CheckColumns(Matrix a)
        {
            int n = a.dim;
            Matrix N = new Matrix(n);
            Matrix A = a.Transpose;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    N[i, j] = A[i, j] / A[0, j];
                }
            }

            return N;
        }
        
        private static Matrix CheckRows(Matrix A)
        {
            int n = A.dim;
            Matrix N = new Matrix(n);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    N[i, j] = A[i, j] / A[0, j];
                }
            }

            return N;
        }
    }
}