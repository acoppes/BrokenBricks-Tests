using ECS;
using Gemserk.ECS;
using MyTest.Components;
using UnityEngine;

namespace MyTest.Systems
{
	public class JumpSystem : ComponentSystem
	{
		[InjectDependency]
		protected EntityManager _entityManager;

		ComponentTuple<ControllerComponent, JumpComponent, PositionComponent, DelegatePhysicsComponent> _tuple;

		public override void OnStart ()
		{
			base.OnStart ();
			_tuple = new ComponentTuple<ControllerComponent, JumpComponent, PositionComponent, DelegatePhysicsComponent>(_entityManager);
		}

		public override void OnFixedUpdate ()
		{
			base.OnFixedUpdate ();

			float dt = Time.deltaTime;

			for (int i = 0; i < _tuple.Count; i++) {
				_tuple.EntityIndex = i;

				var controllerComponent = _tuple.component1;
				var jumpComponent = _tuple.component2;
				var positionComponent = _tuple.component3;
				var physicsComponent = _tuple.component4;

				if (!jumpComponent.isFalling && !jumpComponent.isJumping && Mathf.Abs(physicsComponent.position.y) < Mathf.Epsilon) {
					jumpComponent.isJumping = controllerComponent.isJumpPressed;
					if (jumpComponent.isJumping) {
						jumpComponent.currentJumpForce = jumpComponent.jumpForce;
					}
				}

				// TODO: we could use our custom GlobalTime

				var position = positionComponent.position;
				position.y = physicsComponent.position.y;
			
				if (jumpComponent.isJumping) {

					physicsComponent.AddForce (new Vector3 (0, 1, 0) * jumpComponent.currentJumpForce);
					jumpComponent.currentJumpForce -= jumpComponent.jumpStopFactor * dt;

					if (jumpComponent.currentJumpForce <= 0 || !controllerComponent.isJumpPressed) {
						jumpComponent.currentJumpForce = 0;

						if (physicsComponent.velocity.y <= 0) {
							jumpComponent.isJumping = false;
							jumpComponent.isFalling = true;					
						}
					}
						
				} else if (!jumpComponent.isFalling && position.y > 0) {
					jumpComponent.isFalling = true;
				}

				if (jumpComponent.isFalling) {

					if (Mathf.Abs(position.y - 0.0f) < Mathf.Epsilon) {
						position.y = 0;
						jumpComponent.isFalling = false;
					}
				}

				positionComponent.position = position;

				_tuple.component2 = jumpComponent;
				_tuple.component3 = positionComponent;
				_tuple.component4 = physicsComponent;
			}
		}

	}
}