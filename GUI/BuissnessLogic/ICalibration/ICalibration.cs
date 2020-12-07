﻿using System;
using System.Collections.Generic;
using DTO;
using DataAccessLogic;

namespace BuissnessLogic
{
    public interface ICalibration
    {
        public List<DTO_CalVal> GetCalVal();

        public double CalculateR2Val(List<int> calReference, List<double> calMeasured,double r2);

        public List<DTO_CalVal> SaveCalval(List<int> calReference, List<double> calMeasured, double r2, double a, double b,
            double zv, DateTime datetime);


        public double getZeroval();
        public double getCalibration();

        public List<DTO_CalVal> CalculateAAndB(List<int> calReference, List<double> calMeasured, double r2, double a,
            double b, double zv);

        
    }
}