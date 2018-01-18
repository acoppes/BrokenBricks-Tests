using System.Collections;
using System.Collections.Generic;
using ECS;
using Gemserk.ECS.Scripting;
using MyTest.Components;
using UnityEngine;

public class MoveTestScript : ScriptUnityImplementation
{
    public override void OnUpdate(Entity entity)
    {
        if (Input.GetKey(KeyCode.Alpha5)){
			var controllerComponent = entity.GetComponent<ControllerComponent>();
			controllerComponent.movement = new Vector3(1, 0, 0);
			entity.SetComponent(controllerComponent);
		}
    }
}
