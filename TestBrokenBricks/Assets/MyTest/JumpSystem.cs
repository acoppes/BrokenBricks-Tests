using ECS;
using MyTest.Components;
using UnityEngine;

namespace MyTest.Systems
{
	public class JumpSystem : ComponentSystem
	{
		ComponentGroup _group;

		[InjectDependency]
		protected EntityManager _entityManager;

		public override void OnStart ()
		{
			base.OnStart ();
			_group = _entityManager.GetComponentGroup (typeof(ControllerComponent), 
				typeof(JumpComponent), 
				typeof(PositionComponent), 
				typeof(DelegatePhysicsComponent));
		}

		public override void OnFixedUpdate ()
		{
			base.OnFixedUpdate ();

			var controllersArray = _group.GetComponent<ControllerComponent> ();
			var jumpsArray = _group.GetComponent<JumpComponent> ();
			var positionsArray = _group.GetComponent<PositionComponent> ();
			var physicsArray = _group.GetComponent<DelegatePhysicsComponent> ();

			for (int i = 0; i < controllersArray.Length; i++) {
				var jumpComponent = jumpsArray [i];
				var physicsComponent = physicsArray [i];
				var positionComponent = positionsArray[i];
				var controllerComponent = controllersArray [i];

				if (!jumpComponent.isFalling && !jumpComponent.isJumping && Mathf.Abs(physicsComponent.position.z) < Mathf.Epsilon) {
					jumpComponent.isJumping = controllerComponent.isJumpPressed;
					if (jumpComponent.isJumping) {
						jumpComponent.currentJumpForce = jumpComponent.jumpForce;
					}
				}

				// TODO: we could use our custom GlobalTime

				var position = positionComponent.position;
				position.z = physicsComponent.position.z;
			
				if (jumpComponent.isJumping) {

					physicsComponent.AddForce (new Vector3 (0, 0, 1) * jumpComponent.currentJumpForce);
					jumpComponent.currentJumpForce -= jumpComponent.jumpStopFactor * Time.deltaTime;

					if (jumpComponent.currentJumpForce <= 0 || !controllerComponent.isJumpPressed) {
						jumpComponent.currentJumpForce = 0;

						if (physicsComponent.velocity.z <= 0) {
							jumpComponent.isJumping = false;
							jumpComponent.isFalling = true;					
						}
					}
						
				} else if (!jumpComponent.isFalling && position.z > 0) {
					jumpComponent.isFalling = true;
				}

				if (jumpComponent.isFalling) {

					if (Mathf.Abs(position.z - 0.0f) < Mathf.Epsilon) {
						position.z = 0;
						jumpComponent.isFalling = false;
					}
				}

				positionComponent.position = position;

				positionsArray.GetEntity(i).SetComponent(positionComponent);
				jumpsArray.GetEntity(i).SetComponent(jumpComponent);
			}
		}

	}
}