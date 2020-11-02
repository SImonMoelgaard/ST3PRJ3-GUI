using System.Collections.Generic;
using DTO;
using DataAccessLogic;

namespace BuissnessLogic
{
    public interface ICalibration
    {
        public List<DTO_CalVal> GetCalVal()
        {
            return GetCalVal();
        }
        public void calculateR2Val(){}


    }
}