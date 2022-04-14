using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Actions
{
    public class Collect : Action
    {
        public override string ActionText(Actor actor, HexCell target)
        {
            return $"{actor} collect {target.Actor}";
        }

        /// <summary>
        /// Collect an item and move to a cell.
        /// </summary>
        public override IEnumerator DoAction(Actor actor, HexCell target)
        {
            // Collect the actor on the target cell
            target.Actor.GetComponent<Collectable>().Collect();
            
            // Move to the cell
            yield return new Move().DoAction(actor, target);
        }
    }
}
