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
            e.Handled = !keyRules.IsNumberKey(e.Key) && !keyRules.IsDelOrBackspaceOrTabKey(e.Key);
        }

        //stops people copy/pasting non-numbers in!
        private void NumberBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            this.numberBox.Text = InputRules.SanitiseInput(this.numberBox.Text);
        }

        //the notes!
        private string[] noteTypes = new string[12]
        private void SaveButtonClick(object sender, RoutedEventArgs e)
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

        {
            iGenerator iG = new iGenerator();
            iG.startNote = NoteNamesBox.SelectedIndex;
            iG.iSeriesGenerate(Convert.ToInt32(numberBox.Text), Convert.ToInt32(startBox.Text));
        }

        //loads the notes into the combobox
        private void SetupStartingNoteNameSelector(object sender, RoutedEventArgs e)
        {
            //... A List.
	    List<string> data = iGenerator.noteTypes.Cast<string>().ToList();
	    
	    // ... Get the ComboBox reference.
        var comboBox = sender as System.Windows.Controls.ComboBox;

	    // ... Assign the ItemsSource to the List.
	    comboBox.ItemsSource = data;

	    // ... Make the first item selected.
	    comboBox.SelectedIndex = 0;
        }
    }
}
