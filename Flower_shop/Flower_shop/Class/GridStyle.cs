using System;
using System.Drawing;
using System.Windows.Forms;

namespace Flower_shop
{
    public class GridStyle
    {
        //Цвета проекта
        private static readonly Color BgColor = Color.FromArgb(255, 240, 245);
        private static readonly Color TextColor = Color.Black;
        private static readonly Color SelectBgColor = Color.FromArgb(255, 182, 193);
        private static readonly Color HeaderBgColor = Color.FromArgb(255, 220, 230);
        private static readonly Color HeaderTextColor = Color.DarkRed;
        private static readonly Color GridLineColor = Color.LightPink;

        //Шрифты
        private static readonly Font CellFont = new Font("Monotype Corsiva", 16, FontStyle.Italic);
        private static readonly Font HeaderFont = new Font("Monotype Corsiva", 14, FontStyle.Bold | FontStyle.Italic);

        public static void Apply(DataGridView grid)
        {
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.RowHeadersVisible = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.BackgroundColor = BgColor;

            //Ячейки
            grid.DefaultCellStyle.Font = CellFont;
            grid.DefaultCellStyle.ForeColor = TextColor;
            grid.DefaultCellStyle.BackColor = BgColor;
            grid.DefaultCellStyle.SelectionBackColor = SelectBgColor;
            grid.DefaultCellStyle.SelectionForeColor = TextColor;

            // перенос текста, чтобы пользователь видел длинные данные полностью
            grid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            //Шапка
            grid.ColumnHeadersDefaultCellStyle.Font = HeaderFont;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = HeaderTextColor;
            grid.ColumnHeadersDefaultCellStyle.BackColor = HeaderBgColor;
            grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = HeaderBgColor;
            grid.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;

            grid.ColumnHeadersHeight = 45;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            //Строки
            grid.RowTemplate.Height = 70;

            // Это предотвращает "скачки" и тормоза при фильтрации, но позволяет длинному тексту занять 2-3 строки.
            grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;

            // Запрещает пользователю случайно изменить высоту мышкой и сломать дизайн
            grid.AllowUserToResizeRows = false;

            grid.EnableHeadersVisualStyles = false;

            //Границы
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grid.GridColor = GridLineColor;
        }
    }
}