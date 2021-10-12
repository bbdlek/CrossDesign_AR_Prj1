using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class BallManager : MonoBehaviour
{
    public ARRaycastManager arRaycaster;

    SelectBall _selectBallManager;
    [SerializeField] List<GameObject> Ball;
    GameObject _ball;

    private Vector2 touchBeganPos;
    public float swipe_x;
    public float swipe_y;

    // Start is called before the first frame update
    void Start()
    {
        _selectBallManager = GetComponent<SelectBall>();
    }

    void Update()
    {
        //���� ���� �Ǿ�����, ��뿡 �ʹ� ������� Ȯ�� �� ������ ����
        if (_ball && !_ball.GetComponent<CheckBall>().isTooClose)
            Throwing();
    }

    //�������� ����/�� �� �� ���� �� ����
    public void SpawnBall()
    {
        _ball = Instantiate(Ball[_selectBallManager.CheckSelected()], Camera.current.transform.position + Camera.current.transform.forward * 0.2f, Quaternion.identity);
    }

    public void DestroyBall()
    {
        Destroy(_ball);
    }

    //������
    public void Throwing()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchBeganPos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Vector2 touchEndedPos = Input.mousePosition;
            swipe_x = touchEndedPos.x - touchBeganPos.x;
            swipe_y = touchEndedPos.y - touchBeganPos.y;
            _ball.GetComponent<CheckBall>().ThrowBall(swipe_x, swipe_y);
            Destroy(_ball, 4f);
            Invoke("SpawnBall", 0.1f);
        }
    }

}
