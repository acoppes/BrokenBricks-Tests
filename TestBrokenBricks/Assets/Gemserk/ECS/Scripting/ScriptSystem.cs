
using ECS;

namespace Gemserk.ECS.Scripting
{
    public class ScriptSystem : ComponentSystem, IEntityAddedEventListener, IEntityRemovedEventListener {

		[InjectDependency]
		protected EntityManager _entityManager;

		ComponentTuple<ScriptComponent> _tuple;

		public override void OnStart ()
		{
			base.OnStart ();
			_tuple = new ComponentTuple<ScriptComponent>(_entityManager);
			_tuple.Group.SubscribeOnEntityAdded(this);
			_tuple.Group.SubscribeOnEntityRemoved(this);
		}

        public void OnEntityAdded(object sender, Entity entity)
        {
			var scriptComponent = entity.GetComponent<ScriptComponent>();

			var scriptsCount = scriptComponent.scriptContainer.Count;

			for (int j = 0; j < scriptsCount; j++)
			{
				var script = scriptComponent.scriptContainer.GetScript(j);
				script.OnAdded(entity);
			}
        }

        public void OnEntityRemoved(object sender, Entity entity)
        {
			var scriptComponent = entity.GetComponent<ScriptComponent>();

			var scriptsCount = scriptComponent.scriptContainer.Count;

			for (int j = 0; j < scriptsCount; j++)
			{
				var script = scriptComponent.scriptContainer.GetScript(j);
				script.OnRemoved(entity);
			}
        }

		public override void OnFixedUpdate ()
		{
			base.OnFixedUpdate ();

			for (int i = 0; i < _tuple.Count; i++) {
				_tuple.EntityIndex = i;

				var entity = _tuple.Entity;

                var scriptComponent = _tuple.component1;

				var scriptsCount = scriptComponent.scriptContainer.Count;

				for (int j = 0; j < scriptsCount; j++)
				{
					var script = scriptComponent.scriptContainer.GetScript(j);
					script.OnUpdate(entity);
				}

			}
		}

	}
}