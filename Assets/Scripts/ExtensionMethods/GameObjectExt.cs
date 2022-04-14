using UnityEngine;

namespace ExtensionMethods
{
    public static class GameObjectExt
    {
        /// <summary>
        /// Check if a GameObject has a component attached.
        /// </summary>
        public static bool HasComponent<T>(this GameObject go)
        {
            return go.GetComponent(typeof(T)) != null;
        }
    }
}