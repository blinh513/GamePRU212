using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class Follow : MonoBehaviour
{
    public Transform character;
    public float x = 0;
    public float y = 0;
    //public float distanceE = 0;

    // Update is called once per frame
    void Update()
    {
        if ( character != null )
        {
            Vector3 offset = new Vector3( x, y, 0 );
            Vector3 screenPositionC = Camera.main.WorldToScreenPoint(character.position +offset);
            //screenPositionE.y += distanceE;
            // Đặt vị trí của Image trên Canvas theo vị trí màn hình
            transform.position = screenPositionC;
            //canvasImageE.transform.position = screenPositionE;
        }
    }
}
