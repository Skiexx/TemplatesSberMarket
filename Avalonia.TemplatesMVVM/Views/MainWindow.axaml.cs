using System;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Avalonia.TemplatesMVVM.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FirstName.Text = "";
        }

        private void Reason_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            FirstName.IsVisible = false;
            switch (ReasonComboBox.SelectedIndex)
            {
                case (0):
                {
                    FirstName.IsVisible = true;
                    ReasonTextBox.IsVisible = true;
                    break;
                }
            }
        }

        private void ClearFields_Click(object? sender, RoutedEventArgs e)
        {
            FirstName.Text = "";
            PhoneNumber.Text = "";
            OrderNumber.Text = "";
            FullText.Text = "";
            MorningRadioButton.IsChecked = true;
            DatePicker.SelectedDate = DateTime.Now;
            ReasonComboBox.SelectedIndex = -1;
            FirstName.IsVisible = false;
        }
    }
}