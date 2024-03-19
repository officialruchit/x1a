namespace Skillup.TaxCalculator.Model
{
    /// <summary>
    /// Tax calculating information for men
    /// </summary>
    public class MaleTaxPayer : ITaxPayer
    {
        #region Properties
        /// <summary>
        /// Maximum amount for slab 1
        /// </summary>
        public decimal FirstSlabUpperLimit { get; set; }

        /// <summary>
        /// Maximum amount for slab 2
        /// </summary>
        public decimal SecondSlabUpperLimit { get; set; }

        /// <summary>
        /// Maximum amount for slab 3
        /// </summary>
        public decimal ThirdSlabUpperLimit { get; set; }

        /// <summary>
        /// Interest rate of slab 1
        /// </summary>
        public int FirstSlabTaxRate { get; set; }

        /// <summary>
        /// Interest rate of slab 2
        /// </summary>
        public int SecondSlabTaxRate { get; set; }

        /// <summary>
        /// Interest rate of slab 3
        /// </summary>
        public int ThirdSlabTaxRate { get; set; }

        /// <summary>
        /// Interest rate of slab 4
        /// </summary>
        public int FourthSlabTaxRate { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Instance of Male taxPayer object
        /// </summary>
        /// <param name="slabUpperLimitsForMale"> Slab uppper limits for male </param>
        /// <param name="slabTaxRatesForMale"> Slab tax rates for male  </param>
        public MaleTaxPayer(decimal[] slabUpperLimitsForMale, int[] slabTaxRatesForMale)
        {
            FirstSlabUpperLimit = slabUpperLimitsForMale[0];
            SecondSlabUpperLimit = slabUpperLimitsForMale[1];
            ThirdSlabUpperLimit = slabUpperLimitsForMale[2];

            FirstSlabTaxRate = slabTaxRatesForMale[0];
            SecondSlabTaxRate = slabTaxRatesForMale[1];
            ThirdSlabTaxRate = slabTaxRatesForMale[2];
            FourthSlabTaxRate = slabTaxRatesForMale[3];
        }
        #endregion
    }
}
