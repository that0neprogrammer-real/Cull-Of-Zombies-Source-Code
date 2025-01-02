using TMPro;
using UnityEngine;

public class UIPopup : MonoBehaviour
{
    [SerializeField] private Interactions interactions;
    [SerializeField] private PlayerState playerState;
    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private GameObject objectivesTab;
    [SerializeField] private LayerMask interactable;
    [SerializeField] private float range;

    private readonly string empty = string.Empty;

    void Update()
    {
        DetectObjects();

        if (!playerState.completedMission || playerState.readingJournal)
            OpenMissionTab();
        else if (playerState.usingTab)
            ActivateObjectivesTab(false);
    }

    private void DetectObjects()
    {
        if (playerState.usingTab || interactions.HitRegister().transform == null)
        {
            displayText.SetText(empty);
            return;
        }

        else
        {
            Journal journal = interactions.HitRegister().transform.GetComponent<Journal>();
            if (journal != null) displayText.SetText("Read Journal");

            IDisplay objectName = interactions.HitRegister().transform.GetComponent<IDisplay>();
            if (objectName != null) displayText.SetText(objectName.DisplayName());
        }
    }

    private void OpenMissionTab()
    {
        if (Input.GetKey(KeyCode.Tab))
            ActivateObjectivesTab(true);
        else if (Input.GetKeyUp(KeyCode.Tab))
            ActivateObjectivesTab(false);
    }

    private void ActivateObjectivesTab(bool value)
    {
        objectivesTab.SetActive(value);

        if (playerState.usingTab) return;
        playerState.usingTab = value;
    }
}
