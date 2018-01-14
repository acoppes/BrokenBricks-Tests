using ECS;
using UnityEngine;
using MyTest.Components;

namespace MyTest.Systems
{
	public class IABehaviourSystem : ComponentSystem
	{
		ComponentGroup _group;

		[InjectDependency]
		protected EntityManager _entityManager;

		public override void OnStart ()
		{
			base.OnStart ();

			_group = _entityManager.GetComponentGroup (
				typeof(IABehaviourComponent), 
				typeof(ControllerComponent),
				typeof(PositionComponent)
			);
		}

		public override void OnFixedUpdate ()
		{
			base.OnFixedUpdate ();

			var dt = Time.deltaTime;

			var behaviourArray = _group.GetComponent<IABehaviourComponent> ();
			var controllerArray = _group.GetComponent<ControllerComponent> ();
			var positionArray = _group.GetComponent<PositionComponent> ();

			for (int i = 0; i < behaviourArray.Length; i++) {
			
				var behaviourComponent = behaviourArray [i];
				var controllerComponent = controllerArray [i];
				var positionComponent = positionArray [i];

				controllerComponent.movement = Vector2.zero;

				if (behaviourComponent.waitingForAction) {
					behaviourComponent.actionTime += dt;

					if (behaviourComponent.actionTime > behaviourComponent.waitForActionTime) {
						// decide next action...

						var nextAction = UnityEngine.Random.Range (0, 2);
						if (nextAction == 0) {
							behaviourComponent.waitingForAction = true;
							behaviourComponent.walking = false;
							behaviourComponent.actionTime = 0;
						} else if (nextAction == 1) {
							behaviourComponent.walking = true;
							behaviourComponent.waitingForAction = false;
							behaviourComponent.destination = (Vector3) UnityEngine.Random.insideUnitCircle * behaviourComponent.maxRandomDistance;
						}
					}
				} else if (behaviourComponent.walking) {
				
					// walk to destination

					controllerComponent.movement = (behaviourComponent.destination - positionComponent.position).normalized;

					if (Vector3.Distance (positionComponent.position, behaviourComponent.destination) < 0.1f) {

						var nextAction = UnityEngine.Random.Range (0, 2);
						if (nextAction == 0) {
							behaviourComponent.waitingForAction = true;
							behaviourComponent.walking = false;
							behaviourComponent.actionTime = 0;
						} else if (nextAction == 1) {
							behaviourComponent.walking = true;
							behaviourComponent.waitingForAction = false;
							behaviourComponent.destination = (Vector3) UnityEngine.Random.insideUnitCircle * behaviourComponent.maxRandomDistance;
						}
							
					}

				}
				
				behaviourArray.GetEntity(i).SetComponent(behaviourComponent);
				controllerArray.GetEntity(i).SetComponent(controllerComponent);
			}
		}
	}

}