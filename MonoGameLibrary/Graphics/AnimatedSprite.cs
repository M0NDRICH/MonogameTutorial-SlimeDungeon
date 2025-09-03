﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.Graphics
{
    public class AnimatedSprite : Sprite
    {
        private int _currentFrame;
        private TimeSpan _elapsed;
        private Animation _animation;

        // Gets or Sets the animation for this animated sprite.
        public Animation Animation
        {
            get => _animation;
            set
            {
                _animation = value;
                Region = _animation.Frames[0];
            }
        }

        // Creates a new animated sprite.
        public AnimatedSprite() { }

        // Creates a new animated sprite with the specified frames and delay.
        // param name="animation" - The animation for this animated sprite.
        public AnimatedSprite(Animation animation)
        {
            Animation = animation;
        }

        // Updates this animated sprite.
        // param name="gameTime" - A snapshot of the game timing values provided by the framework.
        public void Update(GameTime gameTime)
        {
            _elapsed += gameTime.ElapsedGameTime;

            if (_elapsed >= _animation.Delay)
            {
                _elapsed -= _animation.Delay;
                _currentFrame++;

                if (_currentFrame >= _animation.Frames.Count)
                {
                    _currentFrame = 0;
                }

                Region = _animation.Frames[_currentFrame];
            }
        }
    }
}
