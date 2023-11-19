using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeWorkModule12
{
    using System;

    //уведомления об изменении свойств
    public interface IPropertyChanged
    {
        event PropertyChangedEventHandler PropertyChanged;
    }

    //обработкa события изменения свойства
    public delegate void PropertyChangedEventHandler(object sender, PropertyChangedEventArgs e);

    //аргументы события изменения свойства
    public class PropertyChangedEventArgs : EventArgs
    {
        public string PropertyName { get; }

        public PropertyChangedEventArgs(string propertyName)
        {
            PropertyName = propertyName;
        }
    }

    //интерфейс IPropertyChanged
    public class ExampleClass : IPropertyChanged
    {
        private string _name;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChange(nameof(Name));
                }
            }
        }

        private void OnPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    class Program
    {
        // Делегат для выполнения арифметических операций
        public delegate double MathOperation(double x, double y);

        // Методы для выполнения арифметических операций
        public static double Add(double x, double y) => x + y;
        public static double Subtract(double x, double y) => x - y;
        public static double Multiply(double x, double y) => x * y;
        public static double Divide(double x, double y) => y != 0 ? x / y : double.NaN;

        // Метод для выполнения операции, представленной делегатом
        public static double PerformOperation(double x, double y, MathOperation operation)
        {
            return operation(x, y);
        }

        static void Main()
        {
            // Пример использования делегата и методов для арифметических операций
            MathOperation addOperation = Add;
            MathOperation subtractOperation = Subtract;
            MathOperation multiplyOperation = Multiply;
            MathOperation divideOperation = Divide;

            double result1 = PerformOperation(5, 3, addOperation);
            double result2 = PerformOperation(8, 4, subtractOperation);
            double result3 = PerformOperation(6, 2, multiplyOperation);
            double result4 = PerformOperation(9, 3, divideOperation);

            Console.WriteLine($"Сложение: {result1}");
            Console.WriteLine($"Вычитание: {result2}");
            Console.WriteLine($"Умножение: {result3}");
            Console.WriteLine($"Деление: {result4}");

            // Пример использования анонимного метода для инициализации делегата
            MathOperation powerOperation = delegate (double x, double y) { return Math.Pow(x, y); };
            double result5 = PerformOperation(2, 3, powerOperation);
            Console.WriteLine($"Возведение в степень: {result5}");

            // Пример использования лямбда-выражения для инициализации делегата
            MathOperation squareOperation = (x, y) => Math.Pow(x, 2);
            double result6 = PerformOperation(4, 0, squareOperation);
            Console.WriteLine($"Квадрат: {result6}");

            // Пример цепочки делегатов для выполнения нескольких операций подряд
            MathOperation chainedOperation = addOperation + multiplyOperation + subtractOperation;
            double result7 = chainedOperation(3, 2);
            Console.WriteLine($"Цепочка операций: {result7}");
        }
    }

}
