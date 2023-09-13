using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static bool canThrow;
    [SerializeField] GameObject pausePanel;
    private void Awake()
    {
        canThrow = true;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausePanel.SetActive(true);
            canThrow = false;
            Time.timeScale = 0;
        }
    }
    IEnumerator EnableThrow()
    {
        yield return new WaitForSeconds(.5f);
        canThrow = true;
    }
    public void Continue()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        StartCoroutine(EnableThrow());
    }
    public void Menu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
