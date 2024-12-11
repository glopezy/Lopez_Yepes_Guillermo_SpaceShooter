using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class LifesScript : MonoBehaviour
{

    [SerializeField] private PlayerScript player;
    private int lifes = 0;
    public TextMeshProUGUI lifesText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        lifes = player.lifes;
        lifesText.text = "Health: " + lifes;
    }
}
