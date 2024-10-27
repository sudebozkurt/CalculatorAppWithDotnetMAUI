using System;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace MauiApp2
{
    public partial class CalculatorPage : ContentPage
    {
        public CalculatorPage()
        {
            InitializeComponent();
            BindingContext = new CalculatorViewModel();
        }
    }

    public class CalculatorViewModel : INotifyPropertyChanged
    {
        private string _displayText = "0";
        private double _firstNumber;
        private string _operator;
        private bool _isNewEntry = true;

        public event PropertyChangedEventHandler PropertyChanged;

        public string DisplayText
        {
            get => _displayText;
            set
            {
                _displayText = value;
                OnPropertyChanged(nameof(DisplayText));
            }
        }

        public ICommand NumberCommand => new Command<string>(OnNumberPressed);
        public ICommand OperationCommand => new Command<string>(OnOperatorPressed);
        public ICommand EqualsCommand => new Command(OnEqualsPressed);
        public ICommand BackspaceCommand => new Command(OnBackspacePressed);

        private void OnNumberPressed(string number)
        {
            if (_isNewEntry)
            {
                DisplayText = number;
                _isNewEntry = false;
            }
            else
            {
                DisplayText += number;
            }
        }

        private void OnOperatorPressed(string op)
        {
            if (double.TryParse(DisplayText, out _firstNumber))
            {
                _operator = op;
                _isNewEntry = true;
            }
        }

        private void OnEqualsPressed()
        {
            if (double.TryParse(DisplayText, out double secondNumber))
            {
                double result = 0;
                switch (_operator)
                {
                    case "+":
                        result = _firstNumber + secondNumber;
                        break;
                    case "-":
                        result = _firstNumber - secondNumber;
                        break;
                    case "*":
                        result = _firstNumber * secondNumber;
                        break;
                    case "/":
                        result = secondNumber != 0 ? _firstNumber / secondNumber : 0;
                        break;
                }

                DisplayText = result.ToString();
                _isNewEntry = true;
            }
        }

        private void OnBackspacePressed()
        {
            if (!string.IsNullOrEmpty(DisplayText) && DisplayText.Length > 1)
            {
                DisplayText = DisplayText.Substring(0, DisplayText.Length - 1);
            }
            else
            {
                DisplayText = "0";
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
