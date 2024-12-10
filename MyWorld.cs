using System;
using System.Collections.Generic;
using System.Linq;
using EasyMonoGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace AdvancedGame
{
    internal class MyWorld : World
    {
        private Player player;
        private List<Enemy> enemies;
        private int score;
        private Random random;

        public MyWorld() : base(800, 600) // Skärmens storlek: 800x600
        {
            // Tile background with the file "bluerock" in the Content folder.
            BackgroundTileName = "bluerock";

            // Initiera komponenter
            player = new Player(400, 500); // Startposition för spelaren
            Add(player);

            enemies = new List<Enemy>();
            random = new Random();
            score = 0;

            // Skapa initiala fiender
            SpawnEnemies(5);
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            // Kolla om fiender kolliderar med spelaren
            foreach (var enemy in enemies.ToList())
            {
                if (enemy.CollidesWith(player))
                {
                    GameOver();
                }
            }

            // Uppdatera poäng
            if (Input.IsKeyPressed(Keys.Space))
            {
                score++;
            }

            // Uppdatera och ta bort fiender som gått ur skärmen
            foreach (var enemy in enemies.ToList())
            {
                if (enemy.Y > Height)
                {
                    enemies.Remove(enemy);
                    Remove(enemy);
                }
            }

            // Lägg till nya fiender om det behövs
            if (enemies.Count < 5)
            {
                SpawnEnemies(1);
            }
        }

        private void SpawnEnemies(int count)
        {
            for (int i = 0; i < count; i++)
            {
                float x = random.Next(0, (int)(Width - 50)); // Fienden placeras inom skärmens bredd
                var enemy = new Enemy(x, 0); // Starta fienden högst upp
                enemies.Add(enemy);
                Add(enemy);
            }
        }

        private void GameOver()
        {
            Console.WriteLine("Game Over!");
            Console.WriteLine($"Final Score: {score}");
            Environment.Exit(0); // Avsluta spelet
        }
    }

    // Spelarklass
    internal class Player : GameObject
    {
        public Player(float x, float y) : base("playerTexture", x, y)
        {
            Speed = 300; // Spelarens hastighet
        }

      
            var game = new Game(new MyWorld());
            game.Run();
        }
    }
}
