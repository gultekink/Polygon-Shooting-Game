using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private int levelLock;
    private bool _resetLevels;
    public Button[] levelButton;

    private Target target;
    // Start is called before the first frame update

    void Start()
    {
        target = GetComponent<Target>();
        levelLock = PlayerPrefs.GetInt("LevelLock", 1);
        LevelLock();
    }

    void LevelLock()
    {
        for (int i = 0; i < levelButton.Length; i++)
        {
            levelButton[i].interactable = false;
        }

        for (int i = 0; i < levelLock; i++)
        {
            levelButton[i].interactable = true;
        }
    }

    public void OpenLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    void Update()
    {
        NextLevel();

        if (_resetLevels)
        {
            SceneManager.LoadScene("MenuScene");
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

        }
    }

    public static void NextLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;

        if (currentLevel >= PlayerPrefs.GetInt("LevelLock"))
        {
            PlayerPrefs.SetInt("LevelLock", currentLevel + 1);

            SceneManager.LoadScene(currentLevel + 1);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void ResetLevels()
    {
       
        PlayerPrefs.DeleteAll();
        _resetLevels=true;
        
    }

}
