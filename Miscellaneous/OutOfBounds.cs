using UnityEngine;
using TMPro;
using System.Collections;

public class OutOfBounds : MonoBehaviour
{
    public GameObject warningScreen;
    public GameObject uiHolder;
    public TextMeshProUGUI text;
    public DeathScreen death;
    public P_HealthManager player;
    public bool[] triggerCheck;

    public float timerBeforeDeath = 5f;
    private Coroutine countdownCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (countdownCoroutine != null)
                StopCoroutine(countdownCoroutine);

            warningScreen.SetActive(false);
            uiHolder.SetActive(true);

            timerBeforeDeath = 5f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) countdownCoroutine = StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        uiHolder.SetActive(false);
        warningScreen.SetActive(true);

        while (timerBeforeDeath > 0f)
        {
            timerBeforeDeath -= Time.deltaTime;
            text.SetText(timerBeforeDeath.ToString("0.00"));
            yield return null;
        }

        player.Kill();
    }
}
