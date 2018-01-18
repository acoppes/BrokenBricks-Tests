
using ECS;

namespace Gemserk.ECS.Scripting
{
    public struct ScriptComponent : IComponent
	{
		public IScriptContainer scriptContainer;

        // could be a list of strings identifying the scripts assets, cached in the system
	}
}