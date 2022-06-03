using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject knight;

    void Update()
    {
        transform.position = new Vector3(knight.transform.position.x, transform.position.y, transform.position.z);   
    }
}
