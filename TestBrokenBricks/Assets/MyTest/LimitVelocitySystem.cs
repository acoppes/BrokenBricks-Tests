using ECS;
using Gemserk.ECS;
using MyTest.Components;
using UnityEngine;

namespace MyTest.Systems
{
	public class LimitVelocitySystem : ComponentSystem
	{
		ComponentTuple<DelegatePhysicsComponent, LimitVelocityComponent> _tuple;

		[InjectDependency]
		protected EntityManager _entityManager;

		public override void OnStart ()
		{
			base.OnStart ();
			_tuple = new ComponentTuple<DelegatePhysicsComponent, LimitVelocityComponent>(_entityManager);
		}

		public override void OnFixedUpdate ()
		{
			base.OnFixedUpdate ();

			var dt = Time.deltaTime;

			Vector3 horizontalForce = new Vector3();

			// this will limit also an explosion impulse for example

			for (int i = 0; i < _tuple.Count; i++) {
				_tuple.EntityIndex = i;

				var physicsComponent = _tuple.component1;
				var limitVelocityComponent = _tuple.component2;

				var force = physicsComponent.force;

				horizontalForce.Set(force.x, force.y, 0);
				var vh = physicsComponent.velocity + horizontalForce * dt;
				vh.Set (vh.x, vh.y, 0);

				var maxSpeedHorizontal = limitVelocityComponent.maxSpeedHorizontal;

				if (maxSpeedHorizontal > 0) {
					if (vh.sqrMagnitude > (maxSpeedHorizontal * maxSpeedHorizontal) && dt > 0) {
						var limitForce = (vh - (vh.normalized * maxSpeedHorizontal)) / dt;
						physicsComponent.AddForce (limitForce * -1);
					} 
				}
					
				_tuple.component1 = physicsComponent;
			}
		}

	}
}