namespace Skillup.TaxCalculator.Model
{

    /// <summary>
    /// Taxpayer interface
    /// </summary>
    public interface ITaxPayer
    {
        #region Properties
        /// <summary>
        /// Maximum amount for slab 1
        /// </summary>
        decimal FirstSlabUpperLimit { get; set; }

        /// <summary>
        /// Maximum amount for slab 2
        /// </summary>
        decimal SecondSlabUpperLimit { get; set; }

        /// <summary>
        /// Maximum amount for slab 3
        /// </summary>
        decimal ThirdSlabUpperLimit { get; set; } 

        /// <summary>
        /// Interest rate of slab 1
        /// </summary>
        int FirstSlabTaxRate { get; set; } 

        /// <summary>
        /// Interest rate of slab 2
        /// </summary>
        int SecondSlabTaxRate { get; set; } 

        /// <summary>
        /// Interest rate of slab 3
        /// </summary>
        int ThirdSlabTaxRate { get; set; } 

        /// <summary>
        /// Interest rate of slab 4
        /// </summary>
        int FourthSlabTaxRate { get; set; }
        #endregion
    }
}
