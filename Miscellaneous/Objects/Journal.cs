using UnityEngine;
using TMPro;

public class Journal : MonoBehaviour, IInteractable, IDisplay
{
    [SerializeField] private ObjectData data;
    [SerializeField] private PlayerState playerState;
    [SerializeField] private GameObject subText;
    [SerializeField] private GameObject journalInterface;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] [TextArea(5, 10)] private string noteText;
    [SerializeField] private bool hasInteracted = false;

    public void Interact()
    {
        if (!HasInteracted())
        {
            playerState.readingJournal = true;
            text.SetText(DisplayText());
            CanPlayerMove(false);

            subText.SetActive(false);
            journalInterface.SetActive(true);
            hasInteracted = true;
        }
        else
        {
            playerState.readingJournal = false;
            journalInterface.SetActive(false);
            CanPlayerMove(true);

            subText.SetActive(true);
            text.SetText(string.Empty);
            hasInteracted = false;
        }
    }
    private void CanPlayerMove(bool val)
    {
        FPPlayerLook.Instance.CanPlayerLook(val);
        FPMovement.Instance.SetPlayerMove(val);
    }

    private string DisplayText() => noteText;
    public bool HasInteracted() => hasInteracted;
    public int InteractType() => (int)data.interactType;

    public string DisplayName() => data.name;
}
