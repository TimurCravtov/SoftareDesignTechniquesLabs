using System.Drawing;
using Laboratory.Characters;
using Laboratory.Characters.Enemies;
using Laboratory.Game;
using Laboratory.InputController;
using Laboratory.Renderer;
using Laboratory.Reports;

namespace Laboratory
{
    internal static class Program
    {
        private static void Main()
        {
            Console.CursorVisible = false;
            Console.Clear();

            var robotType = new CharacterType(10, [".\\", "\\_/"], "Robot");
            var playerType = new CharacterType(20, ["(o-o)", " /П\\", "  л"], "Player");
            
            
            var entityRenderer = new EntityRenderer();

            var player = new Player(playerType, new Point(10, 10));

            // create enemies directly
            var robot1 = new MrBob(robotType, new Point(5, 5), player);

            List<GameEntity> entities = new() { player, robot1 };

            // add a RandomShooterEnemy (UngaBunga) that moves randomly and shoots symbols
            var shooterType = new CharacterType(8, ["(x-x)"], "Shooter");
            var shooter = new UngaBunga(shooterType, new Point(15, 5), entities);
            entities.Add(shooter);

            var bulletPool = new Laboratory.GameEntities.Ammo.BulletPoolManager();

            var playerController = new PlayerController(player, entities, bulletPool);

            // GameMap map = new(40, 20);
            // new MapRenderer(map).Draw();

            var gameLoop = new GameLoop(entities, entityRenderer, playerController);
            gameLoop.Run();
        }
    }
}