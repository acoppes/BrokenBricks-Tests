using ECS;
using UnityEngine;
using MyTest.Components;

namespace MyTest.Systems
{
	public class ViewSystem : ComponentSystem, IEntityAddedEventListener, IEntityRemovedEventListener
	{
		ComponentGroup _group;

		[InjectDependency]
		protected EntityManager _entityManager;

		public override void OnStart ()
		{
			base.OnStart ();
			_group = _entityManager.GetComponentGroup (typeof(PositionComponent), typeof(ViewComponent), typeof(MovementPhysicsComponent), typeof(JumpComponent));
			_group.SubscribeOnEntityAdded (this);
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

			var positionsArray = _group.GetComponent<PositionComponent> ();
			var viewsArray = _group.GetComponent<ViewComponent> ();
			var movementsArray = _group.GetComponent<MovementPhysicsComponent> ();
			var jumpsArray = _group.GetComponent<JumpComponent> ();

			for (int i = 0; i < viewsArray.Length; i++) {
				var viewComponent = viewsArray [i];

				if (viewComponent.view == null)
					continue;

                var positionComponent = positionsArray[i];
                var movementPhysicsComponent = movementsArray[i];
                var jumpComponent = jumpsArray[i];

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