using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    public GameObject Tile;
    public int Width;
    public int Height;
    
    void Start()
    {
        int x;
        int y;
        
        for (x = 0; x < Width; x++)
        {
            for (y = 0; y < Height; y++)
            {
                GenerateTile(x, y);
            }
        }
    }

    public void GenerateTile(int x, int y)
    {
        var tile = Instantiate(Tile);
        tile.name = $"Tile ({x}, {y})";
        tile.transform.position = Utilities.GridToWorldPosition(x, y);
    }

    void Update()
    {
        
    }
}
