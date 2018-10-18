namespace CarCruiseTally
{
    /// <summary>
    /// Ballot object containing all of the data that can be entered for a ballot
    /// </summary>
    public class Ballot
    {
        /// <summary>
        /// List of the top 20 cars in the show
        /// </summary>
        public int[] Top20 { get; private set; }

        /// <summary>
        /// Voter's choice for best Streetrod
        /// </summary>
        public int StreedRod { get; private set; }

        /// <summary>
        /// Voter's choice for best modern car
        /// </summary>
        public int Modern { get; private set; }

        /// <summary>
        /// Voter's choice for best import
        /// </summary>
        public int Import { get; private set; }

        /// <summary>
        /// Voter's choice for best rat rod
        /// </summary>
        public int RatRod { get; private set; }

        /// <summary>
        /// Voter's choice for best classic truck
        /// </summary>
        public int ClassicTruck { get; private set; }

        /// <summary>
        /// Voter's choice for best bike
        /// </summary>
        public int Bike { get; private set; }

        /// <summary>
        /// Voter's choice for best Classic car
        /// </summary>
        public int ClassicCar { get; private set; }

        /// <summary>
        /// Voter's choice for best 4wd
        /// </summary>
        public int FourWD { get; private set; }

        /// <summary>
        /// Voter's choice for best in show
        /// </summary>
        public int BestInShow { get; private set; }

        /// <summary>
        /// Constructs a new <see cref="Ballot"/>
        /// </summary>
        /// <param name="top20"><see cref="Top20"/></param>
        /// <param name="streedrod"><see cref="StreedRod"/></param>
        /// <param name="modern"><see cref="Modern"/></param>
        /// <param name="import"><see cref="Import"/></param>
        /// <param name="ratrod"><see cref="RatRod"/></param>
        /// <param name="classictruck"><see cref="ClassicTruck"/></param>
        /// <param name="bike"><see cref="Bike"/></param>
        /// <param name="classiccar"><see cref="ClassicCar"/></param>
        /// <param name="fourwd"><see cref="FourWD"/></param>
        /// <param name="bestinshow"><see cref="BestInShow"/></param>
        public Ballot(int[] top20, int streedrod, int modern, int import, int ratrod, int classictruck, int bike, int classiccar, int fourwd, int bestinshow)
        {
            Top20 = top20;
            StreedRod = streedrod;
            Modern = modern;
            Import = import;
            RatRod = ratrod;
            ClassicTruck = classictruck;
            Bike = bike;
            ClassicCar = classiccar;
            FourWD = fourwd;
            BestInShow = bestinshow;
        }
    }
}
