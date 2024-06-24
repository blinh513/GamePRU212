using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    public static UIController ui;
    [SerializeField]
    public Text timePlay;
    [SerializeField]
    public GameObject PauseCanvas;

    [SerializeField]
    public int TotalTime=120;
    public int maxTimePlay { get; set; }
    [SerializeField]
    private List<GameObject> CanvasHide=new List<GameObject>();

    [SerializeField]
    private GameObject CanvasName;

    [SerializeField]
    private InputField inputName;

    [SerializeField]
    private Text displayName;
    [SerializeField]
    public GameObject player;
    public string Name { get; set; }
    public string Status { get; set; }
    public string TimePlay { get; set; }
    private bool isPause { get; set; }
    float time;
    //bool isPause=false;
    private void Awake()
    {
        ui = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        maxTimePlay = TotalTime;
        timePlay.text = "Time: " + TotalTime;
        time = 0;
        foreach(GameObject go in CanvasHide)
        {
            go.SetActive(false);
        }
        CanvasName.SetActive(true);
        Time.timeScale = 0;
        inputName.Select();
        PauseCanvas.SetActive(false);
        isPause=false;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= 1)
        {
            TotalTime -= 1;
            time = 0;
            timePlay.text = "Time: " + TotalTime;
            
        }
        if(TotalTime <= 0)
        {

            //GameObject gameover = new GameObject();
            //GameOverManager over = gameover.AddComponent<GameOverManager>();
            //over.GameOver();
            Status = "Timeout";
            int timPlay = maxTimePlay - TotalTime;
            TimePlay = timPlay.ToString();
            //Debug.Log("TimePlay-" + TimePlay);
            Name = displayName.text;
            //Debug.Log("Name-" + Name);
            SceneManager.LoadScene(2);
            Time.timeScale = 0;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!PauseCanvas.activeSelf)
            {
                PauseGame();
            }
            else
            {
                Time.timeScale = 1;
                PauseCanvas.SetActive(false);
            }
            
        }
    }

    public void PauseGame()
    {
            Time.timeScale = 0;
            PauseCanvas.SetActive(true);
            isPause= !isPause;
    }

    public void CreateName()
    {
        Time.timeScale = 1;
        displayName.text = inputName.text;
        Name= displayName.text;

        foreach (GameObject go in CanvasHide)
        {
            go.SetActive(true);
        }
        CanvasName.SetActive(false);
    }

}
