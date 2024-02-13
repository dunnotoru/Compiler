using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace IDE.View
{
    public partial class CodeTabControl : UserControl
    {
        private int _fontSizeModifier = 1;
        private const int MAX_FONT_SIZE = 47;
        private const int MIN_FONT_SIZE = 10;

        public CodeTabControl()
        {
            InitializeComponent();
            FontSize = 12;
        }

        private void editor_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
                IncreaseFontSize();
            else
                DecreaseFontSize();
        }

        private void IncreaseFontSize()
        {
            if (FontSize + _fontSizeModifier > MAX_FONT_SIZE) return;
            FontSize += _fontSizeModifier;
        }
        private void DecreaseFontSize()
        {
            if (FontSize - _fontSizeModifier < MIN_FONT_SIZE) return;
            FontSize -= _fontSizeModifier;
        }
    }
}
