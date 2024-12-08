using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake() {
        Instance = this;
    }

    [Header("Unit Parent")]
    public Transform weaponLootParent;
    public Transform charckterParent;

    [Header("Progress Battle Royale")]
    public int totalAliveCharacters;
    public int currentAliveCharacters;
    public int CurrentAliveCharacters{
        get => currentAliveCharacters;

        set{
            currentAliveCharacters = value;
            characterAliveText.text = $"Total Alive: {currentAliveCharacters} / {totalAliveCharacters}";
            if(currentAliveCharacters <= 1){
                GameOver();
            }
        }
    }
    public TextMeshProUGUI characterAliveText;

    [Header("Game Over")]
    public GameObject gameOverCanvas;
    public TextMeshProUGUI rankText;
    public GameObject winCanvas;
    public GameObject loseCanvas;

    public bool playerWin;

    private void Start() {
        totalAliveCharacters = charckterParent.childCount;
        CurrentAliveCharacters = totalAliveCharacters;
    }

    private void Update() {
        RestartGame();
    }

    public void DecreaseAliveCharacter(Transform deadCharacter){
        deadCharacter.parent = null;
        CurrentAliveCharacters--;
    }

    public void GameOver(){
        Time.timeScale = 0.5f;
        gameOverCanvas.SetActive(true);
        rankText.text = $"Rank: {CurrentAliveCharacters + 1} / {totalAliveCharacters}";
        if(playerWin){
            winCanvas.SetActive(true);
        } else{
            loseCanvas.SetActive(true);
        }
        print("Game Over");
    }

    public void RestartGame(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}
