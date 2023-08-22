using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Functions
{
    public enum Direction
    {
        NONE,
        UP,
        DOWN,
        LEFT,
        RIGHT
    }
    internal static class CreamUtilities
    {
        public static Vector2 GetDirectionVector(Direction direction)
        {
            switch(direction)
            {
                case Direction.NONE: return Vector2.zero;
                case Direction.UP: return Vector2.up;
                case Direction.DOWN: return Vector2.down;
                case Direction.LEFT: return Vector2.left;
                case Direction.RIGHT: return Vector2.right;
                default: return Vector2.zero;
            }
        }
        public static Direction GetOppositeDirection(Direction direction)
        {
            switch(direction)
            {
                case Direction.UP: return Direction.DOWN;
                case Direction.DOWN: return Direction.UP;
                case Direction.LEFT: return Direction.RIGHT;
                case Direction.RIGHT: return Direction.LEFT;
                default: return Direction.NONE;
            }
        }
    }
}
