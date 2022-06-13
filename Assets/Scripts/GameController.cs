using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public float timePerQuestion;
    float m_curTime;
    //l?u tr? s? câu h?i ng??i ch?i tr? l?i ?úng
    int m_rightCount;
    //tr?ng hái tr??c khi b?t ??u
    private void Awake()
    {
        m_curTime = timePerQuestion;
    }
    // Start is called before the first frame update
    void Start()
    {
        CreateQuestion();
        UIManage.Ins.SetTimeText("00 : " + m_curTime);
        StartCoroutine(TimeCountingDown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CreateQuestion()
    {
        
        QuestionData qs = QuestionManage.Ins.GetRadomQuestion();
        if(qs != null)
        {
            UIManage.Ins.SetQuestionText(qs.Question); 
            string[] wrongAnswer = new string[] { qs.answerA, qs.answerB, qs.answerC };
            UIManage.Ins.ShuffleAnswer();
            var temp = UIManage.Ins.answerButtons;
            if(temp != null && temp.Length > 0)
            {
                int wrongAnswerCount = 0;
                for (int i = 0; i < temp.Length; i++)
                {
                    int answerId = i;
                    if(string.Compare(temp[i].tag, "RightAnswer") == 0)
                    {
                        temp[i].SetAnswerText(qs.rightAnswer);
                    }
                    else
                    {
                        temp[i].SetAnswerText(wrongAnswer[wrongAnswerCount]);
                        wrongAnswerCount++;
                    }
                    // khi ng??i dùng click thì Event ?c g?i
                    temp[answerId].btnComp.onClick.RemoveAllListeners(); 
                    temp[answerId].btnComp.onClick.AddListener(() => CheckRightAnswerEvent(temp[answerId]));
                }
            }    
        }    
    } 
    void CheckRightAnswerEvent(AnswerQuestion answerButton)
    {
        if(answerButton.CompareTag("RightAnswer"))
        {
            m_curTime = timePerQuestion;
            UIManage.Ins.SetTimeText("00 : " + m_curTime);
            m_rightCount++;
            if (m_rightCount == QuestionManage.Ins.questions.Length)
            {
                UIManage.Ins.dialog.SetDialogContent("Ban da pha dao !.");
                UIManage.Ins.dialog.Show(true);
                AudioCntroller.Ins.Playwinsound();
                StopAllCoroutines();
            }
            else
            {
                CreateQuestion();
                AudioCntroller.Ins.Playrightsound();
                Debug.Log("Ban da tra loi dung");
            }    
            
        }    
        else
        {
            UIManage.Ins.dialog.SetDialogContent("Tiec quá ! Thua mat roi");
            UIManage.Ins.dialog.Show(true);
            AudioCntroller.Ins.Playlosesound();
            Debug.Log("Ban da tra loi sai , tro choi ket thuc");
        }    
    }
    IEnumerator TimeCountingDown()
    {
        //??i 1 kho?ng th?i gian nh?t ??nh
        yield return new WaitForSeconds(1);
        if (m_curTime > 0)
        {
            m_curTime--;
            UIManage.Ins.SetTimeText("00 : " + m_curTime);
            //t??ng ???ng vòng l?p
            StartCoroutine(TimeCountingDown());
        }
        else
        {
            UIManage.Ins.dialog.SetDialogContent("Da het thoi gian. Ban da thua.Tro choi ket thuc");
            UIManage.Ins.dialog.Show(true);
            StopAllCoroutines();
            AudioCntroller.Ins.Playlosesound();

        }    
        
    }
    public void Replay()
    {
        AudioCntroller.Ins.StopMusic();
        SceneManager.LoadScene("Gameplay");
    }    
    public void Exit()
    {
        Application.Quit();
    }    
}
