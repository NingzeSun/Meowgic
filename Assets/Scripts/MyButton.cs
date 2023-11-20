using UnityEngine;
using UnityEngine.SceneManagement;

public class MyButton : MonoBehaviour {

    public void LoadSceneByName(string name) {
        Time.timeScale = 1;
        SceneManager.LoadScene(name);
    }
    public void QuitGame() {
        Application.Quit();
    }
}
