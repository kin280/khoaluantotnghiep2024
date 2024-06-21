using UnityEngine;
using UnityEngine.SceneManagement;

public class UIResetGame : MonoBehaviour
{
    public void panelGameOverhine()
    {
        AudioManager.Instance.PlaySoundEffectMusic(AudioManager.Instance.click);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
