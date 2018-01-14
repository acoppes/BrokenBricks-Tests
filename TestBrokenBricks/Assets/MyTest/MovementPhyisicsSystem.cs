using ECS;
using UnityEngine;
using MyTest.Components;

namespace MyTest.Systems
{
	public class MovementPhyisicsSystem : ComponentSystem
	{
		ComponentGroup _group;

		[InjectDependency]
		protected EntityManager _entityManager;

		public override void OnStart ()
		{
			base.OnStart ();

			_group = _entityManager.GetComponentGroup (
				typeof(MovementPhysicsComponent), 
				typeof(PositionComponent),
				typeof(DelegatePhysicsComponent)
			);
		}

		public override void OnFixedUpdate ()
		{
			base.OnFixedUpdate ();

			var dt = Time.deltaTime;

			var movementArray = _group.GetComponent<MovementPhysicsComponent> ();
			var positionArray = _group.GetComponent<PositionComponent> ();
			var physicsArray = _group.GetComponent<DelegatePhysicsComponent> ();

			Vector3 horizontalForce = new Vector3();

			for (int i = 0; i < movementArray.Length; i++) {
				var movementComponent = movementArray [i];
				var positionComponent = positionArray [i];
				var physicsComponent = physicsArray [i];

				var v = physicsComponent.velocity;
				v.z = 0.0f;

				if (movementComponent.direction.sqrMagnitude > 0) {

					var moveForce = (Vector3) movementComponent.direction.normalized * movementComponent.force;

					var maxSpeedHorizontal = movementComponent.maxSpeedHorizontal;

					if (maxSpeedHorizontal > 0) {
						// this does some "estimation" of physics behaviour.. not sure if here is the right
						// place but want to do this logic only for movement...
						var vh = physicsComponent.velocity + moveForce * dt;
						vh.Set (vh.x, vh.y, 0);

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
				position.z = positionComponent.position.z;

				if (Mathf.Abs (physicsComponent.velocity.x) > 0) { 
					positionComponent.lookingDirection = physicsComponent.velocity.normalized;
				}

				positionComponent.position = position;

				positionArray.GetEntity(i).SetComponent(positionComponent);
			}
		}

	}

}