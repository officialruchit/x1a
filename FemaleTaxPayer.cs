namespace Skillup.TaxCalculator.Model
{
    /// <summary>
    /// Tax calculating information for men
    /// </summary>
    public class FemaleTaxPayer : ITaxPayer
    {
        #region Properties
        /// <summary>
        /// Maximum amount for slab 1
        /// </summary>
        public decimal FirstSlabUpperLimit { get; set; }

        /// <summary>
        /// Maximum amount for slab 1
        /// </summary>
        public decimal SecondSlabUpperLimit { get; set; }

        /// <summary>
        /// Maximum amount for slab 1
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
        /// Instance of Female taxPayer object
        /// </summary>
        /// <param name="slabUpperLimitsForFemale"> Slab uppper limits for female </param>
        /// <param name="slabTaxRatesForFemale"> Slab tax rates for female  </param>
        public FemaleTaxPayer(decimal[] slabUpperLimitsForFemale, int[] slabTaxRatesForFemale)
        {
            FirstSlabUpperLimit = slabUpperLimitsForFemale[0];
            SecondSlabUpperLimit = slabUpperLimitsForFemale[1];
            ThirdSlabUpperLimit = slabUpperLimitsForFemale[2];

            FirstSlabTaxRate = slabTaxRatesForFemale[0];
            SecondSlabTaxRate = slabTaxRatesForFemale[1];
            ThirdSlabTaxRate = slabTaxRatesForFemale[2];
            FourthSlabTaxRate = slabTaxRatesForFemale[3];
        }
        #endregion
    }
}
