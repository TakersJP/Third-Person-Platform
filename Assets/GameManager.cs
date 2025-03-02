using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
   [SerializeField] private float score = 0;
   [SerializeField] private TextMeshProUGUI scoreText;

   private void Start(){
       scoreText.text = "Score: " + score;
   }

   private void IncrementScore(){
       score++;
       scoreText.text = "Score: " + score;
   }
}
