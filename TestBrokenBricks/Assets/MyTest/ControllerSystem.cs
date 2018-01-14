using ECS;
using MyTest.Components;

namespace MyTest.Systems
{
	public class ControllerSystem : ComponentSystem
	{
		ComponentGroup _group;

		[InjectDependency]
		protected EntityManager _entityManager;

		public override void OnStart ()
		{
			base.OnStart ();
			_group = _entityManager.GetComponentGroup (typeof(ControllerComponent), typeof(MovementPhysicsComponent));
		}

		public override void OnFixedUpdate ()
		{
			base.OnFixedUpdate ();

			var controllers = _group.GetComponent<ControllerComponent> ();
			var movements = _group.GetComponent<MovementPhysicsComponent> ();

			for (int i = 0; i < controllers.Length; i++) {
				movements [i].direction = controllers [i].movement.normalized;
			}
		}

	}

}