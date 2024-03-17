using System;
using System.Globalization;

namespace Tax_calculator
{
    public class Utility
    {
        public bool IsValidName(string name, out string errorCode)
        {
            errorCode = null;
            if (string.IsNullOrWhiteSpace(name))
            {
                errorCode = "E02";
                return false;
            }
            if (name.Length > 50)
            {
                errorCode = "E01";
                return false;
            }
            if (!IsAlpha(name))
            {
                errorCode = "E03";
                return false;
            }
            // Additional validation logic if needed
            return true;
        }

        public bool IsValidDOB(string dob, out int age, out string errorCode)
        {
            errorCode = null;
            if (string.IsNullOrWhiteSpace(dob))
            {
                errorCode = "E06";
                age = 0;
                return false;
            }
            if (!DateTime.TryParseExact(dob, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateOfBirth) || dateOfBirth.Year < 1900 || dateOfBirth.Year > 2010)
            {
                errorCode = "E04";
                age = 0;
                return false;
            }
            // Calculate age
            age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age -= 1;
            if (age < 0)
            {
                errorCode = "E05";
                return false;
            }
            // Additional validation logic if needed
            return true;
        }

        public bool IsValidGender(char gender, out string errorCode)
        {
            errorCode = null;
            if (char.ToUpper(gender) != 'M' && char.ToUpper(gender) != 'F')
            {
                errorCode = "E08";
                return false;
            }
            // Additional validation logic if needed
            return true;
        }

        public bool IsValidIncome(string incomeStr, out decimal income, out string errorCode)
        {
            errorCode = null;
            if (string.IsNullOrWhiteSpace(incomeStr))
            {
                errorCode = "E09";
                income = 0;
                return false;
            }
            if (!decimal.TryParse(incomeStr, out income) || income < 0)
            {
                errorCode = "E07";
                return false;
            }
            // Additional validation logic if needed
            return true;
        }

        public bool IsValidInvestment(string investmentStr, decimal totalIncome, out decimal investment, out string errorCode)
        {
            errorCode = null;
            if (string.IsNullOrWhiteSpace(investmentStr))
            {
                errorCode = "E10";
                investment = 0;
                return false;
            }
            if (!decimal.TryParse(investmentStr, out investment) || investment < 0 || investment > totalIncome)
            {
                errorCode = "E07";
                return false;
            }
            // Additional validation logic if needed
            return true;
        }

        public bool IsValidHomeLoan(string homeLoanStr, decimal totalIncome, decimal totalInvestment, out decimal homeLoan, out string errorCode)
        {
            errorCode = null;
            if (string.IsNullOrWhiteSpace(homeLoanStr))
            {
                errorCode = "E11";
                homeLoan = 0;
                return false;
            }
            if (!decimal.TryParse(homeLoanStr, out homeLoan) || homeLoan < 0 || homeLoan + totalInvestment > totalIncome)
            {
                errorCode = "E07";
                return false;
            }
            // Additional validation logic if needed
            return true;
        }

        private bool IsAlpha(string input)
        {
            foreach (char c in input)
            {
                if (!char.IsLetter(c))
                    return false;
            }
            return true;
        }
    }
}

namespace Tax_calculator
{
    public class InvestmentInfo
    {
        public decimal Income { get; set; }
        public decimal Investment { get; set; }
        public decimal HomeLoan { get; set; }

        public InvestmentInfo(decimal income, decimal investment, decimal homeLoan)
        {
            Income = income;
            Investment = investment;
            HomeLoan = homeLoan;
        }
    }
}

namespace Tax_calculator
{
    public class TaxCalcForSenior : Tax_calculator
    {
        public TaxCalcForSenior(decimal slab1Amount, decimal slab2Amount, decimal slab3Amount, int slab2TaxRate, int slab3TaxRate, int slab4TaxRate)
            : base(slab1Amount, slab2Amount, slab3Amount, slab2TaxRate, slab3TaxRate, slab4TaxRate)
        {
            // Additional initialization specific to TaxCalcForMale if needed
        }
    }
}

namespace Tax_calculator
{
    public class TaxCalcForFemale : Tax_calculator
    {
        public TaxCalcForFemale(decimal slab1Amount, decimal slab2Amount, decimal slab3Amount, int slab2TaxRate, int slab3TaxRate, int slab4TaxRate)
            : base(slab1Amount, slab2Amount, slab3Amount, slab2TaxRate, slab3TaxRate, slab4TaxRate)
        {
            // Additional initialization specific to TaxCalcForFemale if needed
        }
    }
}

namespace Tax_calculator
{
    public class TaxCalcForMale : Tax_calculator
    {
        // Constructor
        public TaxCalcForMale(decimal slab1Amount, decimal slab2Amount, decimal slab3Amount, int slab2TaxRate, int slab3TaxRate, int slab4TaxRate)
            : base(slab1Amount, slab2Amount, slab3Amount, slab2TaxRate, slab3TaxRate, slab4TaxRate) { }
    }
}

namespace Tax_calculator
{
    public class Tax_calculator : InvestmentInfo
    {
        public Tax_calculator(decimal income, decimal investment, decimal homeLoan,
            int slab2TaxRate, int slab3TaxRate, int slab4TaxRate) : base(income, investment, homeLoan)
        {
        }
        public decimal NonTaxableAmount { get; set; }
        public decimal TaxableAmount { get; set; }
        public decimal TotalTax { get; set; }

        public decimal Slab1Amount { get; set; }
        public decimal Slab2Amount { get; set; }
        public decimal Slab3Amount { get; set; }

        public int Slab2TaxRate { get; set; }
        public int Slab3TaxRate { get; set; }
        public int Slab4TaxRate { get; set; }

        // Method to calculate tax details
        public string CalculateTaxDetails(PersonalInfo personalInfo, InvestmentInfo investmentInfo, out Tax_calculator taxInfo)
        {
            taxInfo = new Tax_calculator(); // Initialize taxInfo (may not be needed depending on your implementation)

            // Initialize error code to empty string
            string errorCode = "";
            Utility utility = new Utility();
            // Check for errors in PersonalInfo
            if (string.IsNullOrWhiteSpace(personalInfo.PersonName))
                errorCode = "E02"; // Input not given for "name"
            else if (!utility.IsValidName(personalInfo.PersonName, out errorCode))
                errorCode = "E03"; // Input contains invalid characters
            else if (!utility.IsValidDOB(personalInfo.DOB, out int age, out errorCode))
                errorCode = "E04"; // Date format is invalid
            else if (!IsBirthYearInRange(personalInfo.DOB))
                errorCode = "E05"; // Birth year is out of range
            else if (string.IsNullOrWhiteSpace(personalInfo.DOB))
                errorCode = "E06"; // Input not given for "date of birth"
            else if (!utility.IsValidGender(personalInfo.Gender, out errorCode))
                errorCode = "E07"; // Invalid input for "gender"

            // Check for errors in InvestmentInfo
            else if (investmentInfo.Income <= 0)
                errorCode = "E09"; // Input not given for "income"
            else if (investmentInfo.Investment <= 0)
                errorCode = "E10"; // Input not given for "investment"
            else if (investmentInfo.HomeLoan <= 0)
                errorCode = "E11"; // Input not given for "house loan/rent"
            else if (investmentInfo.Investment > investmentInfo.Income)
                errorCode = "E12"; // Investment cannot be more than income
            else if (investmentInfo.Investment + investmentInfo.HomeLoan > investmentInfo.Income)
                errorCode = "E13"; // Investment combined with house loan/rent cannot be more than income

            // If no errors found, calculate tax details
            if (string.IsNullOrEmpty(errorCode))
            {
                // Calculate taxable amount
                decimal taxableAmount = CalcTaxableAmount(investmentInfo, out decimal nonTaxableAmount);

                // Calculate total tax
                decimal totalTax = CalcTotalTax(taxableAmount);

                // Return tax details or store them in taxInfo (depending on your implementation)
                return $"Tax details calculated successfully. Total tax: {totalTax}";
            }
            else
            {
                // Return error code
                return errorCode;
            }
        }

        // Internal method to calculate taxable amount
        private decimal CalcTaxableAmount(InvestmentInfo investmentInfo, out decimal nonTaxableAmount)
        {
            // Calculate non-taxable amount as 80% of home loan exemption
            decimal homeLoanExemption = 0.8m * investmentInfo.HomeLoan;

            // If valid investment is less than or equal to 100,000, add it to the non-taxable amount
            if (investmentInfo.Investment <= 100000)
            {
                nonTaxableAmount = homeLoanExemption + investmentInfo.Investment;
            }
            else
            {
                nonTaxableAmount = homeLoanExemption + 100000;
            }

            // Assuming starting with 0 taxable amount
            decimal taxableAmount = 0;

            // Calculate taxable amount
            taxableAmount = investmentInfo.Income - nonTaxableAmount;

            return taxableAmount;
        }

        private decimal CalcTotalTax(decimal taxableAmount)
        {
            decimal totalTax = 0;

            // Tax slabs for men
            decimal[,] menTaxSlabs = {
                {160000, 0},        // No tax up to 1,60,000
                {300000, 0.10m},    // 10% tax for income between 1,60,001 to 3,00,000
                {500000, 0.20m},    // 20% tax for income between 3,00,001 to 5,00,000
                {decimal.MaxValue, 0.30m}   // 30% tax for income above 5,00,001
            };

            // Tax slabs for women
            decimal[,] womenTaxSlabs = {
                {190000, 0},        // No tax up to 1,90,000
                {300000, 0.10m},    // 10% tax for income between 1,90,001 to 3,00,000
                {500000, 0.20m},    // 20% tax for income between 3,00,001 to 5,00,000
                {decimal.MaxValue, 0.30m}   // 30% tax for income above 5,00,001
            };

            // Tax slabs for senior citizens
            decimal[,] seniorCitizenTaxSlabs = {
                {240000, 0},        // No tax up to 2,40,000 for senior citizens
                {300000, 0.10m},    // 10% tax for income between 2,40,001 to 3,00,000
                {500000, 0.20m},    // 20% tax for income between 3,00,001 to 5,00,000
                {decimal.MaxValue, 0.30m}   // 30% tax for income above 5,00,001
            };

            // Check the taxable amount against each slab and calculate tax
            if (taxableAmount <= menTaxSlabs[0, 0])
            {
                totalTax = taxableAmount * menTaxSlabs[0, 1]; // No tax up to a certain limit
            }
            else if (taxableAmount <= menTaxSlabs[1, 0])
            {
                totalTax = menTaxSlabs[0, 0] * menTaxSlabs[0, 1] + (taxableAmount - menTaxSlabs[0, 0]) * menTaxSlabs[1, 1]; // Tax for the first slab
            }
            else if (taxableAmount <= menTaxSlabs[2, 0])
            {
                totalTax = menTaxSlabs[0, 0] * menTaxSlabs[0, 1] + (menTaxSlabs[1, 0] - menTaxSlabs[0, 0]) * menTaxSlabs[1, 1] + (taxableAmount - menTaxSlabs[1, 0]) * menTaxSlabs[2, 1]; // Tax for the second slab
            }
            else
            {
                totalTax = menTaxSlabs[0, 0] * menTaxSlabs[0, 1] + (menTaxSlabs[1, 0] - menTaxSlabs[0, 0]) * menTaxSlabs[1, 1] + (menTaxSlabs[2, 0] - menTaxSlabs[1, 0]) * menTaxSlabs[2, 1] + (taxableAmount - menTaxSlabs[2, 0]) * menTaxSlabs[3, 1]; // Tax for the third slab
            }

            return totalTax;
        }
    }
}

namespace Tax_calculator
{
    public class PersonalInfo
    {
        public string PersonName { get; set; }
        public string DOB { get; set; }
        public char Gender { get; set; }

        // Constructor
        public PersonalInfo(string personName, string dob, char gender)
        {
            PersonName = personName;
            DOB = dob;
            Gender = gender;
        }
    }
}

namespace Tax_calculator
{
    public class Program
    {
        static void Main(string[] args)
        {
            Utility utility = new Utility();
            Tax_calculator taxCalculator = new Tax_calculator();

            // Test Cases
            Console.WriteLine("Test Cases:");

            // Test Case 1: Valid input
            Console.WriteLine("Test Case 1:");
            PersonalInfo personalInfo1 = new PersonalInfo("John Doe", "1990/01/01", 'M');
            InvestmentInfo investmentInfo1 = new InvestmentInfo(500000, 200000, 100000);
            string result1 = taxCalculator.CalculateTaxDetails(personalInfo1, investmentInfo1, out Tax_calculator taxInfo1);
            Console.WriteLine(result1);

            // Test Case 2: Invalid name
            Console.WriteLine("Test Case 2:");
            PersonalInfo personalInfo2 = new PersonalInfo("", "1990/01/01", 'M');
            InvestmentInfo investmentInfo2 = new InvestmentInfo(500000, 200000, 100000);
            string result2 = taxCalculator.CalculateTaxDetails(personalInfo2, investmentInfo2, out Tax_calculator taxInfo2);
            Console.WriteLine(result2);

            // Test Case 3: Invalid date of birth
            Console.WriteLine("Test Case 3:");
            PersonalInfo personalInfo3 = new PersonalInfo("Jane Doe", "1990/13/01", 'F');
            InvestmentInfo investmentInfo3 = new InvestmentInfo(500000, 200000, 100000);
            string result3 = taxCalculator.CalculateTaxDetails(personalInfo3, investmentInfo3, out Tax_calculator taxInfo3);
            Console.WriteLine(result3);

            // Test Case 4: Invalid gender
            Console.WriteLine("Test Case 4:");
            PersonalInfo personalInfo4 = new PersonalInfo("Alice", "1990/01/01", 'X');
            InvestmentInfo investmentInfo4 = new InvestmentInfo(500000, 200000, 100000);
            string result4 = taxCalculator.CalculateTaxDetails(personalInfo4, investmentInfo4, out Tax_calculator taxInfo4);
            Console.WriteLine(result4);

            // Test Case 5: Invalid income
            Console.WriteLine("Test Case 5:");
            PersonalInfo personalInfo5 = new PersonalInfo("Bob", "1990/01/01", 'M');
            InvestmentInfo investmentInfo5 = new InvestmentInfo(-500000, 200000, 100000);
            string result5 = taxCalculator.CalculateTaxDetails(personalInfo5, investmentInfo5, out Tax_calculator taxInfo5);
            Console.WriteLine(result5);

            // Test Case 6: Invalid investment
            Console.WriteLine("Test Case 6:");
            PersonalInfo personalInfo6 = new PersonalInfo("Eve", "1990/01/01", 'F');
            InvestmentInfo investmentInfo6 = new InvestmentInfo(500000, -200000, 100000);
            string result6 = taxCalculator.CalculateTaxDetails(personalInfo6, investmentInfo6, out Tax_calculator taxInfo6);
            Console.WriteLine(result6);

            // Test Case 7: Invalid house loan
            Console.WriteLine("Test Case 7:");
            PersonalInfo personalInfo7 = new PersonalInfo("Jack", "1990/01/01", 'M');
            InvestmentInfo investmentInfo7 = new InvestmentInfo(500000, 200000, -100000);
            string result7 = taxCalculator.CalculateTaxDetails(personalInfo7, investmentInfo7, out Tax_calculator taxInfo7);
            Console.WriteLine(result7);
        }

        static bool IsBirthYearInRange(string dob)
        {
            int year = int.Parse(dob.Substring(0, 4));
            return year >= 1900 && year <= 2010;
        }
    }
}
