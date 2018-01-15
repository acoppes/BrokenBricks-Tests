using ECS;
using Gemserk.ECS;
using MyTest.Components;
using UnityEngine;

namespace MyTest.Systems
{
	public class DelegatePhysicsSystem : ComponentSystem
	{
		[InjectDependency]
		protected EntityManager _entityManager;

		ComponentTuple<DelegatePhysicsComponent> _tuple;

		readonly Vector3 gravity = new Vector3(0, -9.8f, 0.0f);

		public override void OnStart ()
		{
			base.OnStart ();
			_tuple = new ComponentTuple<DelegatePhysicsComponent>(_entityManager);
		}

		public override void OnFixedUpdate ()
		{
			base.OnFixedUpdate ();

			var dt = Time.deltaTime;

			for (int i = 0; i < _tuple.Count; i++) {
				_tuple.EntityIndex = i;

				var physicsComponent = _tuple.component1;

				if (!physicsComponent.IsOnFloor())
					physicsComponent.AddForce (gravity * physicsComponent.gravityMultiplier);

				physicsComponent.force = Vector3.ClampMagnitude(physicsComponent.force, physicsComponent.maxForce);

				if (physicsComponent.force.sqrMagnitude > 0.0001f) {
					Vector3 deltaV = physicsComponent.force * dt;
					physicsComponent.velocity += deltaV;
				}

				if (physicsComponent.velocity.sqrMagnitude < 0.0001f)
					physicsComponent.velocity.Set(0, 0, 0);

				physicsComponent.position += physicsComponent.velocity * dt;

				physicsComponent.force = Vector3.zero;

				// colliison with floor
				if (physicsComponent.position.y < 0.0f) {
					physicsComponent.StopAtHeight (0.0f);
				}

				_tuple.component1 = physicsComponent;
			}
		}

	}

}