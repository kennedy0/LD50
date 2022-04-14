using System;
using UnityEngine;

namespace Datatypes
{
    /// <summary>
    /// Int-based hex coordinate
    /// </summary>
    [Serializable]
    public readonly struct Hex : IEquatable<Hex>
    {
        public readonly int Q;
        public readonly int R;
        public readonly int S;
        
        public Hex(int q, int r, int s)
        {
            Q = q;
            R = r;
            S = s;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Q;
                hashCode = (hashCode * 397) ^ R;
                hashCode = (hashCode * 397) ^ S;
                return hashCode;
            }
        }

        public bool Equals(Hex other)
        {
            return Q == other.Q && R == other.R && S == other.S;
        }

        public override bool Equals(object obj)
        {
            return obj is Hex other && Equals(other);
        }

        public static bool operator ==(Hex a, Hex b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Hex a, Hex b)
        {
            return !(a == b);
        }

        public static Hex Zero = new Hex(0, 0, 0);

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

        public Region GetRegion()
        {
            var qh = (R + Region.SHIFT * Q) / Region.AREA;
            var rh = (S + Region.SHIFT * R) / Region.AREA;
            var sh = (Q + Region.SHIFT * S) / Region.AREA;
            
            var q = (1 + qh - rh) / 3;
            var r = (1 + rh - sh) / 3;
            var s = (1 + sh - qh) / 3;

            return new Region(q, r, s);
        }
    }
}
