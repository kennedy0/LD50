using System.Collections;
using UnityEngine;

namespace Actions
{
    public class Kindle : Action
    {
        public override string ActionText(Actor actor, HexCell target)
        {
            return $"{actor} kindle {target.Actor}.";
        }

        /// <summary>
        /// Kindle the fire.
        /// </summary>
        public override IEnumerator DoAction(Actor actor, HexCell target)
        {
            // Feed wood to campfire.
            var campfire = target.Actor.gameObject.GetComponent<Campfire>();
            campfire.Kindle(Player.Inventory.Wood);
            Player.Inventory.Wood = 0;
            
            yield return null;
        }
    }
}