using System.Collections;

namespace Actions
{
    public abstract class Action
    {
        public abstract string ActionText(Actor actor, HexCell target);
        public abstract IEnumerator DoAction(Actor actor, HexCell target);
    }
}
