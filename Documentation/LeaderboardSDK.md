# Leaderboard SDK Documentation

## Overview

The Leaderboard SDK provides a simple way to integrate online leaderboard functionality into your Unity game. It connects to a Node.js server with a SQLite database to store and retrieve player scores.

## Components

### LeaderboardClient

A low-level client that makes HTTP requests to the server.

#### Methods

- `GetPlayerHighScore(int leaderboardId, string playerName)`: Gets a specific player's high score
- `GetLeaderboard(int leaderboardId, int limit = 100)`: Gets an entire leaderboard with a limit on results
- `SubmitScore(int leaderboardId, string playerName, int score)`: Submits a new score for a player
- `CreatePlayer(string playerName)`: Creates a new player in the system

### LeaderboardManager

A MonoBehaviour wrapper around the LeaderboardClient that handles coroutines and callbacks.

#### Methods

- `Initialize(string serverUrl)`: Sets up the manager with the server URL
- `GetPlayerHighScore(int leaderboardId, string playerName, Action<Player> callback, Action<string> errorCallback)`: Gets a player's high score
- `GetLeaderboard(int leaderboardId, int limit, Action<Leaderboard> callback, Action<string> errorCallback)`: Gets an entire leaderboard
- `SubmitScore(int leaderboardId, string playerName, int score, Action<Player> callback, Action<string> errorCallback)`: Submits a score
- `CreatePlayer(string playerName, Action<Player> callback, Action<string> errorCallback)`: Creates a new player

## Data Models

### Player

```csharp
public class Player
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Score { get; set; }
    public DateTime Timestamp { get; set; }
}
```

### Leaderboard

```csharp
public class Leaderboard
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Player> Players { get; set; }
}
```

## Server API Endpoints

- `GET /leaderboards/:id`: Get a leaderboard by ID
- `GET /leaderboards/:id/players/:name`: Get a player's high score in a leaderboard
- `POST /leaderboards/:id/scores`: Submit a new score
- `POST /players`: Create a new player
- `POST /leaderboards`: Create a new leaderboard

## Example Implementation

See the sample scene in `Samples~/Example` for a full implementation example.

## Troubleshooting

### Common Issues

1. **Connection refused errors**: Ensure your server is running and accessible from Unity
2. **CORS errors**: If testing in the Unity Editor, you may need to configure CORS in your server
3. **Player not found**: Ensure the player exists before trying to get their high score

### Debugging

- Enable debug logs by setting the Unity console log level to "Debug"
- Check server logs for any API errors