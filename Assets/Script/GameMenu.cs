using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private string sceneName = "Name";
    [SerializeField] private GameObject panelMenu;
    [SerializeField] private GameObject panelOption;
    [SerializeField] private GameObject panelCredits;

    private bool isOptionPannel;
    private bool isCreditsPannel;
    private bool isMenu;

    private void Start() => Time.timeScale = 1;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isMenu = !isMenu;

            if (isMenu) OnMenu();
            if (!isMenu) ExMenu();
        }
    }
    private void ManagerPannel()
    {
        panelMenu.SetActive(isMenu);
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
    public void OnMenu()
    {
        Time.timeScale = 0.1f;
        ManagerPannel();
    }
    public void ExMenu()
    {
        Time.timeScale = 1;
        isCreditsPannel = false;
        isOptionPannel = false;
        isMenu = false;
        ManagerPannel();
    }
    public void Menu() => SceneManager.LoadScene(sceneName);
    public void QuitGame()
    {
        EditorApplication.ExitPlaymode();
        Application.Quit();
    }
}
