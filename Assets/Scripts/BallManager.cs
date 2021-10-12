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
        //공이 생성 되었는지, 골대에 너무 가까운지 확인 후 던지기 가능
        if (_ball && !_ball.GetComponent<CheckBall>().isTooClose)
            Throwing();
    }

    //스테이지 시작/끝 시 공 스폰 및 삭제
    public void SpawnBall()
    {
        _ball = Instantiate(Ball[_selectBallManager.CheckSelected()], Camera.current.transform.position + Camera.current.transform.forward * 0.2f, Quaternion.identity);
    }

    public void DestroyBall()
    {
        Destroy(_ball);
    }

    //던지기
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
