using ECS;
using UnityEngine;
using MyTest.Components;

namespace MyTest.Systems
{
	public class InputSystem : ComponentSystem
	{
		ComponentGroup _group;

		[InjectDependency]
		protected EntityManager _entityManager;

		public override void OnStart ()
		{
			base.OnStart ();
			_group = _entityManager.GetComponentGroup (typeof(InputComponent), typeof(ControllerComponent));
		}

		public override void OnFixedUpdate ()
		{
			base.OnFixedUpdate ();

			var inputArray = _group.GetComponent<InputComponent> ();
			var controllerArray = _group.GetComponent<ControllerComponent> ();

			for (int i = 0; i < inputArray.Length; i++) {
				var controller = controllerArray [i];

				controller.movement = new Vector2 () { 
					x = Input.GetAxis(inputArray[i].horizontalAxisName),
					y = Input.GetAxis(inputArray[i].verticalAxisName),
				};

				controller.isJumpPressed = Input.GetButton (inputArray [i].jumpActionName);
				
				_entityManager.SetComponent(controllerArray.GetEntity(i), controller);
			}
		}

	}
	
}