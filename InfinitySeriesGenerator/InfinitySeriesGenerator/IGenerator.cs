using System;
using System.Collections.Generic;

/* The main class used to create the Infinity Series, according to input.
Author: Richard Gaynor
*/
namespace InfinitySeriesGenerator
{
    class IGenerator
    {
        //holds the note names
        public static readonly IList<string> NOTE_TYPES = new List<string>
            {
                "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B"
            }.AsReadOnly();

        public int startNote = 0;
        private int octave = 3;

        //generates the notes
        public void iSeriesGenerate(int total, int start)
        {
            int[] numbers = new int[start*2 + total];
            string[] notes = new string[start*2 + total];
            int first;
            int second;
            int previous;

            //j is used to add to the notes array, as it sometimes won't be synced with the i in the for loop.
            int j;
            //if j is 0, the counting in the for loop with j starts at 0. This would override the below code for 0 and 1 outside the for loop, so this changes j accordingly.
            if (start <= 1)
            {
                j = 1;
            }
            else
            {
                j = 0;
            }

            //0 and 1 are always the same in the series, and don't adhere to the formula. So they are done before the for loop
                numbers[0] = 0;
                if (start <= 0)
                {
                    notes[0] = "0 = " + noteTypes[transferCheck(startNote + numbers[0])] + octave;
                }

                numbers[1] = 1;
                if (start <= 1)
                {
                    notes[1] = "1 = " + noteTypes[transferCheck(startNote + numbers[1])] + octave;
                }



            //the for loop to determine the infinity series
            for (int i = 1; i < start + total + 1; i++)
            {
                octave = 3;
                first = 2*i-2;
                second = 2*i -1;
                previous = i-1;

                //breaks if at the end
                if (i * 2 >= start + total || i * 2 + 1 >= start + total)
                {
                    break;
                }

                //first (even) number entry
                numbers[2*i] = numbers[first] - (numbers[i] - numbers[previous]);

                if (i*2 >= start)
                {
                    notes[2 * j] = 2 * i + " = " + noteTypes[transferCheck(numbers[2*i] + startNote)] + octave;
                }

                //restarts octave for next note
                octave = 3;

                //second (odd) number entry
                numbers[2 * i + 1] = numbers[second] + (numbers[i] - numbers[previous]);

                if (i*2 + 1 >= start)
                {
                    notes[2 * j + 1] = 2 * i + 1 + " = " + noteTypes[transferCheck(numbers[2 * i + 1] + startNote)] + octave;
                    j++;
                }



            }

            //Saves the series as a text file
            if (start >= 0)
            {
                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.FileName = "InfinitySeries"; // Default file name
                dlg.DefaultExt = ".text"; // Default file extension
                dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

                // Show save file dialog box
                Nullable<bool> result = dlg.ShowDialog();
                string filename = string.Empty;
                // Process save file dialog box results
                if (result == true)
                {
                    // Save document
                    filename = dlg.FileName;
                    System.IO.File.WriteAllLines(filename, notes);
                }
            }
        }

        //makes sure the infinity series stays within 0 and 11 (i.e. an octave)
        private int transferCheck(int j)
        {
            var number = j;
            while (number < 0 || number > 11)
            {
                if (number >= 12)
                {
                    number -= 12;
                    ++octave;
                }

                if (number < 0)
                {
                    number += 12;
                    --octave;
                }
            }

            return number;
        }
    }
}
