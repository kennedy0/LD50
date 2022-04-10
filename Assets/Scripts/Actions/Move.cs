using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Actions
{
    public class Move : Action
    {
        public override string ActionText(Actor actor, HexCell target)
        {
            return $"{actor} move to {target}";
        }

        /// <summary>
        /// Move to a cell.
        /// </summary>
        public override IEnumerator DoAction(Actor actor, HexCell target)
        {
            var oldCell = actor.Cell;
            var newCell = target;
            
            // Set the actor's cell
            actor.SetCell(newCell);
            
            // Token move animation
            yield return actor.Token.Move(oldCell.Tile, newCell.Tile);
        }
    }
}
