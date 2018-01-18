
using ECS;

namespace Gemserk.ECS.Scripting
{
    public interface IScript
	{
		void OnAdded(Entity entity);

		void OnRemoved(Entity entity);

		void OnUpdate(Entity entity);
	}
}