using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseLevelButton : MonoBehaviour
{
    [SerializeField]
    private Text buttonName;
    [SerializeField]
    private Text buttonStar;

    private LevelInfo levelInfo;

    public void Init(LevelInfo _levelInfo)
    {
        levelInfo = _levelInfo;
        buttonName.text = levelInfo.levelName;
        buttonStar.text = GameManager.GetStartByCount(levelInfo.star);
        GetComponent<Button>().onClick.AddListener(EnterLevelButton);
    }



    void EnterLevelButton()
    {
        GameManager.SetNowLevel(levelInfo);
        SceneChange.Instance.Load(levelInfo.sceneName);
    }
}
