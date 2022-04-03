using System;
using System.Linq;
using UnityEngine;

public static class Utilities
{
    public const float TILE_SIZE = .5f;  // The distance from the center of the hex tile to one of its corners.
    
    private static System.Random _random = new System.Random();

    /// <summary>
    /// Convert a grid position to a world position.
    /// Y coordinate is always zero.
    /// </summary>
    public static Vector3 GridToWorldPosition(int q, int r, int s)
    {
        float x;
        float z;
        x = TILE_SIZE * (3f/2f * q);
        z = TILE_SIZE * (Mathf.Sqrt(3f)/2f * q + Mathf.Sqrt(3) * r) * -1;
        return new Vector3(x, 0f, z);
    }

    /// <summary>
    /// Convert Cube coordinates (Q, R, S) to Axial coordinates (Q, R).
    /// </summary>
    public static Tuple<int, int> CubeToAxial(int q, int r, int s)
    {
        return new Tuple<int, int>(q, r);
    }

    /// <summary>
    /// Convert Axial coordinates (Q, R) to Cube coordinates (Q, R, S).
    /// </summary>
    /// <returns></returns>
    public static Tuple<int, int, int> AxialToCube(int q, int r)
    {
        int s;
        s = -q - r;
        return new Tuple<int, int, int>(q, r, s);
    }

    /// <summary>
    /// Convert Axial coordinates (Q, R) to Offset coordinates (X, Y).
    /// </summary>
    public static Vector2Int AxialToOffset(int q, int r)
    {
        int x;
        int y;
        x = q;
        y = r + (q - (q&1)) / 2;
        return new Vector2Int(x, y);
    }

    /// <summary>
    /// Convert Offset coordinates (X, Y) to Axial coordinates (Q, R).
    /// </summary>
    public static Tuple<int, int> OffsetToAxial(Vector2Int pos)
    {
        int q;
        int r;
        q = pos.x;
        r = pos.y - (pos.x - (pos.x&1)) / 2;
        return new Tuple<int, int>(q, r);
    }

    public static int CalculateDistance(int q1, int r1, int s1, int q2, int r2, int s2)
    {
        var dq = Mathf.Abs(q1 - q2);
        var dr = Mathf.Abs(r1 - r2);
        var ds = Mathf.Abs(s1 - s2);
        return (dq + dr + ds) / 2;
    }
    
    /// <summary>
    /// Returns a random Direction.
    /// </summary>
    public static Direction RandomDirection()
    {
        var directionList = Enum.GetValues(typeof(Direction))
            .Cast<Direction>()
            .ToList();
        var index = _random.Next(directionList.Count);
        return directionList[index];
    }

    /// <summary>
    /// Return a random rotation facing one of the 6 Directions.
    /// </summary>
    /// <returns></returns>
    public static Quaternion RandomRotation()
    {
        return RotationFromDirection(RandomDirection());
    }

    /// <summary>
    /// Get a rotation angle from a direction
    /// </summary>
    public static Quaternion RotationFromDirection(Direction direction)
    {
        float rot = 0f;
        
        switch (direction)
        {
            case Direction.North:
                rot = 0f;
                break;
            case Direction.South:
                rot = 180f;
                break;
            case Direction.NorthWest:
                rot = -60f;
                break;
            case Direction.NorthEast:
                rot = 60f;
                break;
            case Direction.SouthWest:
                rot = -120f;
                break;
            case Direction.SouthEast:
                rot = 120f;
                break;
        }
        
        return Quaternion.Euler(0f, rot, 0f);
    }
}
