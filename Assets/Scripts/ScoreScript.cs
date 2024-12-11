using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class ScoreScript : MonoBehaviour
{

    [SerializeField] private PlayerScript player;
    private int score = 0;
    public TextMeshProUGUI scoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        score = player.score;
        scoreText.text = "Score: " + score;
    }
}
