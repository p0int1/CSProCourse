using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Logistic.WpfClient
{
    public enum FildType
    {
        IsString,
        Isint,
        IsDouble
    }
    public static class AppTools
    { 
        public static void DeleteAndCargoButtonsIsEnable(this MainWindow form, bool flag)
        {
            form.deleteBtn.IsEnabled = flag;
            form.cargoBtn.IsEnabled = flag;
        }

        private static void ShowError(TextBox textBox, string text)
        {
            textBox.BorderBrush = Brushes.Red;
            MessageBox.Show(text, "Input error", MessageBoxButton.OK, MessageBoxImage.Warning);
            textBox.BorderBrush = Brushes.Gray;
            textBox.Focus();
        }

        public static bool ValidateTextBox(this TextBox textBox, FildType expect, int minValue, int maxValue)
        {
            var userInput = textBox.Text.Trim();
 
            var check = expect switch
            {
                FildType.IsString => minValue <= userInput.Length && userInput.Length <= maxValue,
                FildType.Isint => int.TryParse(userInput, out int number) && minValue <= number && number <= maxValue,
                FildType.IsDouble => double.TryParse(userInput, out double number) && minValue <= number && number <= maxValue,
                _ => throw new NotImplementedException()
            };

            if (check) return true;

            if (expect == FildType.IsString) ShowError(textBox, $"String length must be between {minValue} and {maxValue}");
            if (expect == FildType.Isint) ShowError(textBox, $"Expect an integer between {minValue} and {maxValue}");
            if (expect == FildType.IsDouble) ShowError(textBox, $"Expect an double between {minValue} and {maxValue}");
            return false;
        }
    }
}
