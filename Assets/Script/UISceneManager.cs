using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UISceneManager : MonoBehaviour
{
    [SerializeField] string level;
    private Button _button;
    // Start is called before the first frame update
    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(UpdateScene);
    }

    // Update is called once per frame
    void UpdateScene()
    {
        AudioManager.Instance.PlaySoundEffectMusic(AudioManager.Instance.click);
        SceneManager.LoadScene(level);
    }
}