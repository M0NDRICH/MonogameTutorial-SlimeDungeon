using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogameTutorial.GameObjects
{
    internal struct SlimeSegment
    {
        /// <summary>
        /// The position this slime segment is at before the movement cycle occurs.
        /// </summary>
        public Vector2 At;

        /// <summary>
        /// The position this slime segment should move to during the next movement cycle.
        /// </summary>
        public Vector2 To;

        /// <summary>
        /// The direction this slime segment is moving.
        /// </summary>
        public Vector2 Direction;

        /// <summary>
        /// The opposite direction this slime segment is moving.
        /// </summary>
        public Vector2 ReverseDirection => new Vector2(-Direction.X, -Direction.Y);
    }
}
