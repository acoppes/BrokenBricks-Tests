using ECS;

namespace Gemserk.ECS
{
	public struct ComponentTuple<T1, T2>
		where T1 : struct, IComponent 
		where T2: struct, IComponent {

		// why is the ComponentGroup in the middile of all, I believe it shouldn't be necessary.
		ComponentGroup group;

		ComponentArray<T1> componentArray1;
		ComponentArray<T2> componentArray2;

		int _entityIndex;

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

        public ComponentGroup Group
        {
            get
            {
                return group;
            }
        }

        public ComponentTuple(EntityManager entityManager)
		{
			_entityIndex = 0;
			group = entityManager.GetComponentGroup(typeof(T1), typeof(T2));
			componentArray1 = group.GetComponent<T1>();
			componentArray2 = group.GetComponent<T2>();
		}

	}
}