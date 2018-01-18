
using System;
using ECS;

namespace Gemserk.ECS
{
    public class EventHandlerDelegate : IEntityAddedEventListener, IEntityRemovedEventListener
    {
        public event Action<object, Entity> EntityAddedEvent;
        public event Action<object, Entity> EntityRemoveEvent;

        public void OnEntityAdded(object sender, Entity entity)
        {
            if (EntityAddedEvent != null)
                EntityAddedEvent(sender, entity);
        }

        public void OnEntityRemoved(object sender, Entity entity)
        {
            if (EntityRemoveEvent != null)
                EntityRemoveEvent(sender, entity);
        }
    }

}