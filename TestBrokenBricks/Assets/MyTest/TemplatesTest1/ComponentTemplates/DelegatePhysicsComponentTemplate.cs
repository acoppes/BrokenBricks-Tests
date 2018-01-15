using MyTest.Components;
using UnityEngine;

[ExecuteInEditMode]
public class DelegatePhysicsComponentTemplate : GenericEntityTemplate<DelegatePhysicsComponent> {

	#if UNITY_EDITOR
	void Update()
	{
		if (Application.isPlaying)
			return;
		if (component.position != transform.position) {
			component.position = transform.position;	
			UnityEditor.EditorUtility.SetDirty (this);
			UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty (UnityEngine.SceneManagement.SceneManager.GetActiveScene ());
		}
	}
	#endif

}
	