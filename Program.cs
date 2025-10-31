using System.Drawing;
using Laboratory.Characters;
using Laboratory.Characters.Enemies;
using Laboratory.Game;
using Laboratory.GameEntities.Ammo;
using Laboratory.InputController;
using Laboratory.Renderer;
using Laboratory.GameEntities.Items.Factories;
using Laboratory.Reports;

namespace Laboratory
{
    internal static class Program
    {
        private static void Main()
        {
            Console.CursorVisible = false;
            Console.Clear();
            
            int width = Math.Min(120, Console.LargestWindowWidth);
            int height = Math.Min(30, Console.LargestWindowHeight);
            
            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);

            var robotType = new CharacterType(10, [".\\", "\\_/"], "Robot");
            var playerType = new CharacterType(5, ["(o-o)", " /П\\", "  л"], "Player");
            
            
            var entityRenderer = new EntityRenderer();

            var itemFactory = new SciFiItemFactory();
            var menu = itemFactory.CreateMenuToRender();
            var menuRenderer = new ConsoleMenuRenderer();

            var player = new Player(playerType, new Point(10, 10));

            // register player in global GameState so UI/menu renderers can access HP
            GameState.Instance.SetPlayer(player);

            // create enemies directly
            var robot1 = new MrBob(robotType, new Point(5, 5), player);

            List<GameEntity> entities = new() { player, robot1 };

            // add a RandomShooterEnemy (UngaBunga) that moves randomly and shoots symbols
            var shooterType = new CharacterType(8, ["👻"], "Shooter");
            var shooter = new UngaBunga(shooterType, new Point(15, 5), entities);
            entities.Add(shooter);

            var bulletPool = new BulletPoolManager();

            var playerController = new PlayerController(player, entities, bulletPool);

            // GameMap map = new(40, 20);
            // new MapRenderer(map).Draw();

            var gameLoop = new GameLoop(entities, entityRenderer, playerController, menu, menuRenderer);
            gameLoop.Run();
        }
    }
}