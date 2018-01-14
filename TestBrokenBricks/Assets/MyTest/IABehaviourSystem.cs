using ECS;
using UnityEngine;
using MyTest.Components;
using Gemserk.ECS;

namespace MyTest.Systems
{
    public class IABehaviourSystem : ComponentSystem
    {
        [InjectDependency]
        protected EntityManager _entityManager;

        ComponentTuple<IABehaviourComponent, ControllerComponent, PositionComponent> _tuple;

        public override void OnStart()
        {
            base.OnStart();
            _tuple = new ComponentTuple<IABehaviourComponent, ControllerComponent, PositionComponent>(_entityManager);
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            var dt = Time.deltaTime;

            for (int i = 0; i < _tuple.Count; i++)
            {
                _tuple.EntityIndex = i;

                var behaviourComponent = _tuple.component1;
                var controllerComponent = _tuple.component2;
                var positionComponent = _tuple.component3;

                controllerComponent.movement = Vector2.zero;

                if (behaviourComponent.waitingForAction)
                {
                    behaviourComponent.actionTime += dt;

                    if (behaviourComponent.actionTime > behaviourComponent.waitForActionTime)
                    {
                        // decide next action...

                        var nextAction = UnityEngine.Random.Range(0, 2);
                        if (nextAction == 0)
                        {
                            behaviourComponent.waitingForAction = true;
                            behaviourComponent.walking = false;
                            behaviourComponent.actionTime = 0;
                        }
                        else if (nextAction == 1)
                        {
                            behaviourComponent.walking = true;
                            behaviourComponent.waitingForAction = false;
                            behaviourComponent.destination = (Vector3)UnityEngine.Random.insideUnitCircle * behaviourComponent.maxRandomDistance;
                        }
                    }
                }
                else if (behaviourComponent.walking)
                {

                    // walk to destination

                    controllerComponent.movement = (behaviourComponent.destination - positionComponent.position).normalized;

                    if (Vector3.Distance(positionComponent.position, behaviourComponent.destination) < 0.1f)
                    {

                        var nextAction = UnityEngine.Random.Range(0, 2);
                        if (nextAction == 0)
                        {
                            behaviourComponent.waitingForAction = true;
                            behaviourComponent.walking = false;
                            behaviourComponent.actionTime = 0;
                        }
                        else if (nextAction == 1)
                        {
                            behaviourComponent.walking = true;
                            behaviourComponent.waitingForAction = false;
                            behaviourComponent.destination = (Vector3)UnityEngine.Random.insideUnitCircle * behaviourComponent.maxRandomDistance;
                        }

                    }

                }

                _tuple.component1 = behaviourComponent;
                _tuple.component2 = controllerComponent;
            }
        }
    }

}