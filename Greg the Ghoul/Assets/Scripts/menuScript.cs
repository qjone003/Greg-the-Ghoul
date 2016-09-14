using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class menuScript : MonoBehaviour {
    public Canvas quitMenu;
    public Button startText;
    public Button exitText;
	// Use this for initialization
	void Start () {
        quitMenu = quitMenu.GetComponent<Canvas>();
        startText = startText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();
        quitMenu.enabled = false;
	}

    public void ExitPress() {
        quitMenu.enabled = true;
        startText.enabled = false;
        exitText.enabled = false;
    }

    public void noPress() {
        quitMenu.enabled = false;
        startText.enabled = true;
        exitText.enabled = true;
    }
    public void StartLevel() {
        SceneManager.LoadScene("Scene 1");
    }

    public void exitGame() {
        Application.Quit();
    }
	// Update is called once per frame
	void Update () {
	
	}
}
