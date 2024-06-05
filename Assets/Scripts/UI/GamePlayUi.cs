using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUi : MonoBehaviour
{
    public static GamePlayUi Instance { get; private set; }
    [SerializeField] private Button menuButton;

    public event EventHandler OnMenuButtonClick;

    private void Awake()
    {
        

        menuButton.onClick.AddListener(() =>
        {
            OnMenuButtonClick?.Invoke(this, EventArgs.Empty);
        });

        Instance = this;
    }


}