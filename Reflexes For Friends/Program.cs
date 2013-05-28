using System;
using System.Collections.Generic;
using System.Diagnostics;
using SFML.Graphics;
using SFML.Window;

namespace Reflexes_For_Friends
{
    static class Program
    {
        public const int WINDOW_WIDTH = 800;
        public const int WINDOW_HEIGHT = 800;
        // 25ms update frequency = 40 updates per second
        const long UPDATE_FREQUENCY_IN_MS = 25;

        private static Stopwatch timer;
        private static RenderWindow window;
        private static GameWorld world;
        private static KeyboardModule keyboardModule;
        private static Player player;
        private static Friend friend;
        private static IList<Enemy> enemies;
        private static Text gameOverText;
        private static bool gameOver;

        public static void Main()
        {
            #region Initialize Variables
            window = new RenderWindow(new VideoMode(WINDOW_WIDTH, WINDOW_HEIGHT), "Reflexes For Friends", Styles.Default, new ContextSettings(32, 0));
            timer = new Stopwatch();
            world = new GameWorld(new Texture("resources/background-grass.png"));
            player = new Player(new Texture("resources/player.png"));
            friend = new Friend(new Texture("resources/friend.png"));
            enemies = new List<Enemy>();
            gameOver = false;
            #endregion

            #region Register Event Handlers
            window.Closed += new EventHandler(OnClosed);
            window.KeyPressed += new EventHandler<KeyEventArgs>(OnKeyPressed);
            window.KeyReleased += new EventHandler<KeyEventArgs>(OnKeyReleased);
            #endregion

            #region Setup Modules
            keyboardModule = new KeyboardModule();
            #endregion

            RegisterKeyBindings();
            GenerateEnemies();

            timer.Start();
            long timeSinceLastUpdate = 0;

            window.SetVerticalSyncEnabled(true);
            window.SetActive();
            while (window.IsOpen())
            {
                window.DispatchEvents();

                if (!gameOver)
                {
                    timeSinceLastUpdate += timer.ElapsedMilliseconds;
                    timer.Restart();
                    if (timeSinceLastUpdate >= UPDATE_FREQUENCY_IN_MS)
                    {
                        UpdateGame(UPDATE_FREQUENCY_IN_MS);

                        timeSinceLastUpdate -= UPDATE_FREQUENCY_IN_MS;
                    }
                }

                DrawGame();
                window.Display();
            }
            timer.Stop();
        }

        private static void UpdateGame(long timeSinceLastUpdate)
        {
            player.Update(timeSinceLastUpdate);
            friend.Update(timeSinceLastUpdate);
            foreach (var enemy in enemies)
            {
                enemy.Update(timeSinceLastUpdate);
            }

            if (GameWinningCollisionOccurred())
            {
                ShowGameOverText("Congratulations, Friendship Formed!!", Color.White);
                gameOver = true;
                RemoveKeyBindings();
            }
            else if (GameLosingCollisionOccurred())
            {
                ShowGameOverText("Aww, Sorry, Friendship Blocked!! :( :(", Color.White);
                gameOver = true;
                RemoveKeyBindings();
            }
        }

        private static void RemoveKeyBindings()
        {
            KeyEventArgs key;

            key = CreateBinding(false, false, false, false, Keyboard.Key.Left);
            keyboardModule.RemoveBinding(key, false);

            key = CreateBinding(false, false, false, false, Keyboard.Key.Right);
            keyboardModule.RemoveBinding(key, false);

            key = CreateBinding(false, false, false, false, Keyboard.Key.Up);
            keyboardModule.RemoveBinding(key, false);

            key = CreateBinding(false, false, false, false, Keyboard.Key.Down);
            keyboardModule.RemoveBinding(key, false);
        }

        private static void ShowGameOverText(string textToShow, Color color)
        {
            gameOverText = new Text(textToShow, new Font("resources/fonts/TitilliumWeb-Regular.ttf"))
                {
                    Position = new Vector2f(100f, 250f),
                    Color = color
                };
        }

        private static bool GameWinningCollisionOccurred()
        {
            IntRect playerRect = new IntRect((int)player.Position.X, (int)player.Position.Y, player.TextureRect.Width, player.TextureRect.Height);
            IntRect friendRect = new IntRect((int)friend.Position.X, (int)friend.Position.Y, friend.TextureRect.Width, friend.TextureRect.Height);
            return playerRect.Intersects(friendRect);
            // These TextureRects don't move as the Sprite changes position. They stay at 0, 0.
//            return player.TextureRect.Intersects(friend.TextureRect);
        }

        private static bool GameLosingCollisionOccurred()
        {
            IntRect playerRect = new IntRect((int)player.Position.X, (int)player.Position.Y, player.TextureRect.Width, player.TextureRect.Height);
            IntRect enemyRect = new IntRect();
            foreach (var enemy in enemies)
            {
                enemyRect.Left = (int)enemy.Position.X;
                enemyRect.Top = (int)enemy.Position.Y;
                enemyRect.Width = enemy.TextureRect.Width;
                enemyRect.Height = enemy.TextureRect.Height;
                if (playerRect.Intersects(enemyRect))
                {
                    return true;
                }
            }
            return false;
        }

        private static void DrawGame()
        {
            window.Clear(Color.Black);
            window.Draw(world);
            window.Draw(player);
            window.Draw(friend);
            foreach (var enemy in enemies)
            {
                window.Draw(enemy);
            }

            if (gameOver)
            {
                window.Draw(gameOverText);
            }
        }

        private static void GenerateEnemies()
        {
            var enemyTexture = new Texture("resources/enemy.png");
            for (var i = 0; i < 30; i++)
            {
                enemies.Add(new Enemy(enemyTexture));
            }
        }

        private static void RegisterKeyBindings()
        {
            KeyEventArgs key;

            key = CreateBinding(false, false, false, false, Keyboard.Key.Escape);
            keyboardModule.AddBinding(key, false, ExitApp);

            key = CreateBinding(true, false, false, false, Keyboard.Key.F2);
            keyboardModule.AddBinding(key, false, ExitApp);

            key = CreateBinding(false, false, false, false, Keyboard.Key.Left);
            keyboardModule.AddBinding(key, false, player.MoveLeft);

            key = CreateBinding(false, false, false, false, Keyboard.Key.Right);
            keyboardModule.AddBinding(key, false, player.MoveRight);

            key = CreateBinding(false, false, false, false, Keyboard.Key.Up);
            keyboardModule.AddBinding(key, false, player.MoveUp);

            key = CreateBinding(false, false, false, false, Keyboard.Key.Down);
            keyboardModule.AddBinding(key, false, player.MoveDown);
        }

        private static KeyEventArgs CreateBinding(bool alt, bool ctrl, bool shift, bool system, Keyboard.Key key)
        {
            KeyEventArgs binding = new KeyEventArgs(new KeyEvent());
            binding.Alt = alt;
            binding.Control = ctrl;
            binding.Shift = shift;
            binding.System = system;
            binding.Code = key;

            return binding;
        }

        private static void ExitApp()
        {
            window.Close();
        }

        static void OnClosed(object sender, EventArgs e)
        {
            ExitApp();
        }

        static void OnKeyPressed(object sender, KeyEventArgs e)
        {
            keyboardModule.ProcessKeyEvent(e, true);
        }

        static void OnKeyReleased(object sender, KeyEventArgs e)
        {
            keyboardModule.ProcessKeyEvent(e, false);
        }
    }
}
