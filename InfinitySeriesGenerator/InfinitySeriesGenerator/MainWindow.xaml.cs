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
            InitializeComponent();
        }

        //checks for numerical input
        private void NumberBoxPreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !keyRules.IsNumberKey(e.Key) && !keyRules.IsDelOrBackspaceOrTabKey(e.Key);
        }

        //stops people copy/pasting non-numbers in!
        private void NumberBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            this.seriesSizeInput.Text = InputRules.SanitiseInput(this.seriesSizeInput.Text);
        }

        //the notes!
        private string[] noteTypes = new string[12]
        {
            "C",
            "C#",
            "D",
            "D#",
            "E",
            "F",
            "F#",
            "G",
            "G#",
            "A",
            "A#",
            "B"
        };

        //activates the algorithm
        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            iGenerator iG = new iGenerator();
            iG.startNote = NoteNamesBox.SelectedIndex;
            iG.iSeriesGenerate(Convert.ToInt32(numberBox.Text), Convert.ToInt32(startBox.Text));
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
            comboBox.ItemsSource = NoteCalculator.Notes;

            // Make the first item selected.
            comboBox.SelectedIndex = 0;
        }


	    // ... Make the first item selected.
	    comboBox.SelectedIndex = 0;
        }
    }
}
