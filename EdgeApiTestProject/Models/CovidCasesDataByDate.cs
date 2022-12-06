using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdgeApiTestProject.Models
{
    public class CovidCasesDataByDate
    {
        public string? id { get; set; } 
        public string? symbol { get; set; }
        public string Country { get; set; }
        public string Continent { get; set; }  
        public string date { get; set; }
        public int total_case { get; set; }
        public int new_cases { get; set; }
        public int total_deaths { get; set; }
        public int new_deathS { get; set; }
        public int total_tests { get; set; }
        public int new_tests { get; set; }
    }
}
