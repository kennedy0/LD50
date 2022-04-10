using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Actor))]
public class LightSource : MonoBehaviour
{
    public int Radius = 1;

    private Actor _actor;

    private void Awake()
    {
        _actor = GetComponent<Actor>();
    }

    /// <summary>
    /// Update cell visibility after the light source has changed positions.
    /// </summary>
    public void UpdateVisibility(HexCell oldCell, HexCell newCell)
    {
        // Get list of cells to update
        var newCells = newCell != null ? Board.Grid.GetCells(newCell.Position, Radius) : new List<HexCell>();
        var oldCells = oldCell != null ? Board.Grid.GetCells(oldCell.Position, Radius) : new List<HexCell>();
        oldCells = oldCells.Where(cell => !newCells.Contains(cell)).ToList();
        
        // All new cells are visible
        if (newCell != null)
        {
            foreach (var cell in newCells)
            {
                cell.SetVisibility(true);
            }
        }
        
        // Some old cells are no longer visible
        if (oldCell != null)
        {
            // Get list of all light sources on the board
            var lightSources = FindObjectsOfType<LightSource>();
            
            // Check each old cell to see if it's in range of any light source
            foreach (var cell in oldCells)
            {
                // If the cell is in range of a light source, make the cell visible.
                var foundLightSource = false;
                foreach (var lightSource in lightSources)
                {
                    if (lightSource.CellInRange(cell))
                    {
                        cell.SetVisibility(true);
                        foundLightSource = true;
                        break;
                    }
                }

                // If we were unable to find a light source, make the cell not visible.
                if (!foundLightSource)
                {
                    cell.SetVisibility(false);
                }
            }
        }
    }

    /// <summary>
    /// Returns whether or not a given cell is in range of this light source.
    /// </summary>
    public bool CellInRange(HexCell cell)
    {
        if (cell.DistanceTo(_actor.Cell) <= Radius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
