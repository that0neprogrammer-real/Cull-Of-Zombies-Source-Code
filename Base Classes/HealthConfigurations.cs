using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class HealthConfigurations
{
    public float maxHealth;
    public float currentHealth;

    [Space]
    public bool isHealing;
    public float adrenalineDuration;
    public float hulkDuration;
    public Abilities abilityType;

    private float fillVelocity;
    private float currentAmount;
    private float accumulatedDamage;
    private float accumulatedHealth;

    public bool fullHealth => currentHealth == maxHealth;

    #region //Buff types (Condition)
    public bool noBuff => abilityType == Abilities.none;
    public bool armorToggled => abilityType == Abilities.damageReduction;
    public bool sprintToggled => abilityType == Abilities.fasterMovement;
    public bool buffsToggled => abilityType == Abilities.superhuman;
    #endregion

    public enum Abilities
    {
        none,
        damageReduction,
        fasterMovement,
        superhuman
    }

    public void SetHealth(float amount)
    {
        maxHealth = amount;
        currentHealth = maxHealth;
    }

    public IEnumerator Medkit(float amount, float timeToHeal, Image[] interfaces)
    {
        isHealing = true;
        float timeSinceStarted = 0f;
        accumulatedHealth = 0f;

        while (timeSinceStarted < timeToHeal)
        {
            timeSinceStarted += Time.deltaTime;
            float healAmount = amount * (Time.deltaTime / timeToHeal);
            //float fillAmount = Mathf.Lerp(0f, 1f, timeSinceStarted / timeToHeal);

            currentHealth += healAmount;
            accumulatedHealth += healAmount;
            //interfaces[1].fillAmount = fillAmount;

            if (accumulatedHealth >= 10f)
            {
                BloodOverlay(false, interfaces);
                accumulatedHealth = 0f;
            }

            yield return null;
        }

        interfaces[1].fillAmount = 0f;
        accumulatedHealth = 0f;
        isHealing = false;
    }

    public IEnumerator Bandage(float amount, float timeToHeal, Image[] interfaces)
    {
        isHealing = true;
        float timeSinceStarted = 0f;
        accumulatedHealth = 0f;

        while (timeSinceStarted < timeToHeal)
        {
            timeSinceStarted += Time.deltaTime;
            float healAmount = amount * (Time.deltaTime / timeToHeal);

            currentHealth += healAmount;
            accumulatedHealth += healAmount;

            if (accumulatedHealth >= 10f)
            {
                BloodOverlay(false, interfaces);
                accumulatedHealth = 0f;
            }

            yield return null;
        }

        accumulatedHealth = 0f;
        isHealing = false;
    }

    public IEnumerator Hulk(float timeToExpire, GameObject ui, Image[] interfaces)
    {
        if (abilityType != Abilities.none) abilityType = Abilities.superhuman;
        else abilityType = HealthConfigurations.Abilities.damageReduction;

        ui.transform.SetParent(interfaces[3].transform, false);
        float timeSinceStarted = 0f;

        while (timeSinceStarted < timeToExpire)
        {
            timeSinceStarted += Time.deltaTime;
            float fillAmount = Mathf.Lerp(1f, 0f, timeSinceStarted / timeToExpire);

            hulkDuration = timeToExpire - timeSinceStarted;
            AbilityBar(ui).fillAmount = fillAmount;
            yield return null;
        }

        hulkDuration = 0f;
    }

    public IEnumerator Adrenaline(float timeToExpire, GameObject ui, Image[] interfaces)
    {
        if (abilityType != Abilities.none) abilityType = Abilities.superhuman;
        else abilityType = HealthConfigurations.Abilities.fasterMovement;

        FPMovement.Instance.AdjustSpeed(2f, true);
        ui.transform.SetParent(interfaces[3].transform, false);
        float timeSinceStarted = 0f;

        while (timeSinceStarted < timeToExpire)
        {
            timeSinceStarted += Time.deltaTime;
            float fillAmount = Mathf.Lerp(1f, 0f, timeSinceStarted / timeToExpire);

            adrenalineDuration = timeToExpire - timeSinceStarted;
            
            AbilityBar(ui).fillAmount = fillAmount;
            yield return null;
        }

        adrenalineDuration = 0f;
        FPMovement.Instance.AdjustSpeed(2f, false);
    }

    public void Damage(bool isPlayer,float amount, Image[] interfaces)
    {
        if (isPlayer)
        {
            if (currentHealth > 0f)
            {
                currentHealth -= amount;
                accumulatedDamage += amount;

                if (accumulatedDamage >= 10f && currentHealth <= 40f)
                {
                    BloodOverlay(true, interfaces);
                    accumulatedDamage = 0f;
                }
            }
        }
        else
        {
            if (currentHealth > 0f)
                currentHealth -= amount;
        }
    }

    #region
    private Image AbilityBar(GameObject ui)
    {
        Image img = ui.GetComponent<Image>();
        return img;
    }
    private void BloodOverlay(bool value, Image[] interfaces)
    {
        Color currentColor = interfaces[0].color;
        float newAlpha = value ? Mathf.Clamp01(currentColor.a + 0.1f) : Mathf.Clamp01(currentColor.a - 0.1f);

        currentColor.a = newAlpha;
        interfaces[0].color = currentColor;
    }
    public void UpdateHealthUI(Image[] interfaces, float transition)
    {
        currentAmount = currentHealth / maxHealth;
        interfaces[2].fillAmount = Mathf.SmoothDamp(interfaces[2].fillAmount, currentAmount, ref fillVelocity, transition);
    }
    public void UpdateHealthText(TextMeshProUGUI display)
    {
        int convert = (int)currentHealth;
        display.SetText(convert.ToString());
    }
    #endregion
}
