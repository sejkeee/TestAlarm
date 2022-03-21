using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera _mainCam;

    private void Awake()
    {
        _mainCam = GetComponent<Camera>();    
    }
    private void Update()
    {
        if(Screen.width >= Screen.height)
        {
            _mainCam.orthographicSize = 5f;
        }
        else
        {
            _mainCam.orthographicSize = 15f;
        }
    }
}
