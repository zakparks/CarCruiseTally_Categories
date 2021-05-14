namespace CarCruiseTally
{
    /// <summary>
    /// Ballot object containing all of the data that can be entered for a ballot
    /// </summary>
    public class Ballot
    {
        /// <summary>
        /// List of the top 10 cars in the show
        /// </summary>
        public int[] Top10 { get; set; }

        /// <summary>
        /// Voter's choice for best Streetrod
        /// </summary>
        public int StreetRod { get; set; } = 0;

        /// <summary>
        /// Voter's choice for best modern car
        /// </summary>
        public int Modern { get; set; } = 0;

        /// <summary>
        /// Voter's choice for best import
        /// </summary>
        public int Import { get; set; } = 0;

        /// <summary>
        /// Voter's choice for best rat rod
        /// </summary>
        public int RatRod { get; set; } = 0;

        /// <summary>
        /// Voter's choice for best classic truck
        /// </summary>
        public int ClassicTruck { get; set; } = 0;

        /// <summary>
        /// Voter's choice for best bike
        /// </summary>
        public int Bike { get; set; } = 0;

        /// <summary>
        /// Voter's choice for best Classic car
        /// </summary>
        public int ClassicCar { get; set; } = 0;

        /// <summary>
        /// Voter's choice for best 4wd
        /// </summary>
        public int FourWD { get; set; } = 0;

        /// <summary>
        /// Voter's choice for best in show
        /// </summary>
        public int BestInShow { get; set; } = 0;
    }
}
