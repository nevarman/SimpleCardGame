using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : EventReceiverBehaviour {
    public GameObject panelStart,panelEnd;
    public GameObject[] winLoseText;
    public void StartGame()
    {
        panelStart.SetActive(false);
        GameController.instance.StartGame();
    }

    public override void OnPause(bool openMenu)
    {
        
    }

    public override void OnLevelComplete(int winner)
    {
        base.OnLevelComplete(winner);
        panelEnd.SetActive(true);
        winLoseText[winner].SetActive(true);
    }
    public void ReloadLevel()
    {
        Application.LoadLevel(Application.loadedLevelName);
    }
}
