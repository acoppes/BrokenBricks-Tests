using ECS;
using UnityEngine;
using MyTest.Components;
using Gemserk.ECS;

namespace MyTest.Systems
{
	public class InputSystem : ComponentSystem
	{
		[InjectDependency]
		protected EntityManager _entityManager;

		ComponentTuple<InputComponent, ControllerComponent> tuple;

		public override void OnStart ()
		{
			base.OnStart ();
			tuple = new ComponentTuple<InputComponent, ControllerComponent>(_entityManager);
		}

		public override void OnFixedUpdate ()
		{
			base.OnFixedUpdate ();

			for (int i = 0; i < tuple.Count; i++)
			{
				tuple.EntityIndex = i;

				var input = tuple.component1;
				var controller = tuple.component2;
				
				controller.movement = new Vector3 () { 
					x = Input.GetAxis(input.horizontalAxisName),
					y = 0,
					z = Input.GetAxis(input.verticalAxisName),
				};

				controller.isJumpPressed = Input.GetButton (input.jumpActionName);
				
				tuple.component2 = controller;
			}
		}

	}
	
}