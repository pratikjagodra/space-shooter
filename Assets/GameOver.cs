using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] Text score;
    Text gameOver;

    void Start()
    {
        gameOver = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        gameOver.text = ("Game over" + "\nYour score is:- " + score.text + "\nPress \"ENTER\" to play again");
    }
}
