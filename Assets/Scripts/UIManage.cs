using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManage : MonoBehaviour
{
    public static UIManage Ins;
    public Text timeText;
    public Text questionText;
    public Dialog dialog;
    public AnswerQuestion[] answerButtons;
    private void Awake()
    {
        MakeSingleton();
    }
    private void Start()
    {
        ShuffleAnswer();
    }
    public void SetTimeText(string content)
    {
        if (timeText)
            timeText.text = content;
    }
    public void SetQuestionText(string content)
    {
        if (timeText)
            questionText.text = content;
    }
    public void ShuffleAnswer()
    {
        if(answerButtons != null && answerButtons.Length > 0)
        {
            for (int i = 0; i < answerButtons.Length; i++)
            {
                if(answerButtons[i])
                {
                    answerButtons[i].tag = "Untagged";
                }
                
            }
            int randIdx = Random.Range(0, answerButtons.Length);
            if(answerButtons[randIdx])
                {
                    answerButtons[randIdx].tag = "RightAnswer";
                }    
        }    
    }    
    public void MakeSingleton()
    {
        if(Ins == null)
        {
            Ins = this;
        }    
        else
        {
            Destroy(gameObject);
        }    
    }    
}
