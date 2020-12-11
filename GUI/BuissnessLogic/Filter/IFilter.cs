using System.Collections.Generic;
using DTO;

namespace BuissnessLogic
{
    public interface IFilter
    {
        public List<DTO_Measurement> GetMeasurementDataFilter();
    }
}