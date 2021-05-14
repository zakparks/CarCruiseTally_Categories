using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

//TODO - add inputs for children's choice votes

namespace CarCruiseTally
{
    /// <summary>
    /// Interaction logic for InputForm.xaml
    /// </summary>
    public partial class InputForm
    {
        /// <summary>
        ///  list of all the ballots submitted
        /// </summary>
        private readonly List<Ballot> Ballots = new List<Ballot>();

        /// <summary>
        /// Current ordered list of all the unique cars
        /// </summary>
        private List<int> CurrentOrderedTally { get; set; }

        #region BestInX properties
        /// <summary>
        /// Current best in the street rod category
        /// </summary>
        int BestStreetRod { get; set; } = 0;

        /// <summary>
        /// Current best in the Modern category
        /// </summary>
        int BestModern { get; set; } = 0;

        /// <summary>
        /// Current best in the Import category
        /// </summary>
        int BestImport { get; set; } = 0;

        /// <summary>
        /// Current best in the rat rod category
        /// </summary>
        int BestRatRod { get; set; } = 0;

        /// <summary>
        /// Current best in the truck category
        /// </summary>
        int BestClassicTruck { get; set; } = 0;

        /// <summary>
        /// Current best in the motorcycle category
        /// </summary>
        int BestBike { get; set; } = 0;

        /// <summary>
        /// Current best in the classic car category
        /// </summary>
        int BestClassicCar { get; set; } = 0;

        /// <summary>
        /// Current best in the four wheel drive category
        /// </summary>
        int BestFourWD { get; set; } = 0;

        /// <summary>
        /// Current best in show
        /// </summary>
        int BestInShow { get; set; } = 0;

        /// <summary>
        /// Current reserve in show, or second place behind best in show
        /// </summary>
        int ReserveInShow { get; set; } = 0;

        #endregion

        public InputForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Save input data as a ballot
        /// </summary>
        /// <param name="sender">object snder</param>
        /// <param name="e">event arguments</param>
        private void btn_submit_Click(object sender, RoutedEventArgs e)
        {
            Ballots.Add(new Ballot()
            {
                Top10 = Array.ConvertAll(txt_carNumbers.Text.Split(), int.Parse),
                StreetRod = int.Parse(txt_StreetRod.Text),
                Modern = int.Parse(txt_Modern.Text),
                Import = int.Parse(txt_Import.Text),
                RatRod = int.Parse(txt_RatRod.Text),
                ClassicTruck = int.Parse(txt_ClassicTruck.Text),
                Bike = int.Parse(txt_Bike.Text),
                ClassicCar = int.Parse(txt_ClassicCar.Text),
                FourWD = int.Parse(txt_FourWD.Text),
                BestInShow = int.Parse(txt_BestInShow.Text)
            });

            // clear the form
            ClearForm();

            // calculate current totals
            TallyCurrentBallots();
        }

        /// <summary>
        /// Calculate the cars that are in the lead based on the current ballots
        /// </summary>
        private void TallyCurrentBallots() 
        {
            // Get Bests/non top 10 votes
            List<int> bestInShowList = Ballots.Select(i => i.BestInShow).ToList()
                .GroupBy(j => j)
                .OrderByDescending(k => k.Count())
                .Select(l => l.Key)
                .ToList();
            bestInShowList.RemoveAll(m => m == 0);

            if (bestInShowList.Count() > 2)
            {
                BestInShow = bestInShowList[0];
                ReserveInShow = bestInShowList[1];
            }

            BestStreetRod = GetTopValue(Ballots.Select(i => i.StreetRod).ToList());
            BestModern = GetTopValue(Ballots.Select(i => i.Modern).ToList());
            BestImport = GetTopValue(Ballots.Select(i => i.Import).ToList());
            BestRatRod = GetTopValue(Ballots.Select(i => i.RatRod).ToList());
            BestClassicTruck = GetTopValue(Ballots.Select(i => i.ClassicTruck).ToList());
            BestBike = GetTopValue(Ballots.Select(i => i.Bike).ToList());
            BestClassicCar = GetTopValue(Ballots.Select(i => i.ClassicCar).ToList());
            BestFourWD = GetTopValue(Ballots.Select(i => i.FourWD).ToList());
                        
            // calculate car that is in the lead for general vote
            CurrentOrderedTally = Ballots.SelectMany(a => a.Top10).GroupBy(b => b).OrderByDescending(c => c.Count()).Select(d => d.Key).ToList();
            CurrentOrderedTally.RemoveAll(m => m == 0);
            
            string top10display = "";
            for (int i = 0; i < CurrentOrderedTally.Count; i++) 
            {
                // check to see if the current car has already won something, skip if true
                if (new int[] { BestInShow, ReserveInShow, BestStreetRod, BestModern, BestImport, BestRatRod, BestClassicTruck, BestBike, BestClassicCar, BestFourWD }.Contains(CurrentOrderedTally[i]))
                {
                    continue;
                }
                
                //add to the string
                top10display += CurrentOrderedTally[i] + ", ";
            }

            // add strings to the interface
            txt_curWinners.Text = top10display.Substring(0, top10display.Length - 2);
            txt_StreetRodWinner.Text = BestStreetRod.ToString();
            txt_ModernWinner.Text = BestModern.ToString();
            txt_ImportWinner.Text = BestImport.ToString();
            txt_RatRodWinner.Text = BestRatRod.ToString();
            txt_ClassicTruckWinner.Text = BestClassicTruck.ToString();
            txt_BikeWinner.Text = BestBike.ToString();
            txt_ClassicCarWinner.Text = BestClassicCar.ToString();
            txt_FourWDWinner.Text = BestFourWD.ToString();
            txt_BestInShowWinner.Text = BestInShow.ToString();
            txt_ReserveInShowWinner.Text = ReserveInShow.ToString();
        }

        private int GetTopValue(List<int> list)
        {
            list = list.GroupBy(j => j)
                .OrderByDescending(k => k.Count())
                .Select(l => l.Key).ToList();
            list.RemoveAll(m => m == 0);
            
            foreach (int i in list.Where(i => i != BestInShow || i != ReserveInShow))
            {
                return i;
            }

            return 0;
        }

        /// <summary>
        /// Clear all the input forms
        /// </summary>
        /// <param name="sender">object snder</param>
        /// <param name="e">event arguments</param>
        private void btn_clear_Click(object sender, RoutedEventArgs e) => ClearForm();

        /// <summary>
        /// Clear all the input forms
        /// </summary>
        private void ClearForm()
        {
            // clear the form
            txt_carNumbers.Clear();
            txt_StreetRod.Clear();
            txt_Modern.Clear();
            txt_Import.Clear();
            txt_RatRod.Clear();
            txt_ClassicTruck.Clear();
            txt_Bike.Clear();
            txt_ClassicCar.Clear();
            txt_FourWD.Clear();
            txt_BestInShow.Clear();

            // reset focus
            txt_carNumbers.Focus();
        }

        /// <summary>
        /// Saves all the ballot data to a .csv file
        /// </summary>
        /// <param name="sender">object snder</param>
        /// <param name="e">event arguments</param>
        private void btn_export_Click(object sender, RoutedEventArgs e)
        {
            // create the save file dialog
            StringBuilder sb = new StringBuilder();
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Comma Seperated Values (*.csv)|*.csv",
                RestoreDirectory = true
            };

            // add the header line with the column titles
            sb.AppendLine("Street Rod,Modern,Import,Rat Rod,Classic Truck,Bike,Classic Car,4wd,Best in Show,Top 10");

            // build all the rows for the csv
            foreach (Ballot bal in Ballots)
            {
                sb.AppendLine(bal.StreetRod + "," +
                    bal.Modern + "," +
                    bal.Import + "," +
                    bal.RatRod + "," +
                    bal.ClassicTruck + "," +
                    bal.Bike + "," +
                    bal.ClassicCar + "," +
                    bal.FourWD + "," +
                    bal.BestInShow + "," +
                    string.Join(" ", bal.Top10.Select(i => i.ToString())));
            }

            // open the save file dialog 
            sfd.ShowDialog();

            // save file
            if (sfd.FileName != "")
            {
                using (StreamWriter sw = new StreamWriter(sfd.FileName))
                {
                    sw.Write(sb);
                }
            }
        }

        /// <summary>
        /// Exit the program
        /// </summary>
        /// <param name="sender">object snder</param>
        /// <param name="e">event arguments</param>
        private void btn_exit_Click(object sender, RoutedEventArgs e) => Environment.Exit(0);

        /// <summary>
        /// Update the interface and recalculate the ballots
        /// </summary>
        /// <param name="sender">object snder</param>
        /// <param name="e">event arguments</param>
        private void btn_update_Click(object sender, RoutedEventArgs e) => TallyCurrentBallots();

        /// <summary>
        /// Remove the last ballot entered
        /// </summary>
        /// <param name="sender">object snder</param>
        /// <param name="e">event arguments</param>
        private void btn_undoPrev_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Delete the previously entered ballot?","Undo", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                Ballots.Remove(Ballots.Last());
            }
        }

        /// <summary>
        /// Trims trailing whitespace after a textbox is left
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="routedEventArgs"></param>
        private void txt_lostFocus(object sender, RoutedEventArgs routedEventArgs)
        {
            TextBox tx = (TextBox)sender;
            tx.Text = tx.Text.TrimEnd(' ');
            tx.Text = tx.Text.Replace("  ", " ");
        }
    }
}
