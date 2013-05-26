using System;
using System.Diagnostics;
using SFML;
using SFML.Graphics;
using SFML.Window;

namespace Reflexes_For_Friends
{
    static class Program
    {
        // 25ms update frequency = 40 updates per second
        static const long UPDATE_FREQUENCY_IN_MS = 25;
        static KeyboardModule keyboardModule;
        static RenderWindow window;
        static Stopwatch timer;

        static void Main()
        {
            timer = new Stopwatch();
            window = new RenderWindow(new VideoMode(800, 800), "Reflexes For Friends", Styles.Default, new ContextSettings(32, 0));
            window.SetVerticalSyncEnabled(true);

            #region Register Event Handlers
            window.Closed += new EventHandler(OnClosed);
            window.KeyPressed += new EventHandler<KeyEventArgs>(OnKeyPressed);
            window.KeyReleased += new EventHandler<KeyEventArgs>(OnKeyReleased);
            #endregion

            #region Setup Modules
            keyboardModule = new KeyboardModule();
            #endregion

            RegisterKeyBindings();

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

                window.Clear(Color.Cyan);
                window.Display();
            }
            timer.Stop();
        }

        private static void UpdateGame(long UPDATE_FREQUENCY_IN_MS)
        {
            throw new NotImplementedException();
        }

        private static void RegisterKeyBindings()
        {
            KeyEventArgs key;

            key = CreateBinding(false, false, false, false, Keyboard.Key.Escape);
            keyboardModule.AddBinding(key, false, ExitApp);
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
