using ECS;
using UnityEngine;
using MyTest.Components;

namespace MyTest.Systems
{
	public class InputSystem : ComponentSystem
	{
		[InjectDependency]
		protected EntityManager _entityManager;

		Gemserk.ECS.ComponentTuple<InputComponent, ControllerComponent> tuple;

		public override void OnStart ()
		{
			base.OnStart ();
			tuple = new Gemserk.ECS.ComponentTuple<InputComponent, ControllerComponent>(_entityManager);
		}

		public override void OnFixedUpdate ()
		{
			base.OnFixedUpdate ();

			for (int i = 0; i < tuple.Count; i++)
			{
				tuple.EntityIndex = i;

				var input = tuple.component1;
				var controller = tuple.component2;
				
				controller.movement = new Vector2 () { 
					x = Input.GetAxis(input.horizontalAxisName),
					y = Input.GetAxis(input.verticalAxisName),
				};

				controller.isJumpPressed = Input.GetButton (input.jumpActionName);
				
				tuple.component2 = controller;
			}
		}

	}
	
}