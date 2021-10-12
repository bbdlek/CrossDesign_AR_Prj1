using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    GameManager _gameManager;

    [SerializeField] int _currentScore = 0;
    [SerializeField] int[] _targetScore = { 50, 150, 10000 };
    [SerializeField] int[] _goalScore = { 2, 3 };
    [SerializeField] float[] _time = { 60f, 50f, 50f };


    int targetScore = 0;
    public float _timeLeft;
    int goalScore;

    //UI
    [SerializeField] GameObject _timetxt;
    [SerializeField] GameObject _stagetxt;
    [SerializeField] GameObject _scoretxt;
    [SerializeField] GameObject _endScoretxt;

    private void Awake()
    {
        _gameManager = GetComponent<GameManager>();
    }

    //���ھ� �ʱ�ȭ
    public void ClearScore()
    {
        _currentScore = 0;
    }

    //�������� ���� �� ��ǥ ����, ���� �ð� ���� �� Ÿ�̸� ����
    public void EnterStage()
    {
        _timetxt.SetActive(true);
        switch (_gameManager.currentGameState)
        {
            case GameManager.GameState.Stage1:
                Debug.Log(_time[0]);
                targetScore = _targetScore[0];
                _timeLeft = _time[0];
                StartCoroutine(TimeCal());
                break;
            case GameManager.GameState.Stage2:
                Debug.Log(_time[1]);
                targetScore = _targetScore[1];
                _timeLeft = _time[1];
                StartCoroutine(TimeCal());
                break;
            case GameManager.GameState.Stage3:
                Debug.Log(_time[2]);
                targetScore = _targetScore[2];
                _timeLeft = _time[2];
                StartCoroutine(TimeCal());
                break;
        }
    }

    private void Update()
    {
        //TimeSys
        CheckGoalScore();

        //SetUI
        SetUI();
    }

    //Ÿ�̸�
    IEnumerator TimeCal()
    {
        while(_timeLeft >= 0)
        {
            _timeLeft -= Time.deltaTime;
            yield return null;
        }
        CheckScore();
    }

    //�������� ���� �� ���� üũ
    private void CheckScore()
    {
        SaveHighScore();
        _timetxt.SetActive(false);
        if (_gameManager.currentGameState == GameManager.GameState.Stage3)
        {
            _gameManager.SetState(GameManager.GameState.EndMenu);
            return;
        }

        if (_currentScore < targetScore)
        {
            _gameManager.SetState(GameManager.GameState.FailMenu);
        }
        else
        {
            _gameManager.NextState();
        }
    }

    //15�� ���� �� �� ���� ����
    private void CheckGoalScore()
    {
        if (_timeLeft < 15f)
            goalScore = _goalScore[1];
        else
            goalScore = _goalScore[0];
    }

    //���� Plus
    public void AddScore()
    {
        _currentScore += goalScore;
    }

    //Stage �ܰ�, �ð�, ���ھ� UI ������Ʈ
    public void SetUI()
    {
        if(_stagetxt)
            _stagetxt.GetComponent<Text>().text = "Stage" + ((int)_gameManager.currentGameState - 1);
        if(_timetxt)
            _timetxt.GetComponent<Text>().text = "Time\n" + Mathf.Round(_timeLeft).ToString();
        if (_scoretxt)
            _scoretxt.GetComponent<Text>().text = "Score\n" + _currentScore;
        if (_endScoretxt)
            _endScoretxt.GetComponent<Text>().text = _currentScore.ToString();
    }

    public void SaveHighScore()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            if(_currentScore > PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", _currentScore);
            }
        }
        PlayerPrefs.Save();
    }
}
