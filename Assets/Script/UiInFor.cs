using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiInFor : MonoBehaviour
{

    public GameObject infor;
    public GameObject main;



    public void Onclickinfor()
    {

        main.SetActive(false);
        infor.SetActive(true);

        AudioManager.Instance.PlaySoundEffectMusic(AudioManager.Instance.click);

    }
    public void OnclickBack()
    {

        main.SetActive(true);
        infor.SetActive(false);

        AudioManager.Instance.PlaySoundEffectMusic(AudioManager.Instance.click);
    }
}
