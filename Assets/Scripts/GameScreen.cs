using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScreen : MonoBehaviour
{
    [SerializeField] private HealthView healthView;

    private void OnEnable()
    {
        Refresh();

        UserData.OnUserDataChanged += Refresh;
    }

    private void OnDisable()
    {
        UserData.OnUserDataChanged -= Refresh;
    }

    public void Refresh()
    {
        healthView.Refresh();
    }
}
