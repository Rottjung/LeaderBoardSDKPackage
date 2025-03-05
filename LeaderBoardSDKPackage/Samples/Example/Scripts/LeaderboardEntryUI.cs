// UI Component for leaderboard entries
using TMPro;
using UnityEngine;

public class LeaderboardEntryUI : MonoBehaviour
{
    [SerializeField] private TMP_Text rankText;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text scoreText;

    public void SetData(int rank, string playerName, int score)
    {
        rankText.text = rank.ToString();
        nameText.text = playerName;
        scoreText.text = score.ToString();
    }
}