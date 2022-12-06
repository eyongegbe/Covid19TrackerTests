namespace EdgeApiTestProject.Models
{
    public class CountryCovidData
    {

        public string id { get; set; }
        public int rank { get; set; }
        public string Country { get; set; }
        public string Continent { get; set; }
        public string TwoLetterSymbol { get; set; }
        public string ThreeLetterSymbol { get; set; }
        public double Infection_Risk { get; set; }
        public double Case_Fatality_Rate { get; set; }
        public double Test_Percentage { get; set; }
        public double Recovery_Proporation { get; set; }
        public int TotalCases { get; set; }
        public int NewCases { get; set; }
        public int TotalDeaths { get; set; }
        public int NewDeaths { get; set; }
        public string TotalRecovered { get; set; }
        public int NewRecovered { get; set; }
        public int ActiveCases { get; set; }

        public string TotalTests { get; set; }
        public string Population { get; set; }
        public int one_Caseevery_X_ppl { get; set; }
        public int one_Deathevery_X_ppl { get; set; }
        public int one_Testevery_X_ppl { get; set; }
        public int Deaths_1M_pop { get; set; }
        public int Serious_Critical { get; set; }
        public int Tests_1M_Pop { get; set; }
        public int TotCases_1M_Pop { get; set; }


    }
}
