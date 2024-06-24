using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectArrow : MonoBehaviour
{
    [SerializeField]
    private RectTransform[] options;

    private RectTransform rect;
    private int currentPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangePos(1);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangePos(-1);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Enter");
            Interact();
        }
    }

    private void Awake()
    {
        rect=GetComponent<RectTransform>();
    }

    private void ChangePos(int _change)
    {
        currentPos += _change;

        if(currentPos < 0) {
            currentPos = options.Length - 1;
        }else if(currentPos>options.Length-1){
        currentPos = 0;}

        rect.position = new Vector3(rect.position.x, options[currentPos].position.y, 0);
    }

    private void Interact()
    {
        Debug.Log("options[currentPos]" + currentPos);
        options[currentPos].GetComponent<Button>().onClick.Invoke();
    }
}
