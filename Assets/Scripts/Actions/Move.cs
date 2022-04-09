using System.Collections;
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
            yield return actor.Token.Move(oldCell, newCell);
            actor.SetCell(newCell);
        }
    }
}
