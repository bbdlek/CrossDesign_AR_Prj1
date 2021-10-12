using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class SpawnField : MonoBehaviour
{

    public ARRaycastManager arRaycaster;
    public GameObject _basketField;

    GameManager _gameManager;

    GameObject BasketField;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) && _gameManager.currentGameState == GameManager.GameState.mainMenu)
            SpawnBasketField();
    }

    //농구 골대 소환
    private void SpawnBasketField()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            if (arRaycaster.Raycast(touch.position, hits, TrackableType.Planes))
            {
                Pose hitPose = hits[0].pose;

                if (!BasketField)
                {
                    BasketField = Instantiate(_basketField, hitPose.position, hitPose.rotation);
                }
                else
                {
                    BasketField.transform.position = hitPose.position;
                    BasketField.transform.rotation = hitPose.rotation;
                }

            }
        }
    }

    //골대 생성되어 있는지 확인(버튼에서 호출)
    public void CheckBuilt()
    {
        if (BasketField)
        {
            _gameManager.SetState(GameManager.GameState.SelectBall);
            //gameObject.SetActive(false);
        }
        else
            return;
            
    }

    public void DestroyBasket()
    {
        Destroy(BasketField);
    }

    public void ResetBasketPosition()
    {
        BasketField.GetComponentInChildren<BasketMover>().ResetPosition();
    }

}
