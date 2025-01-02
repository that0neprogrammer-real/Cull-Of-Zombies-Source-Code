using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using DG.Tweening;

public class InteractableObjects : MonoBehaviour
{
    /*[SerializeField] private RaycastFunctions ray;
    [SerializeField] private StateController func;
    [SerializeField] private TextMeshProUGUI journalText;
    [SerializeField] private Image radialBar;
    [SerializeField] private LayerMask interactable;
    [SerializeField] private GameObject[] userInterface;

    //Activator Properties
    [Space]
    [SerializeField] private float holdTime;
    public Coroutine holding;
    private bool isHolding = false;

    private void Start()
    {
        ray = GetComponent<RaycastFunctions>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !func.usingMissionTab) 
            Interact();
    }

    private void Interact()
    {
        RaycastHit hit;
        if (hit.transform == null)
            return;

        if (hit.transform.CompareTag("Mission 2"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Mission2 m2 = hit.transform.GetComponentInParent<Mission2>();
                m2.hasGasCan = true;
                Destroy(hit.transform.gameObject);
            }
        }

        if (hit.transform.CompareTag("Mission 3"))
        {
            Mission3 m3 = hit.transform.GetComponent<Mission3>();
            if (!m3.hasActivated)
                m3.ActivateMission();
        }

        IInteractable getID = hit.transform.GetComponent<IInteractable>();
        if (getID != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
                PressKey(hit, getID);
            else if (Input.GetKey(KeyCode.E))
                HoldKey(getID);
            else if (Input.GetKeyUp(KeyCode.E))
                ReleaseKey();
        }
    }

    private void PressKey(RaycastHit hit, IInteractable id)
    {
        switch (id.GetData().id)
        {
            case 1:
                DoorInteract(hit.transform.parent, id);
                break;
            case 2:
                GateInteract(hit.transform.GetChild(0), id);
                break;

        }
    }

    private void HoldKey(IInteractable id)
    {
        switch (id.GetData().id)
        {
            case 0:
                StartActivation(id);
                break;
        }
    }

    private void ReleaseKey()
    {
        if (isHolding && holding != null)
            StopActivation();
    }

    #region Door System
    private void DoorInteract(Transform door, IInteractable hasOpened)
    {
        if (!hasOpened.CheckInteract())
        {
           hasOpened.Interact(true);
           door.DORotate(new(0f, hasOpened.SetRotation(true), 0f), 0.5f).SetEase(Ease.Linear);
        }
        else
        {
            hasOpened.Interact(false);
            door.DORotate(new(0f, hasOpened.SetRotation(false), 0f), 0.5f).SetEase(Ease.Linear);
        }    
    }

    private void GateInteract(Transform gate, IInteractable hasOpened)
    {
        if (!hasOpened.CheckInteract())
        {
            hasOpened.Interact(true);
            gate.DOLocalMoveX(0f, 0.5f);
        }
        else
        {
            hasOpened.Interact(false);
            gate.DOLocalMoveX(-8f, 0.5f);
        }
    }
    #endregion

    #region //Hold Button to Interact
    private void StartActivation(IInteractable hasClicked)
    {
        if (!isHolding && !hasClicked.CheckInteract())
            holding = StartCoroutine(Activator(hasClicked));
    }

    private void StopActivation()
    {
        StopCoroutine(holding);
        radialBar.fillAmount = 0f;
        holding = null;
        isHolding = false;
        Debug.Log("Hold Stopped!");
    }

    private IEnumerator Activator(IInteractable hasClicked)
    {
        isHolding = true;
        float timeSinceStarted = 0f;

        while (timeSinceStarted < holdTime)
        {
            timeSinceStarted += Time.deltaTime;
            float targetFill = Mathf.Lerp(0f, 1f, timeSinceStarted / holdTime);

            radialBar.fillAmount = targetFill;
            yield return null;
        }

        radialBar.fillAmount = 0f;
        isHolding = false;
        Debug.Log("Task Completed");
        hasClicked.Interact(true);
    }
    #endregion
    */
}
