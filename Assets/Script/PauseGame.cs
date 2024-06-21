using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    private QuestionManager questionManager;

    private void Start()
    {
        // Đảm bảo trò chơi bắt đầu ở trạng thái không tạm dừng
        Time.timeScale = 1;

        // Lấy component QuestionManager
        questionManager = FindObjectOfType<QuestionManager>();
    }
    public void OnPause()
    {
        Time.timeScale = 0;
        if (questionManager != null)
        {
            questionManager.SetReplyButtonsInteractable(false);
        }

    }
    public void OnPlay()
    {
        Time.timeScale = 1;
        if (questionManager != null)
        {
            questionManager.SetReplyButtonsInteractable(true);
        }

    }
}
