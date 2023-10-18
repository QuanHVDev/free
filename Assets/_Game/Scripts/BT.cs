using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT : MonoBehaviour
{
    private List<Task> listCauHoi = new List<Task>()
    {
        new Task()
        {
            Question = "1. Điền từ còn thiếu vào chỗ trống: “Đảng Cộng sản Việt Nam là đội tiền phong của giai cấp công nhân, đồng thời là đội tiền phong của nhân dân lao động và của dân tộc Việt Nam, đại biểu trung thành lợi ích của giai cấp công nhân, nhân dân lao động và của dân tộc. Đảng lấy chủ nghĩa Mác - Lênin và Tư tưởng Hồ Chí Minh làm nền tảng tư tưởng, kim chỉ nam cho hành động, lấy ……làm nguyên tắc tổ chức cơ bản” (Văn kiện Đại hội XI của Đảng)",
            ListAnwser = new List<Answer>()
            {
                new Answer("a. Phê bình và tự phê bình", false),
                new Answer("b. Tập trung dân chủ", true),
                new Answer("c. Đoàn kết thống nhất trong Đảng", false),
                new Answer("d. Kỷ luật nghiêm minh, tự giác", false)
            }
        },
        
        
    };
    
    public class Task
    {
        public string Question;
        public List<Answer> ListAnwser;
    }

    public class Answer
    {
        public Answer(string answer, bool isCorrect)
        {
            this.answer = answer;
            this.isCorrect = isCorrect;
        }
        public string answer;
        public bool isCorrect;
    }
}
