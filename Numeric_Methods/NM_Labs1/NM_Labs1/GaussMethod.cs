using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;

namespace NM_Labs1
{
    public class GaussMethod
    {
        public static float[] TridiagonalMethod(float[] a, float[] b, float[] c, float[] d)
        {
            int n = a.Length;
            float[] P = new float[n];
            float[] Q = new float[n];
            float[] x = new float[n];
                
            P[0] = (float)-c[0] / b[0];
            Q[0] = (float) d[0] / b[0];
                
            for (int i = 1; i < n; i++)
            {
                P[i] = -c[i] / (b[i] + a[i] * P[i - 1]);
                Q[i] = (d[i] - a[i] * Q[i - 1]) / (b[i] + a[i] * P[i - 1]);
            }

            x[n - 1] = Q[n - 1];

            for (int i = n - 2; i >= 0; i--)
            {
                x[i] = P[i] * x[i + 1] + Q[i];
            }
                
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"P{i + 1} = {P[i]:f4}; Q{i + 1} = {Q[i]:f4}");
            }
            Console.WriteLine();
            int j = 1;
            x.ToList().ForEach(x => { Console.WriteLine($"x{j} = {x:f4}"); j++; });

            return x;
        }
        
        public static List<Matrix> LUDecomp(Matrix A)
        {
            Matrix L = Matrix.EMatrix(A.dim);
            Matrix U = new Matrix(A);
            Matrix M = Matrix.EMatrix(A.dim);
            for (int k = 0; k < A.dim; k++)
            {
                M = Matrix.EMatrix(A.dim);
                for (int i = k + 1; i < A.dim; i++)
                {
                    M[i, k] = U[i, k] / U[k, k];
                    for (int j = 0; j < A.dim; j++)
                    {
                        U[i, j] -= M[i, k] * U[k, j];
                    }
                }
                
                L = L.Multiply(M);
            }

            return new List<Matrix>(){L, U};
        }

        public static Matrix LUSolve(Matrix L, Matrix U, Matrix b)
        {
            Matrix z = new Matrix(L.dim, 1);
            for (int i = 0; i < L.dim; i++)
            {
                z[i, 0] = b[i, 0];
                for (int j = 0; j < i; j++)
                {
                    z[i, 0] -= L[i, j] * z[j, 0];
                }
            }

            Matrix x = new Matrix(U.dim, 1);
            for (int i = U.dim - 1; i >= 0; i--)
            {
                x[i, 0] = z[i, 0];
                for (int j = U.dim - 1; j > i; j--)
                {
                    x[i, 0] -= U[i, j] * x[j, 0];
                }
                x[i, 0] = x[i, 0] / U[i, i];
            }

            return x;
        }

        public static Matrix LUSolve(Matrix A, Matrix b)
        {
            List<Matrix> LU = LUDecomp(A);
            return LUSolve(LU[0], LU[1], b);
        }

        public static Matrix Inversed(Matrix L, Matrix U)
        {
            Matrix inv = new Matrix(L.dim);
            for (int i = 0; i < L.dim; i++)
            {
                inv[i] = LUSolve(L, U, new Matrix(L.dim, 1){[i, 0] = 1});
            }

            return inv;
        }
    }
}