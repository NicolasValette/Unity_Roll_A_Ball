using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    
    [SerializeField]
    private TMP_Text _scoreText;
    [SerializeField]
    private Image _fadeScreen;
    private Rigidbody _rigidbody;
    private int _scoreValue;
    private bool isJumping = false;
    private bool _isWin = false;
    

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody= GetComponent<Rigidbody>();
        _scoreValue = 0;
        _scoreText.text = "Score : " + _scoreValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isWin)
        {
            Debug.Log("WIN");
            UnityEngine.Color color = _fadeScreen.color;

            color.a = Mathf.Lerp(_fadeScreen.color.a, 1f, Time.deltaTime);

            _fadeScreen.color = color;

        }
        else
        {


            if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
            {
                _rigidbody.AddForce(Input.GetAxis("Horizontal") * 0.5f, 0f, Input.GetAxis("Vertical") * 0.5f);
            }

            if (!isJumping && Input.GetAxis("Jump") != 0)
            {
                Debug.Log("Saut : " + isJumping);
                isJumping = true;
                _rigidbody.AddForce(0f, Input.GetAxis("Jump") * 200f, 0f);
            }

            //if (Input.GetKey(KeyCode.UpArrow))
            //{
            //    _rigidbody.AddForce(0f, 0f, 0.5f);
            //}
            //else if (Input.GetKey(KeyCode.DownArrow))
            //{
            //    _rigidbody.AddForce(0f, 0f, -0.5f);
            //}
            //if (Input.GetKey(KeyCode.RightArrow))
            //{
            //    _rigidbody.AddForce(0.5f, 0f, 0f);
            //}
            //else if (Input.GetKey(KeyCode.LeftArrow))
            //{
            //    _rigidbody.AddForce(-0.5f, 0f, 0f);
            //}

        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Target_Trigger"))
        {
            Destroy (other.gameObject);
            _scoreValue++;
            _scoreText.text = "Score : " + _scoreValue;
        }

        if (other.gameObject.CompareTag("Win") && _scoreValue == 1)
        {
            Debug.Log("Score = " + _scoreValue);
            _scoreText.text = "Win";
            _isWin = true;
                
        }

        //if (other.GetComponent<Target>() != null)       // Si le game object a un script attaché
        //{

        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            Destroy(collision.gameObject);
            _scoreValue++;
            _scoreText.text = "Score : " + _scoreValue;
        }
        if (collision.gameObject.CompareTag("Ground") && isJumping)
        {
            isJumping = false;

        }

        
    }
}
