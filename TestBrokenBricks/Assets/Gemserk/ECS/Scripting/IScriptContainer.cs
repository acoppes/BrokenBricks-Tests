namespace Gemserk.ECS.Scripting
{
    public interface IScriptContainer
	{
		int Count {
			get;
		}
		
		IScript GetScript(int i);
	}
}