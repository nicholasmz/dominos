namespace Logic;

public class Game
{
    public GameStatus Status { get ; private set;}
    public string Name {get ; set;}
    public int NumberOfPlayers {get; set;}
    public Game()
    {
        Status = GameStatus.NOT_STARTED;
    }
}
