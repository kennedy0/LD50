using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAt : MonoBehaviour
{
    public Transform Target;
    
    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(Target.position - transform.position);
    }
}
