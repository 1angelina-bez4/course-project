using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Flower_shop
{
    /// <summary>
    /// Универсальный помощник для валидации. 
    /// Можно использовать в любой форме проекта (UserForm, Guide и т.д.)
    /// </summary>
    public static class ValidationHelper
    {
        // ==========================================================
        // 1. ЗАПРЕТ ВВОДА (для события KeyPress)
        // ==========================================================
        public static void OnlyRussianLetters(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            if (char.IsLetter(e.KeyChar) && Regex.IsMatch(e.KeyChar.ToString(), @"[а-яА-ЯёЁ]")) return;
            if (e.KeyChar == ' ' || e.KeyChar == '-') return;
            e.Handled = true;
        }

        public static void OnlyDigits(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            if (!char.IsDigit(e.KeyChar)) e.Handled = true;
        }

        public static void OnlyLatinAndDigits(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            if (char.IsLetterOrDigit(e.KeyChar) && Regex.IsMatch(e.KeyChar.ToString(), @"[a-zA-Z0-9]")) return;
            e.Handled = true;
        }

        public static void OnlyEmailChars(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            if (char.IsLetterOrDigit(e.KeyChar)) return;
            if (e.KeyChar == '@' || e.KeyChar == '.' || e.KeyChar == '-' || e.KeyChar == '_') return;
            e.Handled = true;
        }

        // Проверка  введеных данных для сохранения 
        public static bool IsValidRussianName(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;
            return Regex.IsMatch(input.Trim(), @"^[а-яА-ЯёЁ\s-]{2,50}$");
        }

        public static bool IsValidPhone(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;
            // Оставляем только цифры и проверяем, что их 10 или 11
            string digits = Regex.Replace(input, @"\D", "");
            return Regex.IsMatch(digits, @"^\d{10,11}$");
        }

        public static bool IsValidLogin(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;
            return Regex.IsMatch(input.Trim(), @"^[a-zA-Z0-9_]{4,20}$");
        }

        //Для формы Guide (Поставщики и Категории)
        public static bool IsValidEmail(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;
            return Regex.IsMatch(input.Trim(), @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
        }

        public static bool IsValidCompanyName(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;
            return Regex.IsMatch(input.Trim(), @"^[a-zA-Zа-яА-ЯёЁ0-9\s\-]{2,100}$");
        }

        public static bool IsValidCategoryName(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;
            return Regex.IsMatch(input.Trim(), @"^[a-zA-Zа-яА-ЯёЁ\s]{2,50}$");
        }
    }
}