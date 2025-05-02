using UnityEngine;
using UnityEngine.UI;

public class dialogueManager : MonoBehaviour
{
    [System.Serializable]
    public class DialogueLine
    {
        public string speakerName;
        public Sprite faceSprite;
        [TextArea(2, 5)]
        public string dialogueText;
    }

    public GameObject dialoguePanel;
    public Text nameText;
    public Image faceImage;
    public Text dialogueText;

    public DialogueLine[] dialogueLines;
    private int currentLine = 0;
    private bool hasTalked = false;

    public DialogueLine[] repeatedLines;

    public PlayerMovement playerMovement;  //  reference to your PlayerMovement script

    public GameObject npcArrow;

    public void StartDialogue()
    {
        dialoguePanel.SetActive(true);
        currentLine = 0;

        if (playerMovement != null)
            playerMovement.canMove = false;  // Freeze player

        if (hasTalked)
            dialogueLines = repeatedLines;

        DisplayLine();
    }

    public void DisplayLine()
    {
        if (currentLine < dialogueLines.Length)
        {
            DialogueLine line = dialogueLines[currentLine];
            nameText.text = line.speakerName;
            faceImage.sprite = line.faceSprite;
            dialogueText.text = line.dialogueText;
            currentLine++;
        }
        else
        {
            dialoguePanel.SetActive(false);
            hasTalked = true;

            if (npcArrow != null)
                npcArrow.SetActive(false);  // Hide the arrow after first full dialogue

            if (playerMovement != null)
                playerMovement.canMove = true;  // Unfreeze player
        }

    }

    void Update()
    {
        if (dialoguePanel.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            DisplayLine();
        }
    }
}
