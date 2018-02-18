
using System;

namespace CityBuilding
{
    public class CityBuilder
    {
        private readonly EmptyAreaGroupGetter _emptyAreaGroupGetter;
        private readonly StreetsAppender _StreetsAppender;

        public CityBuilder(EmptyAreaGroupGetter emptyAreaGroupGetter,
            StreetsAppender streetsAppender)
        {
            _emptyAreaGroupGetter = emptyAreaGroupGetter;
            _StreetsAppender = streetsAppender;
        }

        public virtual void FillMap(IMap initialMap)
        {
            var building = new Building(Guid.NewGuid());


            var emptyAreas = _emptyAreaGroupGetter.Get(initialMap);

            var streets = _StreetsAppender.AppendStreets(initialMap, emptyAreas);
        }
    }
}
