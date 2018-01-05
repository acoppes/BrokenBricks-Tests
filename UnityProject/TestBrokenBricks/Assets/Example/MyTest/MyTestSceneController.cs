﻿using ECS;
using MyTest.Systems;
using UnityEngine;

public class MyTestSceneController : ECSController<UnityStandardSystemRoot, UnityEntityManager> {

	public GameObject viewPrefab;

	protected override void Initialize() {
		AddSystem<MovementSystem>();
		AddSystem<ViewSystem>();
		AddSystem<LoadLevelSystem> ();
		AddSystem<InputSystem> ();
		AddSystem<ControllerSystem> ();
		AddSystem<JumpSystem> ();
		AddSystem<DelegatePhysicsSystem> ();
	}
}
