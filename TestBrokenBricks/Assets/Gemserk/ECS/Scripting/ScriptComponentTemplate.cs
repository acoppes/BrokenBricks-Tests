

using System.Collections.Generic;
using ECS;
using UnityEngine;

namespace Gemserk.ECS.Scripting
{
    public class ScriptComponentTemplate : ComponentTemplateBehaviour
    {
        public List<UnityEngine.Object> unityScripts = new List<UnityEngine.Object>();

        public override void Apply (Entity e)
        {
            var scriptComponent = new ScriptComponent();

            var scripts = new List<IScript>();

            foreach (var unityScript in unityScripts) 
            {
                scripts.Add(unityScript as IScript);
            }

            scriptComponent.scriptContainer = new ListScriptContainer(scripts);

            e.AddComponent(scriptComponent);
        }
    }
}