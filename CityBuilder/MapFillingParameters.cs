using System;
using System.Collections.Generic;

namespace CityBuilder
{
    public class MapFillingParameters
    {
        public decimal MapFillFactor { get; set; }
        public Dictionary<Type, int> BuildingTypesCounts { get; set; }
    }
}