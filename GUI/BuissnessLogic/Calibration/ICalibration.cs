using System;
using System.Collections.Generic;
using DTO;
using DataAccessLogic;

namespace BuissnessLogic
{
    public interface ICalibration
    {
        public double CalculateR2Val(List<int> calReference, List<double> calMeasured,double r2);

        public List<DTO_CalVal> SaveCalval(List<int> calReference, List<double> calMeasured, double r2, double a, double b,
            double zv, DateTime datetime);


        public double GetZeroVal();
        public double GetCalibration();

        public List<DTO_CalVal> CalculateAAndB(List<int> calReference, List<double> calMeasured, double r2, double a,
            double b, double zv);

        
    }
}