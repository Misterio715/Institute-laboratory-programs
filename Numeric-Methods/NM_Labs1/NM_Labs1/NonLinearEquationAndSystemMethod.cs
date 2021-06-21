using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Channels;

namespace NM_Labs1
{
    public class NonLinearEquationAndSystemMethod
    {
        public static float FixedPointIterationMethod(float a, float b, Func<float, float> phi, 
            Func<float,float> derphi, float e, out int k)
        {
            k = 0;
            float xp;
            float x = (a + b) / 2f;
            float q = MathF.Abs(Max(a, b, derphi));
            float ek = 2 * e;
            while (ek > e)
            {
                xp = x;
                x = phi(xp);
                // Console.WriteLine($"x:{x}");
                ek = q / (1 - q) * MathF.Abs(x - xp);
                k++;
            }
            
            return x;
        }

        public static float NewtonMethod(float a, float b, Func<float, float> f, 
            Func<float, float> derf, Func<float, float> der2f, float e, out int k)
        {
            k = 0;
            float xp;
            float x = 0;
            if (f(a) * der2f(a) > 0)
            {
                x = a;
            }
            else if (f(b) * der2f(b) > 0)
            {
                x = b;
            }
            float ek = 2 * e;
            while (ek > e)
            {
                xp = x;
                x = xp - f(xp) / derf(xp);
                // Console.WriteLine(x);
                ek = MathF.Abs(x - xp);
                k++;
            }

            return x;
        }

        public static void FixedPointIterationMethodSystem(Func<Matrix, Matrix> phi, out Matrix x,
            Func<Matrix, Matrix> derphi, float[] a, float[] b, float e, out int k)
        {
            k = 0;
            Matrix xp;
            x = new Matrix(new float[,] {{(a[0] + b[0]) / 2}, {(a[1] + b[1]) / 2}});
            float q = NormC(derphi(x));
            float ek = 2 * e;
            while (ek > e)
            {
                // Console.WriteLine(x);
                xp = new Matrix(x);
                x = phi(xp);
                ek = q / (1 - q) * x.Subtract(xp).NormC();
                k++;
            }
        }

        public static void NewtonMethodSystem(Func<Matrix, Matrix> J, Func<Matrix, Matrix> jx, 
            out Matrix x, float[] a, float[] b, float e, out int k)
        {
            k = 0;
            Matrix xp;
            x = new Matrix(new float[,] {{(a[0] + b[0]) / 2}, {(a[1] + b[1]) / 2}});
            float ek = 2 * e;
            while (ek > e)
            {
                // Console.WriteLine($"x={x}");
                xp = new Matrix(x);
                Matrix c = CramerMethod(J(xp), jx(xp));
                // Console.WriteLine($"c={c}");
                x = xp.Subtract(c);
                ek = x.Subtract(xp).NormC();
                k++;
            }
        }

        private static float Max(float a, float b, Func<float, float> f)
        {
            float max = 0;
            float func;
            for (float i = a; i < b; i += 0.01f)
            {
                func = f(i);
                if (func > max)
                {
                    max = func;
                }
            }

            return max;
        }
        
        private static float NormC(Matrix A)
        {
            float max = 0;
            for (int i = 0; i < A.rows; i++)
            {
                float sum = 0;
                for (int j = 0; j < A.columns; j++)
                {
                    sum += Math.Abs(A[i, j]);
                }

                if (sum > max & sum < 1)
                {
                    max = sum;   
                }
            }

            return max;
        }

        public static Matrix CramerMethod(Matrix A, Matrix b)
        {
            int n = A.dim;
            Matrix ans = new Matrix(n, 1);
            List<float> delta = new List<float>() {A.Determinant4};
            // Console.WriteLine($"det={delta[0]}");
            if (delta[0] > 0.0000000001f)
            {
                for (int i = 0; i < n; i++)
                {
                    Matrix d = new Matrix(A);
                    d[i] = b;
                    // Console.WriteLine(d);
                    delta.Add(d.Determinant4);
                }

                for (int i = 0; i < n; i++)
                {
                    ans[i, 0] = delta[i + 1] / delta[0];
                }
            }
            
            return ans;
        }
    }
}