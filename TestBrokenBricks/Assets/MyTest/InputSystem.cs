using ECS;
using UnityEngine;
using MyTest.Components;

namespace MyTest.Systems
{
	public class InputSystem : ComponentSystem
	{
		public struct ComponentTuple {

			ComponentGroup group;

			ComponentArray<InputComponent> inputArray;
			ComponentArray<ControllerComponent> controllerArray;
			int _entityIndex;

			public InputComponent inputComponent {
				get {
					return inputArray[_entityIndex];
				} 
				set {
					inputArray.GetEntity(_entityIndex).SetComponent(value);
				}
			}
			
			public ControllerComponent controllerComponent {
				get {
					return controllerArray[_entityIndex];
				} 
				set {
					controllerArray.GetEntity(_entityIndex).SetComponent(value);
				}
			}

			public int EntityIndex {
				set {
					_entityIndex = value;
				}
			}

			public int Count {
				get {
					return inputArray.Length;
				}
			}

			public ComponentTuple(EntityManager manager)
			{
				_entityIndex = 0;
				group = manager.GetComponentGroup(typeof(InputComponent), typeof(ControllerComponent));
				inputArray = group.GetComponent<InputComponent>();
				controllerArray = group.GetComponent<ControllerComponent>();
			}

		}

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