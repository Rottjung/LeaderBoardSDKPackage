using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LeaderboardSDK;
using TMPro;
using System.Runtime.InteropServices;
using System;
using UnityEngine.SocialPlatforms.Impl;

public class LeaderboardExample : MonoBehaviour
{
    [SerializeField] private string serverUrl = "http://localhost:3000";
    [SerializeField] private int leaderboardId = 1;
    [SerializeField] private string leaderboardName;
    [SerializeField] private TMP_InputField playerNameInput;
    [SerializeField] private TMP_InputField scoreInput;
    [SerializeField] private TMP_InputField leaderboardIDInput;
    [SerializeField] private TMP_InputField leaderboardNameInput;
    [SerializeField] private Button submitButton;
    [SerializeField] private Button retrieveButton;
    [SerializeField] private Button createButton;
    [SerializeField] private Transform leaderboardContainer;
    [SerializeField] private GameObject leaderboardEntryPrefab;

    //[DllImport("LeaderboardSDK")]
    //public static extern void Initialize(string serverUrl);

    //[DllImport("LeaderboardSDK.dll")]
    //public static extern IEnumerator GetPlayerHighScore(int leaderboardId, string playerName, Action<Player> callback, Action<string> errorCallback);

    //[DllImport("LeaderboardSDK.dll")]
    //public static extern IEnumerator GetLeaderboard(int leaderboardId, int limit, Action<LeaderboardSDK.Leaderboard> callback, Action<string> errorCallback);

    //[DllImport("LeaderboardSDK.dll")]
    //public static extern IEnumerator SubmitScore(int leaderboardId, string playerName, int score, Action<Player> callback, Action<string> errorCallback);

    LeaderboardManager leaderboardManager;

    void Start()
    {
        leaderboardManager = gameObject.AddComponent<LeaderboardManager>();
        // Initialize the LeaderboardManager
        leaderboardManager.Initialize(serverUrl);
        
        // Set up button events
        submitButton.onClick.AddListener(SubmitScore);
        retrieveButton.onClick.AddListener(RefreshLeaderboard);
        createButton.onClick.AddListener(CreateLeaderboard);
        leaderboardIDInput.onEndEdit.AddListener(SetID);
        leaderboardNameInput.onEndEdit.AddListener(SetName);

        CheckForLeaderboard();
        // Load leaderboard on start
        RefreshLeaderboard();
    }

    private void SetName(string name)
    {
        leaderboardName = name;
    }

    private void SetID(string id)
    {
        leaderboardId = Int32.Parse(id);
    }

    private void CreateLeaderboard()
    {
        CheckForLeaderboard();
    }

    void CheckForLeaderboard()
    {
        StartCoroutine(leaderboardManager.CreateOrGetLeaderboard(leaderboardId, leaderboardName,
            leaderboard => {
                Debug.Log($"Leaderboard loaded: {leaderboard.Name}");

                //// Add entries to UI
                //int rank = 1;
                //foreach (Player player in leaderboard.Players)
                //{
                //    GameObject entryObj = Instantiate(leaderboardEntryPrefab, leaderboardContainer);
                //    LeaderboardEntryUI entry = entryObj.GetComponent<LeaderboardEntryUI>();
                //    entry.SetData(rank, player.Name, player.Score);
                //    rank++;
                //}
            },
            error => {
                Debug.LogError($"Error loading leaderboard: {error}");
            }
        ));
    }

    void SubmitScore()
    {
        CheckForLeaderboard();

        string playerName = playerNameInput.text;
        
        if (string.IsNullOrEmpty(playerName))
        {
            Debug.LogError("Player name cannot be empty");
            return;
        }
        
        if (!int.TryParse(scoreInput.text, out int score))
        {
            Debug.LogError("Invalid score value");
            return;
        }
        
        StartCoroutine(leaderboardManager.SubmitScore(
            leaderboardId,
            playerName,
            score,
            player => {
                Debug.Log($"Score submitted: {player.Name} - {player.Score}");
                RefreshLeaderboard();
            },
            error => {
                Debug.LogError($"Error submitting score: {error}");
            }
        ));
    }
    
    void RefreshLeaderboard()
    {
        // Clear existing entries
        foreach (Transform child in leaderboardContainer)
        {
            Destroy(child.gameObject);
        }
        
        StartCoroutine(leaderboardManager.GetLeaderboard(
            leaderboardId,
            100,
            leaderboard => {
                Debug.Log($"Leaderboard loaded: {leaderboard.Name}");
                
                // Add entries to UI
                int rank = 1;
                foreach (Player player in leaderboard.Players)
                {
                    GameObject entryObj = Instantiate(leaderboardEntryPrefab, leaderboardContainer);
                    LeaderboardEntryUI entry = entryObj.GetComponent<LeaderboardEntryUI>();
                    entry.SetData(rank, player.Name, player.Score);
                    rank++;
                }
            },
            error => {
                Debug.LogError($"Error loading leaderboard: {error}");
            }
        ));
    }
    
    public void GetPlayerHighScore(string playerName)
    {
        StartCoroutine(leaderboardManager.GetPlayerHighScore(
            leaderboardId,
            playerName,
            player => {
                Debug.Log($"Player high score: {player.Name} - {player.Score}");
            },
            error => {
                Debug.LogError($"Error getting player high score: {error}");
            }
        ));
    }
}

