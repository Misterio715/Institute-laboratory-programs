using System;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace NM_Labs1
{
    public class IterationMethod
    {
        public static Matrix FixedPointIterationMethod(Matrix _A, Matrix _b, float e, out int k)
        {
            Matrix A = new Matrix(_A.dim);
            Matrix b = new Matrix(_b.rows, _b.columns);
            int n = A.dim;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    A[i, j] = i != j ? -_A[i, j] / _A[i, i] : 0;
                }
                b[i, 0] = _b[i, 0] / _A[i, i];
            }

            Matrix xp = new Matrix(4, 1);
            Matrix x = new Matrix(b);
            float ek = 1;
            k = 0;
            
            while (ek > e)
            {
                xp = new Matrix(x);
                x = b.Add(A.Multiply(x));
                ek = A.NormC() / (1 - A.NormC()) * x.Subtract(xp).NormC();
                k++;
            }

            return x;
        }

        public static Matrix SeidelMethod(Matrix _A, Matrix _b, float e, out int k)
        {
            Matrix A = new Matrix(_A.dim);
            Matrix b = new Matrix(_b.rows, _b.columns);
            int n = A.dim;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    A[i, j] = i != j ? -_A[i, j] / _A[i, i] : 0;
                }
                b[i, 0] = _b[i, 0] / _A[i, i];
            }


            Matrix B = new Matrix(A);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (j >= i)
                    {
                        B[i, j] = 0;
                    }
                }
            }
            Matrix C = A.Subtract(B);

            Matrix xp = new Matrix(4, 1);
            Matrix x = new Matrix(b);
            float ek = 1;
            k = 0;
            
            while (ek > e)
            {
                xp = new Matrix(x);
                x = b.Add(A.Multiply(x));
                x = b.Add(B.Multiply(x).Add(C.Multiply(xp)));
                ek = C.NormC() / (1 - A.NormC()) * x.Subtract(xp).NormC();
                k++;
            }

            return (x); 
        }
    }
}