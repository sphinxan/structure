using System;

namespace structure
{
    //структура
    struct Point
    {
        public int X;
        public int Y;
        public void Test() { }

        //Инициализация полей структуры
        public void Print() { Console.WriteLine("{0} {1}", X, Y); }
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
    public class Program
    {
        static Point staticPoint;
        Point dynamicPoint;
        static void MainX()
        {
            //Для структур все эти инструкции будут работать
            //Для классов - не скомпилируются или выкинут null reference exception
            Point localPoint;
            localPoint.X = 10;
            localPoint.Y = 10;

            var array = new Point[10];
            array[0].X = 10;

            staticPoint.X = 10;

            var instance = new Program();
            instance.dynamicPoint.X = 10;
        }
    }

    //Инициализация полей структуры
    public class Program1
    {
        static Point staticPoint;

        static void MainX()
        {
            Point point;

            point.X = 10;
            Console.WriteLine(point.X);
            //Console.WriteLine(point.Y); //ошибка компиляции, point.Y не инициализировано
            //point.Print(); //ошибка компиляции, point не инициализирован в целом

            point.Y = 20;
            //Сейчас все будет работать, т.к. point целиком инициализирован
            Console.WriteLine(point.Y);
            point.Print();

            Point point1 = new Point();
            //вызов конструктора по умолчанию инициализирует все поля значениями по умолчанию, Поэтому все работает
            Console.WriteLine(point1.Y);
            point1.Print();

            point1 = new Point(30, 40);
            //Вызов другого конструктора перезаписывает поля
            point1.Print();

            //Полей, как статических, так и динамических, проблемы инициализации не касаются
            //Поля-структуры, как и всегда, инициализированы значением по умолчанию, То есть для них пустой конструктор вызывается автоматически
            staticPoint.Print();

            //Разумеется, и для поля эта инструкция новой памяти не выделяет
            staticPoint = new Point(4, 5);
        }
    }

    //Передача структуры в метод
    struct PointStruct
    {
        public int X;
        public int Y;
    }
    class PointClass
    {
        public int X;
        public int Y;
    }
    public class Program2
    {
        static void ProcessStruct(PointStruct point)
        {
            point.X = 10;
        }
        static void ProcessClass(PointClass point)
        {
            point.X = 10;
        }

        public static void Main()
        {
            var pointStruct = new PointStruct();
            ProcessStruct(pointStruct);
            Console.WriteLine(pointStruct.X);//напечатает 0, т.е. структуры копируются

            var pointClass = new PointClass();
            ProcessClass(pointClass);
            Console.WriteLine(pointClass.X); //напечатает 10, т.к. объект передается по ссылке
        }
    }

    public class MyClass
    {
        public int classField;
    }
    public struct MyStruct
    {
        public MyClass myObject;
    }
    public class Program3
    {
        public static void ProcessStruct(MyStruct str)
        {
            str.myObject.classField = 10;
        }

        public static void Main2()
        {
            var str = new MyStruct();
            str.myObject = new MyClass();
            ProcessStruct(str);
            Console.WriteLine(str.myObject.classField); //напечатает 10
        }
    }


    //Ключевое слово ref
    public class Program4
    {
        struct Point
        {
            public int X;
            public int Y;
        }
        static void ProcessStruct(ref Point point)
        {
            point.X = 10;
        }
        static void ProcessInt(ref int n)
        {
            n = 10;
        }
        static void ProcessArray(ref int[] array)
        {
            array = new int[2];
        }
        public static void Main4()
        {
            Point point = new Point();
            ProcessStruct(ref point);
            Console.WriteLine(point.X);

            int n = 0;
            ProcessInt(ref n);
            Console.WriteLine(n);

            var array = new int[3];
            ProcessArray(ref array);
            Console.WriteLine(array.Length);
        }
    }

    //Применение ref
    public class Program5
    {
        public static void SkipSpaces(string text, ref int pos) //пропускает все пробельные символы в text, начиная с позиции pos
        {
            while (pos < text.Length && char.IsWhiteSpace(text[pos]))
                pos++; //В конце pos оказывается установлен в первый непробельный символ
        }
        public static string ReadNumber(string text, ref int pos) //пропускает все цифры в text, начиная с позиции pos, а затем возвращает все пропущенные символы
        {
            var start = pos;
            while (pos < text.Length && char.IsDigit(text[pos]))
                pos++;
            return text.Substring(start, pos - start); //В конце pos оказывается установлен в первый символ не цифру
        }
        public static void WriteAllNumbersFromText(string text) //функция читает все числа из строки и выводит их, разделяя единственным пробелом
        {
            var pos = 0; //position
            while (true)
            {
                SkipSpaces(text, ref pos);
                var num = ReadNumber(text, ref pos); // read next number
                if (num == "") break;
                Console.Write(num + " ");
            }
            Console.WriteLine();
        }
    }


    //Boxing / unboxing
    public class Program6
    {
        struct MyStruct
        {
            public int field;
        }

        public static void Main()
        {
            MyStruct original;
            original.field = 10; //?неявная упаковка(сам boxing)

            object boxed = (object)original; //?явная упаковка
            MyStruct unboxed = (MyStruct)boxed;

            unboxed.field = 20;

            Console.WriteLine(original.field);
            Console.WriteLine(unboxed.field);

            //пример
            int i = 123;      // a value type
            object o = i;     // boxing
            int j = (int)o;   // unboxing
        }
    }
}
