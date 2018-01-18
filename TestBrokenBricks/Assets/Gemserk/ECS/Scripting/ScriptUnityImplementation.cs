using ECS;
using UnityEngine;

namespace Gemserk.ECS.Scripting
{
    public abstract class ScriptUnityImplementation : MonoBehaviour, IScript
    {
        public virtual void OnAdded(Entity entity)
        {

        }

        public virtual void OnRemoved(Entity entity)
        {

        }
        public virtual void OnUpdate(Entity entity)
        {
            
        }
    }
}