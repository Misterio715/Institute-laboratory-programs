using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using static System.MathF;

namespace NM_Labs1
{
    class Program
    {
        static void Main(string[] args)
        {
            #region ЛР 1 (Линейная алгебра)
            Console.WriteLine("Лабораторная работа 1");
            
            // ЛР 1.1
            Console.WriteLine("\nМетод Гаусса (1.1):\nВариант 2\n" +
                              "2x1 + 7x2 - 8x3 + 6x4 = -39\n4x1 + 4x2 - 7x4 = 41\n" +
                              "-x1 - 3x2 + 6x3 + 3x4 = 4\n9x1 - 7x2 - 2x3 - 8x4 = 113\n");
            Matrix A = new Matrix(new float[,] {{2, 7, -8, 6}, {2, 4, 0, -7}, {-1, -3, 6, 3}, {9, -7, -2, -8}});
            Matrix b = new Matrix(new float[,] {{-39}, {41}, {4}, {113}});
            Console.WriteLine($"A:\n{A}");
            List<Matrix> LU = GaussMethod.LUDecomp(A);
            Matrix L = LU[0];
            Matrix U = LU[1];
            Console.WriteLine($"L:\n{L}");
            Console.WriteLine($"U:\n{U}");
            Console.WriteLine($"Определитель: {U.Determinant:f4}");
            Matrix x = GaussMethod.LUSolve(L, U, b);
            Console.WriteLine($"Решение СЛАУ:\n{x}");
            Matrix AI = GaussMethod.Inversed(L, U);
            Console.WriteLine($"Обраятная матрица:\n{AI}");
            //

            // ЛР 1.2
            Console.WriteLine("\nМетод прогонки (1.2):\nВариант 2\n" +
                              "10x1 + 5x2 = -120\n3x1 +10x2 -2x3 = -91\n2x2 - 9x3 -5x4 = 5" +
                              "\n5x3 + 16x4 -4x5 = -74\n-8x4 + 16x5 = -56\n");

            GaussMethod.TridiagonalMethod(new float[] {0, 3, 2, 5, -8}, new float[] {10, 10, -9, 16, 16},
                new float[] {5, -2, -5, -4, 0}, new float[] {-120, -91, 5, -74, -56});
            //

            // ЛР 1.3
            Console.WriteLine("\nМетод простых итераций (1.3):\nВариант 2\n" +
                              "24x1 + 2x2 + 4x3 - 9x4 = -9\n-6x1 - 27x2 - 8x3 - 6x4 = -76\n" +
                              "-4x1 + 8x2 + 19x3 + 6x4 = -79\n4x1 + 5x2 - 3x3 - 13x4 = -70\n");
            A = new Matrix(new float[,] {{24, 2, 4, -9}, {-6, -27, -8, -6}, {-4, 8, 19, 6}, {4, 5, -3, -13}});
            b = new Matrix(new float[,] {{-9}, {-76}, {-79}, {-70}});
            // A = new Matrix(new float[,] {{19, -4, -9, -1}, {-2, 20, -2, -7}, {6, -5, -25, 9}, {0, -3, -9, 12}});
            // b = new Matrix(new float[,] {{100}, {-5}, {34}, {69}});
            int k;
            x = IterationMethod.FixedPointIterationMethod(A, b, 0.0001f, out k);
            Console.WriteLine($"Итераций: {k}\nРешение СЛАУ методом простых итераций:\n{x}\n");
            x = IterationMethod.SeidelMethod(A, b, 0.0001f, out k);
            Console.WriteLine($"Итераций: {k}\nРешение СЛАУ методом Зейделя:\n{x}\n");
            //

            // ЛР 1.4
            A = new Matrix(new float[,] {{-9, 7, 5}, {7, 8, 9}, {5, 9, 8}});
            Console.WriteLine("\nМетод вращения Якоби (1.4):\nВариант 2\n" + A);
            List<Matrix> AV = EigenvectorsMethod.RotationMethod(A, 0.01f);
            Console.WriteLine($"Матрица собственных значений:\n{AV[0]}");
            Console.WriteLine($"Матрица собственных векторов:\n{AV[1]}");
            //
            
            // ЛР 1.5
            A = new Matrix(new float[,] {{-6, -4, 0}, {-7, 6, -7}, {-2, -6, -7}});
            // A = new Matrix(new float[,] {{3, -7, -1}, {-9, -8, 7}, {5, 2, 2}});
            Console.WriteLine("\nМетод QR - разложения матриц (1.5):\nВариант 2\n" + A);
            List<Complex> lamb = EigenvectorsMethod.QRMethod(A, 0.0001f);
            Console.WriteLine("Собственные значения:");
            lamb.ForEach(lamb => Console.WriteLine($"Действительная чать: {lamb.Real}, " +
                                                   $"мнимая часть: {lamb.Imaginary}"));
            //
            
            #endregion

            #region ЛР 2 (Элементарная алгебра)
            Console.WriteLine("\nЛабораторная работа 2");
            
            // ЛР 2.1
            Console.WriteLine("\nМетод простой итерации и метод Ньютона для нелинейных уравнений (2.1):\nВариант 2\n"
            + "f(x) = ln(x + 2) + x^2\n");
            Func<float, float> f = x => Log(x + 2) - Pow(x, 2);
            Func<float, float> derf = x => 1 / (x + 2) - 2 * x;
            Func<float, float> der2f = x => -2 - 1 / Pow(x + 2, 2);
            Func<float, float> phi = x => Sqrt(Log(x + 2));
            Func<float, float> derphi = x => 1 / ((2 * x + 4) * Sqrt(Log(x + 2)));
            float u1 = NonLinearEquationAndSystemMethod.FixedPointIterationMethod(0.9f, 1.3f, phi, derphi, 0.000001f, out k);
            Console.WriteLine($"Метод простой итерации\nКоличество итераций: {k}\nКорень уравнения = {u1}\n");
            
            float u2 = NonLinearEquationAndSystemMethod.NewtonMethod(0.9f, 1.3f, f, derf, der2f,0.000001f, out k);
            Console.WriteLine($"Метод Ньютона\nКоличество итераций: {k}\nКорень уравнения = {u2}\n");
            //
            
            // ЛР 2.2
            Console.WriteLine("\nМетод простой итерации и метод Ньютона для систем нелинейных уравнений (2.2):\nВариант 2\n"
                              + "(x1^2+9)x2 - 27 = 0\n(x1 - 1.5)^2 + (x2 - 1.5)^2 - 9 = 0\n");
            Func<Matrix, Matrix> phi12 = (x) =>
            {
                float x1 = x[0, 0];
                float x2 = x[1, 0];
                Matrix p = new Matrix(2, 1);
                p[0, 0] = Sqrt(6.75f + 3 * x1 - Pow(x2 - 1.5f, 2));
                p[1, 0] = 27 / (x1 * x1 + 9f);
                return p;
            };
            Func<Matrix, Matrix> derphi12 = (x) =>
            {
                float x1 = x[0, 0];
                float x2 = x[1, 0];
                Matrix j = new Matrix(2);
                j[0, 0] = (x2 * x2 - 3 * x2 - 4.5f) / Pow(x1 - 3, 2);
                j[0, 1] = -2 * (x2 - 1.5f) / (x1 - 3f);
                j[1, 0] = -54 * x1 / Pow(x1 * x1 + 9, 2);
                j[1, 1] = 0;
                return j;
            };
            Func<Matrix, Matrix> jacobi = x =>
            {
                float x1 = x[0, 0];
                float x2 = x[1, 0];
                Matrix j = new Matrix(2);
                j[0, 0] = 2 * x1 - 3;
                j[0, 1] = 2 * x2 - 3;
                j[1, 0] = 2 * x1 * x2;
                j[1, 1] = x1 * x1 + 9;
                return j;
            };
            Func<Matrix, Matrix> jx = x =>
            {
                float x1 = x[0, 0];
                float x2 = x[1, 0];
                Matrix j = new Matrix(2, 1);
                j[0, 0] = Pow(x1 - 1.5f, 2) + Pow(x2 - 1.5f, 2) - 9;
                j[1, 0] = (x1 * x1 + 9) * x2 - 27;
                return j;
            };
            Matrix u3;
            NonLinearEquationAndSystemMethod.FixedPointIterationMethodSystem(phi12, out u3, derphi12, 
                new float[]{4.2f, 0.8f}, new float[]{4.6f, 1.2f},0.000001f, out k);
            Console.WriteLine($"Метод простой итерации\nКоличество итераций: {k}\nКорни уравнения = \n{u3}");
            
            Matrix u4;
            NonLinearEquationAndSystemMethod.NewtonMethodSystem(jacobi, jx, out u4, 
                new float[]{4.2f, 0.8f}, new float[]{4.6f, 1.2f}, 0.000001f, out k);
            Console.WriteLine($"Метод Нььютона\nКоличество итераций: {k}\nКорни уравнения = \n{u4}\n");
            //
            
            #endregion

            #region ЛР 3 (Приближение функций)
            Console.WriteLine("\nЛабораторная работа 3");

            // ЛР 3.1
            Console.WriteLine("\nИнтерполяционные многочлены Лагранжа и Ньютона (3.1):\nВариант 2\n"
                              + "y = cos(x)\nXi = 0, pi/6, 2pi/6, 3pi/6\nX* = pi/4\n");
            float[] arrx = {0, PI / 6, PI / 3, PI / 2};
            float argx = PI / 4;
            float p1 = FunctionApproximationMethod.LagrangePolynomialMethod(x => Cos(x), arrx, argx);
            Console.WriteLine($"Интерполяционный многочлен Лагранжа\nL(X*) = {p1}\n" +
                              $"Погрешность = {Abs(p1 - Cos(argx))}\n");
            
            float p2 = FunctionApproximationMethod.NewtonPolynomialMethod(x => Cos(x), arrx, argx);
            Console.WriteLine($"Интерполяционный многочлен Ньютона\nN(X*) = {p2}\n" +
                              $"Погрешность = {Abs(p2 - Cos(argx))}\n");
            //
            
            // ЛР 3.2
            Console.WriteLine("\nКубический сплайн (3.2):\nВариант 2\nX* = 1.5\n" + 
                              $"|{"i",-7}|{0,-7}|{1,-7}|{2,-7}|{3,-7}|{4,-7}|\n" +
                              $"|{"Xi",-7}|{0,-7:f1}|{1,-7:f1}|{2,-7:f1}|{3,-7:f1}|{4,-7:f1}|\n" +
                              $"|{"fi",-7}|{1,-7:f1}|{0.86603,-7:f5}|{0.5,-7:f1}|{0,-7:f1}|{-0.5,-7:f1}|\n");
            float s =FunctionApproximationMethod.SplineMethod(new List<float>() {1f, 0.86603f, 0.5f, 0f, -0.5f},
                new List<float>(){0f, 1f, 2f, 3f, 4f}, 1.5f);
            // s = FunctionApproximationMethod.SplineMethod(new List<float>() {0f, 1.8415f, 2.9093f, 3.1411f, 3.2432f},
            //      new List<float>(){0f, 1f, 2f, 3f, 4f}, 1.5f);
            Console.WriteLine($"\nЗначение функции в точке X*: f(1.5) = {s}\n");
            //
            
            // ЛР 3.3
            Console.WriteLine("\nМетод наименьших квадратов (3.3):\nВариант 2\n" + 
                              $"|{"i",-7}|{0,-7}|{1,-7}|{2,-7}|{3,-7}|{4,-7}|{5,-7}|\n" +
                              $"|{"Xi",-7}|{-1,-7:f1}|{0,-7:f1}|{1,-7:f1}|{2,-7:f1}|{3,-7:f1}|{4,-7:f1}|\n" +
                              $"|{"fi",-7}|{0.86603,-7:f5}|{1,-7:f1}|{0.86603,-7:f5}|{0.5,-7:f1}|{0,-7:f1}|{-0.5,-7:f1}|\n");
            // Matrix a1 = FunctionApproximationMethod.LeastSquaresMethod(new List<float>() {0f, 1.3038f, 1.8439f, 2.2583f, 2.6077f, 2.9155f},
            //     new List<float>(){0f, 1.7f, 3.4f, 5.1f, 6.8f, 8.5f}, 2);
            // Matrix a2 = FunctionApproximationMethod.LeastSquaresMethod(new List<float>() {0f, 1.3038f, 1.8439f, 2.2583f, 2.6077f, 2.9155f},
            //     new List<float>(){0f, 1.7f, 3.4f, 5.1f, 6.8f, 8.5f}, 3);
            Matrix a1 = FunctionApproximationMethod.LeastSquaresMethod(new List<float>() {0.86603f, 1f, 0.86603f, 0.5f, 0f, -0.5f},
                new List<float>(){-1f, 0f, 1f, 2f, 3f, 4f}, 2);
            Console.WriteLine($"Приближающий многочлен первой степени: P1(x) = {a1[0, 0]} + {a1[1, 0]}x\n");
            Matrix a2 = FunctionApproximationMethod.LeastSquaresMethod(new List<float>() {0.86603f, 1f, 0.86603f, 0.5f, 0f, -0.5f},
                new List<float>(){-1f, 0f, 1f, 2f, 3f, 4f}, 3);
            Console.WriteLine($"Приближающий многочлен первой степени: P2(x) = {a2[0, 0]} + {a2[1, 0]}x + " +
                              $"{a2[2, 0]}x^2\n");
            //
            
            // ЛР 3.4
            Console.WriteLine("\nВычисление первой и второй производной таблично заданной функции (3.4):\nВариант 2\nX* = 1.0\n" + 
                              $"|{"i",-7}|{0,-7}|{1,-7}|{2,-7}|{3,-7}|{4,-7}|\n" +
                              $"|{"Xi",-7}|{-1,-7:f1}|{0,-7:f1}|{1,-7:f1}|{2,-7:f1}|{3,-7:f1}|\n" +
                              $"|{"fi",-7}|{-0.5,-7:f1}|{0,-7:f1}|{0.5,-7:f1}|{0.86603,-7:f5}|{1,-7:f1}|\n");
            FunctionApproximationMethod.DerivationMethod(new List<float>(){-0.5f, 0f, 0.5f, 0.86603f, 1f}, 
                new List<float>(){-1f, 0f, 1f, 2f, 3f}, 1f);
            // FunctionApproximationMethod.DerivationMethod(new List<float>(){1f, 1.1052f, 1.2214f, 1.3499f, 1.4918f}, 
            //     new List<float>(){0f, 0.1f, 0.2f, 0.3f, 0.4f}, 0.2f);
            //
            
            // ЛР 3.5
            Console.WriteLine("Вычисление определенного интеграла (3.5):\nВариант 2\ny = x / (3x + 4)^2\n" +
                              "X0 = 0, Xk = 4, h1 = 1.0, h2 = 0.5\n");
            FunctionApproximationMethod.IntegrationMethods(x => x / MathF.Pow(3 * x + 4, 2), 0, 4, 1, 0.5f);
            //
            
            #endregion

            #region ЛР 4 (ОДУ)
            Console.WriteLine("\nЛабораторная работа 4");

            // ЛР 4.1
            Console.WriteLine("\nРешение задачи Коши (4.1):\nВариант 2\nЗадача Коши:\ny\" + y - 2cosx = 0\n" +
                              "y(0) = 1\ny'(0) = 1\n0 <= x <= 1, h = 0.1\nТочное решение:\n" +
                              "y = xsinx + cosx");
            CauchyODEMethod.CauchyProblemMethods((x, y, z) => -y + 2 * Cos(x), 1, 
                0, new float[]{0, 1}, 0.1f);
            //
            
            // ЛР 4.2
            Console.WriteLine("\nРешение краевой задачи для ОДУ(4.2):\nВариант 2\nКраевая задача:\nxy\" + 2y' - xy = 0\n" +
                              "y'(1) = e^-1\ny(2) = 0.5e^-2\nТочное решение:\ny(x) = e^-x / x");
            BoundaryValueODEMethod.BoundaryValueProblemMethod((x, y, z) => (x * y - 2 * z) / x,
                x => 2 / x, x => -1, x => 0, new float[]{1, 2}, 
                0.1f, Exp(-1), 0.5f * Exp(-2), x => Exp(-x) / x);
            //

            #endregion
            Console.WriteLine("\n\n\n\n");

            #region Курсовая Работа (Вариант 20)
            Console.WriteLine("Аппроксимация матриц матрицами малого ранга");
            
            Matrix P;
            Random random = new Random();
            P = new Matrix(random.Next(2, 10));
            for (int i = 0; i < P.dim; i++)
            {
                for (int j = 0; j < P.dim; j++)
                {
                    P[i, j] = random.Next(-100, 100);
                }
            }
            Console.WriteLine($"Начальная матрица A (размерности {P.dim}):\n{P}");
            MatrixApproximationMethod.LowRankMatrixApproximationMethod(P);
            
            #endregion
        }
    }
}
