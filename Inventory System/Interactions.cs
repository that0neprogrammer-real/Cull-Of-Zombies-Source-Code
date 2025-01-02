using System.Collections;
using UnityEngine.UI;
using Nomnom.RaycastVisualization;
using UnityEngine;

public class Interactions : MonoBehaviour
{
    [SerializeField] private Transform playerCamera;
    [SerializeField] private Image radialBar;
    [SerializeField] private FPSound soundHandler;
    [SerializeField] private LayerMask interactableLayer;

    [Space][SerializeField] private float range;
    [SerializeField] private float holdTime;

    private RaycastHit hit;
    private Coroutine holding;
    private PlayerState playerState;
    private WeaponInventory weap;
    private UtilityInventory util;

    private void Start()
    {
        playerState = GetComponent<PlayerState>();
        weap = GetComponent<WeaponInventory>();
        util = GetComponent<UtilityInventory>();
    }

    private void Update()
    {
        if (VisualPhysics.Raycast(playerCamera.position, playerCamera.forward, out hit, range, interactableLayer))
            if (hit.transform != null)
            {
                IPickable item = hit.transform.GetComponent<IPickable>();
                IInteractable interactable = hit.transform.GetComponent<IInteractable>();

                if (item != null || interactable != null)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        KeyPress(item, interactable);
                        HandleSounds(hit.transform.tag);
                    }
                    else if (Input.GetKey(KeyCode.E)) KeyHold(interactable);
                    else if (Input.GetKeyUp(KeyCode.E)) KeyRelease();
                }
            }
    }

    private void HandleSounds(string name)
    {
        switch (name)
        {
            case "Weapon":
            case "Ammo":
                soundHandler.Pickups(1);
                break;
            case "Pickup":
                soundHandler.Pickups(0);
                break;
            case "Door":
                soundHandler.Interact(0);
                break;
            case "Metal Door":
                soundHandler.Interact(1);
                break;
            case "Journal":
                if (playerState.readingJournal) soundHandler.Interact(2);
                break;
            case "Generator":
                soundHandler.Interact(3);
                break;
            case "Gas Tank":
                soundHandler.Interact(4);
                break;
            default:
                Debug.Log("Undefined Tag!");
                break;
        }
    }

    public RaycastHit HitRegister() => hit;

    public bool HasHitSomething() => hit.transform != null;

    #region //Inventory
    private void AddItemToInventory(Item data)
    {
        switch (data.GetData().itemType)
        {
            case ItemData.ItemType.primary:
                Weapon(0, data);
                break;
            case ItemData.ItemType.sidearm:
                Weapon(1, data);
                break;
            case ItemData.ItemType.grenade:
                Grenade(data);
                break;
            case ItemData.ItemType.healthItem:
                HealthItem(data);
                break;
            case ItemData.ItemType.healthPack:
                HealthPack(data);
                break;
            default:
                Debug.Log("Invalid Item");
                break;
        }
    }
    #endregion

    #region //Items
    public void Weapon(int index, Item item)
    {
        if (index == 0) weap.AddPrimary(item.GetData(), item);
        else if (index == 1) weap.AddSidearm(item.GetData(), item);
    }
    public void Grenade(Item item)
    {
        switch (item.GetData().id)
        {
            case 0: //frag
                util.AddNewGrenade(item.GetData(), item);
                break;
            case 1: //molly
                util.AddNewGrenade(item.GetData(), item);
                break;
            case 2: //concussor
                util.AddNewGrenade(item.GetData(), item);
                break;
            default:
                Debug.Log("Invalid Grenade");
                break;
        }
    }
    public void HealthItem(Item item)
    {
        switch (item.GetData().id)
        {
            case 0: //bandage
                util.AddNewHealthItem(item.GetData(), item);
                break;
            case 1: //pain killer
                util.AddNewHealthItem(item.GetData(), item);
                break;
            case 2: //energy drink
                util.AddNewHealthItem(item.GetData(), item);
                break;
            default:
                Debug.Log("Invalid Health Item");
                break;
        }
    }
    public void HealthPack(Item item)
    {
        switch (item.GetData().id)
        {
            case 0: //health pack
                util.AddNewHealtPack(item.GetData(), item);
                break;
            default:
                Debug.Log("Invalid Health Pack");
                break;
        }
    }
    #endregion

    #region //Interaction (Key Press)
    private void KeyPress(IPickable item, IInteractable itr)
    {
        if (item != null) AddItemToInventory(item.GetPickupData());
        else if (itr != null)
        {
            switch (itr.InteractType())
            {
                case 0: //Press
                    itr.Interact();
                    break;
                default:
                    break;
            }
        }
    }

    private void KeyHold(IInteractable itr)
    {
        if (itr != null && !itr.HasInteracted())
        {
            switch (itr.InteractType())
            {
                case 1: //Hold
                    StartInteracting(itr);
                    break;
                default:
                    break;
            }
        }
    }

    private void KeyRelease()
    {
        if (playerState.holdingKey && holding != null) StopInteracting();
    }
    #endregion

    #region //Interaction (Hold)
    private void StartInteracting(IInteractable itr)
    {
        if (!playerState.holdingKey)
            holding = StartCoroutine(Interacting(itr));
    }

    private IEnumerator Interacting(IInteractable itr)
    {
        playerState.holdingKey = true;
        float timeSinceStarted = 0f;

        while (timeSinceStarted < holdTime)
        {
            timeSinceStarted += Time.deltaTime;
            float targetFill = Mathf.Lerp(0f, 1f, timeSinceStarted / holdTime);

            radialBar.fillAmount = targetFill;
            yield return null;
        }

        itr.Interact();
        radialBar.fillAmount = 0f;
        soundHandler.StopAudio();
        playerState.holdingKey = false;
    }

    private void StopInteracting()
    {
        StopCoroutine(holding);
        soundHandler.StopAudio();

        radialBar.fillAmount = 0f;
        holding = null;
        playerState.holdingKey = false;
    }
    #endregion
}
