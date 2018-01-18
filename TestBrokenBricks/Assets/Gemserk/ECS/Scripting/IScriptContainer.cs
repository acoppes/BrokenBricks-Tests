using System.Collections.Generic;

namespace Gemserk.ECS.Scripting
{
    public interface IScriptContainer
	{
		int Count {
			get;
		}
		
		IScript GetScript(int i);
	}

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
    }
}