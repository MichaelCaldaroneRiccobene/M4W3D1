using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private string sceneName = "Name";
    [SerializeField] private GameObject panelOption;
    [SerializeField] private GameObject panelCredits;

    private bool isOptionPannel;
    private bool isCreditsPannel;

    private void ManagerPannel()
    {
        panelOption.SetActive(isOptionPannel);
        panelCredits.SetActive(isCreditsPannel);
    }
    public void Credit()
    {
        isCreditsPannel = !isCreditsPannel;
        isOptionPannel = false;
        ManagerPannel();
    }
    public void Option()
    {
        isOptionPannel = !isOptionPannel;
        isCreditsPannel = false;
        ManagerPannel();
    }
    public void NewGame() => SceneManager.LoadScene(sceneName);
    public void QuitGame()
    {
        EditorApplication.ExitPlaymode();
        Application.Quit();
    }
}
