using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineController : MonoBehaviour
{

    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    public void SetVirtualCamera(Transform player)
    {

        _virtualCamera.transform.localPosition = player.localPosition + _virtualCamera.transform.localPosition;
        _virtualCamera.LookAt = player;
        _virtualCamera.Follow = player;

    }

}
