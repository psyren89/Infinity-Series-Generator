namespace InfinitySeriesGenerator
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public partial class MainWindow
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        //checks for numerical input
        private void NumberBoxPreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !InputRules.IsPermitted(e.Key);
        }

        //stops people copy/pasting non-numbers in!
        private void NumberBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            this.numberBox.Text = InputRules.SanitiseInput(this.numberBox.Text);
        }

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            Generator.GenerateSeries(this.NoteNamesBox.SelectedIndex, Convert.ToInt32(this.numberBox.Text), Convert.ToInt32(this.startBox.Text));
        }

        //loads the notes into the combobox
        private void SetupStartingNoteNameSelector(object sender, RoutedEventArgs e)
        {
            // Get the ComboBox reference.
            var comboBox = sender as ComboBox;

            if (comboBox == null)
            {
                return;
            }

            // Assign the ItemsSource to the List.
            comboBox.ItemsSource = Generator.Notes;

            // Make the first item selected.
            comboBox.SelectedIndex = 0;
        }
    }
}
