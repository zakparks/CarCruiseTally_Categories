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
        private readonly List<Ballot> _ballots = new List<Ballot>();

        /// <summary>
        /// Current ordered list of all the unique cars
        /// </summary>
        private List<int> CurrentOrderedTally { get; set; }

        /// <summary>
        /// Current best in show
        /// </summary>
        private List<int> WonOtherAward { get; set; }

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
            // get information from the UI
            if (txt_carNumbers.Text == "")
            {
                return;
            }
            
            int [] carNumbers = Array.ConvertAll(txt_carNumbers.Text.Split(), int.Parse);
            int streedrod = txt_StreetRod.Text == "" ? 0 : int.Parse(txt_StreetRod.Text);
            int modern = txt_Modern.Text == "" ? 0 : int.Parse(txt_Modern.Text);
            int import = txt_Import.Text == "" ? 0 : int.Parse(txt_Import.Text);
            int ratrod = txt_RatRod.Text == "" ? 0 : int.Parse(txt_RatRod.Text);
            int classictruck = txt_ClassicTruck.Text == "" ? 0 : int.Parse(txt_ClassicTruck.Text);
            int bike = txt_Bike.Text == "" ? 0 : int.Parse(txt_Bike.Text);
            int classiccar = txt_ClassicCar.Text == "" ? 0 : int.Parse(txt_ClassicCar.Text);
            int fourwd = txt_FourWD.Text == "" ? 0 : int.Parse(txt_FourWD.Text);
            int bestinshow = txt_BestInShow.Text == "" ? 0 : int.Parse(txt_BestInShow.Text);

            // save the data
            Ballot b = new Ballot(carNumbers, streedrod, modern, import, ratrod, classictruck, bike, classiccar, fourwd, bestinshow);
            _ballots.Add(b);

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
            // Get Bests/non top 20 votes
            List<int> bestInShowList = _ballots.Select(i => i.BestInShow).ToList().GroupBy(j => j).OrderByDescending(k => k.Count()).Select(l => l.Key).ToList();
            bestInShowList.RemoveAll(m => m == 0);

            if (bestInShowList.Count() > 2)
            {
                BestInShow = bestInShowList[0];
                ReserveInShow = bestInShowList[1];
            }

            List<int> bestStreetRodList = _ballots.Select(i => i.StreedRod).ToList().GroupBy(j => j).OrderByDescending(k => k.Count()).Select(l => l.Key).ToList();
            bestStreetRodList.RemoveAll(m => m == 0);
            BestStreetRod = GetTopValue(bestStreetRodList);

            List<int> bestModernList = _ballots.Select(i => i.Modern).ToList().GroupBy(j => j).OrderByDescending(k => k.Count()).Select(l => l.Key).ToList();
            bestModernList.RemoveAll(m => m == 0);
            BestModern = GetTopValue(bestModernList);

            List<int> bestImportList = _ballots.Select(i => i.Import).ToList().GroupBy(j => j).OrderByDescending(k => k.Count()).Select(l => l.Key).ToList();
            bestImportList.RemoveAll(m => m == 0);
            BestImport = GetTopValue(bestImportList);

            List<int> bestRatRodList = _ballots.Select(i => i.RatRod).ToList().GroupBy(j => j).OrderByDescending(k => k.Count()).Select(l => l.Key).ToList();
            bestRatRodList.RemoveAll(m => m == 0);
            BestRatRod = GetTopValue(bestRatRodList);

            List<int> bestClassicTruckList = _ballots.Select(i => i.ClassicTruck).ToList().GroupBy(j => j).OrderByDescending(k => k.Count()).Select(l => l.Key).ToList();
            bestClassicTruckList.RemoveAll(m => m == 0);
            BestClassicTruck = GetTopValue(bestClassicTruckList);

            List<int> bestBikeList = _ballots.Select(i => i.Bike).ToList().GroupBy(j => j).OrderByDescending(k => k.Count()).Select(l => l.Key).ToList();
            bestBikeList.RemoveAll(m => m == 0);
            BestBike = GetTopValue(bestBikeList);

            List<int> bestClassicCarList = _ballots.Select(i => i.ClassicCar).ToList().GroupBy(j => j).OrderByDescending(k => k.Count()).Select(l => l.Key).ToList();
            bestClassicCarList.RemoveAll(m => m == 0);
            BestClassicCar = GetTopValue(bestClassicCarList);

            List<int> bestFourWDList = _ballots.Select(i => i.FourWD).ToList().GroupBy(j => j).OrderByDescending(k => k.Count()).Select(l => l.Key).ToList();
            bestFourWDList.RemoveAll(m => m == 0);
            BestFourWD = GetTopValue(bestFourWDList);

            // Populate in the UI
            txt_BestInShowWinner.Text = BestInShow.ToString();
            txt_ReserveInShowWinner.Text = ReserveInShow.ToString();
                        
            // calculate car that is in the lead for general vote
            CurrentOrderedTally = _ballots.SelectMany(a => a.Top20).GroupBy(b => b).OrderByDescending(c => c.Count()).Select(d => d.Key).ToList();
            CurrentOrderedTally.RemoveAll(m => m == 0);

            // Get the top 20
            int top20 = 0;
            string top20display = "";
            for (int i = 0; i < CurrentOrderedTally.Count; i++) 
            {
                // check to see if the current car has already won something, skip if true
                if (new int[] { BestInShow, ReserveInShow, BestStreetRod, BestModern, BestImport, BestRatRod, BestClassicTruck, BestBike, BestClassicCar, BestFourWD }.Contains(CurrentOrderedTally[i]))
                {
                    continue;
                }
                
                //add to the string
                top20display += CurrentOrderedTally[i] + ", ";

                // insert a special character to denote that 20 cars have been selected. 
                // Sometimes things are changed after calculation and it helps to have more than 20 cars displayed
                if (i == 20)
                {
                    top20display += " | ";
                }
            }

            // add strings to the interface
            txt_curWinners.Text = top20display.Substring(0, top20display.Length - 2);
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

        /// <summary>
        /// Gets the winner of the specified list, ignoring BoS and RoS as they are priority winners
        /// </summary>
        /// <param name="list">Sorted list of votes for a particular category</param>
        /// <returns></returns>
        private int GetTopValue(List<int> list)
        {
            foreach (int i in list)
            {
                if (i == BestInShow || i == ReserveInShow)
                {
                    continue;
                }

                return i;
            }

            return 0;
        }
        
        /// <summary>
        /// Clear all the input forms
        /// </summary>
        /// <param name="sender">object snder</param>
        /// <param name="e">event arguments</param>
        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

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
            sb.AppendLine("Street Rod,Modern,Import,Rat Rod,Classic Truck,Bike,Classic Car,4wd,Best in Show,Top 20");

            // build all the rows for the csv
            foreach (Ballot bal in _ballots)
            {
                sb.AppendLine(bal.StreedRod + "," +
                    bal.Modern + "," +
                    bal.Import + "," +
                    bal.RatRod + "," +
                    bal.ClassicTruck + "," +
                    bal.Bike + "," +
                    bal.ClassicCar + "," +
                    bal.FourWD + "," +
                    bal.BestInShow + "," +
                    string.Join(" ", bal.Top20.Select(i => i.ToString())));
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
        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// Update the interface and recalculate the ballots
        /// </summary>
        /// <param name="sender">object snder</param>
        /// <param name="e">event arguments</param>
        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            TallyCurrentBallots();
        }

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
                _ballots.Remove(_ballots.Last());
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
