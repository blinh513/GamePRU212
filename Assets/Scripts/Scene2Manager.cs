using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Scene2Manager : MonoBehaviour
{
    public Text status;
    public Text namePlayer;
    public Text timePlay;
    public Text point;
    // Start is called before the first frame update
    void Start()
    {

        string sta = PlayerController.scene1.Status;
        if (sta == null || string.IsNullOrEmpty(sta))
        {
            status.text = UIController.ui.Status;
        }
        else
        {
            status.text=sta;
        }
        point.text = PlayerController.scene1.textPoint.text;
        namePlayer.text = "Name: " + UIController.ui.Name;
        string tim =  UIController.ui.TimePlay;
        if (tim == null || string.IsNullOrEmpty(tim))
        {
            timePlay.text = "Time: " + PlayerController.scene1.TimePlay;
        }
        else
        {
            timePlay.text = "Time: " + tim;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    
}
