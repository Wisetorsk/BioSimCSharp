﻿namespace Biosim.Parameters
{
    public class Position
    {
        public int x = 0;
        public int y = 0;

        public override string ToString()
        {
            return $"Position\n X: {x}\tY: {y}";
        }
    }
}