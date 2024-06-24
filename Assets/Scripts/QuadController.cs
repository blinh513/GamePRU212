using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuadController : MonoBehaviour
{
    float distance;
    private Transform cam;
    Vector3 camStartPos;
    [SerializeField]
    float speed = 1f;
    float offset;

    private Material mat;
    // Start is called before the first frame update
    void Start()
    {
        //cam = Camera.main.transform;
        //camStartPos = cam.position;
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        //distance = cam.position.x - camStartPos.x;
        offset = Time.time * speed;
        //gameObject.GetComponent<Renderer>().material.mainTextureOffset=new Vector2(offset,0);
        mat.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        //transform.position = new Vector3(cam.position.x, transform.position.y, transform.position.z);
        //mat.SetTextureOffset("_MainTex", new Vector2(distance, 0) * speed);
    }
}
