using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject PauseCanvas;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame()
    {
        SceneManager.LoadScene(1);
    }

    public void menuGame()
    {
        SceneManager.LoadScene(0);
    }

    public void quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void resume()
    {
        Time.timeScale = 1;
        PauseCanvas.SetActive(false);
    }
}
