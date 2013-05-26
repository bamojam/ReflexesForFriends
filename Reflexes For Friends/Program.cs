using System;
using SFML;
using SFML.Graphics;
using SFML.Window;

namespace Reflexes_For_Friends
{
    static class Program
    {
        static KeyboardModule keyboardModule;
        static RenderWindow window;

        static void Main()
        {
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

            window.SetActive();
            while (window.IsOpen())
            {
                window.DispatchEvents();

                window.Clear(Color.Cyan);

                window.Display();
            }
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
