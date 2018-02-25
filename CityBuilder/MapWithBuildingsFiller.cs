using CityBuilding;

namespace CityBuilder
{
    public class MapWithBuildingsFiller
    {
        private readonly EmptyAreaGroupGetter _emptyAreaGroupGetter;
        private readonly StreetsAppender _streetsAppender;
        private readonly AreaWithBuildingFiller _areaWithBuildingFiller;
        private IMap _map;

        public MapWithBuildingsFiller(
            IMap map,
            EmptyAreaGroupGetter emptyAreaGroupGetter,
            StreetsAppender streetsAppender, 
            AreaWithBuildingFiller areaWithBuildingFiller)
        {
            _map = map;
            _emptyAreaGroupGetter = emptyAreaGroupGetter;
            _streetsAppender = streetsAppender;
            _areaWithBuildingFiller = areaWithBuildingFiller;
        }

        public virtual void FillMap()
        {
            var emptyAreas = _emptyAreaGroupGetter.Get(_map);

            var streets = _streetsAppender.AppendStreets(_map, emptyAreas);

            foreach (var emptyAreaGroup in emptyAreas)
            {
                _areaWithBuildingFiller.Fill(emptyAreaGroup);
            }
        }

    }
}
