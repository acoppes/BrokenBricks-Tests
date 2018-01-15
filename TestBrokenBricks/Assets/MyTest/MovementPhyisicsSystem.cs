using ECS;
using UnityEngine;
using MyTest.Components;
using Gemserk.ECS;

namespace MyTest.Systems
{
	public class MovementPhyisicsSystem : ComponentSystem
	{
		[InjectDependency]
		protected EntityManager _entityManager;

		ComponentTuple<MovementPhysicsComponent, PositionComponent, DelegatePhysicsComponent> _tuple;

		public override void OnStart ()
		{
			base.OnStart ();
			_tuple = new ComponentTuple<MovementPhysicsComponent, PositionComponent, DelegatePhysicsComponent>(_entityManager);
		}

		public override void OnFixedUpdate ()
		{
			base.OnFixedUpdate ();

			var dt = Time.deltaTime;

			Vector3 horizontalForce = new Vector3();

			for (int i = 0; i < _tuple.Count; i++) {
				_tuple.EntityIndex = i;

				var movementComponent = _tuple.component1;
				var positionComponent = _tuple.component2;
				var physicsComponent = _tuple.component3;

				var velocity = physicsComponent.velocity;
				velocity.y = 0.0f;

				if (movementComponent.direction.sqrMagnitude > 0) {

					var moveForce = (Vector3) movementComponent.direction.normalized * movementComponent.force;

					var maxSpeedHorizontal = movementComponent.maxSpeedHorizontal;

					if (maxSpeedHorizontal > 0) {
						// this does some "estimation" of physics behaviour.. not sure if here is the right
						// place but want to do this logic only for movement...
						var vh = physicsComponent.velocity + moveForce * dt;
						vh.Set (vh.x, 0, vh.z);

						if (vh.sqrMagnitude > (maxSpeedHorizontal * maxSpeedHorizontal) && dt > 0) {
							var limitForce = (vh - (vh.normalized * maxSpeedHorizontal)) / dt;
							moveForce += (limitForce * -1);
						} 
					}

					physicsComponent.AddForce (moveForce);

				} else {
					if (physicsComponent.IsOnFloor ()) {
						physicsComponent.AddForce (physicsComponent.velocity * physicsComponent.frictionMultiplier * -1.0f);
					}
				}

				var position = physicsComponent.position;
				position.y = positionComponent.position.y;

				if (Mathf.Abs (physicsComponent.velocity.x) > 0) { 
					positionComponent.lookingDirection = physicsComponent.velocity.normalized;
				}

				positionComponent.position = position;

				_tuple.component2 = positionComponent;
				_tuple.component3 = physicsComponent;
			}
		}

	}

}