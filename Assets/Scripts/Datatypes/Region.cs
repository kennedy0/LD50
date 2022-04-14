namespace Datatypes
{
    public struct Region
    {
        public const int RADIUS = 2;
        public const int AREA = (3 * RADIUS * RADIUS) + (3 * RADIUS) + 1;
        public const int SHIFT = (3 * RADIUS) + 2;
        
        public readonly int Q;
        public readonly int R;
        public readonly int S;
        
        public Region(int q, int r, int s)
        {
            Q = q;
            R = r;
            S = s;
        }

        public override string ToString()
        {
            return $"Region({Q}, {R}, {S})";
        }
    }
}