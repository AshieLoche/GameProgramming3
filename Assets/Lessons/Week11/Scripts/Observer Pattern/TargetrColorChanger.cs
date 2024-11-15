using UnityEngine;

public class TargetColorChanger : MonoBehaviour
{
    [SerializeField] private CollideEventListener _collideEventListener;

    [SerializeField] private Color _color;
    private MeshRenderer _meshRenderer;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _collideEventListener.OnChangeColor += OnChangeColor;
    }

    private void OnChangeColor()
    {
        _meshRenderer.material.color = _color;
    }
}