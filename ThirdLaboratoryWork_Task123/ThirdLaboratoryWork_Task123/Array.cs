using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThirdLaboratoryWork_Task123
{
    internal class Matrix
    {
        private int[,] array;

        public int[,] Array() => array;

        public Matrix(int row, int column)
        {
            this.array = new int[row, column];
        }

        public Matrix(int RowColumn, string type)
        {
            this.array = new int[RowColumn, RowColumn];
        }

        public Matrix(int[,] array)
        {
            this.array = array;
        }

        public int CountRow() => array.GetUpperBound(0) + 1; // 0 - индекс первого измерения (строки)

        public int CountColumn() => array.GetUpperBound(1) + 1; // 1 - индекс второго измерения (колонки)

        public string Task2()
        {
            // Блок нахождения среднего значения первого столбца
            float AverageValue = 0, Count = 0;
            for (int row = 0; row < CountRow(); row++)
                AverageValue += array[row,0];
            AverageValue /= CountRow();

            // Блок нахождения количества элементов превышающих среднее значение
            for (int row = 0; row < CountRow(); row++) 
                for (int column = 1; column < CountColumn(); column++)
                    if (array[row, column] > AverageValue)
                        Count++;

            return $"В матрице среднее значение равно {AverageValue}, количество элементов превышающих среднее значение: {Count}";
        }

        public Matrix Transpose()
        {
            int[,] TransposeArray = new int[CountColumn(), CountRow()];
            for (int row = 0; row < CountRow(); row++)
                for (int column = 0; column < CountColumn(); column++)
                    TransposeArray[column, row] = array[row, column];

            return new Matrix(TransposeArray);
        }

        static public Matrix operator -(Matrix matrixM1, Matrix matrixM2)
        {
            if (matrixM1.CountRow() == matrixM2.CountRow() && matrixM1.CountColumn() == matrixM2.CountColumn())
            {
                int[,] ResultArray = new int[matrixM1.CountRow(), matrixM1.CountColumn()];
                for (int row = 0; row < matrixM1.CountRow(); row++)
                    for (int column = 0; column < matrixM1.CountColumn(); column++)
                        ResultArray[row, column] = matrixM1.array[row, column] - matrixM2.array[row, column];
                return new Matrix(ResultArray);
            }
            else throw new ArgumentException("Операция '-' не может быть выполнена из-за разного размера матриц!");
        }

        static public Matrix operator *(int number, Matrix matrix)
        {
            for (int row = 0; row < matrix.CountRow(); row++)
                for (int column = 0; column < matrix.CountColumn(); column++)
                    matrix.array[row, column] *= number;
            return matrix;
        }

        static public Matrix operator *(Matrix matrixM1, Matrix matrixM2)
        {
            if (matrixM1.CountColumn() == matrixM2.CountRow())
            {
                int[,] ResultArray = new int[matrixM1.CountRow(), matrixM2.CountColumn()];
                for (int row = 0; row < matrixM1.CountRow(); row++)
                    for (int column = 0; column < matrixM2.CountColumn(); column++)
                        for (int index = 0; index < matrixM2.CountColumn(); index++)
                            ResultArray[row, column] += matrixM1.array[row, index] * matrixM2.array[index, row];                        

                return new Matrix(ResultArray);
            }
            else throw new ArgumentException("Невозможно перемножить две матрицы, так как количество слобцов первой не равно строкам второй!");
        }

        public override string ToString()
        {
            string message = "";
            for (int row = 0; row < CountRow(); row++)
            {
                for (int column = 0; column < CountColumn(); column++)
                    message += string.Format("{0,4}", array[row, column]);

                message += "\n";
            }
            return message;
        }
    }
}
