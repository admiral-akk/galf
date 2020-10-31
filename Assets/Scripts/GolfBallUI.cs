using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GolfBallUI : MonoBehaviour
{
    [SerializeField] Image hitBar;
    [SerializeField] Image powerBar;

    public void IsAiming(bool isAiming)
    {
        hitBar.gameObject.SetActive(isAiming);
    }

    public void SetIndicator(Vector3 direction, float magnitude)
    {
        powerBar.transform.localScale = new Vector3(magnitude, 1f, 1f);
        powerBar.color = new Color(Mathf.Clamp(2f * magnitude, 0f, 1f), Mathf.Clamp(2f - 2f * magnitude, 0f, 1f), 0f);

        hitBar.transform.localPosition = -direction * 2.75f;
        hitBar.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 180.0f + 180.0f * Mathf.Atan2(direction.y, direction.x) / Mathf.PI);
    }
}
