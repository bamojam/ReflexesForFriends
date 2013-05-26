using System;
using System.Diagnostics;
using SFML;
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
        static KeyboardModule keyboardModule;
        static RenderWindow window;
        static Stopwatch timer;
        static Player player;

        static void Main()
        {
            #region Initialize Variables
            window = new RenderWindow(new VideoMode(WINDOW_WIDTH, WINDOW_HEIGHT), "Reflexes For Friends", Styles.Default, new ContextSettings(32, 0));
            timer = new Stopwatch();
            player = new Player(new Texture("resources/player.png"));
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
            window.SetVerticalSyncEnabled(true);

            long timeSinceLastUpdate = 0;
            timer.Start();
            window.SetActive();
            while (window.IsOpen())
            {
                window.DispatchEvents();

                timeSinceLastUpdate += timer.ElapsedMilliseconds;
                timer.Restart();
                if (timeSinceLastUpdate >= UPDATE_FREQUENCY_IN_MS)
                {
                    UpdateGame(UPDATE_FREQUENCY_IN_MS);

                    timeSinceLastUpdate -= UPDATE_FREQUENCY_IN_MS;
                }

                DrawGame();
                window.Display();
            }
            timer.Stop();
        }

        private static void DrawGame()
        {
            window.Clear(Color.Black);
            window.Draw(player);
        }

        private static void UpdateGame(long UPDATE_FREQUENCY_IN_MS)
        {
            player.Update();
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
