﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS {

    [RequireComponent(typeof(GameObjectEntity))]
    public abstract class ComponentWrapper : MonoBehaviour {
        public abstract IComponent Component{ get; set; }
        public abstract Type ComponentType { get; }
        public abstract void AddComponentToEntity(Entity entity, EntityManager entityManager);

        /// <summary>
        /// Use this for Initializeation for example Unity Components like Transforms
        /// </summary>
        //public virtual void Initialize() { }
        public abstract void Initialize(Entity entity, EntityManager entityManager);
        public abstract void SetComponentToEntityManager();
        //public abstract void GetComponentFromEntityManager();
    }

    [DisallowMultipleComponent]
    public class ComponentWrapper<TComponent> : ComponentWrapper, IComponentChangedEventListener<TComponent> where TComponent : class, IComponent, ICloneable, new() {
        [SerializeField]
        private TComponent _component = new TComponent();
        public TComponent TypedComponent { get { return _component; } }

        public override IComponent Component {
            get { return _component; }
            set { _component = (TComponent)value; }
        }

        public override Type ComponentType { get { return typeof(TComponent); } }

        public override void AddComponentToEntity(Entity entity, EntityManager entityManager) {
            if (!enabled) {
                return;
            }
            entityManager.AddComponent(entity, (TComponent)_component.Clone());
        }

        public override void SetComponentToEntityManager() { }
        

        public override void Initialize(Entity entity, EntityManager entityManager) {            
            entityManager.SubscribeComponentChanged(entity, this);
            if (!enabled) {
                return;
            }
            if (entityManager.HasComponent<TComponent>(entity)) {
                _component = entityManager.GetComponent<TComponent>(entity);
            } else {
                entityManager.AddComponent(entity, _component);
            }
        }

        private void OnEnable() {
            GameObjectEntity gameObjectEntity = GetComponent<GameObjectEntity>();
            if (gameObjectEntity.IsInitialized) {
                gameObjectEntity.EntityManager.SubscribeComponentChanged(gameObjectEntity.Entity, this);
                if (gameObjectEntity.EntityManager.HasComponent<TComponent>(gameObjectEntity.Entity)) {
                    _component = gameObjectEntity.GetComponentFromEntityManager<TComponent>();
                } else {
                    gameObjectEntity.EntityManager.AddComponent(gameObjectEntity.Entity, _component);
                }
            }
        }

        private void OnDisable() {
            GameObjectEntity gameObjectEntity = GetComponent<GameObjectEntity>();
            if (gameObjectEntity.IsInitialized) {
                gameObjectEntity.EntityManager.UnsubscribeComponentChanged(gameObjectEntity.Entity, this);

                if (gameObjectEntity.EntityManager.HasComponent<TComponent>(gameObjectEntity.Entity)) {
                    gameObjectEntity.EntityManager.RemoveComponent(gameObjectEntity.Entity, _component);
                }
            }
        }
        
        public void OnComponentChanged(object sender, Entity entity, TComponent component) {
            _component = component;
        }

    }

    [DisallowMultipleComponent]
    public class ComponentDataWrapper<TComponent> : ComponentWrapper, IComponentChangedEventListener<TComponent> where TComponent : struct, IComponent {
        [SerializeField]
        private TComponent _component;

        public TComponent TypedComponent { get { return _component; } }

        public override IComponent Component {
            get { return _component; }
            set { _component = (TComponent)value; }
        }
        public override Type ComponentType { get { return typeof(TComponent); } }

        public override void Initialize(Entity entity, EntityManager entityManager) {
            entityManager.SubscribeComponentChanged(entity, this);
            if (entityManager.HasComponent<TComponent>(entity)) {
                _component = entityManager.GetComponent<TComponent>(entity);
            } else {
                entityManager.AddComponent(entity, _component);
            }
        }


        public override void AddComponentToEntity(Entity entity, EntityManager entityManager) {
            entityManager.AddComponent(entity, _component);
        }
        
        public void OnComponentChanged(object sender, Entity entity, TComponent component) {
            _component = component;
        }

        public override void SetComponentToEntityManager() {
            GetComponent<GameObjectEntity>().SetComponentToEntityManager(_component);
        }
        private void OnEnable() {
            GameObjectEntity gameObjectEntity = GetComponent<GameObjectEntity>();
            if (enabled == false) {
                return;
            }

            if (gameObjectEntity.IsInitialized) {
                gameObjectEntity.EntityManager.SubscribeComponentChanged(gameObjectEntity.Entity, this);
                if (gameObjectEntity.EntityManager.HasComponent<TComponent>(gameObjectEntity.Entity)) {
                    _component = gameObjectEntity.GetComponentFromEntityManager<TComponent>();
                } else {
                    gameObjectEntity.EntityManager.AddComponent(gameObjectEntity.Entity, _component);
                }
            }
        }

        private void OnDisable() {
            GameObjectEntity gameObjectEntity = GetComponent<GameObjectEntity>();
            if (gameObjectEntity.IsInitialized) {
                gameObjectEntity.EntityManager.UnsubscribeComponentChanged(gameObjectEntity.Entity, this);

                if (gameObjectEntity.EntityManager.HasComponent<TComponent>(gameObjectEntity.Entity)) {
                    gameObjectEntity.EntityManager.RemoveComponent(gameObjectEntity.Entity, _component);
                }
            }
        }
    }
}
