using System;
using SFML.Graphics;
using SFML.Window;

namespace Reflexes_For_Friends
{
    class Enemy : Sprite
    {
        private static Random random = new Random();
        private bool xMover;
        private bool xDirectionPositive = true;
        private bool yMover;
        private bool yDirectionPositive = true;
        private int framesPerMove;
        private int framesSinceLastMove = 0;

        public Enemy(Texture texture) : base(texture)
        {
            xMover = random.Next(0, 2) == 0 ? false : true;
            yMover = random.Next(0, 2) == 0 ? false : true;
            // 0 = doesn't move; 80 = moves every two seconds
            framesPerMove = random.Next(0, 81);

            var initialXPosition = random.Next(2, Program.WINDOW_WIDTH/TextureRect.Width) * TextureRect.Width;
            var initialYPosition = random.Next(2, Program.WINDOW_HEIGHT/TextureRect.Height) * TextureRect.Height;
            Position = new Vector2f(initialXPosition, initialYPosition);
        }

        public void Update(long timeSinceLastUpdate)
        {
            if (framesSinceLastMove >= framesPerMove)
            {
                var newLocation = new Vector2f(Position.X, Position.Y);
                if (xMover)
                {
                    if (xDirectionPositive)
                    {
                        newLocation.X = Position.X + 20;
                    }
                    else
                    {
                        newLocation.X = Position.X - 20;
                    }
                }

                if (yMover)
                {
                    if (yDirectionPositive)
                    {
                        newLocation.Y = Position.Y + 20;
                    }
                    else
                    {
                        newLocation.Y = Position.Y - 20;
                    }
                }

                KeepValuesOnScreen(newLocation);
                Position = newLocation;

                framesSinceLastMove = 0;
            }
            else
            {
                framesSinceLastMove++;
            }
        }

        private void KeepValuesOnScreen(Vector2f newLocation)
        {
            if (newLocation.X <= 0)
            {
                newLocation.X = 0;
                xDirectionPositive = !xDirectionPositive;
            }
            else if (newLocation.X >= Program.WINDOW_WIDTH - TextureRect.Width)
            {
                newLocation.X = Program.WINDOW_WIDTH - TextureRect.Width;
                xDirectionPositive = !xDirectionPositive;
            }

            if (newLocation.Y <= 0)
            {
                newLocation.Y = 0;
                yDirectionPositive = !yDirectionPositive;
            }
            else if (newLocation.Y >= Program.WINDOW_HEIGHT - TextureRect.Height)
            {
                newLocation.Y = Program.WINDOW_HEIGHT - TextureRect.Height;
                yDirectionPositive = !yDirectionPositive;
            }
        }
    }
}
