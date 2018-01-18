using ECS;
using Gemserk.ECS;
using MyTest.Components;

namespace MyTest.Systems
{
	public class ControllerSystem : ComponentSystem
	{
		[InjectDependency]
		protected EntityManager _entityManager;

		ComponentTuple<ControllerComponent, MovementPhysicsComponent> _tuple;

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

				var movementDirection = controllerComponent.movement;
				movementDirection.y = 0;

                movementPhysicsComponent.direction = movementDirection.normalized;

				controllerComponent.movement = new UnityEngine.Vector3();
				
				_tuple.component1 = controllerComponent;
				_tuple.component2 = movementPhysicsComponent;	
			}
		}

	}

}