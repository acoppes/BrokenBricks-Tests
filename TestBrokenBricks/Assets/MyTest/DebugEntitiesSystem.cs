using ECS;
using UnityEngine;
using MyTest.Components;

public class DebugEntitiesSystem : ComponentSystem, IEntityAddedEventListener, IEntityRemovedEventListener
{
	class DebugBehaviour<T> : ScriptBehaviour where T : struct, IComponent
	{
		[InjectDependency]
		protected EntityManager _entityManager;

		public Entity entity;

		public T debugComponent = new T();

		bool _serializedOnce;

		protected bool HasComponent()
		{
			return _entityManager.HasComponent<T> (entity);
		}

		protected T GetComponent()
		{
			return _entityManager.GetComponent<T> (entity);
		}

		void SerializeFromEntity()
		{
			if (HasComponent()) {
				var component = GetComponent();
				// JsonUtility.FromJsonOverwrite (JsonUtility.ToJson (component), debugComponent);
				debugComponent = JsonUtility.FromJson<T>(JsonUtility.ToJson (component));
			}			
		}

		void SerializeToEntity()
		{
			if (HasComponent()) {
				// var component = GetComponent ();
				// JsonUtility.FromJsonOverwrite (JsonUtility.ToJson (debugComponent), component);
				var component = JsonUtility.FromJson<T>(JsonUtility.ToJson (debugComponent));
				_entityManager.SetComponent<T>(entity, component);
			}			
		}

		void FixedUpdate()
		{
			SerializeFromEntity();		
		}

		void OnValidate()
		{
			// serialize back	
			if (!_serializedOnce) {
				SerializeFromEntity ();
				_serializedOnce = true;
			}
			SerializeToEntity();		
		}
	}
	
	class DelegatePhysicsBehaviour : DebugBehaviour<DelegatePhysicsComponent> 
	{
		#if UNITY_EDITOR

		void OnDrawGizmos()
		{
			if (HasComponent()) {
				var physicsComponent = GetComponent();

				var unityPosition = new Vector3(physicsComponent.position.x, physicsComponent.position.z, physicsComponent.position.y);
				var unityVelocity = new Vector3(physicsComponent.velocity.x, physicsComponent.velocity.z, physicsComponent.velocity.y);

				UnityEngine.Gizmos.DrawLine(unityPosition, unityPosition + unityVelocity);

			}	
		}

		#endif
	}

	class DelegateLimitVelocityBehaviour : DebugBehaviour<LimitVelocityComponent>
	{
		
	}

	class DelegateTargetingComponent : DebugBehaviour<TargetingComponent>
	{
		void OnDrawGizmos()
		{
			if (HasComponent()) {
				var targetingComponent = GetComponent();
				
				var color = UnityEngine.Gizmos.color;

				for (int i = 0; i < targetingComponent.targetings.Length; i++)
				{
					var targeting = targetingComponent.targetings[i];

					if (_entityManager.HasComponent<PositionComponent>(entity)) {
						UnityEngine.Gizmos.color = Color.blue;
						var positionComponent =  _entityManager.GetComponent<PositionComponent>(entity);
						var p = positionComponent.position;
						var p1 = new Vector3(p.x, p.z, p.y);
						UnityEngine.Gizmos.DrawWireCube(targeting.query.bounds.center + p1, targeting.query.bounds.extents);
					}

					if (targeting.targetNode == null)
						continue;

					UnityEngine.Gizmos.color = Color.red;
					UnityEngine.Gizmos.DrawWireSphere(targeting.targetNode.target.bounds.center, 0.1f);
				}

				UnityEngine.Gizmos.color = color;
			}	
		}
	}

	class DelegateTargetComponent : DebugBehaviour<TargetComponent>
	{
		void OnDrawGizmos()
		{
			if (HasComponent()) {
				var targetComponent = GetComponent();
				
				var color = UnityEngine.Gizmos.color;

				for (int i = 0; i < targetComponent.targets.Length; i++)
				{
					var target = targetComponent.targets[i];

					if (_entityManager.HasComponent<PositionComponent>(entity)) {
						UnityEngine.Gizmos.color = Color.yellow;
						var positionComponent =  _entityManager.GetComponent<PositionComponent>(entity);
						var p = positionComponent.position;
						var p1 = new Vector3(p.x, p.z, p.y);
						UnityEngine.Gizmos.DrawWireCube(target.bounds.center + p1, target.bounds.extents);
					}
				}

				UnityEngine.Gizmos.color = color;
			}	
		}
	}


	class DebugComponent : IComponent
	{
		public DebugBehaviour<DelegatePhysicsComponent> debug;
	}

	[InjectDependency]
	EntityManager _entityManager;

	Transform _entitiesParent;

	public override void OnStart ()
	{
		base.OnStart ();
		_entitiesParent = new GameObject ("~Entities").transform;
	
		_entityManager.SubscribeOnEntityAdded (this);
	}

	public void OnEntityAdded (object sender, Entity entity)
	{
		// base.OnEntityAdded (sender, entity);

		var entityObject = new GameObject ("Entity-" + entity.Id);
//		var debug = entityObject.AddComponent<DelegatePhysicsComponent> ();
		var debug = entityObject.AddComponent<DelegatePhysicsBehaviour> ();
		entityObject.AddComponent<DelegateLimitVelocityBehaviour> ().entity = entity;
		entityObject.AddComponent<DelegateTargetingComponent> ().entity = entity;
		entityObject.AddComponent<DelegateTargetComponent> ().entity = entity;

		debug.entity = entity;

		entityObject.transform.SetParent (_entitiesParent);

		_entityManager.AddComponent (entity, new DebugComponent () {
			debug = debug
		});
	}

	public void OnEntityRemoved (object sender, Entity entity)
	{
		var debugComponent = _entityManager.GetComponent<DebugComponent> (entity);

		if (debugComponent.debug != null) {
			GameObject.Destroy (debugComponent.debug.gameObject);
			debugComponent.debug = null;
		}
	}
}