using Microsoft.VisualStudio.TestTools.UnitTesting;
using Practice_Task_2;

namespace Practice_Task_2.Tests
{
    [TestClass]
    public class MatrixTests
    {
        //тест определителя
        [TestMethod]
        public void Matrix_Determinant_Test_1()
        {
            var TestMatrix = new Matrix(new double[,] { { 9, 5, 0, 1 }, { 1, 12, 20, 3 }, { 0, 4, 9, 5 }, { 8, 1, 33, 17 } });
            var Res = TestMatrix.Determinant;
            Assert.AreEqual(Res, -12522);
        }
        [TestMethod]
        public void Matrix_Determinant_Test_2()
        {
            var TestMatrix = new Matrix(new double[,] { { 17, 0, 5, 4 }, { 64, 0, 12, 9 }, { 17, 0, 5, 4 }, { 12, 4, 26, 3 } });
            var Res = TestMatrix.Determinant;
            Assert.AreEqual(Res, 0);
        }
        [TestMethod]
        public void Matrix_Determinant_Test_3()
        {
            var TestMatrix = new Matrix(new double[,] { { 4, 10, 2, 7 }, { 5, 16, 8, 2 }, { 12, 11, 10, 9 }, { 4, 41, 1, 6 } });
            var Res = TestMatrix.Determinant;
            Assert.AreEqual(Res, -5991);
        }
        [TestMethod]
        public void Matrix_Determinant_Test_4()
        {
            var TestMatrix = new Matrix(new double[,] { { 9, 16, 8, 2 }, { 0, 0, 0, 0 }, { 3, 15, 7, 0 }, { 23, 1, 29, 10 } });
            var Res = TestMatrix.Determinant;
            Assert.AreEqual(Res, 0);
        }
        //-------------------------------------------------------------------------------------------------------------------------
        //тест обратной матрицы
        [TestMethod]
        public void Matrix_Inverse_Test_1()
        {
            var TestMatrix = new Matrix(new double[,] { { 9, 5, 0, 1 }, { 1, 12, 20, 3 }, { 0, 4, 9, 5 }, { 8, 1, 33, 17 } });
            var Res = TestMatrix.Inverse().Determinant;
            Assert.AreEqual(Res, -1/12522);
        }
        [TestMethod]
        public void Matrix_Inverse_Test_2()
        {
            var TestMatrix = new Matrix(new double[,] { { 17, 0, 5, 4 }, { 64, 0, 12, 9 }, { 17, 0, 5, 4 }, { 12, 4, 26, 3 } });
            var Res = TestMatrix.Inverse().Determinant;
            Assert.AreEqual(Res, 0);
        }
        [TestMethod]
        public void Matrix_Inverse_Test_3()
        {
            var TestMatrix = new Matrix(new double[,] { { 4, 10, 2, 7 }, { 5, 16, 8, 2 }, { 12, 11, 10, 9 }, { 4, 41, 1, 6 } });
            var Res = TestMatrix.Inverse().Determinant;
            Assert.AreEqual(Res, -1/5991);
        }
        [TestMethod]
        public void Matrix_Inverse_Test_4()
        {
            var TestMatrix = new Matrix(new double[,] { { 9, 16, 8, 2 }, { 0, 0, 0, 0 }, { 3, 15, 7, 0 }, { 23, 1, 29, 10 } });
            var Res = TestMatrix.Inverse().Determinant;
            Assert.AreEqual(Res, 0);
        }
        //-------------------------------------------------------------------------------------------------------------------------
    }
}
