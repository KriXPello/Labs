using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuibarov_Lab4
{
    internal class Matrix // ! квадратная
    {
        private int size;
        private double[,] data;

        public Matrix(int size, double[,] initialData) // constructor
        {
            this.size = size;
            this.data = initialData;
        }

        public int GetSize() { return this.size; }

        public double GetDet() // определитель
        {
            if (size == 2)
            {
                return this[1, 1] * this[2, 2] - this[2, 1] * this[1, 2];
            }
            // далее идёт расчёт на то, что размер 3

            Matrix A = new Matrix(2, new double[,] {
            { this[2, 2], this[2, 3] },
            { this[3, 2], this[3, 3] }
        });
            Matrix B = new Matrix(2, new double[,] {
            { this[1, 2], this[1, 3] },
            { this[3, 2], this[3, 3] }
        });
            Matrix C = new Matrix(2, new double[,] {
            { this[1, 2], this[1, 3] },
            { this[2, 2], this[2, 3] }
        });

            double detA = A.GetDet();
            double detB = B.GetDet();
            double detC = C.GetDet();

            return this[1, 1] * detA - this[2, 1] * detB + this[3, 1] * detC;
        }
        public Matrix ReplaceColumn(int columnIndex, double[] column)
        {
            Matrix T = new Matrix(
                this.size,
                // создание копии данных чтобы не перезаписывать их в текущем объекте
                (double[,])this.data.Clone()
            );

            for (int i = 1; i <= this.size; i++)
            {
                T[i, columnIndex] = column[i - 1];
            }

            return T;
        }
        override public string ToString()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 1; i <= this.size; i++)
            {
                for (int j = 1; j <= this.size; j++)
                {
                    str.Append($"{this[i, j]}\t");
                }
                str.Append("\n");
            }

            return str.ToString();
        }

        /**
         * https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/indexers/
         * Что-то вроде перегрузки оператора [].
         * Вместо M[0, 0] нужно писать M[1, 1],
         * так проще соответствовать формулам
         */
        public double this[int i, int j]
        {
            get { return data[i - 1, j - 1]; }
            set { data[i - 1, j - 1] = value; }
        }
    }
}
