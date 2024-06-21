using UnityEngine;

// Đánh dấu lớp này để có thể tạo đối tượng ScriptableObject từ menu trong Unity Editor
[CreateAssetMenu(fileName = "NewQuestionData", menuName = "QuestionData")]
public class QtsData : ScriptableObject
{
    // Tạo một cấu trúc để đại diện cho mỗi câu hỏi
    [System.Serializable]
    public struct Question
    {
        public string questionText; // Văn bản câu hỏi
        public string[] replies; // Mảng chứa các câu trả lời
        public int correctReplyIndex; // Chỉ số của câu trả lời đúng
        public float timeLimit; // Giới hạn thời gian cho câu hỏi
        public Sprite questionImage; // Hình ảnh liên quan đến câu hỏi
    }

    // Mảng chứa tất cả các câu hỏi
    public Question[] questions;
}
