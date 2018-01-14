using ECS;
using MyTest.Components;
using MyTest.Systems;
using UnityEngine;

public class MyTestSceneController : ECSController<UnityStandardSystemRoot, UnityEntityManager> {

	public GameObject viewPrefab;

	protected override void Initialize() {

		// InjectionManager.CreateObject(typeof(SpatialStructure<MyTest.Components.TargetNode>));
		
		var spatialStructure = new SpatialStructure<TargetNode>();

		#if UNITY_EDITOR
		AddSystem<DebugEntitiesSystem> ();
		#endif

		AddSystem<MovementPhyisicsSystem>();
		AddSystem<ViewSystem>();

		AddSystem<InputSystem> ();
		AddSystem<IABehaviourSystem> ();

		AddSystem<ControllerSystem> ();
		AddSystem<JumpSystem> ();

		AddSystem<LimitVelocitySystem> ();
		AddSystem<DelegatePhysicsSystem> ();

		AddSystem(new TargetSystem(spatialStructure));
		AddSystem(new TargetingSystem(spatialStructure));

		// now there should be the ScriptSystem or BehaviourSystem.

		AddSystem<LoadLevelSystem> ();
	}
}

