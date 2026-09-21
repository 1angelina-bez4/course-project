using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Flower_shop
{
    public class TextBoxHelper
    {
        private static readonly Color PlaceholderColor = Color.PaleVioletRed;
        private static readonly Color TextColor = Color.PaleVioletRed;

        /// <summary>
        /// Устанавливает placeholder для одного TextBox
        /// </summary>
        public static void SetPlaceholder(TextBox tb, string placeholder)
        {
            tb.Text = placeholder;
            tb.ForeColor = PlaceholderColor;
            tb.Tag = placeholder;   // запоминаем placeholder в Tag

            tb.Enter += (s, e) =>
            {
                if (tb.Text == placeholder)
                {
                    tb.Text = "";
                    tb.ForeColor = TextColor;
                }
            };

            tb.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(tb.Text))
                {
                    tb.Text = placeholder;
                    tb.ForeColor = PlaceholderColor;
                }
            };
        }

        /// <summary>
        /// Устанавливает placeholder для нескольких TextBox сразу
        /// </summary>
        public static void SetPlaceholders(params (TextBox tb, string placeholder)[] pairs)
        {
            foreach (var (tb, placeholder) in pairs)
            {
                SetPlaceholder(tb, placeholder);
            }
        }

        /// <summary>
        /// Проверяет, является ли текст в TextBox placeholder'ом
        /// </summary>
        public static bool IsPlaceholder(TextBox tb)
        {
            return tb.Tag != null && tb.Text == tb.Tag.ToString();
        }

        /// <summary>
        /// Получает реальный текст (без placeholder)
        /// </summary>
        public static string GetRealText(TextBox tb)
        {
            return IsPlaceholder(tb) ? "" : tb.Text.Trim();
        }
    }
}
