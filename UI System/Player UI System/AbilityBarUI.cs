using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AbilityBarUI : MonoBehaviour
{
    void Update()
    {
        Image img = gameObject.GetComponent<Image>();
        if (img != null &&  img.fillAmount <= 0f) Destroy(gameObject, 0.05f);
    }
}
