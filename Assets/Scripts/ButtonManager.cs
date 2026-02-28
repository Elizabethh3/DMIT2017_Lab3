using System;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] GameObject startMenu;
    [SerializeField] GameObject screenFader;

    void Awake()
    {
        startMenu.SetActive(true);
        screenFader.SetActive(false);
    }

    public void OnClickResumeGame()
    {
        //load save data and starts game
        GameStateManager.Instance.GetComponentInChildren<JSonSaving>().LoadData();
        startMenu.SetActive(false);
        screenFader.SetActive(true);
    }

    public void OnClickNew()
    {
        //resets all info and erase save data and starts game
        GameStateManager.Instance.GetComponentInChildren<JSonSaving>().ResetData();
        startMenu.SetActive(false);
        screenFader.SetActive(true);
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }

    public void OnClickSave()
    {
        //saves current gameplay and then closes application
        GameStateManager.Instance.GetComponentInChildren<JSonSaving>().SaveData();
        Application.Quit();
    }
}
