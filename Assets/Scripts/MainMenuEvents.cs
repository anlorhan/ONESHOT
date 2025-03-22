using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class MainMenuEvents : MonoBehaviour
{
    private UIDocument document;
    private Button startButton;

    private List<Button> menuButtons;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        document = GetComponent<UIDocument>();
        startButton = document.rootVisualElement.Q("Start") as Button;
        startButton.RegisterCallback<ClickEvent>(StartGame);

        menuButtons = document.rootVisualElement.Query<Button>().ToList();
        for (int i = 0; i < menuButtons.Count; i++)
        {
            menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnAllButtonsClick(ClickEvent evt)
    {
        audioSource.Play();
    }

    private void OnDisable()
    {
        startButton.UnregisterCallback<ClickEvent>(StartGame);

        for (int i = 0; i < menuButtons.Count; i++)
        {
            menuButtons[i].UnregisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void StartGame(ClickEvent evt)
    {
        SceneManager.LoadScene("Game");
    }
}
