Дана матрица целых чисел int[,] matrix размером MxN. Вернуть все её элементы, развёрнутые в массив с спиральном порядке против часовой стрелки. <br>
Пример 1:<br>
1 2 3<br>
4 5 6<br>
7 8 9<br>
Входной параметр int[,] matrix = new int[3,3]{{1,2,3},{4,5,6},{7,8,9}};<br>
Результирующий массив:[1,4,7,8,9,6,3,2,5]<br>

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
