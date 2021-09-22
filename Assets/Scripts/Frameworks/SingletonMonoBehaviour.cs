using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>, new()
{
    private static T s_instance_;

    public static T Instance
    {
        get
        {
            if (s_instance_ != null)
                return s_instance_;

            s_instance_ = (T)FindObjectOfType(typeof(T));

            if (s_instance_ != null)
                return s_instance_;

            Create();

            return s_instance_;
        }
    }

    public static T Create()
    {
        GameObject obj = new GameObject(nameof(T));
        s_instance_ = obj.AddComponent<T>();
        return s_instance_;
    }

    protected virtual void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
