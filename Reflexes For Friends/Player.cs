using System;
using SFML.Graphics;
using SFML.Window;

namespace Reflexes_For_Friends
{
    class Player : Sprite
    {
        public Player(Texture texture) : base(texture)
        {
        }

        public void Update(long timeSinceLastUpdate)
        {
        }

        public void MoveLeft()
        {
            Position = new Vector2f(Math.Max(0, Position.X - 20), Position.Y);
        }

        public void MoveRight()
        {
            Position = new Vector2f(Math.Min(Program.WINDOW_WIDTH - TextureRect.Width, Position.X + 20), Position.Y);
        }

        public void MoveUp()
        {
            Position = new Vector2f(Position.X, Math.Max(0, Position.Y - 20));
        }

        public void MoveDown()
        {
            Position = new Vector2f(Position.X, Math.Min(Program.WINDOW_HEIGHT - TextureRect.Height, Position.Y + 20));
        }
    }
}
