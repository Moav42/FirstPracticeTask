using System;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace FirstPracticeTask
{
    [Serializable]
    public class Matrix : ICloneable, IComparable, IEnumerable
    {
        private double[,] _matrix;
        public int Rows { get; }
        public int Columns { get; }

        public Matrix()
        {

        }
        public Matrix(int rows, int columns)
        {
            this.Rows = rows;
            this.Columns = columns;
            _matrix = new double[rows, columns];
        }
        public Matrix(double[,] matrix)
        {
            this.Rows = matrix.GetLength(0);
            this.Columns = matrix.GetLength(1);
            this._matrix = matrix;
        }
     
        public double this[int rows, int columns]
        {
            get
            {
                return _matrix[rows, columns];
            }
            set
            {
                _matrix[rows, columns] = value;
            }
        }
        public static Matrix operator +(Matrix matrix, double x)
        {
            Matrix newMatrix = matrix.Clone() as Matrix;
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    newMatrix[i, j] += x;
                }
            }
            return newMatrix;
        }

        public static Matrix operator +(double x, Matrix matrix)
        {
            return matrix + x;
        }

        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            if(m1.Rows != m2.Rows || m1.Columns != m2.Columns)
            {
                throw new MatrixAdditionException("These matrix cannot be added");
            }

            Matrix newMatrix = new Matrix(m1.Rows, m2.Columns);
            for (int i = 0; i < m1.Rows; i++)
            {
                for (int j = 0; j < m1.Columns; j++)
                {
                    newMatrix[i, j] += m1[i, j] + m2[i, j];

                }
            }
            return newMatrix;
        }
       
        public static Matrix operator -(Matrix matrix, double x)
        {
            Matrix newMatrix = matrix.Clone() as Matrix;
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    matrix[i, j] -= x;
                }
            }
            return newMatrix;
        }
        public static Matrix operator -(double x, Matrix matrix)
        {        
            return matrix - x;
        }
        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            if (m1.Rows != m2.Rows || m1.Columns != m2.Columns)
            {
                throw new MatrixSubtractionException("These matrix cannot be subtracted");
            }
            Matrix newMatrix = new Matrix(m1.Rows, m2.Columns);
            for (int i = 0; i < m1.Rows; i++)
            {
                for (int j = 0; j < m1.Columns; j++)
                {
                    newMatrix[i, j] -= m1[i, j] + m2[i, j];

                }
            }
            return newMatrix;
        }
        public static Matrix operator *(Matrix matrix, double x)
        {
            Matrix newMatrix = matrix.Clone() as Matrix;

            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    matrix[i, j] *= x;
                }
            }
            return newMatrix;
        }
        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            if (m1.Columns != m2.Rows)
            {
                throw new MatrixMultiplyingException("These matrix cannot be multiplied, " +
                    "number of columns of the firsts matrix must be equal to the number of rows of the second matrix");
            }

            Matrix newMatrix = new Matrix(m1.Columns, m2.Rows);
            for (int row = 0; row < newMatrix.Columns; row++)
            {
                for (int col = 0; col < newMatrix.Rows; col++)
                {
                    for (int k = 0; k < newMatrix.Columns; k++)
                    {
                        newMatrix[row, col] += m1[row, k] * m2[k, col];
                    }
                }
            }
            return newMatrix;
        }
        public void PrintToConsole()
        {
            int count = 0;
            foreach (var item in _matrix)
            {
                Console.Write(item + " ");
                count++;
                if(count == Columns)
                {
                    count = 0;
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
        }
        //public static void Serialize(Matrix matrix)
        //{
        //    FileStream stream = File.Create("MatrixDate.dat");
        //    BinaryFormatter formatter = new BinaryFormatter();
        //    formatter.Serialize(stream, matrix);
        //    stream.Close();
        //}
        public void Serialize()
        {
            FileStream stream = File.Create("MatrixDate.dat");
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, new Matrix(_matrix));
            stream.Close();
        } 
        public static Matrix Deserialize()
        {
            FileStream stream = File.OpenRead("MatrixDate.dat");
            BinaryFormatter formatter = new BinaryFormatter();
            Matrix matrix = formatter.Deserialize(stream) as Matrix;
            stream.Close();

            return matrix;
        }
        public object Clone()
        {
            return new Matrix((double[,])_matrix.Clone());
        }
        public int CompareTo(Object other)
        {
            Matrix comerable = other as Matrix;
            if(comerable == null)
            {
                throw new MatrixException($"Cannot compare object type {other.GetType()} to Matrix");

            }
            if (this.Rows != comerable.Rows || this.Columns != comerable.Columns)
            {
                return -1;
            }
            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < this.Columns; j++)
                {
                    if(_matrix[i,j] != comerable[i,j])
                    {
                        return -1;
                    }
                }
            }
            return 0;
        }           
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _matrix.GetEnumerator();
        }
    }
}
