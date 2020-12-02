using System;
using System.Collections.Generic;
using System.Text;

namespace BuissnessLogic
{
    public class DataContainer
    {
        private bool _highSys;

        public bool GetHighSys()
        {
            return _highSys;
        }

        public void SetHighSys(bool highSys)
        {
            _highSys = highSys;
        }
    }
}
