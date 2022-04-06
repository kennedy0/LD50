using System;
using UnityEngine;

namespace Datatypes
{
    /// <summary>
    /// Int-based hex coordinate
    /// </summary>
    [Serializable]
    public struct Hex
    {
        public int Q;
        public int R;
        public int S;
        
        public Hex(int q, int r, int s)
        {
            Q = q;
            R = r;
            S = s;
        }
        
        /// <summary>
        /// Calculate the distance between 2 Hex coordinates.
        /// </summary>
        public static int Distance(Hex a, Hex b)
        {
            var dq = Mathf.Abs(a.Q - b.Q);
            var dr = Mathf.Abs(a.R - b.R);
            var ds = Mathf.Abs(a.S - b.S);
            return (dq + dr + ds) / 2;
        }
        
        /// <summary>
        /// Convert a grid position to a world position.
        /// Y coordinate is always zero.
        /// </summary>
        public Vector3 WorldPosition()
        {
            float x;
            float z;
            x = Utilities.TILE_SIZE * (3f/2f * Q);
            z = Utilities.TILE_SIZE * (Mathf.Sqrt(3f)/2f * Q + Mathf.Sqrt(3) * R) * -1;
            return new Vector3(x, 0f, z);
        }
    }
}
