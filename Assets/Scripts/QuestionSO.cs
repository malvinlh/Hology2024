using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Question", menuName = "Question")]
public class QuestionSO : ScriptableObject
{
    public Sprite image;

    public string theme;
    public string question;
    public string[] options;

    public int correctOptionIndex;
}
