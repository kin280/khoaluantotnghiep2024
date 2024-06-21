using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIQuit : MonoBehaviour
{
    public GameObject QuitPopupUI;
    public GameObject main;



    void Awake()
    {
        QuitPopupUI.SetActive(false);
    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            QuitPopupToggle();
        }
    }

   

    public void QuitPopupToggle()
    {
        AudioManager.Instance.PlaySoundEffectMusic(AudioManager.Instance.click);

        if (!QuitPopupUI.activeSelf)
        {

            QuitPopupUI.SetActive(true);
            main.SetActive(false);
        }
        else
        {

            QuitPopupUI.SetActive(false);
            main.SetActive(true);
        }
    }

    public void Quit()
    {
        AudioManager.Instance.PlaySoundEffectMusic(AudioManager.Instance.click);
        Debug.Log("Tôi đã thoát");

        Application.Quit();
    }
}
