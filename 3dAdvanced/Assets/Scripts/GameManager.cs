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
                playerWin = true;
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
        if(playerWin){
            winCanvas.SetActive(true);
            rankText.text = $"Rank: <color=green>1</color> / {totalAliveCharacters}";
        } else{
            rankText.text = $"Rank: <color=red>{CurrentAliveCharacters + 1}</color> / {totalAliveCharacters}";
            loseCanvas.SetActive(true);
        }
        print("Game Over");
    }

    public void RestartGame(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}
