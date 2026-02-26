using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI References")]
    public GameObject dialoguePanel;
    public Image portraitImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    private DialogueData currentDialogue;
    private int currentIndex;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void StartDialogue(DialogueData dialogue)
    {
        currentDialogue = dialogue;
        currentIndex = 0;
        dialoguePanel.SetActive(true);
        ShowLine();
    }

    public void ShowNextLine()
    {
        currentIndex++;

        if (currentIndex >= currentDialogue.lines.Length)
        {
            EndDialogue();
            return;
        }

        ShowLine();
    }

    private void ShowLine()
    {
        DialogueLine line = currentDialogue.lines[currentIndex];

        nameText.text = line.characterName;
        portraitImage.sprite = line.portrait;
        dialogueText.text = line.sentence;
    }

    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        GameManager.Instance.StartGame(); 
    }
}