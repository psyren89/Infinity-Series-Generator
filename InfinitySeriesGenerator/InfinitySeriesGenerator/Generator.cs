/*
Author: Richard Gaynor
*/

namespace InfinitySeriesGenerator
{
    using System.Collections.Generic;
    using System.IO;

    using Microsoft.Win32;

    internal static class Generator
    {
        /// <summary>
        /// The notes of the scale.
        /// </summary>
        public static readonly IList<string> Notes =
            new List<string> { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" }.AsReadOnly();

        public static void GenerateSeries(int startNoteIndex, int total, int start)
        {
            var numbers = new int[start * 2 + total];
            var notes = new string[start * 2 + total];
            var octave = 3;

            //j is used to add to the notes array, as it sometimes won't be synced with the i in the for loop.
            //if j is 0, the counting in the for loop with j starts at 0. This would override the below code for 0 and 1 outside the for loop, so this changes j accordingly.
            var j = start <= 1 ? 1 : 0;

            //0 and 1 are always the same in the series, and don't adhere to the formula. So they are done before the for loop
            numbers[0] = 0;
            if (start <= 0)
            {
                notes[0] = "0 = " + Generator.Notes[Generator.TransferCheck(startNoteIndex + numbers[0], ref octave)] + octave;
            }

            numbers[1] = 1;
            if (start <= 1)
            {
                notes[1] = "1 = " + Generator.Notes[Generator.TransferCheck(startNoteIndex + numbers[1], ref octave)] + octave;
            }

            for (var i = 1; i < start + total + 1; i++)
            {
                octave = 3;
                var first = 2 * i - 2;
                var second = 2 * i - 1;
                var previous = i - 1;

                //breaks if at the end
                if (i * 2 >= start + total || i * 2 + 1 >= start + total)
                {
                    break;
                }

                //first (even) number entry
                numbers[2 * i] = numbers[first] - (numbers[i] - numbers[previous]);

                if (i * 2 >= start)
                {
                    notes[2 * j] = 2 * i + " = " + Generator.Notes[Generator.TransferCheck(numbers[2 * i] + startNoteIndex, ref octave)] + octave;
                }

                //restarts octave for next note
                octave = 3;

                //second (odd) number entry
                numbers[2 * i + 1] = numbers[second] + (numbers[i] - numbers[previous]);

                if (i * 2 + 1 >= start)
                {
                    notes[2 * j + 1] = 2 * i + 1 + " = " + Generator.Notes[Generator.TransferCheck(numbers[2 * i + 1] + startNoteIndex, ref octave)] + octave;
                    j++;
                }
            }

            Generator.WriteToFile(notes);
        }

        private static void WriteToFile(IEnumerable<string> notes)
        {
            //Saves the series as a text file
            var dialog = new SaveFileDialog
                             {
                                 FileName = "InfinitySeries",
                                 DefaultExt = ".text",
                                 Filter = "Text documents (.txt)|*.txt"
                             };

            // Show save file dialog box
            var result = dialog.ShowDialog();
            // Process save file dialog box results
            if (result.GetValueOrDefault())
            {
                // Save document
                var filename = dialog.FileName;
                File.WriteAllLines(filename, notes);
            }
        }

        /// <summary>
        /// Calculates the index into Notes of the next item in the series.
        /// Adjusts octave as required while keeping the result within the bounds (0..Notes.Count).
        /// </summary>
        private static int GetNextNoteIndex(int index, ref int octave)
        {
            var number = index;
            while (number < 0 || number > 11)
            {
                if (number >= 12)
                {
                    number -= 12;
                    ++octave;
                }
                else if (number < 0)
                {
                    number += 12;
                    --octave;
                }
            }

            return number;
        }
    }
}
