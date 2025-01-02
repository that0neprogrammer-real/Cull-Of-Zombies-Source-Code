using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(Interactions))]
public class UIHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private PlayerState playerState;
    [SerializeField] private Interactions interactions;
    [SerializeField] private GameObject[] gameTabs = new GameObject[5]; //0 - ui, 1 - objectives menu, 2 - notify , 3 - completed

    private readonly string empty = string.Empty;

    private void Update()
    {
        if (!FPMovement.Instance.gameObject.activeSelf) return;

        DisplayText();

        PauseMenu();
        ObjectivesMenu();
    }

    private void DisplayText()
    {
        if (playerState.usingTab || interactions.HitRegister().transform == null)
        {
            displayText.SetText(empty);
            return;
        }
        else
        {
            IDisplay objectName = interactions.HitRegister().transform.GetComponent<IDisplay>();
            if (objectName != null) displayText.SetText(objectName.DisplayName());
        }
    }

    private void PauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameTabs[0].activeSelf)
            {
                playerState.gamePaused = true;
                gameTabs[0].SetActive(false);
                if (!gameTabs[1].activeSelf) gameTabs[1].SetActive(true);

                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                Time.timeScale = 0;
            }
            else
            {
                if (!gameTabs[1].activeSelf) return;

                playerState.gamePaused = false;
                gameTabs[0].SetActive(true);
                if (gameTabs[1].activeSelf) gameTabs[1].SetActive(false);

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1;
            }
        }
    }

    private void ObjectivesMenu()
    {
        if (Input.GetKey(KeyCode.Tab) && !gameTabs[2].activeSelf && !gameTabs[3].activeSelf && !gameTabs[4].activeSelf && !playerState.gamePaused)
        {
            gameTabs[2].SetActive(true);

            if (playerState.usingTab) return;
            playerState.usingTab = true;
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            gameTabs[2].SetActive(false);

            if (!playerState.usingTab) return;
            playerState.usingTab = false;
        }
    }

    public void UnPause()
    {
        playerState.gamePaused = false;
        gameTabs[0].SetActive(true);
        if (gameTabs[1].activeSelf) gameTabs[1].SetActive(false);

        Cursor.visible = false;
        Time.timeScale = 1;
    }
}
