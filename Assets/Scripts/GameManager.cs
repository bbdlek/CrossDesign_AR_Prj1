using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class GameManager : MonoBehaviour
{
    public GameState currentGameState;

    //메뉴 리스트
    public enum GameState
    {
        mainMenu,
        SelectBall,
        Stage1,
        Stage2,
        Stage3,
        EndMenu,
        FailMenu
    }

    ScoreManager _scoreManager;
    BallManager _ballManager;

    [SerializeField] ARPlaneManager arPlaneManager;

    [SerializeField] GameObject[] UIObjects;

    [SerializeField] GameObject _stageStartTimer;

    [SerializeField] SpawnField _spawnField;


    private void Awake()
    {
        LoadHighScore();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentGameState = GameState.mainMenu;

        _scoreManager = GetComponent<ScoreManager>();
        _ballManager = GetComponent<BallManager>();
    }

    //다음 스테이트로 이동
    public void NextState()
    {
        if (currentGameState != GameState.Stage3)
            currentGameState += 1;
        else
            currentGameState = GameState.EndMenu;

        CheckState();
    }

    //정해진 스테이트로 이동
    public void SetState(GameState _stageName)
    {
        currentGameState = _stageName;

        CheckState();
    }

    //스테이트에 맞는 동작
    void CheckState()
    {
        if (currentGameState == GameState.mainMenu)
        {
            arPlaneManager.enabled = true;
            _scoreManager.ClearScore();
            _ballManager.DestroyBall();
            
        }
        else if(currentGameState == GameState.SelectBall)
        {
            foreach(var plane in arPlaneManager.trackables)
            {
                Destroy(plane.gameObject);
            }
            arPlaneManager.enabled = false;
            UIObjects[0].SetActive(false);
            UIObjects[1].SetActive(true);
        }
        else if(currentGameState == GameState.Stage1)
        {
            UIObjects[1].SetActive(false);
            _stageStartTimer.SetActive(true);
            UIObjects[2].SetActive(true);
        }
        else if (currentGameState == GameState.Stage2)
        {
            _ballManager.DestroyBall();
            _stageStartTimer.SetActive(true);
        }
        else if (currentGameState == GameState.Stage3)
        {
            _ballManager.DestroyBall();
            _stageStartTimer.SetActive(true);
        }
        else if (currentGameState == GameState.EndMenu)
        {
            _ballManager.DestroyBall();
            UIObjects[2].SetActive(false);
            UIObjects[3].SetActive(true);
        }
        else if (currentGameState == GameState.FailMenu)
        {
            _ballManager.DestroyBall();
            UIObjects[2].SetActive(false);
            UIObjects[4].SetActive(true);
        }

    }

    public void GoMain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Retry()
    {

        for (int i = 0; i < UIObjects.Length; i++)
        {
            UIObjects[i].SetActive(false);
        }
        GetComponent<ScoreManager>().ClearScore();
        _spawnField.ResetBasketPosition();
        SetState(GameState.Stage1);
    }

    public void LoadHighScore()
    {
        if (!PlayerPrefs.HasKey("HighScore"))
            PlayerPrefs.SetInt("HighScore", 0);
    }

}
