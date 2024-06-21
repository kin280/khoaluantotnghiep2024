using UnityEngine;

public class ActiveUI : MonoBehaviour
{

    public GameObject option;
    public GameObject main;



    public void OnclickOption()
    {

        main.SetActive(false);
        option.SetActive(true);
       
        AudioManager.Instance.PlaySoundEffectMusic(AudioManager.Instance.click);

    }
    public void OnclickBack()
    {

        main.SetActive(true);
        option.SetActive(false);
        
        AudioManager.Instance.PlaySoundEffectMusic(AudioManager.Instance.click);
    }
}

