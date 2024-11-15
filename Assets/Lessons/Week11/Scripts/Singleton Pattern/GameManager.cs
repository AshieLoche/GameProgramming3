using UnityEngine;

public class GameManager : SingletonPersistent<GameManager>
{
    private SoundManager _soundManager;

    public SoundManager SoundManager => _soundManager;

    private void Start()
    {
        _soundManager = GetComponentInChildren<SoundManager>();
    }
}