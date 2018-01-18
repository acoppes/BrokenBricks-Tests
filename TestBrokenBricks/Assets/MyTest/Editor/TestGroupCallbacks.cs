using NUnit.Framework;
using ECS;
using System;

public class TestGroupCallbacks {

	class Component1 : IComponent
	{
		
	}

	class Component2 : IComponent
	{
		
	}

	class Component3 : IComponent
	{

	}

	class ListenerMock : IEntityAddedEventListener, IEntityRemovedEventListener, IEntityRemovingEventListener
	{
		public int addedCalls;

		public int removedCalls;

		public int removingCalls;

		public event Action<Entity> RemovedEvent;

		public event Action<Entity> RemovingEvent;

		#region IEntityAddedEventListener implementation
		public void OnEntityAdded (object sender, Entity entity)
		{
			addedCalls++;
		}

        public void OnEntityRemoved(object sender, Entity entity)
        {
            removedCalls++;
			if (RemovedEvent != null)
				RemovedEvent(entity);
        }

        public void OnEntityRemoving(object sender, Entity entity)
        {
            removingCalls++;
			if (RemovingEvent != null)
				RemovingEvent(entity);
        }
        #endregion

    }

	[Test]
	public void EntityAddedShouldBeCalledOnceWhenAllComponentsFromGroup() {
		// Use the Assert class to test conditions.

		var entityManager = new EntityManager ();
		var group = entityManager.GetComponentGroup (typeof(Component1), typeof(Component2));

		var listenerMock = new ListenerMock ();
		group.SubscribeOnEntityAdded (listenerMock);

		Assert.That (listenerMock.addedCalls, Is.EqualTo (0));

		var e = entityManager.CreateEntity ();
		entityManager.AddComponent (e, new Component1 ());

		Assert.That (listenerMock.addedCalls, Is.EqualTo (0));

		entityManager.AddComponent (e, new Component2 ());
		Assert.That (listenerMock.addedCalls, Is.EqualTo (1));
	}

	[Test]
	public void EntityAddedToGroupShouldNotBeCalledIfDifferentComponent() {
		// Use the Assert class to test conditions.

		var entityManager = new EntityManager ();
		var group = entityManager.GetComponentGroup (typeof(Component1), typeof(Component2));

		var listenerMock = new ListenerMock ();
		group.SubscribeOnEntityAdded (listenerMock);

		var e = entityManager.CreateEntity ();
		Assert.That (listenerMock.addedCalls, Is.EqualTo (0));
		entityManager.AddComponent (e, new Component1 ());
		Assert.That (listenerMock.addedCalls, Is.EqualTo (0));
		entityManager.AddComponent (e, new Component2 ());
		Assert.That (listenerMock.addedCalls, Is.EqualTo (1));
		entityManager.AddComponent (e, new Component3 ());
		Assert.That (listenerMock.addedCalls, Is.EqualTo (1));
	}

	[Test]
	public void EntityRemovedCallbackShouldBeCalledWithComponents2() {
		var entityManager = new EntityManager ();

		var systemRoot = new SystemRoot<EntityManager>(entityManager);

		var group = entityManager.GetComponentGroup (typeof(Component1), typeof(Component2));

		var listenerMock = new ListenerMock ();
		// group.SubscribeOnEntityAdded(listenerMock);
		// group.SubscribeOnEntityRemoved (listenerMock);
		group.SubscribeOnEntityRemoving(listenerMock);

		// group.component

		var e = entityManager.CreateEntity ();

		entityManager.AddComponent (e, new Component1());
		entityManager.AddComponent (e, new Component2());

		// Assert.That (listenerMock.addedCalls, Is.EqualTo (1));

		// listenerMock.RemovedEvent += delegate (Entity e1) {
		// 	Assert.True(entityManager.HasComponent<Component1>(e1));
		// 	Assert.True(entityManager.HasComponent<Component2>(e1));
		// };

		listenerMock.RemovingEvent += delegate (Entity e1) {
			Assert.True(entityManager.HasComponent<Component1>(e1));
			Assert.True(entityManager.HasComponent<Component2>(e1));
		};


		entityManager.RemoveComponent<Component1>(e);
		// entityManager.RemoveComponentImmediate<Component1>(e);

		systemRoot.Update();

		Assert.That (listenerMock.removingCalls, Is.EqualTo (1));
	}


}
