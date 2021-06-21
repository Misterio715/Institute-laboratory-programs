using System;
using System.Linq;

namespace NM_Labs1
{
    public class CauchyODEMethod
    {
        public static void CauchyProblemMethods(Func<float,float, float, float> f, float y0, float yder0, float[] xInt, float h)
        {
            int n = (int)((xInt[1]- xInt[0]) / h + 1);
            var x = Enumerable.Range(0, n).Select(c => xInt[0] + c * h).ToArray();
            float[] y = new float[n];
            y[0] = y0;
            float[] z = new float[n];
            z[0] = yder0;

            float h2 = h / 2;
            
            float[] e1 = EulerMethod(f, x, y, z, h);
            float[] e2 = EulerMethod(f, x, y, z, h2);
            float[] eDiff = RungeRomberg(e1, e2, 2);
            float[] rk1 = RungeKuttaMethod(f, x, y, z, h)[0];
            float[] rk2 = RungeKuttaMethod(f, x, y, z, h2)[0];
            float[] rkDiff = RungeRomberg(rk1, rk2, 4);
            float[] ad1 = AdamsMethod(f, x, y, z, h);
            float[] ad2 = AdamsMethod(f, x, y, z, h2);
            float[] adDiff = RungeRomberg(ad1, ad2, 4);
            
            // Console.WriteLine($"Метод Эйлера:\ny: {String.Join(" ", ans1)}");
            // Console.WriteLine($"Метод Рунге-Кутты:\ny: {String.Join(" ", ans2)}");
            // Console.WriteLine($"Метод Адамса:\ny: {String.Join(" ", ans3)}");
            Console.WriteLine($"{"Метод Эйлера",-15} {"Метод Рунге-Кутты",-15} {"Метод Адамса",-15}");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"{e1[i],15:f12} {rk1[i],15:f12} " +
                                  $"{ad1[i],15:f12}");
            }

            Console.WriteLine($"Погрешность методом Рунге-Ромберга:");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"{eDiff[i],15:f12} {rkDiff[i],15:f12} " +
                                  $"{adDiff[i],15:f12}");
            }
        }

        public static float[] EulerMethod(Func<float, float, float, float> f, float[] _x, float[] _y, float[] _z, float h)
        {
            int n = _x.Length;
            float[] x = (float[]) _x.Clone();
            float[] y = (float[]) _y.Clone();
            float[] z = (float[]) _z.Clone();
            for (int i = 1; i < n; i++)
            {
                z[i] = z[i - 1] + h * f(x[i - 1], y[i - 1], z[i - 1]);
                y[i] = y[i - 1] + h * g(x[i - 1], y[i - 1], z[i - 1]);
            }

            return y;
        }

        public static float[][] RungeKuttaMethod(Func<float, float, float, float> f, float[] _x, float[] _y, float[] _z, float h)
        {
            int n = _x.Length;
            float[] x = (float[]) _x.Clone();
            float[] y = (float[]) _y.Clone();
            float[] z = (float[]) _z.Clone();
            for (int i = 0; i < n - 1; i++)
            {
                float k1 = h * g(x[i], y[i], z[i]);
                float q1 = h * f(x[i], y[i], z[i]);
                float k2 = h * g(x[i] + 0.5f * h, y[i] + 0.5f * k1, z[i] + 0.5f * q1);
                float q2 = h * f(x[i] + 0.5f * h, y[i] + 0.5f * k1, z[i] + 0.5f * q1);
                float k3 = h * g(x[i] + 0.5f * h, y[i] + 0.5f * k2, z[i] + 0.5f * q2);
                float q3 = h * f(x[i] + 0.5f * h, y[i] + 0.5f * k2, z[i] + 0.5f * q2);
                float k4 = h * g(x[i] + h, y[i] + k3, z[i] + q3);
                float q4 = h * f(x[i] + h, y[i] + k3, z[i] + q3);
                y[i + 1] = y[i] + (k1 + 2 * k2 + 2 * k3 + k4) / 6;
                z[i + 1] = z[i] + (q1 + 2 * q2 + 2 * q3 + q4) / 6;
            }

            return new float[][] {y, z};
        }

        public static float[] AdamsMethod(Func<float, float, float, float> f, float[] _x, float[] _y, float[] _z, float h)
        {
            int n = _x.Length;
            float[] x = (float[]) _x.Clone();
            float[] y = (float[]) _y.Clone();
            float[] z = (float[]) _z.Clone();
            float[][] yz = RungeKuttaMethod(f, x, y, z, h);
            y = yz[0];
            z = yz[1];

            for (int i = 3; i < n - 1; i++)
            {
                z[i + 1] = z[i] + h / 24 * (55 * f(x[i], y[i], z[i]) -
                                       59 * f(x[i - 1], y[i - 1], z[i - 1]) +
                                       37 * f(x[i - 2], y[i - 2], z[i - 2]) -
                                       9 * f(x[i - 3], y[i - 3], z[i - 3]));
                y[i + 1] = y[i] + h / 24 * (55 * g(x[i], y[i], z[i]) -
                                       59 * g(x[i - 1], y[i - 1], z[i - 1]) +
                                       37 * g(x[i - 2], y[i - 2], z[i - 2]) -
                                       9 * g(x[i - 3], y[i - 3], z[i - 3]));
                x[i + 1] = x[i]+h;

            }

            return y;
        }

        public static float[] RungeRomberg(float[] y1, float[] y2, float p)
        {
            return y1.Zip(y2, (arg1, arg2) => arg1 + (arg1 - arg2) / (MathF.Pow(2, p) - 1)).ToArray();
        }

        private static float g(float x, float y, float z) => z;
    }
}