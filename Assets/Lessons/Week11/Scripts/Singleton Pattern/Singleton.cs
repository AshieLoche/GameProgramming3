using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;

    public  static T Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject newSingleton = new GameObject();
                newSingleton.name = typeof(T).Name;
                _instance = newSingleton.AddComponent<T>();
            }
            return _instance;
        }
    }

    public virtual void Awake()
    {
        if (_instance != null)
            _instance = gameObject.GetComponent<T>();
        else
            Destroy(gameObject);
    }
}