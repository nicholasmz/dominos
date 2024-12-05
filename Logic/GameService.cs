using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    public static class GameService
    {
        public static event Action<string>? OnGameStateChanged;
        public static List<GameSession> LobbyGames { get; private set; } = new List<GameSession>();
        public static List<HighScore> HighScores { get; private set; } = new List<HighScore>();

        public static GameSession CreateGame(string hostPlayerName)
        {
            var host = new Player(Guid.NewGuid().ToString(), hostPlayerName);
            var session = new GameSession(host, null);
            LobbyGames.Add(session);
            return session;
        }

        public static GameSession? GetGameSession(string gameId)
        {
            return LobbyGames.FirstOrDefault(g => g.SessionId == gameId);
        }

        public static GameSession? JoinGame(string gameId, string playerName)
        {
            var session = GetGameSession(gameId);
            if (session != null && session.Player2 == null)
            {
                session.Player2 = new Player(Guid.NewGuid().ToString(), playerName);
            }
            return session;
        }

        public static void RegisterWin(Player player)
        {
            var existingPlayer = HighScores.FirstOrDefault(h => h.PlayerName == player.Name);
            if (existingPlayer != null)
            {
                existingPlayer.Score += 1;
            }
            else
            {
                HighScores.Add(new HighScore { PlayerName = player.Name, Score = 1 });
            }

            HighScores = HighScores.OrderByDescending(h => h.Score).ToList();
        }

        public static void NotifyGameStateChanged(string gameId)
        {
            OnGameStateChanged?.Invoke(gameId);
        }
    }

    public class HighScore
    {
        public string PlayerName { get; set; } = string.Empty;
        public int Score { get; set; }
    }
}
