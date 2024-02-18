using ICSharpCode.AvalonEdit;
using Microsoft.Xaml.Behaviors;
using System.Windows;
using System;

namespace IDE.Helper
{
    public sealed class AvalonEditBehavior : Behavior<TextEditor>
    {
        public static readonly DependencyProperty GiveMeTheTextProperty =
            DependencyProperty.Register("Content", typeof(string), typeof(AvalonEditBehavior),
            new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, PropertyChangedCallback));

        public string Content
        {
            get { return (string)GetValue(GiveMeTheTextProperty); }
            set { SetValue(GiveMeTheTextProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            if (AssociatedObject != null)
                AssociatedObject.TextChanged += AssociatedObjectOnTextChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            if (AssociatedObject != null)
                AssociatedObject.TextChanged -= AssociatedObjectOnTextChanged;
        }

        private void AssociatedObjectOnTextChanged(object? sender, EventArgs eventArgs)
        {
            var textEditor = sender as TextEditor;
            if (textEditor != null)
            {
                if (textEditor.Document != null)
                    Content = textEditor.Document.Text;
            }
        }

        private static void PropertyChangedCallback(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            AvalonEditBehavior? behavior = dependencyObject as AvalonEditBehavior;
            if (behavior == null) return;
            if (behavior.AssociatedObject is null) return;

            TextEditor? editor = behavior.AssociatedObject as TextEditor;
            if (editor is null) return;
            if(editor.Document is null) return;

            var caretOffset = editor.CaretOffset;
            editor.Document.Text = dependencyPropertyChangedEventArgs.NewValue.ToString();
            if (editor.Document.Text is null) return;

            if(editor.Document.Text.Length < caretOffset)
                editor.CaretOffset = 0;
            else
                editor.CaretOffset = caretOffset;
        }
    }
}

