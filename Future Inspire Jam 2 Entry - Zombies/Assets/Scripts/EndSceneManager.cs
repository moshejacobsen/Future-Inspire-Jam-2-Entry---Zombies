using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI waveText;
    [SerializeField] TextMeshProUGUI killsText;
    [SerializeField] TextMeshProUGUI bestWaveText;
    [SerializeField] TextMeshProUGUI bestKillsText;
    private void Awake()
    {
        waveText.text = "Wave: " + ZombieSpawner.wave;
        killsText.text = "Kills: " + ZombieSpawner.kills;
        if (ZombieSpawner.bestWave < ZombieSpawner.wave)
            ZombieSpawner.bestWave = ZombieSpawner.wave;
        if (ZombieSpawner.bestKills < ZombieSpawner.kills)
            ZombieSpawner.bestKills = ZombieSpawner.kills;
        bestWaveText.text = "Best Wave: " + ZombieSpawner.bestWave;
        bestKillsText.text = "Best Kills: " + ZombieSpawner.bestKills;
    }
    public void Play()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void Menu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
