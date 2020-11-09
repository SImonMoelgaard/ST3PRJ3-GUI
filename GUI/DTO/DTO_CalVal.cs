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
        public int B { get; set; } //y-intercept
        public double Zv { get; set; }

        public DTO_CalVal(List<int> calReference, List<double> calMeasured, double r2,double a, int b, double zv)
        {
            CalReference = calReference;
            CalMeasured = calMeasured;
            R2 = r2;
            A = a;
            B = b;
            Zv = zv;
        }
    }
}
