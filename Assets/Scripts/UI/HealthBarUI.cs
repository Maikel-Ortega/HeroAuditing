using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    Damageable playerDamageable;
    Slider healthBarSlider;

    void Start()
    {
        playerDamageable = FindFirstObjectByType<PlayerDamage>().GetComponent<Damageable>();
        healthBarSlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDamageable != null)
        {
            healthBarSlider.value = (float)playerDamageable.Hp / playerDamageable.MAX_HP;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
