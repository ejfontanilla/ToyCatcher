using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string characterName;
    public Sprite portrait;
    [TextArea(3, 5)]
    public string sentence;
}