using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenu;

    public void ContineGameBtn()
    {
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
        }
    }
}
