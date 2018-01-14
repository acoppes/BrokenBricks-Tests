using ECS;
using UnityEngine;
using MyTest.Components;
using Gemserk.ECS;

namespace MyTest.Systems
{
	public class ViewSystem : ComponentSystem, IEntityAddedEventListener, IEntityRemovedEventListener
	{
		[InjectDependency]
		protected EntityManager _entityManager;

		ComponentTuple<PositionComponent, ViewComponent, MovementPhysicsComponent, JumpComponent> _tuple;

		public override void OnStart ()
		{
			base.OnStart ();
			_tuple = new ComponentTuple<PositionComponent, ViewComponent, MovementPhysicsComponent, JumpComponent>(_entityManager);
			_tuple.Group.SubscribeOnEntityAdded (this);
		}

		public void OnEntityAdded (object sender, Entity entity)
		{
			var view = _entityManager.GetComponent<ViewComponent> (entity);

			if (view.viewPrefab != null && view.view == null) {
				view.view = GameObject.Instantiate (view.viewPrefab);
				view.animator = view.view.GetComponentInChildren<Animator> ();
				view.sprite = view.view.GetComponentInChildren<SpriteRenderer> ();
			}

			_entityManager.SetComponent(entity, view);
		}

		public override void OnFixedUpdate ()
		{
			base.OnFixedUpdate ();

			for (int i = 0; i < _tuple.Count; i++) {
				_tuple.EntityIndex = i;

				var viewComponent = _tuple.component2;

				if (viewComponent.view == null)
					continue;

                var positionComponent = _tuple.component1;
                var movementPhysicsComponent = _tuple.component3;
                var jumpComponent = _tuple.component4;

                var position = positionComponent.position;

				viewComponent.view.transform.position = new Vector3 (position.x, 0, position.y);
				viewComponent.sprite.transform.localPosition = new Vector3(0, position.z, 0);

				if (viewComponent.animator != null) {
                    viewComponent.animator.SetBool ("Walking", movementPhysicsComponent.direction.sqrMagnitude > 0);
					viewComponent.sprite.flipX = positionComponent.lookingDirection.x < 0;
                    viewComponent.animator.SetBool("Jumping", jumpComponent.isJumping);
					viewComponent.animator.SetBool("Falling", jumpComponent.isFalling);
				}
			}
		}

	}
}