using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;

    private int currentHealth => UserData.Health;
    private int maxHealth => UserData.MaxHealth;

    public void Refresh()
    {
        healthText.text = $"{currentHealth}/{maxHealth}";
    }
}
