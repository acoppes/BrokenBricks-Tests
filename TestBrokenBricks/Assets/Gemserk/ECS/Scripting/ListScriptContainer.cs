using System.Collections.Generic;

namespace Gemserk.ECS.Scripting
{
    public class ListScriptContainer : IScriptContainer
    {
        List<IScript> scripts = new List<IScript>();

        public int Count
        {
            get
            {
                return scripts.Count;
            }
        }

        public IScript GetScript(int i)
        {
            return scripts[i];
        }

        public ListScriptContainer(List<IScript> scripts)
        {
            this.scripts.AddRange(scripts);
        }
    }
}