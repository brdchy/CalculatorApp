using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Calculator
{
    public partial class MainWindow : Window
    {
        private string currentInput = string.Empty; // Текущий введенный текст
        private double firstNumber = 0; // Первое число для выполнения операции
        private string operation = string.Empty; // Выбранная операция

        public MainWindow()
        {
            InitializeComponent();
            PreviewKeyDown += MainWindow_PreviewKeyDown;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string content = button.Content.ToString();

            if (content == "C")
            {
                Clear(); // Очистить текущее состояние калькулятора
            }
            else if (content == "=")
            {
                Calculate(); // Выполнить расчет
            }
            else if (content == "+/-")
            {
                if (!string.IsNullOrEmpty(currentInput))
                {
                    double number = double.Parse(currentInput);
                    number = -number; // Изменить знак числа
                    currentInput = number.ToString();
                    DisplayResult(); // Отобразить результат
                }
            }
            else
            {
                currentInput += content; // Добавить введенный символ к текущему вводу
                DisplayResult(); // Отобразить результат
            }
        }
        private void AppendInput(string input)
        {
            currentInput += input;
            DisplayResult();
        }
        private void SetOperation(string op)
        {
            firstNumber = double.Parse(currentInput);
            operation = op;
            currentInput = string.Empty;
        }
        private void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Shift)
            {
                switch (e.Key)
                {
                    case Key.OemPlus:
                        SetOperation("+");
                        break;
                    case Key.OemMinus:
                        SetOperation("-");
                        break;
                    case Key.D8:
                        SetOperation("*");
                        break;
                    case Key.OemQuestion:
                        SetOperation("/");
                        break;
                }
            }
            else
            {
                switch (e.Key)
                {
                    // Числа
                    case Key.D0:
                    case Key.NumPad0:
                        AppendInput("0");
                        break;
                    case Key.D1:
                    case Key.NumPad1:
                        AppendInput("1");
                        break;
                    case Key.D2:
                    case Key.NumPad2:
                        AppendInput("2");
                        break;
                    case Key.D3:
                    case Key.NumPad3:
                        AppendInput("3");
                        break;
                    case Key.D4:
                    case Key.NumPad4:
                        AppendInput("4");
                        break;
                    case Key.D5:
                    case Key.NumPad5:
                        AppendInput("5");
                        break;
                    case Key.D6:
                    case Key.NumPad6:
                        AppendInput("6");
                        break;
                    case Key.D7:
                    case Key.NumPad7:
                        AppendInput("7");
                        break;
                    case Key.D8:
                    case Key.NumPad8:
                        AppendInput("8");
                        break;
                    case Key.D9:
                    case Key.NumPad9:
                        AppendInput("9");
                        break;

                    // Операции
                    case Key.Add:
                        SetOperation("+");
                        break;
                    case Key.Subtract:
                        SetOperation("-");
                        break;
                    case Key.Multiply:
                        SetOperation("*");
                        break;
                    case Key.Divide:
                        SetOperation("/");
                        break;
                    case Key.Decimal:
                    case Key.OemPeriod:
                        AppendInput(".");
                        break;

                    // Выполнение вычислений
                    case Key.Enter:
                        Calculate();
                        break;

                    // Очистка
                    case Key.Escape:
                        Clear();
                        break;
                }
            }
        }

        private void Clear()
        {
            currentInput = string.Empty; // Очистить текущий ввод
            firstNumber = 0; // Сбросить первое число
            operation = string.Empty; // Сбросить операцию
            DisplayResult(); // Отобразить результат
        }

        private void Calculate()
        {
            if (!string.IsNullOrEmpty(currentInput))
            {
                double secondNumber = double.Parse(currentInput, new CultureInfo("en-us"));
                double result = 0;

                switch (operation)
                {
                    case "+":
                        result = firstNumber + secondNumber; // Сложение
                        break;
                    case "-":
                        result = firstNumber - secondNumber; // Вычитание
                        break;
                    case "*":
                        result = firstNumber * secondNumber; // Умножение
                        break;
                    case "/":
                        if (secondNumber != 0)
                        {
                            result = firstNumber / secondNumber; // Деление
                        }
                        else
                        {
                            MessageBox.Show("В школе нельзя делить на ноль"); // Показать сообщение об ошибке деления на 0
                            Clear(); // Очистить состояние калькулятора
                            return;
                        }
                        break;
                }

                currentInput = result.ToString(); // Присвоить результат текущему вводу
                DisplayResult(); // Отобразить результат
                operation = string.Empty; // Сбросить операцию
            }
        }

        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string content = button.Content.ToString();

            if (!string.IsNullOrEmpty(currentInput))
            {
                firstNumber = double.Parse(currentInput, new CultureInfo("en-us")); // Сохранить первое число
                operation = content; // Сохранить выбранную операцию
                currentInput = string.Empty; // Сбросить текущий ввод
                DisplayResult(); // Отобразить результат
            }
        }

        private void DisplayResult()
        {
            resultTextBox.Text = currentInput; // Отобразить текущий ввод в текстовом поле
        }
    }
}
