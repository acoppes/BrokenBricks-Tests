using ECS;
using UnityEngine;
using MyTest.Components;

public class LoadLevelSystem : ComponentSystem
{
	[InjectDependency]
	protected EntityManager _entityManager;

	public override void OnStart ()
	{
		base.OnStart ();

		var sceneEntities = GameObject.FindObjectsOfType<EntityTemplateBehaviour> ();

		foreach (var sceneEntity in sceneEntities) {
			var e = _entityManager.CreateEntity ();
			sceneEntity.Apply (e);
			sceneEntity.gameObject.SetActive (false);
		}

		var testEntity = _entityManager.CreateEntity();

		_entityManager.AddComponent (testEntity, new PositionComponent () { 
			position = new Vector3(0, 0, 0),
			lookingDirection = new Vector2(1, 0)
		});

		_entityManager.AddComponent(testEntity, new TargetComponent() {
			targets = new Target[1]
		});
	}
}
