using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    public static class GameService
{
     public static event Action<string>? OnGameStateChanged;
    public static List<GameSession> LobbyGames { get; private set; } = new List<GameSession>();

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

     public static void NotifyGameStateChanged(string gameId)
    {
        OnGameStateChanged?.Invoke(gameId);
    }
}

}
