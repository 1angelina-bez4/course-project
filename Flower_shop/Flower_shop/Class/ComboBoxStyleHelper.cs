using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flower_shop
{
    public class ComboBoxStyleHelper
    {
        private static readonly Color TextColor = Color.PaleVioletRed;
        private static readonly Color BgColor = Color.White;
        private static readonly Color SelectColor = Color.FromArgb(255, 220, 230);

        public static void Apply(params ComboBox[] comboBoxes)
        {
            foreach (var cb in comboBoxes)
            {
                cb.DrawMode = DrawMode.OwnerDrawFixed;
                cb.DropDownStyle = ComboBoxStyle.DropDownList;
                cb.FlatStyle = FlatStyle.Flat;
                cb.BackColor = BgColor;
                cb.Font = new Font("Monotype Corsiva", 25, FontStyle.Italic);

                cb.DrawItem += (s, e) => DrawItem(s as ComboBox, e);
                cb.Paint += (s, e) => PaintField(s as ComboBox, e);
            }
                
        }

        private static void DrawItem(ComboBox cb, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            Brush bg = (e.State & DrawItemState.Selected) == DrawItemState.Selected
                ? new SolidBrush(SelectColor)
                : new SolidBrush(BgColor);

            e.Graphics.FillRectangle(bg, e.Bounds);

            e.Graphics.DrawString(cb.Items[e.Index].ToString(),
                                  cb.Font,
                                  new SolidBrush(TextColor),
                                  e.Bounds);
        }

        private static void PaintField(ComboBox cb, PaintEventArgs e)
        {
            if (cb.SelectedIndex < 0) return;

            e.Graphics.FillRectangle(new SolidBrush(BgColor), cb.ClientRectangle);

            e.Graphics.DrawString(cb.Text,
                                  cb.Font,
                                  new SolidBrush(TextColor),
                                  new Point(2, (cb.Height - cb.Font.Height) / 2));
        }
    }
}
