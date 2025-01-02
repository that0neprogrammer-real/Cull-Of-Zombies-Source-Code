using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScreenFade : MonoBehaviour
{
    [SerializeField] private Image fader;
    [SerializeField] private FlashLight player;

    private void Start()
    {
        DOTween.SetTweensCapacity(1000, 1000);

        Invoke(nameof(FadeIn), 4f);
        fader.DOFade(0, 5f).SetEase(Ease.Linear);
    }

    private void FadeIn()
    {
        FPMovement.Instance.SetPlayerMove(true);
        FPPlayerLook.Instance.CanPlayerLook(true);
        player.canUse = true;
    }
}
