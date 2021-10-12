using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBall : MonoBehaviour
{

    //Score
    ScoreManager _scoreManager;
    public bool isEntered = false;

    //Gameplay
    Rigidbody rigid;
    public bool isThrowing = false;
    public bool isTooClose = false;

    //Debug
    [SerializeField] GameObject _text;

    //Audio
    AudioSource _audio;
    [SerializeField] AudioClip[] _clip;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        _scoreManager = GameObject.Find("GameManager").GetComponent<ScoreManager>();
        _audio = GetComponent<AudioSource>();

        rigid.isKinematic = true;

        //Debug
        _text = GameObject.Find("DistanceTxt");
    }

    // Update is called once per frame
    void Update()
    {
        if(!isThrowing)
        {
            transform.position = Camera.current.transform.position + Camera.current.transform.forward * 0.2f;
            transform.rotation = Quaternion.identity;
        }

        if (isTooClose)
        {
            _text.SetActive(true);
        }
        else
        {
            _text.SetActive(false);
        }


    }

    //��븦 ����Ͽ����� �˻�
    private void OnTriggerEnter(Collider other)
    {
        if (!isEntered)
        {
            //��� ���κ�
            if (other.tag == "1stTrigger")
            {
                isEntered = true;
            }
        }
        else
        {
            //��� �Ʒ��κ�
            if (other.tag == "2ndTrigger")
            {
                //����
                _scoreManager.AddScore();
                isEntered = false;
            }


            //�Ÿ� üũ
            if(other.tag == "DistanceTrigger" && !isThrowing)
            {
                isTooClose = true;
            }
        }
    }

    //�Ÿ� üũ
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "DistanceTrigger" && !isThrowing)
        {
            isTooClose = true;
        }
    }

    //�Ÿ� üũ
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "DistanceTrigger" && !isThrowing)
        {
            isTooClose = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "BackBoard")
        {
            _audio.PlayOneShot(_clip[0]);
        }
        else if(collision.transform.tag == "Rim")
        {
            _audio.PlayOneShot(_clip[1]);
        }
    }

    //�� ������ ���� ����
    public void ThrowBall(float swipe_x, float swipe_y)
    {
        isThrowing = true;
        rigid = GetComponent<Rigidbody>();
        rigid.isKinematic = false;
        rigid.AddForce(Camera.current.transform.forward * swipe_y / 6f);
        rigid.AddForce(Camera.current.transform.up * swipe_y / 6f);
        rigid.AddForce(Camera.current.transform.right * swipe_x / 10f);

        Destroy(this, 3f);
    }
}
