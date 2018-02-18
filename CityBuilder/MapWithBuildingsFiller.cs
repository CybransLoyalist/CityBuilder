using CityBuilding;

namespace CityBuilder
{
    public class MapWithBuildingsFiller
    {
        private readonly EmptyAreaGroupGetter _emptyAreaGroupGetter;
        private readonly StreetsAppender _streetsAppender;
        private readonly AreaWithBuildingFiller _areaWithBuildingFiller;

        public MapWithBuildingsFiller(EmptyAreaGroupGetter emptyAreaGroupGetter,
            StreetsAppender streetsAppender, AreaWithBuildingFiller areaWithBuildingFiller)
        {
            _emptyAreaGroupGetter = emptyAreaGroupGetter;
            _streetsAppender = streetsAppender;
            _areaWithBuildingFiller = areaWithBuildingFiller;
        }

        public virtual void FillMap(IMap initialMap)
        {
            var emptyAreas = _emptyAreaGroupGetter.Get(initialMap);

            var streets = _streetsAppender.AppendStreets(initialMap, emptyAreas);

            foreach (var emptyAreaGroup in emptyAreas)
            {
                _areaWithBuildingFiller.Fill(initialMap, emptyAreaGroup);
            }
        }

    }
}
