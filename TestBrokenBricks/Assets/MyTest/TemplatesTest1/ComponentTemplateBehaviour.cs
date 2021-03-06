using ECS;
using UnityEngine;

public abstract class ComponentTemplateBehaviour : MonoBehaviour, EntityTemplate
{
	[InjectDependency]
	protected EntityManager _entityManager;

	void Awake()
	{
		InjectionManager.ResolveDependency(this);
	}

	#region EntityTemplate implementation
	public abstract void Apply (Entity e);
	#endregion
}
