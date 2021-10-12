using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectBall : MonoBehaviour
{

    [SerializeField] ToggleGroup _toggleGroup;
    [SerializeField] List<Toggle> BallSelection;

    public int selected;

    //선택된 공 배열 번호 리턴
    public int CheckSelected()
    {
        for(int i = 0; i < BallSelection.Count; i++)
        {
            if (BallSelection[i].isOn)
                selected = i;
        }
        return selected;
    }
}
