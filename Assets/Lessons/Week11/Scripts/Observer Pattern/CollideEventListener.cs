using System;
using UnityEngine;

public class CollideEventListener : MonoBehaviour
{
    public Action OnChangeColor;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            OnChangeColor.Invoke();
        }
    }
}