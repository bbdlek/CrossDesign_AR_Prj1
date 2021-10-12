using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectBall : MonoBehaviour
{

    [SerializeField] ToggleGroup _toggleGroup;
    [SerializeField] List<Toggle> BallSelection;

    public int selected;

    //���õ� �� �迭 ��ȣ ����
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
