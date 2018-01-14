using ECS;

namespace Gemserk.ECS
{
	public struct ComponentTuple<T1>
		where T1 : struct, IComponent {

		// why is the ComponentGroup in the middile of all, I believe it shouldn't be necessary.
		ComponentGroup group;

		ComponentArray<T1> componentArray1;

		int _entityIndex;

		public T1 component1 {
			get {
				return componentArray1[_entityIndex];
			} 
			set {
				componentArray1.GetEntity(_entityIndex).SetComponent(value);
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
			group = entityManager.GetComponentGroup(typeof(T1));
			componentArray1 = group.GetComponent<T1>();
		}

	}

	public struct ComponentTuple<T1, T2>
		where T1 : struct, IComponent 
		where T2 : struct, IComponent {

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

	public struct ComponentTuple<T1, T2, T3>
		where T1 : struct, IComponent 
		where T2 : struct, IComponent
		where T3 : struct, IComponent {

		// why is the ComponentGroup in the middile of all, I believe it shouldn't be necessary.
		ComponentGroup group;

		ComponentArray<T1> componentArray1;
		ComponentArray<T2> componentArray2;
		ComponentArray<T3> componentArray3;

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

		public T3 component3 {
			get {
				return componentArray3[_entityIndex];
			} 
			set {
				componentArray3.GetEntity(_entityIndex).SetComponent(value);
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
			group = entityManager.GetComponentGroup(typeof(T1), typeof(T2), typeof(T3));
			componentArray1 = group.GetComponent<T1>();
			componentArray2 = group.GetComponent<T2>();
			componentArray3 = group.GetComponent<T3>();
		}

	}
}