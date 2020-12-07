using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class DTO_CalVal
    {
        public List<int> CalReference { get; set; }
        public List<double> CalMeasured { get; set; }
        public double R2 { get; set; }
        public double A { get; set; } //slope
        public double B { get; set; } //y-intercept
        public double Zv { get; set; }
       
        public DateTime Datetime { get; set; }

        public DTO_CalVal(List<int> calReference, List<double> calMeasured, double r2,double a, double b, double zv,DateTime datetime)
        {
            CalReference = calReference;//Værdi valgt på udstyr
            CalMeasured = calMeasured;//Værdi vi får tilbage i Volt
            R2 = r2;//Linearteten
           A = a;//Dette er gangefaktoren fra kalibreringen
            B = b;//Dette er starten på y aksen.
            Zv = zv;//Nulpunktsjusteringen
            Datetime = datetime;
        }
    }
}
