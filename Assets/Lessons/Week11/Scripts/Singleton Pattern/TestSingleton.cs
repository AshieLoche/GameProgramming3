using UnityEngine;

public class TestSingleton : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.SoundManager.Play();
    }
}