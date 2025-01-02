using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private GameObject deathCam;
    [SerializeField] private AmbienceSoundHandler deathMusic;
    [SerializeField] private P_HealthManager player;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject loadingScreen;
    private bool hasPlayed = false;

    private void Update()
    {
        if (player.playerHasDied)
        {
            FPMovement.Instance.SetPlayerMove(false);

            if (!hasPlayed)
            {
                deathMusic.PlayDeathMusic();
                hasPlayed = true;
            }

            if (!deathCam.gameObject.activeSelf) deathCam.SetActive(true);
            if (FPMovement.Instance.gameObject.activeSelf) FPMovement.Instance.gameObject.SetActive(false);
            if (!deathScreen.activeSelf) deathScreen.SetActive(true);
            if (Input.anyKeyDown)
            {
                loadingScreen.SetActive(true);
                SceneManager.LoadScene(0);
            }
        }
    }

}
