# Leaderboard SDK for Unity

A simple SDK for integrating leaderboard functionality into Unity games, connecting to a Node.js/SQLite backend.

## Features

- Retrieve a player's high score from a leaderboard
- Retrieve an entire leaderboard with all players and scores
- Upload new players to the system
- Submit new scores to a leaderboard

## Installation

### Using Unity Package Manager (Git URL)

1. Open your Unity project
2. Open the Package Manager window (Window > Package Manager)
3. Click the "+" button in the top-left corner
4. Select "Add package from git URL..."
5. Enter your Git repository URL: `https://github.com/yourusername/LeaderboardSDK.git`
6. Click "Add"

### Manual Installation

1. Download the latest release
2. Extract the contents to your Unity project's Assets folder

## Server Setup

1. Install Node.js and npm
2. Install the required packages:
   ```
   npm install express sqlite3 body-parser
   ```
3. Run the server:
   ```
   node server.js
   ```

## Usage

```csharp
// Add LeaderboardManager component to a GameObject
LeaderboardManager leaderboardManager = gameObject.AddComponent<LeaderboardManager>();

// Initialize with your server URL
leaderboardManager.Initialize("http://localhost:3000");

// Get a leaderboard
StartCoroutine(leaderboardManager.GetLeaderboard(
    1, // leaderboardId
    100, // limit
    leaderboard => {
        Debug.Log($"Leaderboard loaded: {leaderboard.Name}");
        foreach (Player player in leaderboard.Players) {
            Debug.Log($"{player.Name}: {player.Score}");
        }
    },
    error => {
        Debug.LogError($"Error: {error}");
    }
));

// Submit a score
StartCoroutine(leaderboardManager.SubmitScore(
    1, // leaderboardId
    "PlayerName", // playerName
    1000, // score
    player => {
        Debug.Log($"Score submitted: {player.Score}");
    },
    error => {
        Debug.LogError($"Error: {error}");
    }
));
```

## License

This project is licensed under the MIT License - see the LICENSE.md file for details.