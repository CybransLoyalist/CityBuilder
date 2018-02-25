using CityBuilder.Buildings;

namespace CityBuilder.AreaWithBuildingFilling
{
    public class RandomBuildingLocationGenerator
    {
        public BuildingLocation Generate(
            TileAnglesCombinations tileAnglesCombinations,
            BuildingTypesOfEqualSize buildingTypesOfEqualSize)
        {
            var randomTile = tileAnglesCombinations.GetRandomTile();
            var angle = tileAnglesCombinations.GetRandomAngleForTile(randomTile); 
            var type = buildingTypesOfEqualSize.GetRandomType();

            return new BuildingLocation(type, randomTile, angle);
        }
    }
}