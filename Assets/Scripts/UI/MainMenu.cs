using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public Transform SceneButtonsParent;

    public Fun_Button ButtonTemplate;
    public List<LevelData> Levels;

    private List<Fun_Button> buttons;

    IEnumerator Start()
    {
        //load all buttons from the scene list
        buttons = new List<Fun_Button>();
        foreach (LevelData level in Levels)
        {
            Fun_Button newButton = GameObject.Instantiate(ButtonTemplate, SceneButtonsParent);
            newButton.button.onClick.AddListener(() => LevelData.LoadLevel(level));
            newButton.label.text = level.DisplayName;
            newButton.gameObject.SetActive(false);
            buttons.Add(newButton);
        }
        ButtonTemplate.gameObject.SetActive(false);

        //show buttons one at a time
        foreach(Fun_Button button in buttons)
        {
            button.gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
        }
    }
}
