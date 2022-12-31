using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public Camera _ARCamera;
    private GameObject _ARObject; 
    // Start is called before the first frame update
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {
        if(_ARCamera == null)
        {
            _ARCamera = Camera.main;
        }
        if(_ARObject == null)
        {
            _ARObject = GameObject.FindGameObjectWithTag("Fox");
        }
       
        Vector3 cameraForward = _ARCamera.gameObject.transform.forward.normalized;
        _ARObject.transform.localRotation.SetLookRotation(new Vector3(cameraForward.x, 0, cameraForward.z)); //Fox will always face where the camear if facing
    }
}
