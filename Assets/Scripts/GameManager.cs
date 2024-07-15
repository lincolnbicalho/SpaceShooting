using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameOverText;
    private Text _scoreText;
    private int _score = 0;
    private bool _isGameOver = false;
    void Start()
    {
        _scoreText = gameObject.GetComponentInChildren<Text>();
        _scoreText.text = "Score : " + _score;
    }

    private void Update()
    {
        if (_isGameOver)
            if (Input.GetKeyDown(KeyCode.R))
                SceneManager.LoadScene("Game");
    }

    public void SetScore(int score)
    {
        _score += score;
        _scoreText.text = "Score : " + _score;
    }

    public void GameOver() => StartCoroutine(ShowGameOver());

    IEnumerator ShowGameOver()
    {
        _isGameOver = true;
        while (true)
        {
            _gameOverText.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _gameOverText.SetActive(false);
            yield return new WaitForSeconds(0.4f);
        }
    }
}
