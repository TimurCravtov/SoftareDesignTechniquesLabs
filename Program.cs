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
            Thread.Sleep(2000);
            
            var robotType = new CharacterType(10, new[] { ".\\", "\\_/" }, "Robot");
            var playerType = new CharacterType(5, new[] { "(o-o)", " /П\\", "  л" }, "Player");
            var shooterType = new CharacterType(8, new[] { "👻" }, "Shooter");

            var entityRenderer = new EntityRenderer();

            var itemFactory = new SciFiItemFactory();
            var menu = itemFactory.CreateMenuToRender();
            var menuRenderer = new ConsoleMenuRenderer();

            var player = new Player(playerType, new Point(10, 10));
            
            
            player.Health = 2; // SET ONLY TO DEMONSTRATE NUTRITION USE
            
            GameState.Instance.SetPlayer(player);

            var map = new GameMap(width, height);
            var builder = new GameLevelBuilder()
                .SetFactory(itemFactory)
                .SetMap(map)
                .SetPlayer(player)
                .AddFood(4)
                .AddPowerup(new Random().Next(2, 6))
                .AddEnemy(new UngaBunga(shooterType, new Point(50, 30)))
                .AddEnemy(new MrBob(robotType, new Point(40, 40), player));
                
            var director = new LevelDirector();
            var entities = director.BuildLevel(builder);
            EntityManager.Instance.Initialize(entities);
            
            var bulletPool = new BulletPoolManager();

            var playerController = new PlayerController(player, entities, bulletPool, entityRenderer);

            var gameLoop = new GameLoop(entities, entityRenderer, playerController, menu, menuRenderer);
            gameLoop.Run();
        }
    }
}