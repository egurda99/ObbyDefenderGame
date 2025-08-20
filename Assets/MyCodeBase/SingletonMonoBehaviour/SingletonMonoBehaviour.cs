using UnityEngine;

namespace MyCodeBase.Singleton
{
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        private static bool _alive = true;
        private static readonly object _lock = new();

        public static T Instance
        {
            get
            {
                //lock (_lock)
                {
                    if (_instance != null)
                    {
                        return _instance;
                    }

                    var instances = FindObjectsByType<T>(FindObjectsInactive.Include, FindObjectsSortMode.None);
                    if (instances != null)
                    {
                        if (instances.Length == 1)
                        {
                            _instance = instances[0];
                            DontDestroyOnLoad(_instance);
                            return _instance;
                        }

                        if (instances.Length > 1)
                        {
                            for (var i = 0; i < instances.Length; ++i)
                            {
                                var manager = instances[i];
                                Destroy(manager.gameObject);
                            }
                        }
                    }

                    var go = new GameObject(typeof(T).Name, typeof(T));
                    _instance = go.GetComponent<T>();
                    DontDestroyOnLoad(_instance.gameObject);
                    return _instance;
                }
            }
        }

        public static bool IsAlive
        {
            get
            {
                if (_instance == null)
                    return false;
                return _alive;
            }
        }

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                DontDestroyOnLoad(gameObject);
                _instance = this as T;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        protected virtual void OnDestroy()
        {
            _alive = false;
        }

        protected virtual void OnApplicationQuit()
        {
            _alive = false;
        }
    }
}
