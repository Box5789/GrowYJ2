using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GoToMainBtn : MonoBehaviour
{
    public void GoToMain()
    {
        SceneManager.LoadScene("Main");
    }
}
