using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VanCollisions : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI candyText;
    public TextMeshProUGUI kidzText;

    public AudioSource soundPlayerRecharge;
    public AudioSource soundPlayerPickup;
    public AudioSource soundPlayerDropOff;

    public KidzSpawner kidzSpawner;

    public int score = 0;
    public int candyAmount = 8;
    public int kidzAmount = 0;

    private int _scoreperkidz = 10;

    private void OnTriggerEnter(Collider col) {

        if (col.gameObject.tag == "kid") {
            kidzAmount += 1;
            kidzSpawner.totalKidz -= 1;
            soundPlayerPickup.Play();
            Destroy(col.gameObject);
        } else if (col.gameObject.tag == "recharge") {
            candyAmount = 10;
            soundPlayerRecharge.Play();
        } else if (col.gameObject.tag == "dropoff" && kidzAmount > 0) {
            score += _scoreperkidz * kidzAmount;
            soundPlayerDropOff.Play();
            kidzAmount = 0;
        }
    }

    void Update()
    {
        candyText.text = $": {candyAmount.ToString()}";
        kidzText.text = $": {kidzAmount.ToString()}";
        scoreText.text = $"Score: {score.ToString()}";
    }
}
 