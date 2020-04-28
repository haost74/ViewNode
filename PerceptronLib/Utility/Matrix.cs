using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerceptronLib.Utility
{
    public class Matrix<T>  where T :  ISign, new()
    {
        private T[,] data = null;
        private double precalculatedDeterminant = double.NaN;

        private int row;
        public int Row { get => this.row; }

        private int column;
        public int Column { get => this.column; }

        public bool IsSquare { get => Row == Column; }

        public void ProcessFunctionOverData(Action<int, int> func)
        {           
            for (var i = 0; i < this.Row; i++)
            {
                for (var j = 0; j < this.Column; j++)
                {
                    func(i, j);
                }
            }
        }


        public static Matrix<T> CreateIdentityMatrix(int row, int column)
        {
            var result = new Matrix<T>(row, column);
            var pm = new ParamMin();
            pm.min = -0.5;
            pm.max = 0.5;
            pm.rn = new Random();
            for (int i = 0; i < row; ++i)
                for (int j = 0; j < column; ++j)
                {
                    result[i, j] = new T();
                    result[i, j].Weight = pm.ran();

                    if(j != 0)
                        result[i, j - 1].SignalTransmission += result[i, j].Signal;
                }
            return result;
        }

        public Matrix(int row, int column)
        {
            this.row = row;
            this.column = column;
            this.data = new T[row, column];
        }

        public T this[int x, int y]
        {
            get
            {
                return this.data[x, y];
            }
            set
            {
                this.data[x, y] = value;
            }
        }

        public Matrix<T> CreateTransposeMatrix()
        {
            var result = new Matrix<T>(this.Row, this.Column);
            result.ProcessFunctionOverData((i, j) => result[i, j] = this[j, i]);
            return result;
        }

        public double CalculateDeterminant()
        {
            if (!double.IsNaN(this.precalculatedDeterminant))
            {
                return this.precalculatedDeterminant;
            }

            if (!IsSquare)
            {
                throw new InvalidOperationException("determinant can be calculated only for square matrix");
            }

            if (Column == 2)
                return this[0, 0].Value * this[1, 1].Value - this[0, 1].Value * this[1, 0].Value;

            double res = 0;
            for (var j = 0; j < Column; ++j)
                res += (j % 2 == 1 ? 1 : -1) * this[1, j].Value * CreateMatrixWithoutColumn(j)
                    .CreateMatrixWithoutRow(1).CalculateDeterminant();

            precalculatedDeterminant = res;
            return res;

        }

        private Matrix<T> CreateMatrixWithoutRow(int row)
        {
            if(row < 0 || row >= Row)
            {
                throw new ArgumentException("invalid row index");
            }
            var res = new Matrix<T>(Row - 1, Column);
            res.ProcessFunctionOverData((i, j) => res[i, j] = i < row ? this[i, j] : this[i + 1, j]);
            return res;
        }

        private Matrix<T> CreateMatrixWithoutColumn(int column)
        {
            if (column < 0 || column >= this.Column)
            {
                throw new ArgumentException("invalid column index");
            }
            var res = new Matrix<T>(Row, column);
            res.ProcessFunctionOverData((i, j) => res[i, j] = j < column ? this[i, j] : this[i, j + 1]);
            return res;
        }

        private double CalculateMinor(int i, int j)
        {
            return CreateMatrixWithoutColumn(j).CreateMatrixWithoutRow(i).CalculateDeterminant();
        }


        public static Matrix<T> operator *(Matrix<T> matrix, double value)
        {
            var result = new Matrix<T>(matrix.Row, matrix.Column);
            result.ProcessFunctionOverData((i, j) =>
                result[i, j].Value = matrix[i, j].Value * value);
            return result;
        }

        public static Matrix<T> operator *(Matrix<T> matrix, Matrix<T> matrix2)
        {
            if (matrix.Column != matrix2.Row)
            {
                throw new ArgumentException("matrixes can not be multiplied");
            }

            var res = new Matrix<T>(matrix.Row, matrix2.Column);

            res.ProcessFunctionOverData((i, j) => 
            {
                for (int k = 0; k < matrix.Column; k++)
                    res[i, j].Value += matrix[i, k].Value * matrix2[k, j].Value;
            });
            return res;
        }

        public static Matrix<T> operator + (Matrix<T> matrix, Matrix<T> matrix2)
        {
            if (matrix.Column != matrix2.Row)
            {
                throw new ArgumentException("matrixes can not be multiplied");
            }

            var res = new Matrix<T>(matrix.Row, matrix2.Column);
            res.ProcessFunctionOverData((i, j) => res[i, j].Value = matrix[i, j].Value + matrix2[i, j].Value);
            return res;
        }

        public static Matrix<T> operator -(Matrix<T> matrix, Matrix<T> matrix2)
        {
            return matrix + (matrix2 * -1);
        }

    }
}
