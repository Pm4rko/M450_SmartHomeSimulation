using M320_SmartHome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M320_SmartHome
{
    public interface IWettersensor
    {
        public Wetterdaten GetWetterdaten();
    }
}
