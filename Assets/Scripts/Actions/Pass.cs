using System.Collections;
using UnityEngine;

namespace Actions
{
    public class Pass : Action
    {
        public override string ActionText(Actor actor, HexCell target)
        {
            return $"{actor} pass.";
        }

        /// <summary>
        /// Do nothing.
        /// </summary>
        public override IEnumerator DoAction(Actor actor, HexCell target)
        {
            yield return null;
        }
    }
}
