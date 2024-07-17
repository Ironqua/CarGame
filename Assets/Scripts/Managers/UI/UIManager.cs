using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] List<Image> hpImages = new List<Image>();

    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject gamePanel;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject failPanel;
    [SerializeField] TMP_Text kmText;
    [SerializeField] TMP_Text fuelText;
    [SerializeField] Image fuelImage;

    [SerializeField] GameObject colorCar;
    byte currentLevel = 0;
    float _kmInitialScore = 0;
    float _fuelInitialScore = 0;

    void OnEnable()
    {
        Subscribe();
    }

    void OnDisable()
    {
        UnSubscribe();
    }

    void Subscribe()
    {
        CoreGameSignals.Instance.onLevelFailed += LevelFailed;
        CoreGameSignals.Instance.onLevelRestart += LevelRestart;


        CoreUISignals.Instance.onStartPanel += onStartPanel;
        CoreUISignals.Instance.onPausePanel += onPausePanel;
        CoreUISignals.Instance.onLevelFailedPanel += onLevelFailedPanel;
        CoreUISignals.Instance.onGameScoreTextUpdate += onGameScoreUpdate;
        CoreUISignals.Instance.onGameFuelTextUpdate += onGameFuelPanelUpdate;
        CoreUISignals.Instance.onGameSetHpBarUpdate += onGameSetHpBarUpdate;
        CoreUISignals.Instance.onGameSetHpBarRestartUpdate += onGameSetHpBarRestartUpdate;

    }

    private void onGameSetHpBarRestartUpdate(byte stageValue)
    {
        foreach (var image in hpImages)
        {
            image.DOColor(Color.red, 0.5f);
        }
    }

    private void onGameSetHpBarUpdate(byte stageValue)
    {
        if (stageValue < hpImages.Count)
        {
            hpImages[stageValue].DOColor(Color.white, 0.5f);
            Debug.Log($"Hp: {stageValue}");
        }
    }
    private void onGameFuelPanelUpdate(float value)
    {
        _fuelInitialScore = value;
        fuelText.text = "Fuel: " + (int)_fuelInitialScore;//.ToString();

        fuelImage.fillAmount = _fuelInitialScore / 200;
    }


    private void onGameScoreUpdate(float value)
    {
        _kmInitialScore = value;
        kmText.text = "Score: " + (int)_kmInitialScore;//.ToString();
    }



    private void onLevelFailedPanel()
    {
        failPanel.gameObject.SetActive(true);
    }

    private void onPausePanel()
    {
        pausePanel.gameObject.SetActive(true);
    }

    private void onStartPanel()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
        colorCar.SetActive(false);

    }

    private void LevelRestart()
    {
        CoreUISignals.Instance.onLevelRestartPanel?.Invoke();
    }

    private void LevelFailed()
    {
        CoreUISignals.Instance.onLevelFailedPanel?.Invoke();
        CoreGameSignals.Instance.onGamePause?.Invoke();
    }

    public void GameStart()
    {
        CoreGameSignals.Instance.onLevelInitialized?.Invoke(currentLevel);
        CoreUISignals.Instance.onStartPanel?.Invoke(); // close panel
    }

    public void GamePause()
    {
        CoreGameSignals.Instance.onGamePause?.Invoke();
        CoreUISignals.Instance.onPausePanel?.Invoke();
    }

    public void GameResume()
    {
        CoreGameSignals.Instance.onGameResume?.Invoke();
        pausePanel.gameObject.SetActive(false);
    }

    public void GameRestart()
    {
        CoreGameSignals.Instance.onLevelRestart?.Invoke();
        CoreGameSignals.Instance.onLevelInitialized?.Invoke(currentLevel);
        CoreGameSignals.Instance.onGameResume?.Invoke();
        CoreUISignals.Instance.onGameSetHpBarRestartUpdate?.Invoke(2);
        DataManager.Instance.ResetPlayerData();

        pausePanel.gameObject.SetActive(false);
        failPanel.SetActive(false);
    }




 
    public void GameQuit()
    {
        Application.Quit();
    }


    void UnSubscribe()
    {

        CoreGameSignals.Instance.onLevelFailed -= LevelFailed;
        CoreGameSignals.Instance.onLevelRestart -= LevelRestart;


        CoreUISignals.Instance.onStartPanel -= onStartPanel;
        CoreUISignals.Instance.onPausePanel -= onPausePanel;
        CoreUISignals.Instance.onLevelFailedPanel -= onLevelFailedPanel;
        CoreUISignals.Instance.onGameScoreTextUpdate -= onGameScoreUpdate;
        CoreUISignals.Instance.onGameFuelTextUpdate -= onGameFuelPanelUpdate;
    }

}
