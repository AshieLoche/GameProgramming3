using TMPro;
using UnityEngine;

public class Week12_UIManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _scoreBoard;

    private void Awake()
    {
        Week12_CollectionController.OnAddCoin.AddListener(DisplayScore);
    }

    private void DisplayScore(int score)
    {
        _scoreBoard.text = $"Coins: {score}";
    }

}