using ECS;
using Gemserk.ECS;
using MyTest.Components;

namespace MyTest.Systems
{
	public class ControllerSystem : ComponentSystem
	{
		ComponentTuple<ControllerComponent, MovementPhysicsComponent> _tuple;

		[InjectDependency]
		protected EntityManager _entityManager;

		public override void OnStart ()
		{
			base.OnStart ();
			_tuple = new ComponentTuple<ControllerComponent, MovementPhysicsComponent>(_entityManager);
		}

		public override void OnFixedUpdate ()
		{
			base.OnFixedUpdate ();

			for (int i = 0; i < _tuple.Count; i++) {
				_tuple.EntityIndex = i;

                var controllerComponent = _tuple.component1;
                var movementPhysicsComponent = _tuple.component2;

                movementPhysicsComponent.direction = controllerComponent.movement.normalized;
				
				_tuple.component2 = movementPhysicsComponent;			}
		}

	}

}