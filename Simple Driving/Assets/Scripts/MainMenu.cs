using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private TMP_Text highScoreText;

    [SerializeField] private TMP_Text energyText;

    [SerializeField] private Button playButton;

    [SerializeField] private int maxEnergy;

    [SerializeField] private int energyRechargeDuration;

    [SerializeField] private AndroidNotificationHandler androidNotificationHandler;

    private int energy;

    private const string EnergyKey = "EnergyKey";

    private const string EnergyReadyKey = "EnergyReady";

    private void Start()
    {
        OnApplicationFocus(true);
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
            return;

        CancelInvoke();

        int highScore = PlayerPrefs.GetInt(ScoreSystem.HighScoreKey, 0);

        this.highScoreText.text = $"High Score: {highScore}";

        this.energy = PlayerPrefs.GetInt(EnergyKey, this.maxEnergy);

        if (this.energy == 0)
        {
            string energyReadyString = PlayerPrefs.GetString(EnergyReadyKey, string.Empty);

            if (energyReadyString == string.Empty)
                return;

            DateTime energyReady = DateTime.Parse(energyReadyString);

            if (DateTime.Now > energyReady)
            {
                EnergyRecharged();
            }
            else
            {
                playButton.interactable = false;
                Invoke(nameof(EnergyRecharged), (energyReady - DateTime.Now).Seconds);
            }
        }

        energyText.text = $"Play ({this.energy})";
    }

    private void EnergyRecharged()
    {
        playButton.interactable = true;

        this.energy = this.maxEnergy;
        PlayerPrefs.SetInt(EnergyKey, this.energy);
        energyText.text = $"Play ({this.energy})";
    }

    public void Play()
    {
        if (this.energy == 0)
            return;

        this.energy--;
        PlayerPrefs.SetInt(EnergyKey, this.energy);

        if (this.energy == 0)
        {
            DateTime energyReady = DateTime.Now.AddMinutes(this.energyRechargeDuration);
            PlayerPrefs.SetString(EnergyReadyKey, energyReady.ToString());

#if UNITY_ANDROID
            androidNotificationHandler.ScheduleNotification(energyReady);
#endif
        }

        SceneManager.LoadScene("Scene_Game");
    }

}
