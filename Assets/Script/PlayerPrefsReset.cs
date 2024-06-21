using UnityEngine;

public class PlayerPrefsReset : MonoBehaviour
{
    // Hàm này được gọi khi người chơi muốn đặt lại PlayerPrefs
    public void ResetPlayerPrefs()
    {
        // Đặt lại PlayerPrefs
        PlayerPrefs.DeleteAll();
        Debug.Log("PlayerPrefs đã được đặt lại thành công!");
    }
}
