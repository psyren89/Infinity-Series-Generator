using System;
using System.Windows;
using System.Windows.Controls;

namespace InfinitySeriesGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //checks for numerical input
        private void numberBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            e.Handled = !keyRules.IsNumberKey(e.Key) && !keyRules.IsDelOrBackspaceOrTabKey(e.Key);
        }

        //stops people copy/pasting non-numbers in!
        private void numberBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            numberBox.Text = keyRules.checkForChar(numberBox.Text);
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
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            iGenerator iG = new iGenerator();
            iG.startNote = NoteNamesBox.SelectedIndex;
            iG.iSeriesGenerate(Convert.ToInt32(numberBox.Text), Convert.ToInt32(startBox.Text));
        }

        //loads the notes into the combobox
        private void NoteNamesBox_Loaded(object sender, RoutedEventArgs e)
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
