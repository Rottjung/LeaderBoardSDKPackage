using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace LeaderboardSDK
{
    /// <summary>
    /// Unity MonoBehaviour wrapper for LeaderboardClient to handle coroutines
    /// </summary>
    public class LeaderboardManager : MonoBehaviour
    {
        private LeaderboardClient _client;

        public void Initialize(string serverUrl)
        {
            _client = new LeaderboardClient(serverUrl);
        }

        public IEnumerator GetPlayerHighScore(int leaderboardId, string playerName, Action<Player> callback, Action<string> errorCallback)
        {
            Task<Player> task = _client.GetPlayerHighScore(leaderboardId, playerName);

            while (!task.IsCompleted)
                yield return null;

            if (task.IsFaulted)
            {
                errorCallback?.Invoke(task.Exception.InnerException.Message);
            }
            else
            {
                callback?.Invoke(task.Result);
            }
        }

        public IEnumerator GetLeaderboard(int leaderboardId, int limit, Action<Leaderboard> callback, Action<string> errorCallback)
        {
            Task<Leaderboard> task = _client.GetLeaderboard(leaderboardId, limit);

            while (!task.IsCompleted)
                yield return null;

            if (task.IsFaulted)
            {
                errorCallback?.Invoke(task.Exception.InnerException.Message);
            }
            else
            {
                callback?.Invoke(task.Result);
            }
        }

        public IEnumerator SubmitScore(int leaderboardId, string playerName, int score, Action<Player> callback, Action<string> errorCallback)
        {
            Task<Player> task = _client.SubmitScore(leaderboardId, playerName, score);

            while (!task.IsCompleted)
                yield return null;

            if (task.IsFaulted)
            {
                errorCallback?.Invoke(task.Exception.InnerException.Message);
            }
            else
            {
                callback?.Invoke(task.Result);
            }
        }

        public IEnumerator CreatePlayer(string playerName, Action<Player> callback, Action<string> errorCallback)
        {
            Task<Player> task = _client.CreatePlayer(playerName);

            while (!task.IsCompleted)
                yield return null;

            if (task.IsFaulted)
            {
                errorCallback?.Invoke(task.Exception.InnerException.Message);
            }
            else
            {
                callback?.Invoke(task.Result);
            }
        }

        public IEnumerator CreateOrGetLeaderboard(int leaderboardId, string leaderboardName, Action<Leaderboard> callback, Action<string> errorCallback)
        {
            Task<Leaderboard> task = _client.CreateOrGetLeaderboard(leaderboardId, leaderboardName);

            while (!task.IsCompleted)
                yield return null;

            if (task.IsFaulted)
            {
                errorCallback?.Invoke(task.Exception.InnerException.Message);
            }
            else
            {
                callback?.Invoke(task.Result);
            }
        }
    }
}
