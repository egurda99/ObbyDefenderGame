using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

[assembly: InternalsVisibleTo("Atomic.Entities.Tests")]

namespace Atomic.Entities
{
    public sealed class SceneEntityUpdater : MonoBehaviour
    {
        private const string OBJECT_NAME = "SceneEntityUpdater";

        private static SceneEntityUpdater _instance;
        private static bool installed;

        public static SceneEntityUpdater Instance => instance;

        private static SceneEntityUpdater instance
        {
            get
            {
                if (_instance == null && !installed)
                {
                    var go = new GameObject(OBJECT_NAME);
                    DontDestroyOnLoad(go);
                    _instance = go.AddComponent<SceneEntityUpdater>();
                    installed = true;
                }

                return _instance;
            }
        }

        private readonly List<IEntity> entities = new();
        private readonly List<IEntity> _entities = new();

        public static void AddEntity(IEntity entity)
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
                instance.entities.Add(entity);
            }
#else
            instance.entities.Add(entity);
#endif
        }

        public static void DelEntity(IEntity entity)
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
                instance.entities.Remove(entity);
            }
#else
            instance.entities.Remove(entity);
#endif
        }

        #region Unity

        // private void Update()
        // {
        //     var deltaTime = Time.deltaTime;
        //     var count = entities.Count;
        //     if (count == 0)
        //     {
        //         return;
        //     }
        //
        //     _entities.Clear();
        //     _entities.AddRange(entities);
        //
        //     for (var i = 0; i < count; i++)
        //     {
        //         var entity = _entities[i];
        //         entity.OnUpdate(deltaTime);
        //     }
        // }
        //
        // private void FixedUpdate()
        // {
        //     var deltaTime = Time.fixedDeltaTime;
        //     var count = entities.Count;
        //     if (count == 0)
        //     {
        //         return;
        //     }
        //
        //     _entities.Clear();
        //     _entities.AddRange(entities);
        //
        //     for (var i = 0; i < count; i++)
        //     {
        //         var entity = _entities[i];
        //         entity.OnFixedUpdate(deltaTime);
        //     }
        // }
        //
        //
        // private void LateUpdate()
        // {
        //     var deltaTime = Time.deltaTime;
        //     var count = entities.Count;
        //     if (count == 0)
        //     {
        //         return;
        //     }
        //
        //     _entities.Clear();
        //     _entities.AddRange(entities);
        //
        //     for (var i = 0; i < count; i++)
        //     {
        //         var entity = _entities[i];
        //         entity.OnLateUpdate(deltaTime);
        //     }
        // }

        public void UpdateEntities(float deltaTime)
        {
            var count = entities.Count;
            if (count == 0)
                return;

            _entities.Clear();
            _entities.AddRange(entities);

            for (var i = 0; i < count; i++)
                _entities[i].OnUpdate(deltaTime);
        }

        public void FixedUpdateEntities(float deltaTime)
        {
            var count = entities.Count;
            if (count == 0) return;

            _entities.Clear();
            _entities.AddRange(entities);

            for (var i = 0; i < count; i++)
                _entities[i].OnFixedUpdate(deltaTime);
        }

        public void LateUpdateEntities(float deltaTime)
        {
            var count = entities.Count;
            if (count == 0) return;

            _entities.Clear();
            _entities.AddRange(entities);

            for (var i = 0; i < count; i++)
                _entities[i].OnLateUpdate(deltaTime);
        }

        #endregion
    }
}
