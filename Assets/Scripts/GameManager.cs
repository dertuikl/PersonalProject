using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameScreen;

    private void Start()
    {
        gameScreen.SetActive(true);
    }
}
