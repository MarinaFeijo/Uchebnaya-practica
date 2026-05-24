using System;
using System.Collections.Generic;
using System.Linq;

// Абстрактный класс Транспорт
abstract class Transport
{
    public double Price { get; set; }
    public double Speed { get; set; }
    public int Year { get; set; }

    // Координаты транспорта (динамические характеристики)
    public double X { get; protected set; }
    public double Y { get; protected set; }

    // Конструктор для инициализации общих свойств
    protected Transport(double price, double speed, int year, double x = 0, double y = 0)
    {
        Price = price;
        Speed = speed;
        Year = year;
        X = x;
        Y = y;
    }

    // Методы для перемещения транспорта
    public void Move(double deltaX, double deltaY)
    {
        X += deltaX;
        Y += deltaY;
        Console.WriteLine($"Транспорт переместился в точку ({X}, {Y})");
    }

    public void MoveTo(double newX, double newY)
    {
        X = newX;
        Y = newY;
        Console.WriteLine($"Транспорт прибыл в точку ({X}, {Y})");
    }

    public void ReturnToStart()
    {
        X = 0;
        Y = 0;
        Console.WriteLine("Транспорт вернулся в начальную точку (0, 0)");
    }


    // Метод для получения текущих координат
    public (double, double) GetCoordinates()
    {
        return (X, Y);
    }

    // Абстрактный метод для вывода характеристик
    public abstract void PrintInfo();
}

// Класс Самолет
class Plane : Transport
{
    double Height { get; set; }
    int Passengers { get; set; }

    public Plane(double price, double speed, int year, double height, int passengers, double x = 0, double y = 0)
        : base(price, speed, year, x, y)
    {
        Height = height;
        Passengers = passengers;
    }

    public override void PrintInfo()
    {
        Console.WriteLine("**** Самолёт ****");
        Console.WriteLine($"Цена: {Price} руб.");
        Console.WriteLine($"Скорость: {Speed} км/ч");
        Console.WriteLine($"Год выпуска: {Year}");
        Console.WriteLine($"Высота полёта: {Height} м");
        Console.WriteLine($"Количество пассажиров: {Passengers}");
        var (x, y) = GetCoordinates();
        Console.WriteLine($"Текущие координаты: ({x}, {y})");
        Console.WriteLine();
    }
}

// Класс Корабль
class Ship : Transport
{
    int Passengers { get; set; }
    string Port { get; set; }

    public Ship(double price, double speed, int year, int passengers, string port, double x = 0, double y = 0)
        : base(price, speed, year, x, y)
    {
        Passengers = passengers;
        Port = port;
    }

    public override void PrintInfo()
    {
        Console.WriteLine("**** Корабль ****");
        Console.WriteLine($"Цена: {Price} руб.");
        Console.WriteLine($"Скорость: {Speed} узлов");
        Console.WriteLine($"Год выпуска: {Year}");
        Console.WriteLine($"Количество пассажиров: {Passengers}");
        Console.WriteLine($"Порт приписки: {Port}");
        var (x, y) = GetCoordinates();
        Console.WriteLine($"Текущие координаты: ({x}, {y})");
        Console.WriteLine();
    }
}

// Класс Автомобиль
class Car : Transport
{
    double Power { get; set; }

    public Car(double price, double speed, int year, double power, double x = 0, double y = 0)
        : base(price, speed, year, x, y)
    {
        Power = power;
    }

    public override void PrintInfo()
    {
        Console.WriteLine("**** Автомобиль ****");
        Console.WriteLine($"Цена: {Price} руб.");
        Console.WriteLine($"Скорость: {Speed} км/ч");
        Console.WriteLine($"Год выпуска: {Year}");
        Console.WriteLine($"Мощность двигателя: {Power} кВт");
        var (x, y) = GetCoordinates();
        Console.WriteLine($"Текущие координаты: ({x}, {y})");
        Console.WriteLine();
    }
}

// Главный класс программы
class Program
{
    static void Main()
    {
        Console.Write("Введите вид транспорта (Самолёт, Корабль, Автомобиль): ");
        string? userInput = Console.ReadLine();
        Transport transport;
        switch (userInput?.Trim().ToLower())
        {
            case "самолет":
                transport = CreatePlane();
                break;
            case "корабль":
                transport = CreateShip();
                break;
            case "автомобиль":
                transport = CreateCar();
                break;
            default:
                Console.WriteLine("Неизвестно");
                return;
        }

        // Выводим начальные характеристики
        transport.PrintInfo();


        // Цикл для многократного изменения координат
        bool continueMoving = true;
        while (continueMoving)
        {
            Console.WriteLine("=== Управление перемещением транспорта ===");
            Console.WriteLine("1 — Переместиться на заданное расстояние (относительное перемещение)");
            Console.WriteLine("2 — Переместиться в конкретную точку (абсолютное перемещение)");
            Console.WriteLine("3 — Вернуться в начало (0, 0)");
            Console.WriteLine("4 — Завершить работу с перемещением");
            Console.Write("Выберите действие: ");


            switch (Console.ReadLine())
            {
                case "1":
                    double dx = ReadDouble("Сдвиг по X: ");
                    double dy = ReadDouble("Сдвиг по Y: ");
                    transport.Move(dx, dy);
                    break;

                case "2":
                    double newX = ReadDouble("Новая координата X: ");
                    double newY = ReadDouble("Новая координата Y: ");
                    transport.MoveTo(newX, newY);
                    break;

                case "3":
                    transport.ReturnToStart();
                    break;

                case "4":
                    continueMoving = false;
                    Console.WriteLine("Завершение работы с перемещением транспорта.");
                    break;

                default:
                    Console.WriteLine("Неверный выбор действия. Пожалуйста, выберите число от 1 до 5.");
                    break;
            }

            // Если пользователь не завершил работу, показываем текущие координаты
            if (continueMoving)
            {
                var (x, y) = transport.GetCoordinates();
                Console.WriteLine($"Текущие координаты: ({x}, {y})");
            }
        }

        Console.WriteLine("\nФинальные характеристики транспорта:");
        transport.PrintInfo();

        Console.ReadKey();
    }

    // Метод для создания самолёта с вводом характеристик от пользователя
    static Plane CreatePlane()
    {
        Console.WriteLine("\n--- Ввод характеристик самолёта ---");
        double price = ReadDouble("Цена (руб.): ");
        double speed = ReadDouble("Скорость (км/ч): ");
        int year = ReadInt("Год выпуска: ");
        double height = ReadDouble("Высота полёта (м): ");
        int passengers = ReadInt("Количество пассажиров: ");
        double x = ReadDouble("Начальная координата X: ");
        double y = ReadDouble("Начальная координата Y: ");

        return new Plane(price, speed, year, height, passengers, x, y);
    }

    // Метод для создания корабля с вводом характеристик от пользователя
    static Ship CreateShip()
    {
        Console.WriteLine(" Ввод характеристик корабля");
        double price = ReadDouble("Цена (руб.): ");
        double speed = ReadDouble("Скорость (узлов): ");
        int year = ReadInt("Год выпуска: ");
        int passengers = ReadInt("Количество пассажиров: ");
        Console.Write("Порт приписки: ");
        string? port = Console.ReadLine();
        double x = ReadDouble("Начальная координата X: ");
        double y = ReadDouble("Начальная координата Y: ");
        return new Ship(price, speed, year, passengers, port ?? string.Empty, x, y);
    }

    // Метод для создания автомобиля с вводом характеристик от пользователя
    static Car CreateCar()
    {
        Console.WriteLine("\n--- Ввод характеристик автомобиля ---");
        double price = ReadDouble("Цена (руб.): ");
        double speed = ReadDouble("Скорость (км/ч): ");
        int year = ReadInt("Год выпуска: ");
        double power = ReadDouble("Мощность двигателя (кВт): ");
        double x = ReadDouble("Начальная координата X: ");
        double y = ReadDouble("Начальная координата Y: ");

        return new Car(price, speed, year, power, x, y);
    }

    // Вспомогательный метод для безопасного ввода дробных чисел
    static double ReadDouble(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (double.TryParse(Console.ReadLine(), out double result))
                return result;
            Console.WriteLine("Ошибка! Введите корректное число.");
        }
    }

    // Вспомогательный метод для безопасного ввода целых чисел
    static int ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out int result) && result >= 0)
                return result;
            Console.WriteLine("Ошибка! Введите корректное положительное целое число.");
        }
    }
}
