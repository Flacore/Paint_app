using System;
using System.Text;

namespace CustomAntialiasing.Drawing2DMath
{
    public class Matrix
    {
        // -----| Used Enums |-----

        #region Enums

        /// <summary>
        /// A simple way to distinguish between vertical and horizontal direction.
        /// </summary>
        public enum Direction
        {
            Vertical,
            Horizontal
        }

        #endregion Enums

        // -----| Properties |-----

        #region Properties

        /// <summary>
        /// The array that contains the values of this matrix. Indices are [row, column].
        /// </summary>
        private readonly double[,] MatrixArray;

        public int Width => Columns;
        public int Height => Rows;

        public int Rows => MatrixArray.GetLength(0);
        public int Columns => MatrixArray.GetLength(1);

        public float[,] Vs { get; }
        public bool Transpose { get; }

        public double this[int row, int column]
        {
            get => MatrixArray[row, column];
            set => MatrixArray[row, column] = value;
        }

        #endregion Properties

        // -----| Static Methods |-----

        #region Static Methods

        public static Matrix GetIdentityMatrix(int size)
        {
            var matrix = new Matrix(size, size);
            for (int i = 0; i < size; i++)
            {
                matrix[i, i] = 1;
            }

            return matrix;
        }

        /// <summary>
        /// Returns 2D point in a matrix form - horizontal or vertical vector
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static Matrix GetVectorPointXY(double x, double y, Direction direction = Direction.Horizontal)
		{
            Matrix vector = Matrix.GetVector(3, direction);

            vector[0, 0] = x;

            if (direction == Direction.Horizontal)
            {
                vector[0, 1] = y;
                vector[0, 2] = 1.0;
            }
            else
			{
                vector[1, 0] = y;
                vector[2, 0] = 1.0;
			}

            return vector; 
		}

        /// <summary>
        /// Returns horizontal or vertical vector of a certain length
        /// </summary>
        /// <param name="length"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static Matrix GetVector(int length, Direction direction)
        {
            Matrix matrix;

            if (direction == Direction.Horizontal)
            {
                matrix = new Matrix(1, length);
            }
            else
            {
                matrix = new Matrix(length, 1);
            }

            return matrix;
        }

        public static Matrix operator *(Matrix left, Matrix right)
        {
            var leftRows = left.Rows;
            var leftColumns = left.Columns;

            var rightRows = right.Rows;
            var rightColumns = right.Columns;

            if (leftColumns != rightRows)
            {
                throw new ApplicationException($"These matrices cannot be multiplied! The number of columns on the left ({leftColumns}) must be equal to the number of rows on the right ({rightRows})!");
            }

            Matrix result = new Matrix(leftRows, rightColumns);

            for (int leftRow = 0; leftRow < leftRows; leftRow++)
            {
                for (int rightColumn = 0; rightColumn < rightColumns; rightColumn++)
                {
                    double multiplicationResult = 0;

                    for (int i = 0; i < leftColumns; i++)
                    {
                        multiplicationResult += left[leftRow, i] * right[i, rightColumn];
                    }

                    result[leftRow, rightColumn] = multiplicationResult;
                }
            }

            return result;
        }

        #endregion Static Methods

        // -----| Constructors |-----

        #region Constructors

        /// <summary>
        /// Creates a new empty matrix.
        /// </summary>
        /// <param name="rows">Number of rows of the new matrix</param>
        /// <param name="columns">Number of columns of the new matrix</param>
        public Matrix(int rows, int columns)
        {
            MatrixArray = new double[rows, columns];
        }

        /// <summary>
        /// Creates a new matrix from the provided array. If required, the array can be transposed.
        /// </summary>
        /// <param name="matrix">The array that will be used as this matrix's values.</param>
        /// <param name="transpose">A flag whether the provided array should be transposed before it is used.</param>
        public Matrix(double[,] matrix, bool transpose = false)
        {
            // Usually, the provided array is created inline and its format is [row, column]. That is what we want.
            if (!transpose)
            {
                MatrixArray = matrix;
                return;
            }

            // Sometimes the array was created in other way and has to be transposed.
            // In that case, the provided matrix has format [column, row] and we have to transpose it to [row, column].
            var rows = matrix.GetLength(1);
            var columns = matrix.GetLength(0);

            MatrixArray = new double[rows, columns];

            // Transpose the array
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    MatrixArray[row, column] = matrix[column, row];
                }
            }
        }

        public Matrix(float[,] vs, bool transpose)
        {
            Vs = vs;
            Transpose = transpose;
        }

        #endregion Constructors

        // -----| Public Methods |-----

        #region Public Methods

        public override String ToString()
        {
            var stringBuilder = new StringBuilder();

            for (int row = 0; row < Rows; row++)
            {
                for (int column = 0; column < Columns; column++)
                {
                    stringBuilder.Append($"{this[row, column]:0.0000} ");
                }

                stringBuilder.Append("\n");
            }

            return stringBuilder.ToString();
        }

        #endregion Public Methods
    }
}
