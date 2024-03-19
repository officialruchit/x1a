namespace Skillup.TaxCalculator.Model
{
    /// <summary>
    /// Tax calculating information for Seniour citizen
    /// </summary>
    public class SeniorCitizenTaxPayer : ITaxPayer
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
        /// Instance of Senior citizen taxPayer object
        /// </summary>
        /// <param name="slabUpperLimitsForSeniorCitizen"> Slab uppper limits for senior citizen </param>
        /// <param name="slabTaxRatesForSeniorCitizen"> Slab tax rates for senior citizen </param>
        public SeniorCitizenTaxPayer(decimal[] slabUpperLimitsForSeniorCitizen, int[] slabTaxRatesForSeniorCitizen) 
        {
            FirstSlabUpperLimit = slabUpperLimitsForSeniorCitizen[0];
            SecondSlabUpperLimit = slabUpperLimitsForSeniorCitizen[1];
            ThirdSlabUpperLimit = slabUpperLimitsForSeniorCitizen[2];

            FirstSlabTaxRate = slabTaxRatesForSeniorCitizen[0];
            SecondSlabTaxRate = slabTaxRatesForSeniorCitizen[1];
            ThirdSlabTaxRate = slabTaxRatesForSeniorCitizen[2];
            FourthSlabTaxRate = slabTaxRatesForSeniorCitizen[3];
        }
        #endregion
}
    }
