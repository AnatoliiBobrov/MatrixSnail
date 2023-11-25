using System;

namespace Task_2
{
    public class Program
    {
        private static int length = 0;
        private static int[,] matrix;// здесь будет храниться исходная матрица

        /// <summary>
        /// Производит проверку входной строки на корректность
        /// </summary>
        /// <param name="columns">количество колонок в матрице</param>
        /// <param name="stringRows">строковое представление элементов строки</param>
        /// <param name="row">номер строки, в которую производится запись</param>
        /// <returns> Возращает true, если возможно преобразование, false в противном случае</returns>
        private static bool tryParse(int columns, string[] stringRows, int row)
        {
            //проверка количества входных элементов
            if ((stringRows == null) || (columns != stringRows.Length))
            {
                return false;
            }
            else 
            {
                var i = 0;
                while (i < columns) // проверка качества входных элементов
                {
                    var res = 0;
                    if (int.TryParse(stringRows[i], out res)) // корректные данные
                    {
                        matrix[row, i] = res;
                        i++;
                    }
                    else // неверные данные, даже единственная ошибка прерывает выполнение функции
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        
        /// <summary>
        /// Преобразует двухмерную матрицу в простую при "закручивании во внутрь" против часовой стрелки массива, начиная с элемента 0,0
        /// </summary>
        /// <param name="matr">исходная матрица</param>
        /// <returns>Одномерная матрица после преобразования размером length = M*N</returns>
        public static int[] GetArray(ref int[,] matr)
        {
            // размеры матрицы, не берем данные из введенных пользователем для того,
            // чтобы показать, что метод не завязан на консоль
            var m = matr.GetLength(0);
            var n = matr.GetLength(1);
            length = m * n;

            //хранит результат работы функции
            var result = new int[length];

            // хранят края области обхода массива
            var mBegin = 0;
            var mEnd = m - 1;
            var nBegin = 0;
            var nEnd = n - 1;
            var i = 0;

            // указатели текущего положения в массиве
            var x = 0;
            var y = 0;

            // обход по кругу
            while (i < length)
            {
                // движение вниз
                while (x <= mEnd)
                {
                    result[i] = matr[x, y];
                    i++;
                    x++;
                }
                x--;
                y++;
                nBegin++;

                // движение вправо
                if (i < length)
                {
                    while (y <= nEnd)
                    {
                        result[i] = matr[x, y];
                        i++;
                        y++;
                    }
                    y--;
                    x--;
                    mEnd--;

                    // движение вверх
                    if (i < length)
                    {
                        while (x >= mBegin)
                        {
                            result[i] = matr[x, y];
                            i++;
                            x--;
                        }
                        y--;
                        x++;
                        nEnd--;

                        // движение влево
                        if (i < length)
                        {
                            while (y >= nBegin)
                            {
                                result[i] = matr[x, y];
                                i++;
                                y--;
                            }
                            y++;
                            x++;
                            mBegin++;
                        }
                    }
                }
            }

            return result;
        }

        static void Main(string[] args)
        {
            Console.Write("Введите размер матрицы в формате МхN: ");
            var size = Console.ReadLine().Split('x'); // ввод размеров матрицы
            int m, n; // размеры матрицы

            // проверка правильности введенных данных
            // не приступаем к следующему шагу, пока не получим корректные размеры
            while (!((size != null) && (size.Length == 2) && int.TryParse(size[0], out m) && (m > 0) && int.TryParse(size[1], out n) && (n > 0)))
            {
                Console.Write("Введите корректный размер: ");
                size = Console.ReadLine().Split('x'); // ввод размеров матрицы
            }

            matrix = new int[m, n];// матрица
            Console.WriteLine("Вводите построчно элементы матрицы. В строке элементы разделите пробелами:");

            int i = 0; // счетчик цикла
            while (i < m) // записываем строки матрицы
            {
                // проверка входных данных
                while (!tryParse(n, Console.ReadLine().Split(' '), i)) 
                {
                    Console.WriteLine("Введите корректные данные:");
                }
                i++;
            }

            // вывод результата
            Console.WriteLine("'Развернутая' матрица:\n[{0}]", string.Join(",", GetArray(ref matrix)));

            // нужно, чтобы не закрылась программа
            Console.ReadLine();
        }
    }
}
