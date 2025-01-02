using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ReturnToMenu : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Image blackScreen;
    [SerializeField] private TextMeshProUGUI mainText;
    [SerializeField] private TextMeshProUGUI pressAnyKey;

    private void Update()
    {
        if (pressAnyKey.gameObject.activeSelf)
        {
            if (Input.anyKeyDown)
            {
                loadingScreen.SetActive(true);
                SceneManager.LoadScene(0);
            }
        }
    }

    public void Return()
    {
        loadingScreen.SetActive(true);
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public IEnumerator StartEndScreen()
    {
        yield return new WaitForSeconds(3f);

        Color currentColor = blackScreen.color;
        while (currentColor.a < 1f)
        {
            currentColor.a += 0.25f * Time.deltaTime;
            blackScreen.color = currentColor;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        mainText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        pressAnyKey.gameObject.SetActive(true);
    }
}
