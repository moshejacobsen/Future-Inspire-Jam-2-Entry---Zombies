using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject controlPanel;
    [SerializeField] GameObject buffsPanel;
    [SerializeField] Texture2D cursorTexture;
    private void Awake()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }
    public void Play()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Controlls()
    {
        controlPanel.SetActive(true);
    }
    public void ControllsBack()
    {
        controlPanel.SetActive(false);
    }
    public void Buffs()
    {
        buffsPanel.SetActive(true);
    }
    public void BuffsBack()
    {
        buffsPanel.SetActive(false);
    }
}
