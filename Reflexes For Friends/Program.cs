using System;
using SFML;
using SFML.Graphics;
using SFML.Window;

namespace Reflexes_For_Friends
{
    static class Program
    {
        static void Main()
        {
            RenderWindow window = new RenderWindow(new VideoMode(800, 800), "Reflexes For Friends", Styles.Default, new ContextSettings(32, 0));
            window.SetVerticalSyncEnabled(true);

            #region Register Event Handlers
            window.Closed += new EventHandler(OnClosed);
            window.KeyPressed += new EventHandler<KeyEventArgs>(OnKeyPressed);
            #endregion

            window.SetActive();
            while (window.IsOpen())
            {
                window.DispatchEvents();

                window.Clear(Color.Cyan);

                window.Display();
            }
        }

        static void OnClosed(object sender, EventArgs e)
        {
            Window window = (Window)sender;
            window.Close();
        }

        static void OnKeyPressed(object sender, KeyEventArgs e)
        {
        }
    }
}
