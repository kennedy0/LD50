using System.Collections;
using UnityEngine;

namespace Actions
{
    public class Examine : Action
    {
        public override string ActionText(Actor actor, HexCell target)
        {
            return $"{actor} examine {target.Actor}.";
        }

        public override IEnumerator DoAction(Actor actor, HexCell target)
        {
            yield return target.Actor.GetComponent<Description>().Describe();
        }
    }
}