using UnityEngine;
using UnityEngine.SceneManagement;

public class LocalSceneManager : MonoBehaviour
{
    public void NextScene()
    {
        SceneManager.LoadScene(1);
    }
}