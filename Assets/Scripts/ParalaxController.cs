using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{

    private Transform cam;
    Vector3 camStartPos;
    float distance;

    GameObject[] backgrounds;
    Material[] mat;
    float[] backSpeed;

    float farthesBack;
    // Start is called before the first frame update
    void Start()
    {
        cam=Camera.main.transform;
        camStartPos=cam.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(cam.position.x, transform.position.y, transform.position.z);
    }
}
