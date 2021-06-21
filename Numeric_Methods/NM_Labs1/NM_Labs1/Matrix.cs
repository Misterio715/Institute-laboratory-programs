using System;
using System.Collections.Generic;
using System.Linq;

namespace NM_Labs1
{
    public class Matrix
    {
        private float[,] matrix;

        public int rows => matrix.GetLength(0);
        public int columns => matrix.GetLength(1);

        public int dim
        {
            get
            {
                int d = 0;
                if (rows == columns)
                {
                    d = rows;
                }

                return d;
            }
        }
        
        public float Determinant
        {
            get
            {
                Matrix U = new Matrix(this);
                if (!IsUpperTriangle())
                {
                    U = GaussMethod.LUDecomp(this).ElementAt(1);
                }
                float det = 1;
                for (int i = 0; i < U.dim; i++)
                {
                    det *= U[i, i];
                }

                return det;
            }
        }

        public float Determinant4
        {
            get
            {
                return this[0, 0] * this[1, 1] - this[0, 1] * this[1, 0];
            }
        }

        public Matrix Transpose
        {
            get
            {
                Matrix T = new Matrix(columns, rows);
                for (int i = 0; i < T.rows; i++)
                {
                    for (int j = 0; j < T.columns; j++)
                    {
                        T[i, j] = this[j, i];
                    }
                }

                return T;
            }
        }
        
        public Matrix Inversed
        {
            get
            {
                List<Matrix> LU = GaussMethod.LUDecomp(this);
                Matrix L = LU[0];
                Matrix U = LU[1];
                Matrix inv = new Matrix(L.dim);
                for (int i = 0; i < L.dim; i++)
                {
                    inv[i] = GaussMethod.LUSolve(L, U, new Matrix(dim, 1) {[i, 0] = 1});
                }

                return inv;
            }
        }

        public Matrix(float[,] matrix)
        {
            this.matrix = matrix;
        }

        public Matrix(Matrix clone)
        {
            matrix = new float[clone.rows, clone.columns];
            for (int i = 0; i < clone.rows; i++)
            {
                for (int j = 0; j < clone.columns; j++)
                    matrix[i, j] = clone.matrix[i, j];
            }
        }

        public Matrix(int rows, int columns)
        {
            matrix = new float[rows, columns];
        }

        public Matrix(int dim)
        {
            matrix = new float[dim, dim];
        }
        

        public float this[int i, int j]
        {
            get
            {
                return matrix[i, j];
            }
            set
            {
                matrix[i, j] = value;
            }
        }

        public Matrix this[int j]
        {
            set
            {
                for (int i = 0; i < rows; i++)
                {
                    matrix[i, j] = value[i, 0];
                }
            }
            get
            {
                Matrix ans = new Matrix(rows, 1);
                for (int i = 0; i < rows; i++)
                {
                    ans[i, 0] = this[i, j];
                }

                return ans;
            }
        }

        public static explicit operator Matrix(float arg)
        {
            return new Matrix(new float[,] {{arg}});
        }

        public static explicit operator float(Matrix arg)
        {
            if (arg.dim == 1)
            {
                return arg[0, 0];
            } 
            throw new InvalidCastException(); 
        }
        
        // public static implicit operator Matrix(float arg)
        // {
        //     return new Matrix(new float[,] {{arg}});
        // }
        //
        // public static implicit operator float(Matrix arg)
        // {
        //     if (arg.dim == 1)
        //     {
        //         return arg[1, 1];
        //     } 
        //     throw new InvalidCastException(); 
        // }

        public static Matrix EMatrix(int dim)
        {
            Matrix E = new Matrix(dim);
            for (int i = 0; i < E.dim; i++)
            {
                E[i, i] = 1;
            } 
            return E;
        }

        public override string ToString()
        {
            string A = "";
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    A += $"{this[i, j],8:f3} ";
                }
                A += "\n";
            }
            
            return A;
        }

        public string ToString(int n)
        {
            string A = "";
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    A += $"{MathF.Round(this[i, j], n),12} ";
                }
                A += "\n";
            }
            
            return A;
        }

        public Matrix Multiply(Matrix B)
        {
            Matrix C = new Matrix(rows, B.columns);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < B.columns; j++)
                {
                    for (int k = 0; k < B.rows; k++)
                    {
                        C[i, j] += this[i, k] * B[k, j];
                    }
                }
            }
            
            return C;
        }
        
        public Matrix Multiply(float b)
        {
            Matrix C = new Matrix(rows, columns);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    C[i, j] = this[i, j] * b;
                }
            }
            
            return C;
        }

        public Matrix Add(Matrix B)
        {
            Matrix C = new Matrix(rows, columns);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    C[i, j] = this[i, j] + B[i, j];
                }
            }

            return C;
        }
        
        public Matrix Subtract(Matrix B)
        {
            Matrix C = new Matrix(rows, columns);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    C[i, j] = this[i, j] - B[i, j];
                }
            }

            return C;
        }

        public float Norm2()
        {
            float sum = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    sum += this[i, j] * this[i, j];
                }
            }
            sum = MathF.Sqrt(sum);

            return sum;
        }
        public float NormC()
        {
            float max = 0;
            for (int i = 0; i < rows; i++)
            {
                float sum = 0;
                for (int j = 0; j < columns; j++)
                {
                    sum += Math.Abs(this[i, j]);
                }

                if (sum > max)
                {
                    max = sum;   
                }
            }

            return max;
        }

        public bool IsUpperTriangle()
        {
            for (int i = 0; i < dim; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (this[i, j] != 0)
                    {
                        return false;
                    }
                }
            }
            
            return true;
        }
    }
}