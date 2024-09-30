using UnityEngine;
namespace DireRaven22075
{
    [DisallowMultipleComponent] //싱글톤 패턴의 오브젝트를 1개만 소환하고자 함
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        //만약 해당 싱글톤 패턴을 다른 씬에서 제거하고 싶을 수 있으니
        [SerializeField]
        private bool dontDestroyOnLoad = true;
        private static volatile T _instance;
        private static object _sync = new object();
        public static T Instance
        {
            get
            {
                if (!IsInitialized)
                {
                    Initialize();
                }
                return _instance;
            }
        }
        public static bool IsInitialized => _instance != null;
        public static void Initialize()
        {
            lock (_sync)
            {
                if ((_instance = FindAnyObjectByType<T>()) == null)
                {
                    _instance = new GameObject(typeof(T).FullName).AddComponent<T>();
                }
            }
        }
        protected virtual void Awake()
        {
            if (IsInitialized)
            {
                Destroy(this);
                Debug.Log("Cannot have more than one Singleton of type " + typeof(T).FullName);
            }
            if (dontDestroyOnLoad)
            {
                DontDestroyOnLoad(this);
            }
        }
        protected virtual void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }
    }
}