using UnityEngine.UI;
using TMPro;
using UnityEngine;
using System.Collections;

public class P_HealthManager : StatsConfigurations
{
    [Space][SerializeField] private CameraShake cameraShake;
    [SerializeField] private HealthPack medkit;
    [SerializeField] private TextMeshProUGUI healthCounter;
    [SerializeField] private Transform abilityUI;
    [SerializeField] private GameObject[] abilities; //0 - adrenaline, 1 - hulk, 2 - press x
    [SerializeField] private Image[] interfaces; //0 - overlay, 1 - abilityBar, 2 - radial

    [Space]
    [SerializeField] private float UITransition;
    private float accumulatedHealth;
    private float accumulatedDamage;

    private Coroutine healCoroutine;
    private FPMovement playerMovement;
    [HideInInspector] public bool isHealing;
    [HideInInspector] public bool isHulk;
    [HideInInspector] public bool isFlash;
    public bool superHuman => isHulk && isFlash;
    public bool isFullHealth => currentHealth == maxHealth;
    public bool playerHasDied => isDead;
    public bool hasCancelled = false;

    private void Awake()
    {
        playerMovement = GetComponentInParent<FPMovement>();
    }

    private void Update()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateHealthText(healthCounter);

        if (isHealing && healCoroutine != null)
        {
            playerMovement.SetPlayerMove(false);

            if (Input.GetKeyDown(KeyCode.X))
            {
                StopCoroutine(healCoroutine);
                healCoroutine = null;

                accumulatedHealth = 0f;
                interfaces[2].fillAmount = 0f;
                abilities[2].SetActive(false);
                playerMovement.SetPlayerMove(true);
                isHealing = false;
            }
        }
        else if (!isHealing && healCoroutine != null)
        {
            healCoroutine = null;
            Debug.Log("done healing!");
        }

        if (currentHealth <= 0f) isDead = true;
    }

    public void Kill() => currentHealth = 0;

    public void ExecuteAction(int index, HealthPackData data1, HealthItemData data2)
    {
        switch (index)
        {
            case 0:
                healCoroutine =  StartCoroutine(Medkit(data1));
                break;
            case 1:
                StartCoroutine(Bandage(data2));
                break;
            case 2:
                StartCoroutine(Hulk(data2));
                break;
            case 3:
                StartCoroutine(Flash(data2));
                break;
        }
    }

    private IEnumerator Medkit(HealthPackData data)
    {
        isHealing = true;
        playerMovement.SetPlayerMove(false);
        float timeSinceStarted = 0f;
        accumulatedHealth = 0f;
        abilities[2].SetActive(true);

        while (timeSinceStarted < data.useTime)
        {
            timeSinceStarted += Time.deltaTime;
            float amountHealed = data.healingAmount * (Time.deltaTime / data.useTime);

            accumulatedHealth += amountHealed;
            interfaces[2].fillAmount = Mathf.Lerp(0f, 1f, timeSinceStarted / data.useTime);
            yield return null;
        }

        RemoveBloodOverlay();
        accumulatedHealth = 0f;
        interfaces[2].fillAmount = 0f;
        currentHealth += data.healingAmount;
        playerMovement.SetPlayerMove(true);

        abilities[2].SetActive(false);
        isHealing = false;

        medkit.RemoveHealthPack();
    }

    private IEnumerator Bandage(HealthItemData data)
    {
        isHealing = true;
        float timeSinceStarted = 0f;
        accumulatedHealth = 0f;

        while (timeSinceStarted < data.useTime)
        {
            timeSinceStarted += Time.deltaTime;
            float amountHealed = data.healingAmount * (Time.deltaTime / data.useTime);

            currentHealth += amountHealed;
            accumulatedHealth += amountHealed;

            if (accumulatedHealth >= 10)
            {
                BloodOverlay(false);
                accumulatedHealth = 0f;
            }

            yield return null;
        }

        accumulatedHealth = 0f;
        isHealing = false;
    }

    private IEnumerator Hulk(HealthItemData data)
    {
        isHulk = true;
        float timeSinceStarted = 0f;
        GameObject damageReductionPrefab = SpawnUI(0);

        while (timeSinceStarted < data.useTime)
        {
            timeSinceStarted += Time.deltaTime;
            float amountToFill = Mathf.Lerp(1f, 0f, timeSinceStarted / data.useTime);

            AbilityBar(damageReductionPrefab).fillAmount = amountToFill;
            yield return null;
        }

        isHulk = false;
    }

    private IEnumerator Flash(HealthItemData data)
    {
        isFlash = true;
        float timeSinceStarted = 0f;
        GameObject adrenalinePrefab = SpawnUI(1);
        playerMovement.AdjustSpeed(2f, true);

        while (timeSinceStarted < data.useTime)
        {
            timeSinceStarted += Time.deltaTime;
            float fillAmount = Mathf.Lerp(1f, 0f, timeSinceStarted / data.useTime);

            AbilityBar(adrenalinePrefab).fillAmount = fillAmount;
            yield return null;
        }

        playerMovement.AdjustSpeed(2f, false);
        isFlash = false;
    }

    public void TakeDamage(float amount)
    {
        float damageMultiplier = superHuman || isHulk ? amount * 0.5f : amount;
        base.GetDamaged(damageMultiplier);
        accumulatedDamage += damageMultiplier;

        if (currentHealth <= 40f && accumulatedDamage >= 10f)
        {
            Debug.Log("getting damaged");
            BloodOverlay(true);
            accumulatedDamage = 0f;
        }
    }

    #region
    private void RemoveBloodOverlay()
    {
        Color currentColor = interfaces[0].color;
        currentColor.a = 0f;

        interfaces[0].color = currentColor;
    }
    private void BloodOverlay(bool value)
    {
        Color currentColor = interfaces[0].color;
        float newAlpha = value ? Mathf.Clamp01(currentColor.a + 0.1f) : Mathf.Clamp01(currentColor.a - 0.1f);

        currentColor.a = newAlpha;
        interfaces[0].color = currentColor;
    }
    private Image AbilityBar(GameObject ui)
    {
        Image img = ui.GetComponent<Image>();
        return img;
    }
    private GameObject SpawnUI(int value)
    {
        GameObject obj = Instantiate(abilities[value], abilityUI);
        return obj;
    }
    public void UpdateHealthText(TextMeshProUGUI display)
    {
        int convert = (int)currentHealth;
        display.SetText(convert.ToString());
    }
    #endregion
}
