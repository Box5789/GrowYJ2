using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeSceneManager : MonoBehaviour
{
    Button EndingBtn, GameStartBtn, TestEndingBtn;

    // Start is called before the first frame update
    void Start()
    {
        GameStartBtn = GameObject.Find("Door_btn").gameObject.GetComponent<Button>();
        GameStartBtn.onClick.AddListener(GoToGame);

        EndingBtn = GameObject.Find("Window_btn").gameObject.GetComponent<Button>();
        EndingBtn.onClick.AddListener(GoToEnding);

        TestEndingBtn = GameObject.Find("BookShelf_btn").gameObject.GetComponent<Button>();
        TestEndingBtn.onClick.AddListener(GoToTestEnding);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void GoToEnding()
    {
        //SceneManager.LoadScene("EndingBook");
    }

    public void GoToTestEnding()
    {
        //SceneManager.LoadScene("Test");
    }

}
