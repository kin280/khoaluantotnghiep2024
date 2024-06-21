using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static QtsData;

public class QuestionManager : MonoBehaviour
{
    // Các biến được trỏ tới các đối tượng UI và dữ liệu câu hỏi
    public Text questionText;
    public Image questionImage;
    public Text scoreText;
    public TextMeshProUGUI timerText;
    public Text finalScoreText;
    public Button[] replyButtons;
    public GameObject LevelnextButton;
    public QtsData qtsData;
    public GameObject rightFeedback;
    public GameObject wrongFeedback;
    public GameObject gameFinishedPanel;
    public GameObject UIQuestion;
    public GameObject Replies;
    public GameObject Start1;
    public GameObject Start2;
    public GameObject Start3;

    private const int scoreStep = 1;
    private const int GameOver = 50;
    private const int TryHarder = 60;
    private const int Good = 70;
    private const int Great = 80;

    // Các biến quản lý trạng thái và thời gian
    private int currentQuestion = 0;
    private int score = 0;
    private float timeRemaining;
    private const float feedbackDelay = 1f;
    private IEnumerator timerCoroutine;
    public StarDisplay starDisplay;

    private void Start()
    {
        // Khởi tạo trò chơi khi bắt đầu
        SetupGame();
        // Bắt đầu coroutine để đếm ngược thời gian
        timerCoroutine = StartTimer();
        StartCoroutine(timerCoroutine);
    }

    private void SetupGame()
    {
        // Cài đặt trạng thái ban đầu của trò chơi
        SetQuestion(currentQuestion);
        rightFeedback.SetActive(false);
        wrongFeedback.SetActive(false);
        gameFinishedPanel.SetActive(false);
        scoreText.text = score.ToString();
    }

    private void SetQuestion(int questionIndex)
    {
        // Thiết lập câu hỏi và các câu trả lời tương ứng
        Question question = qtsData.questions[questionIndex];
        questionText.text = question.questionText;
        timeRemaining = question.timeLimit;

        
        // Thiết lập ảnh câu hỏi nếu có anh
        if (question.questionImage != null)
        {
            questionImage.sprite = question.questionImage;
            questionImage.gameObject.SetActive(true); // Hiển thị ảnh
        }
        else
        {
            questionImage.gameObject.SetActive(false); // Ẩn ảnh nếu không có
        }

        for (int i = 0; i < replyButtons.Length; i++)
        {
            replyButtons[i].GetComponentInChildren<Text>().text = question.replies[i];
            int replyIndex = i;
            // Xóa các listeners cũ và thêm listener mới cho các nút trả lời
            replyButtons[i].onClick.RemoveAllListeners();
            replyButtons[i].onClick.AddListener(() =>
            {
                CheckReply(replyIndex);
            });
        }
    }

    private void CheckReply(int replyIndex)
    {
        // Kiểm tra câu trả lời và cập nhật điểm số
        bool isCorrect = replyIndex == qtsData.questions[currentQuestion].correctReplyIndex;
        StopCoroutine(timerCoroutine);
        if (isCorrect)
        {
            score += scoreStep;
            scoreText.text = score.ToString();
            rightFeedback.SetActive(true);
            AudioManager.Instance.PlaySoundEffectMusic(AudioManager.Instance.right);
        }
        else
        {
            AudioManager.Instance.PlaySoundEffectMusic(AudioManager.Instance.wrong);
            wrongFeedback.SetActive(true);
        }
        HandleFeedback();
        timerCoroutine = StartTimer();
        StartCoroutine(timerCoroutine);
    }
    public void SetReplyButtonsInteractable(bool isInteractable)
    {
        foreach (Button button in replyButtons)
        {
            button.interactable = isInteractable;
        }
    }

    private void HandleFeedback()
    {
        // Xử lý hiển thị phản hồi và chuyển sang câu hỏi tiếp theo sau một khoảng thời gian
        foreach (Button button in replyButtons)
        {
            button.interactable = false;
        }
        StartCoroutine(NextQuestion());
    }

    private IEnumerator NextQuestion()
    {
        // Chờ một khoảng thời gian trước khi chuyển sang câu hỏi tiếp theo
        yield return new WaitForSeconds(feedbackDelay);
        currentQuestion++;
        if (currentQuestion < qtsData.questions.Length)
        {
            ResetQuestion();
        }
        else
        {
            ShowGameOver();
        }
    }

    private void ShowGameOver()
    {
        // Hiển thị panel kết thúc trò chơi và điểm số cuối cùng
        gameFinishedPanel.SetActive(true);
        UIQuestion.SetActive(false);
        Replies.SetActive(false);
        StartCoroutine(SetTimeScaleAfterDelay(0f, 0.1f));
        float scorePercentage = Mathf.Min((float)score / (qtsData.questions.Length * scoreStep) * 100, 100);
        finalScoreText.text = "Bạn đạt được " + scorePercentage.ToString("F0") + "%";
        LevelnextButton.SetActive(scorePercentage >= TryHarder);
        starDisplay.SaveStarData(scorePercentage, SceneManager.GetActiveScene().buildIndex);

        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        if (scorePercentage < GameOver)
        {
            AudioManager.Instance?.PlaySoundEffectMusic(AudioManager.Instance.Gameover);
            finalScoreText.text += "\nGame Over";
        }
        else if (scorePercentage < TryHarder)
        {
            AudioManager.Instance?.PlaySoundEffectMusic(AudioManager.Instance.Gameover);
            finalScoreText.text += "\nHãy tiếp tục cố gắng";
        }
        else if (scorePercentage < Good)
        {
            AudioManager.Instance?.PlaySoundEffectMusic(AudioManager.Instance.Clap);
            finalScoreText.text += "\nChúc mừng bạn!";
            UnlockNewLevel();
            Start1.SetActive(true);
        }
        else if (scorePercentage < Great)
        {
            AudioManager.Instance?.PlaySoundEffectMusic(AudioManager.Instance.Clap);
            finalScoreText.text += "\nChúc mừng bạn đã làm tốt!";
            UnlockNewLevel();
            Start1.SetActive(true);
            Start2.SetActive(true);
        }
        else
        {
            AudioManager.Instance?.PlaySoundEffectMusic(AudioManager.Instance.Clap);
            finalScoreText.text += "\nChúc mừng bạn đã làm rất tốt!";
            UnlockNewLevel();
            Start1.SetActive(true);
            Start2.SetActive(true);
            Start3.SetActive(true);
        }
    }

    private void UnlockNewLevel()
    {
        // Mở khóa cấp độ tiếp theo nếu người chơi đạt được điều kiện
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", currentSceneIndex + 1);
            PlayerPrefs.SetInt("Unlockdlevel", PlayerPrefs.GetInt("Unlockdlevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }

    public void ResetQuestion()
    {
        // Đặt lại trạng thái của câu hỏi và các nút trả lời
        rightFeedback.SetActive(false);
        wrongFeedback.SetActive(false);
        foreach (Button button in replyButtons)
        {
            button.interactable = true;
        }
        SetQuestion(currentQuestion);
    }

    private IEnumerator StartTimer()
    {
        // Coroutine để đếm ngược thời gian cho mỗi câu hỏi
        while (timeRemaining > 0)
        {
            yield return new WaitForSeconds(1);
            timeRemaining--;
            timerText.text = timeRemaining.ToString("00");
        }
        TimeUp();
    }

    private void TimeUp()
    {
        // Hiển thị panel kết thúc trò chơi và điểm số cuối cùng
        gameFinishedPanel.SetActive(true);
        UIQuestion.SetActive(false);
        Replies.SetActive(false);
        StartCoroutine(SetTimeScaleAfterDelay(0f, 0.1f));

        // Tính phần trăm điểm và hiển thị
        float scorePercentage = Mathf.Min((float)score / (qtsData.questions.Length * scoreStep) * 100, 100);
        finalScoreText.text = "Bạn đạt được " + scorePercentage.ToString("F0") + "%";
        LevelnextButton.SetActive(scorePercentage >= TryHarder);
        starDisplay.SaveStarData(scorePercentage, SceneManager.GetActiveScene().buildIndex);
        if (scorePercentage < GameOver)
        {
            AudioManager.Instance?.PlaySoundEffectMusic(AudioManager.Instance.Gameover);
            finalScoreText.text += "\nGame Over";
        }
        else if (scorePercentage < TryHarder)
        {
            AudioManager.Instance?.PlaySoundEffectMusic(AudioManager.Instance.Gameover);
            finalScoreText.text += "\nHãy tiếp tục cố gắng";
        }
        else if (scorePercentage < Good)
        {
            AudioManager.Instance?.PlaySoundEffectMusic(AudioManager.Instance.Clap);
            finalScoreText.text += "\nChúc mừng bạn!";
            UnlockNewLevel();
            Start1.SetActive(true);
        }
        else if (scorePercentage < Great)
        {
            AudioManager.Instance?.PlaySoundEffectMusic(AudioManager.Instance.Clap);
            finalScoreText.text += "\nChúc mừng bạn đã làm tốt!";
            UnlockNewLevel();
            Start1.SetActive(true);
            Start2.SetActive(true);
        }
        else
        {
            AudioManager.Instance?.PlaySoundEffectMusic(AudioManager.Instance.Clap);
            finalScoreText.text += "\nChúc mừng bạn đã làm rất tốt!";
            UnlockNewLevel();
            Start1.SetActive(true);
            Start2.SetActive(true);
            Start3.SetActive(true);
        }
    }

    private IEnumerator SetTimeScaleAfterDelay(float timeScale, float delay)
    {
        yield return new WaitForSeconds(delay);
        Time.timeScale = timeScale;
    }  
}
