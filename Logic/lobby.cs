using Logic;

public class Lobby
{
    public static List<Game> Games {get;} = new List<Game>;
    public static event Action? GameListChanged;
}