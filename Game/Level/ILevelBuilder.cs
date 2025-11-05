namespace Laboratory.Game;

using System.Collections.Generic;
using Laboratory.Characters;

public interface ILevelBuilder
{
	ILevelBuilder SetFactory(Laboratory.GameEntities.Items.InGameItemFactory factory);

	ILevelBuilder SetMap(GameMap map);

	ILevelBuilder SetPlayer(Player player);

	ILevelBuilder AddEnemy(Laboratory.Characters.Enemies.IEnemy enemy);

	ILevelBuilder PlaceItems(int foodCount, int powerupCount);

	ILevelBuilder AddFood(int foodCount);
	
	ILevelBuilder AddPowerup(int powerupCount);
	
	List<GameEntity> GetResult();
}