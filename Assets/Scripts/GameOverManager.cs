using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void GameOver()
    {
        // Load scene có tên là "GameOverScene". 
        SceneManager.LoadScene("GameOver");
    }
}
