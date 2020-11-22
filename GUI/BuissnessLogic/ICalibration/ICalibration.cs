using System.Collections.Generic;
using DTO;
using DataAccessLogic;

namespace BuissnessLogic
{
    public interface ICalibration
    {
        public List<DTO_CalVal> GetCalVal();

        public void calculateR2Val();

        public List<DTO_CalVal> SaveCalval(List<int> calReference, List<double> calMeasured, double r2, double a, int b, int zv, string socSecNB);

    }
}