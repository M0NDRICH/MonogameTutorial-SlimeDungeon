using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.Graphics
{
    public class SimpleInputBuffer
    {
        // Use a queue directly for input buffering
        private readonly Queue<Vector2> _inputBuffer;
        private const int MAX_BUFFER = 2;


        public Vector2 EntityPosition {  get; set; } = Vector2.Zero;

        public float Speed { get; set; } = 0.0f;

        public SimpleInputBuffer()
        {
            _inputBuffer = new Queue<Vector2>(MAX_BUFFER);
        }

        public SimpleInputBuffer(Vector2 position, float speed)
        {
            _inputBuffer = new Queue<Vector2>(MAX_BUFFER);
            EntityPosition = position;
            Speed = speed;
        }

        public void HandleKeyboardInput()
        {
            KeyboardState keyboard = Keyboard.GetState();
            Vector2 newDirection = Vector2.Zero;

            if(keyboard.IsKeyDown(Keys.Up))
            {
                newDirection = -Vector2.UnitY;
            }
            else if(keyboard.IsKeyDown(Keys.Down))
            {
                newDirection = Vector2.UnitY;
            }
            else if(keyboard.IsKeyDown(Keys.Left))
            { 
                newDirection = -Vector2.UnitX;
            }
            else if(keyboard.IsKeyDown(Keys.Right))
            {
                newDirection = Vector2.UnitX;
            }

            //if (keyboard.IsKeyUp(Keys.Up))
            //{
            //    newDirection = Vector2.Zero;
            //}
            //else if (keyboard.IsKeyUp(Keys.Down))
            //{
            //    newDirection = Vector2.Zero;
            //}
            //else if (keyboard.IsKeyUp(Keys.Left))
            //{
            //    newDirection = Vector2.Zero;
            //}
            //else if (keyboard.IsKeyUp(Keys.Right))
            //{
            //    newDirection = Vector2.Zero;
            //}

            AddDirection(newDirection);
            Update();
        }

        public void AddDirection(Vector2 direction)
        {
            if(direction != Vector2.Zero && _inputBuffer.Count < MAX_BUFFER)
            {
                _inputBuffer.Enqueue(direction);
            }
        }

        public void Update()
        {
            if (_inputBuffer.Count > 0)
            {
                Vector2 nextDirection = _inputBuffer.Dequeue();
                EntityPosition += nextDirection * Speed;
            }
        }
    }
}
