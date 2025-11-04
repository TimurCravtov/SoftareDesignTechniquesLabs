namespace Laboratory.Game;

using System.Collections.Generic;
using Laboratory.Characters;

public interface ILevelBuilder
{
	// Provide the item factory to the builder (abstract factory for themed items)
	ILevelBuilder SetFactory(Laboratory.GameEntities.Items.InGameItemFactory factory);

	// Provide the game map which defines bounds for placement
	ILevelBuilder SetMap(GameMap map);

	// Optionally set the player so enemies that require a player reference can be created
	ILevelBuilder SetPlayer(Player player);

	// Add a single enemy instance (the builder will position it appropriately)
	ILevelBuilder AddEnemy(Laboratory.Characters.Enemies.IEnemy enemy);

	// Place a number of food and powerup items
	ILevelBuilder PlaceItems(int foodCount, int powerupCount);

	ILevelBuilder AddFood(int foodCount);
	
	ILevelBuilder AddPowerup(int powerupCount);
	
	// Retrieve the built list of entities
	List<GameEntity> GetResult();
}