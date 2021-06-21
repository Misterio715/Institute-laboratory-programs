using System;
using System.Collections.Generic;
using System.Linq;

namespace NM_Labs1
{
    public class BoundaryValueODEMethod
    {
        private float alpha = 1;
        private float beta = 0;
        private float delta = 1;
        private float gamma = 0;
        private float[] xInt;
        private float[] x;
        private float[] y;
        private float[] z;
        private float h;
        private float y0;
        private float y1;
        private int n;

        public BoundaryValueODEMethod(float[] xInt, float h, float y0, float y1)
        {
            this.xInt = xInt;
            n = (int)((xInt[1]- xInt[0]) / h + 1);
            this.x = Enumerable.Range(0, n).Select(c => xInt[0] + c * h).ToArray();
            this.h = h;
            this.y0 = y0;
            this.y1 = y1;
        }

        public static void BoundaryValueProblemMethod(Func<float, float, float, float> func, Func<float, float> p,
            Func<float, float> q, Func<float, float> f, float[] xInt, float h, float y0, float y1,
            Func<float, float> exactSol)
        {
            BoundaryValueODEMethod bvm = new BoundaryValueODEMethod(xInt, h, y0, y1);
            float[] sm = bvm.ShootingMethod(func);
            float[] fd = bvm.FiniteDifferenceMethod(p, q, f);
            Console.WriteLine($"{"Метод стрельбы",15} {"Метод конечной разности",15} {"Точное решение",15}:");
            for (int i = 0; i < bvm.n; i++)
            {
                Console.WriteLine($"{(i <= bvm.n - 1 ? sm[i] : ""),15:f12} {fd[i],15:f12} " +
                                  $"{exactSol(bvm.x[i]),15:f12}");
            }
            
            float h2 = h / 2;
            BoundaryValueODEMethod bvm2 = new BoundaryValueODEMethod(xInt, h2, y0, y1);
            float[] sm2 = bvm2.ShootingMethod(func);
            float[] fd2 = bvm2.FiniteDifferenceMethod(p, q, f);
            float[] smDiff = RungeRombergMethod(sm, sm2, 2);
            float[] fdDiff = RungeRombergMethod(fd, fd2, 4);
            Console.WriteLine($"Погрешность методом Рунге-Румберга:");
            for (int i = 0; i < bvm.n; i++)
            {
                Console.WriteLine($"{(i <= bvm.n - 1 ? smDiff[i] : ""),15:f12} {fdDiff[i],15:f12}");
            }
        }

        public float[] ShootingMethod(Func<float, float, float, float> func)
        {
            float[] _y = new float[n];
            float eta1 = 0;
            float eta2 = 100;
            float yEta1 = Phi(func, eta1, h, out _y);
            float yEta2 = Phi(func, eta2, h, out _y);
            int k = 0;
            while (Math.Abs(eta1 - eta2) > 0.000001)
            {
                k++;
                float etaI = eta2 - yEta2 * (eta2 - eta1) / (yEta2 - yEta1);
                yEta1 = yEta2;
                yEta2 = Phi(func, etaI, h, out _y);
                eta1 = eta2;
                eta2 = etaI;
            }
            
            return _y;
        }
        
        private float Phi (Func<float, float, float, float> func, float eta, float h, out float[] _y)
        {
            y = new float[n];
            z = new float[n];
            var x = Enumerable.Range(0, n).Select(c => xInt[0] + c * h).ToArray();

            z[0] = eta;
            y[0] = alpha > Single.Epsilon ? (y0 - beta * eta) / alpha : 0;

            var rk = CauchyODEMethod.RungeKuttaMethod(func, x, y, z, h);
            _y = rk[0];

            return -delta * rk[0][n - 1] - gamma * rk[1][n - 1] + y1;
        }
        
        public float[] FiniteDifferenceMethod(Func<float, float> p, Func<float, float> q, Func<float, float> f)
        {
            float[] a = new float[n];
            for (int i = 1; i < n - 1; i++)
            {
                a[i] = 1 - p(x[i]) * h / 2;
            }
            a[n - 1] = -gamma;
            
            float[] b = new float[n];
            b[0] = alpha * h - beta;
            for (int i = 1; i < n-1; i++)
            {
                b[i] = q(x[i]) * h * h - 2;
            }
            b[n - 1] = delta * h + gamma;
            
            float[] c = new float[n];
            c[0] = beta;
            for (int i = 1; i < n-1; i++)
            {
                c[i] = 1 + p(x[i]) * h / 2;
            }
            c[n - 1] = 0;
            
            float[] d = new float[n];
            d[0] = y0 * h;
            for (int i = 1; i < n-1; i++)
            {
                d[i] = f(x[i]) * h * h;
            }
            d[n - 1] = y1 * h;

            float[] ans = GaussMethod.TridiagonalMethod(a, b, c, d);

            return ans;
        }

        public static float[] RungeRombergMethod(float[] y1, float[] y2, float p)
        {
            return y1.Zip(y2, (arg1, arg2) => (arg1 - arg2) / (MathF.Pow(2, p) - 1)).ToArray();
        }

    }
}