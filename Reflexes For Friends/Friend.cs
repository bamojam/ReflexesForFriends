using SFML.Graphics;
using SFML.Window;

namespace Reflexes_For_Friends
{
    class Friend : Sprite
    {
        public Friend(Texture texture) : base(texture)
        {
            Position = new Vector2f(Program.WINDOW_WIDTH - TextureRect.Width, Program.WINDOW_HEIGHT - TextureRect.Height);
        }

        public void Update(long timeSinceLastUpdate)
        {
        }
    }
}
