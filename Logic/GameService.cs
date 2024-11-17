using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    public static class GameService
    {
        public static List<GameSession> LobbyGames { get; private set; } = new List<GameSession>();

        public static GameSession CreateGame(string hostPlayerName)
        {
            var host = new Player(System.Guid.NewGuid().ToString(), hostPlayerName);
            var session = new GameSession(host, null);
            LobbyGames.Add(session);
            return session;
        }

        public static GameSession JoinGame(string gameId, string playerName)
        {
            var session = LobbyGames.FirstOrDefault(g => g.SessionId == gameId);
            if (session != null && session.Player2 == null)
            {
                session.Player2 = new Player(System.Guid.NewGuid().ToString(), playerName);
            }
            return session!;
        }

        public static void StartGame(string gameId)
        {
            var session = LobbyGames.FirstOrDefault(g => g.SessionId == gameId);
            if (session != null && session.Player2 != null)
            {
                LobbyGames.Remove(session);
            }
        }
    }
}
