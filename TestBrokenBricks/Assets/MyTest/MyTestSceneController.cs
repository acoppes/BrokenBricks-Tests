using ECS;
using MyTest.Systems;
using UnityEngine;

public class MyTestSceneController : ECSController<UnityStandardSystemRoot, UnityEntityManager> {

	public GameObject viewPrefab;

	protected override void Initialize() {

		InjectionManager.CreateObject(typeof(SpatialStructure));
		
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

		AddSystem<TargetSystem>();
		AddSystem<TargetingSystem>();

		// now there should be the ScriptSystem or BehaviourSystem.

		AddSystem<LoadLevelSystem> ();

	}
}

