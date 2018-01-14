using ECS;
using UnityEngine;
using MyTest.Components;

namespace MyTest.Systems
{
	public class InputSystem : ComponentSystem
	{
		public class GenericComponentTuple<T1, T2> 
			where T1 : struct, IComponent 
			where T2: struct, IComponent 
		{
			ComponentGroup group;

			ComponentArray<T1> componentArray1;
			ComponentArray<T2> componentArray2;

			public T1 component1 {
				get {
					return componentArray1[_entityIndex];
				} 
				set {
					componentArray1.GetEntity(_entityIndex).SetComponent(value);
				}
			}
			
			public T2 component2 {
				get {
					return componentArray2[_entityIndex];
				} 
				set {
					componentArray2.GetEntity(_entityIndex).SetComponent(value);
				}
			}

			int _entityIndex;

			public int EntityIndex {
				set {
					_entityIndex = value;
				}
			}

			public int Count {
				get {
					return componentArray1.Length;
				}
			}

			public GenericComponentTuple(EntityManager manager)
			{
				group = manager.GetComponentGroup(typeof(T1), typeof(T2));
				componentArray1 = group.GetComponent<T1>();
				componentArray2 = group.GetComponent<T2>();
			}

			public void SetEntityIndex(int i)
			{
				component1 = componentArray1[i];
				component2 = componentArray2[i];
			}

		}

		public class ComponentTuple {

			ComponentGroup group;

			ComponentArray<InputComponent> inputArray;
			ComponentArray<ControllerComponent> controllerArray;

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

			int _entityIndex;

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
				group = manager.GetComponentGroup(typeof(InputComponent), typeof(ControllerComponent));
				inputArray = group.GetComponent<InputComponent>();
				controllerArray = group.GetComponent<ControllerComponent>();
			}

			public void SetEntityIndex(int i)
			{
				inputComponent = inputArray[i];
				controllerComponent = controllerArray[i];
			}

		}

		// ComponentGroup _group;

		[InjectDependency]
		protected EntityManager _entityManager;

		// ComponentTuple tuple;
		GenericComponentTuple<InputComponent, ControllerComponent> tuple;

		public override void OnStart ()
		{
			base.OnStart ();
			// _group = _entityManager.GetComponentGroup (typeof(InputComponent), typeof(ControllerComponent));
			// tuple = new ComponentTuple(_entityManager);
			tuple = new GenericComponentTuple<InputComponent, ControllerComponent>(_entityManager);
		}

		public override void OnFixedUpdate ()
		{
			base.OnFixedUpdate ();

			// var inputArray = _group.GetComponent<InputComponent> ();
			// var controllerArray = _group.GetComponent<ControllerComponent> ();

			for (int i = 0; i < tuple.Count; i++)
			{
				tuple.EntityIndex = i;

				// var controller = tuple.controllerComponent;
				// var input = tuple.inputComponent;
				var input = tuple.component1;
				var controller = tuple.component2;
				
				controller.movement = new Vector2 () { 
					x = Input.GetAxis(input.horizontalAxisName),
					y = Input.GetAxis(input.verticalAxisName),
				};

				controller.isJumpPressed = Input.GetButton (input.jumpActionName);
				
				tuple.component2 = controller;

				// tuple.ControllerComponent = (i, controller);
				// controllerArray.GetEntity(i).SetComponent(controller);
			}

			// for (int i = 0; i < inputArray.Length; i++) {
			// 	var controller = controllerArray [i];

			// 	controller.movement = new Vector2 () { 
			// 		x = Input.GetAxis(inputArray[i].horizontalAxisName),
			// 		y = Input.GetAxis(inputArray[i].verticalAxisName),
			// 	};

			// 	controller.isJumpPressed = Input.GetButton (inputArray [i].jumpActionName);
				
			// 	controllerArray.GetEntity(i).SetComponent(controller);
			// }
		}

	}
	
}