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

			var inputs = _group.GetComponent<InputComponent> ();
			var controllers = _group.GetComponent<ControllerComponent> ();

			for (int i = 0; i < inputs.Length; i++) {

				controllers [i].movement = new Vector2 () { 
					x = Input.GetAxis(inputs[i].horizontalAxisName),
					y = Input.GetAxis(inputs[i].verticalAxisName),
				};

				controllers [i].isJumpPressed = Input.GetButton (inputs [i].jumpActionName);
			}
		}

	}
	
}