using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake() {
        Instance = this;
    }
    public Transform weaponLootParent;
    public Transform charckterParent;

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

    private void Start() {
        totalAliveCharacters = charckterParent.childCount;
        CurrentAliveCharacters = totalAliveCharacters;
    }

    public void DecreaseAliveCharacter(Transform deadCharacter){
        deadCharacter.parent = null;
        CurrentAliveCharacters--;
    }

    public void GameOver(){
        print("Game Over");
    }

}
