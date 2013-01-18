using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceDestroyer.Backgrounds
{
    public class AnimatedExplosion
    {
        private readonly float Depth;
        private readonly float Scale;
        private readonly int Speed;
        private readonly List<int> framePerRow = new List<int>();
        private int Frame;
        private int Height;
        private Vector2 Origin;
        private bool Paused;
        private float Rotation;
        private int Row;
        private float TimePerFrame;
        private int Width;
        private int X;
        private int Y;
        private int _rowCount;
        private float elapsedTime;
        public Texture2D myTexture;

        public AnimatedExplosion(Vector2 origin, float rotation,
                                 float scale, float depth, int x, int y, int width, int height, int speed)
        {
            Speed = speed;
            Origin = new Vector2(x, y);
            Rotation = rotation;
            Scale = width/(float) 64.0;
            Depth = depth;
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public bool IsPaused
        {
            get { return Paused; }
        }

        public void Load(Texture2D asset, int frameSpeed)
        {
            TimePerFrame = (float) 1/frameSpeed;
            elapsedTime = 0;
            myTexture = asset;
            Paused = false;
            Frame = 0;
        }

        public void defineFrames(int rows, int[] frames)
        {
            for (int i = 0; i <= rows - 1; i++)
            {
                framePerRow.Add(frames[i]);
            }
            _rowCount = rows;
        }

        public void UpdateFrame(float elapsed)
        {
            if (Paused)
                return;
            elapsedTime += elapsed;
            Origin.X -= Speed;
            if (elapsedTime > TimePerFrame)
            {
                Frame++;
                // Keep the Frame between 0 and the total frames, minus one.
                //Frame = Frame % framePerRow[Row];
                elapsedTime -= TimePerFrame;
                if (Frame == 3)
                {
                    Frame = 0;
                    Row++;
                    if (Row == 4)
                    {
                        Row = 1;
                        Stop();
                    }
                }
            }
        }

        public void DrawFrame(SpriteBatch batch)
        {
            DrawFrame(batch, Row, Frame);
        }

        public void DrawFrame(SpriteBatch batch, int row, int frame)
        {
            int frameWidth = myTexture.Width/framePerRow[row];
            int frameHeight = myTexture.Height/_rowCount;
            int frameHeightStart = 0 + frameHeight*row;
            var sourcerect = new Rectangle(frameWidth*frame, frameHeightStart,
                                           frameWidth, frameHeight);
            batch.Draw(myTexture, Origin, sourcerect, Color.White, Rotation, Vector2.Zero, Scale, SpriteEffects.None,
                       Depth);
        }

        public void setRow(int newRow)
        {
            Row = newRow - 1;
        }

        public void increaseRotation(int degree)
        {
            Rotation += (float) 3.14*degree;
        }

        public void decreaseRotation(int degree)
        {
            Rotation -= (float) 3.14*degree;
        }

        public void Reset()
        {
            Frame = 0;
            elapsedTime = 0f;
        }

        public void Stop()
        {
            Pause();
            Reset();
        }

        public void Play()
        {
            Paused = false;
        }

        public void Pause()
        {
            Paused = true;
        }
    }
}