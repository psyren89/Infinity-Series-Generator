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

            //0 and 1 are always the same in the series and don't adhere to the formula. They are calculated ahead of the other entries.
            numbers[0] = 0;
            if (start <= 0)
            {
                notes[0] = Generator.GetSeriesEntry(0, startNoteIndex + numbers[0], ref octave);
            }

            numbers[1] = 1;
            if (start <= 1)
            {
                notes[1] = Generator.GetSeriesEntry(1, startNoteIndex + numbers[1], ref octave);
            }

            Generator.GenerateSeries(startNoteIndex, total, start, numbers, notes, j);
            return notes;
        }

        private static void GenerateSeries(int startNoteIndex, int total, int start, IList<int> numbers, IList<string> notes, int j)
        {
            var limit = start + total + 1;
            for (var i = 1; i < limit; i++)
            {
                var octave = 3;
                var index = 2 * i;
                var first = index - 2;
                var second = index - 1;
                var previous = i - 1;

                //breaks if at the end
                var nextIndex = index + 1;
                if (index >= start + total || nextIndex >= start + total)
                {
                    break;
                }

                //first (even) number entry
                numbers[index] = numbers[first] - (numbers[i] - numbers[previous]);

                if (index >= start)
                {
                    notes[2 * j] = Generator.GetSeriesEntry(index, numbers[index] + startNoteIndex, ref octave);
                }

                //reset octave for next note
                octave = 3;

                //second (odd) number entry
                numbers[nextIndex] = numbers[second] + (numbers[i] - numbers[previous]);

                if (nextIndex >= start)
                {
                    notes[2 * j + 1] = Generator.GetSeriesEntry(nextIndex, numbers[nextIndex] + startNoteIndex, ref octave);
                    j++;
                }
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

        /// <summary>
        /// Calculates the next note in the series in the format [name][octave].
        /// </summary>
        private static string GetNoteName(int nextNoteIndex, ref int octave)
        {
            var noteIndex = Generator.GetNextNoteIndex(nextNoteIndex, ref octave);
            var note = Generator.Notes[noteIndex];
            return string.Format("{0}{1}", note, octave);
        }

        private static string GetSeriesEntry(int seriesResultIndex, int nextNoteIndex, ref int octave)
        {
            return string.Format("{0} = {1}", seriesResultIndex, Generator.GetNoteName(nextNoteIndex, ref octave));
        }
    }
}
