using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Threading.Channels;
using static System.MathF;

namespace NM_Labs1
{
    public class EigenvectorsMethod
    {
        private static Func<Matrix, float, bool> RotataionEpsilon = (A, e) =>
        {
            int n = A.dim;
            float ek = 0;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    ek += A[i, j] * A[i, j];
                }
            }

            return Sqrt(ek) > e;
        };
        
        private static Func<Matrix, float, bool> QREpsilon = (A, e) =>
        {
            int n = A.dim;
            float ek = 0;
            for (int i = 2; i < n; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    ek += A[i, j] * A[i, j];
                }
            }

            return Sqrt(ek) > e;
        };
        public static List<Matrix> RotationMethod(Matrix A, float e)
        {
            int n = A.dim;
            float phi = 0;
            Matrix V = Matrix.EMatrix(n);
            while (RotataionEpsilon(A, e))
            {
                (int i, int j) maxInd = (0, 1);
                float max = Abs(A[maxInd.i, maxInd.j]);
                for (int i = 0; i < n - 1; i++)
                {
                    for (int j = i + 1; j < n; j++)
                    {
                        if (Abs(A[i, j]) >= max)
                        {
                            max = Abs(A[i, j]);
                            maxInd = (i, j);
                        }
                    }
                }

                if (A[maxInd.i, maxInd.i] != A[maxInd.j, maxInd.j])
                {
                    phi = 0.5f * Atan(2 * A[maxInd.i, maxInd.j] / (A[maxInd.i, maxInd.i] - A[maxInd.j, maxInd.j]));
                }
                else
                {
                    phi = PI / 4;
                }

                Matrix U = CreateUMatrix(maxInd.i, maxInd.j, phi, n);
                V = V.Multiply(U);
                A = U.Transpose.Multiply(A).Multiply(U);
                // Console.WriteLine($"A:{A}");
                // Console.WriteLine($"V:{V}");
            }

            return new List<Matrix>(){A, V};
        }
        private static Matrix CreateUMatrix(int i, int j, float phi, int dim)
        {
            Matrix U = Matrix.EMatrix(dim);
            U[i, i] = Cos(phi);
            U[i, j] = -Sin(phi);
            U[j, i] = Sin(phi);
            U[j, j] = Cos(phi);
            return U;
        }

        public static List<Matrix> QRDecomp(Matrix A)
        {
            int n = A.dim;
            
            Matrix R = new Matrix(A);
            Matrix Q = Matrix.EMatrix(n);
            for (int j = 0; j < n - 1; j++)
            {
                float aNorm = 0;
                for (int i = j; i < n; i++)
                { 
                    aNorm += R[i, j] * R[i, j]; 
                }
                aNorm = Sqrt(aNorm);

                Matrix nu = new Matrix(n, 1);
                for (int i = j; i < n; i++)
                {
                    nu[i, 0] = i == j ? R[i, j] + Sign(R[i, j]) * aNorm : R[i, j];
                }
                
                Matrix H = nu.Multiply(nu.Transpose).Multiply(-2f / (float)nu.Transpose.Multiply(nu));
                H = Matrix.EMatrix(n).Add(H);
                R = H.Multiply(R);
                Q = Q.Multiply(H);
            }
            // Console.WriteLine(R);
            // Console.WriteLine(Q);
            // Console.WriteLine(Q.Multiply(R));
            return new List<Matrix>() {Q, R};
        }

        public static List<Complex> QRMethod(Matrix A, float e)
        {
            Matrix Aq = new Matrix(A);
            List<Complex> lamb = new List<Complex>();
            int k = 0;
            while (k < 10000)
            {
                List<Matrix> QR = QRDecomp(Aq);
                Matrix Q = QR[0];
                Matrix R = QR[1];
                Aq = R.Multiply(Q);

                // Console.WriteLine(Aq);
                k++; 
            }

            for (int i = 0; i < Aq.dim; i++)
            {
                if (i != Aq.dim - 1 && Abs(Aq[i + 1, i]) > e)
                {
                    List<Complex> l = CharacteristicEquation(Aq, i);
                    lamb.AddRange(l);
                    // Console.WriteLine($"{i} {l[0]} {l[1]}");
                    i++;
                }
                else
                {
                    lamb.Add(new Complex(Aq[i, i], 0));
                }
            }
            
            Console.WriteLine($"Квазиверхнетреугольная матрица:\n{Aq}");
            return lamb;
        }

        private static List<Complex> CharacteristicEquation(Matrix a, int j)
        {
            float D = (a[j, j] + a[j + 1, j + 1]) * (a[j, j] + a[j + 1, j + 1]) 
                      - 4 * (a[j, j] * a[j + 1, j + 1] - a[j, j + 1] * a[j + 1, j]);
            Complex lamb1;
            Complex lamb2;
            if (D < 0)
            {
                lamb1 = new Complex((a[j, j] + a[j + 1, j + 1]) / 2f, Sqrt(-D) / 2f);
                lamb2 = new Complex((a[j, j] + a[j + 1, j + 1]) / 2f, - Sqrt(-D) / 2f);
            }
            else
            {
                lamb1 = new Complex((a[j, j] + a[j + 1, j + 1] + Sqrt(D)) / 2f, 0);
                lamb2 = new Complex((a[j, j] + a[j + 1, j + 1] - Sqrt(D)) / 2f, 0);
            }
            return new List<Complex>() {lamb1, lamb2};
        }
    }
}