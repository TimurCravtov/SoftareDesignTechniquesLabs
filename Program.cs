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
            var robot1 = new RobotEntity(robotType, new Point(5, 5), player);

            List<GameEntity> entities = [player, robot1];

            var playerController = new PlayerController(player);

            // GameMap map = new(40, 20);
            // new MapRenderer(map).Draw();

            var gameLoop = new GameLoop(entities, entityRenderer, playerController);
            gameLoop.Run();
        }
    }
}