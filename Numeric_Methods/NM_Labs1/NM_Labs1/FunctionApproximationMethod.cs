using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Channels;

namespace NM_Labs1
{
    public class FunctionApproximationMethod
    {
        public static float LagrangePolynomialMethod(Func<float, float> f, float[] arrx, float x)
        {
            int n = arrx.Length;
            float L = 0;
            string s = "L(x) =";
            for (int i = 0; i < n; i++)
            {
                float l = f(arrx[i]);
                s += $" + {f(arrx[i])}";
                for (int j = 0; j < n; j++)
                {
                    if (i != j)
                    {
                        l *= (x - arrx[j]) / (arrx[i] - arrx[j]);
                        s += $"((x - {arrx[j]} ) / {arrx[i] - arrx[j]})";
                    }
                }
                L += l;
                // Console.WriteLine($"L[i]={l}");
            }
            // Console.WriteLine($"L(x)={L}\nf(x)={f(x)}");
            Console.WriteLine(s);
            return L;
        }

        public static float NewtonPolynomialMethod(Func<float, float> f, float[] arrx, float x)
        {
            int n = arrx.Length;
            Matrix delta = new Matrix(n);
            for (int j = 0; j < n; j++)
            {
                for (int i = 0; i < n - j; i++)
                {
                    if (j != 0)
                    {
                        delta[i, j] = (delta[i, j - 1] - delta[i + 1, j - 1]) / (arrx[i] - arrx[i + j]);
                    }
                    else
                    {
                        delta[i, j] = f(arrx[i]);
                    }
                }
            }
            // Console.WriteLine(delta);

            float N = 0;
            string s = $"N(x) =";
            for (int i = 0; i < n; i++)
            {
                float l = delta[0, i];
                s += $" + {l}";
                for (int j = 0; j < i; j++)
                {
                    l *= x - arrx[j];
                    s += $"(x - {arrx[j]})";
                }
                N += l;
                // Console.WriteLine($"N[i]={d}");
            }
            // Console.WriteLine($"N(x)={N}\nf(x)={f(x)}");
            Console.WriteLine(s);
            return N;
        }

        public static float SplineMethod(List<float> y, List<float> x, float xarg)
        {
            int n = x.Count - 1;
            Func<int, float> h = i => x[i] - x[i - 1];
            Func<int, float> f = i => y[i];
            float[][] ci = new float[3][];
            float[] cx = new float[n - 1];
            ci[0] = new float[n - 1];
            ci[1] = new float[n - 1];
            ci[2] = new float[n - 1];

            ci[1][0] = 2 * (h(1) + h(2));
            ci[2][0] = h(2);
            cx[0] = 3 * ((f(2) - f(1)) / h(2) - (f(1) - f(0)) / h(1));
            for (int i = 3; i <= n - 1; i++)
            {
                ci[0][i - 2] = h(i - 1);
                ci[1][i - 2] = 2 * (h(i - 1) + h(i));
                ci[2][i - 2] = h(i);
                cx[i - 2] = 3 * ((f(i) - f(i - 1)) / h(i) - (f(i - 1) - f(i - 2)) / h(i - 1));
            }
            ci[0][n - 2] = h(n - 1);
            ci[1][n - 2] = 2 * (h(n - 1) + h(n));
            cx[n - 2] = 3 * ((f(n) - f(n - 1)) / h(n) - (f(n - 1) - f(n - 2)) / h(n - 1));

            List<float> c = new List<float>() {0};
            c.AddRange(GaussMethod.TridiagonalMethod(ci[0], ci[1], ci[2], cx));
            Console.WriteLine();

            float[] a = new float[n];
            float[] b = new float[n];
            float[] d = new float[n];
            for (int i = 0; i < n - 1; i++)
            {
                a[i] = f(i);
                b[i] = (f(i + 1) - f(i)) / h(i + 1) - h(i + 1) * (c[i + 1] + 2 * c[i]) / 3;
                d[i] = (c[i + 1] - c[i]) / (3 * h(i + 1));
            }
            a[n - 1] = f(n - 1);
            b[n - 1] = (f(n) - f(n - 1)) / h(n) - 2 * h(n) * c[n - 1] / 3;
            d[n - 1] = -c[n - 1] / (3 * h(n));

            float s = 0;
            for (int i = 0; i < n; i++)
            {
                if (xarg > x[i] & xarg < x[i + 1] )
                {
                    s += a[i] + b[i] * (xarg - x[i]) + c[i] * MathF.Pow(xarg - x[i], 2) +
                        d[i] * MathF.Pow(xarg - x[i], 3);
                }
                Console.WriteLine($"Si = {a[i]} + {b[i]} * (x - {x[i]}) + {c[i]} * (x - {x[i]})^2 + " +
                                  $"{d[i]} * (x - {x[i]})^3");
            }

            return s;
        }

        public static Matrix LeastSquaresMethod(List<float> y, List<float> x, int m)
        {
            Matrix a = new Matrix(m);
            Matrix b = new Matrix(m, 1);
            b[0, 0] = y.Sum();//
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    a[i, j] = x.Sum(x => MathF.Pow(x, i + j));
                }
                b[i, 0] = y.Zip(x, (c, v) => c * MathF.Pow(v, i)).Sum();
            }
            // Console.WriteLine(a);
            // Console.WriteLine(b);

            Matrix A = GaussMethod.LUSolve(a, b);
            Func<float, float> P = x =>
            {
                float sum = 0;
                for (int i = 0; i < m; i++)
                {
                    sum += A[i, 0] * MathF.Pow(x, i);
                }

                return sum;
            };

            float F = 0;
            for (int i = 0; i < x.Count; i++)
            {
                F += MathF.Pow(P(x[i]) - y[i], 2);
            }
            Console.WriteLine($"Сумма квадратов ошибок (m = {m}) = {F}");

            return A;
        }

        public static void DerivationMethod(List<float> y, List<float> x, float xarg)
        {
            int n = x.Count;
            Func<float, float> f = c => y[x.IndexOf(c)];
            Matrix delta = NewtonDeltas(f, x.ToArray());
            // Console.WriteLine(delta);
            
            float der1_2 = delta[0, 1] + delta[0, 2] * ((xarg - x[1]) + (xarg - x[0]));
            float der2_2 = 2 * delta[0, 2];
            Console.WriteLine($"Второй порядок точности\nf'(X*) = {der1_2}\nf\"(X*) = {der2_2}\n");

            float der1_3 = der1_2 + delta[0, 3] * ((xarg - x[1]) * (xarg - x[2]) + (xarg - x[0]) * (xarg - x[2]) +
                                                   (xarg - x[0]) * (xarg - x[1]));
            float der2_3 = der2_2 + 2 * delta[0, 3] * ((xarg - x[0]) + (xarg - x[1]) + (xarg - x[2]));
            Console.WriteLine($"Третий порядок точности\nf'(X*) = {der1_3}\nf\"(X*) = {der2_3}\n");

            float der1_4 = der1_3 + delta[0, 4] * ((xarg - x[0]) * (xarg - x[1]) * (xarg - x[2]) + 
                                                   (xarg - x[0]) * (xarg - x[1]) * (xarg - x[3]) + 
                                                   (xarg - x[0]) * (xarg - x[2]) * (xarg - x[3]) + 
                                                   (xarg - x[1]) * (xarg - x[2]) * (xarg - x[3]));
            float der2_4 = der2_3 + 2 * delta[0, 4] * (6 * xarg * xarg + x[1] * x[2] + x[1] * x[3] + x[2] * x[3] 
                + x[0] * (x[1] + x[2] + x[3]) - 3 * xarg * (x[0] + x[1] + x[2] + x[3]));
            Console.WriteLine($"Четвертый порядок точности\nf'(X*) = {der1_4}\nf\"(X*) = {der2_4}\n");
        }

        public static void IntegrationMethods(Func<float, float> y, float x0, float xk, float h1, float h2)
        {
            int n1 = (int) ((xk - x0) / h1) + 1;
            var x1 = Enumerable.Range(0, n1).Select(c => x0 + c * h1).ToArray();
            int n2 = (int) ((xk - x0) / h2) + 1;
            var x2 = Enumerable.Range(0, n1).Select(c => x0 + c * h2).ToArray();
            
            float F1_1 = RectangleMethod(y, x1, h1);
            float F1_2 = RectangleMethod(y, x2, h2);
            float F1 = RungeRombergMethod(F1_1, F1_2, h1, h2, 2);
            Console.WriteLine($"Метод прямоугольников (h = {h1}): {F1_1}\nМетод прямоугольников (h = {h2}): {F1_2}\n" +
                              $"Рунге-Ромберг: {F1}\n");
            
            float F2_1 = TrapezoidMethod(y, x1, h1);
            float F2_2 = TrapezoidMethod(y, x2, h2);
            float F2 = RungeRombergMethod(F2_1, F2_2, h1, h2, 2);
            Console.WriteLine($"Метод трапеций (h = {h1}): {F2_1}\nМетод трапеций (h = {h2}): {F2_2}\n" +
                              $"Рунге-Ромберг: {F2}\n");
            
            float F3_1 = SimpsonMethod(y, x1, h1);
            float F3_2 = SimpsonMethod(y, x2, h2);
            float F3 = RungeRombergMethod(F3_1, F3_2, h1, h2, 4);
            Console.WriteLine($"Метод Симпсона (h = {h1}): {F3_1}\nМетод Симпсона (h = {h2}): {F3_2}\n" +
                              $"Рунге-Ромберг: {F3}\n");
        }

        public static float RectangleMethod(Func<float, float> y, float[] x, float h)
        {
            int n = x.Length;
            float F = 0;
            for (int i = 1; i < n; i++)
            {
                float arg = (x[i - 1] + x[i]) / 2;
                F += y(arg);
            }
            F *= h;

            return F;
        }

        public static float TrapezoidMethod(Func<float, float> y, float[] x, float h)
        {
            int n = x.Length;
            var f = x.Select(y).ToArray();
            float F = 0;
            for (int i = 1; i < n; i++)
            {
                F += (f[i] + f[i - 1]);
            }
            F *= h * 0.5f;
            
            return F;
        }

        public static float SimpsonMethod(Func<float, float> y, float[] x, float h)
        {
            int n = x.Length;
            var f = x.Select(y).ToArray();
            float F, fEven = 0, fOdd = 0;
            for (int i = 1; i < n - 1; i += 2)
            {
                fOdd += 4 * f[i];
            }
            for (int i = 2; i < n; i += 2)
            {
                fEven += 2 * f[i];
            }
            fEven += f[0] + f[n - 1];
            F = h / 3 * (fEven + fOdd);
            
            return F;
        }

        public static float RungeRombergMethod(float Fh, float Fkh, float h, float kh, float p)
        {
            float k = h / kh;
            float F = Fh + (Fh - Fkh) / (MathF.Pow(k, p) - 1);
            
            return F;
        }

        private static Matrix NewtonDeltas(Func<float, float> f, float[] arrx)
        {
            int n = arrx.Length;
            Matrix delta = new Matrix(n);
            for (int j = 0; j < n; j++)
            {
                for (int i = 0; i < n - j; i++)
                {
                    if (j != 0)
                    {
                        delta[i, j] = (delta[i, j - 1] - delta[i + 1, j - 1]) / (arrx[i] - arrx[i + j]);
                    }
                    else
                    {
                        delta[i, j] = f(arrx[i]);
                    }
                }
            }

            return delta;
        }
    }
}