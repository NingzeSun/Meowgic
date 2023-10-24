using UnityEngine;
using UnityEngine.SceneManagement;

public class MyButton : MonoBehaviour {

    public void LoadSceneByName(string name) {
        SceneManager.LoadScene(name);
    }
    public void QuitGame() {
        Application.Quit();
    }
}
