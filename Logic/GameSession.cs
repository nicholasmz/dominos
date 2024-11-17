namespace Logic
{
    public class GameSession
    {
        public string SessionId { get; private set; }
        public Player Player1 { get; }
        public Player? Player2 { get; set; }
        public Player CurrentPlayer { get; private set; }
        public Game CurrentGame { get; }

        public GameSession(Player player1, Player? player2)
        {
            SessionId = Guid.NewGuid().ToString();
            Player1 = player1;
            Player2 = player2;
            CurrentPlayer = Player1;
            CurrentGame = new Game();
        }

        public bool MakeMove(int column)
        {
            int playerNumber = CurrentPlayer == Player1 ? 1 : 2;
            bool moveSuccessful = CurrentGame.MakeMove(playerNumber, column);
            if (moveSuccessful && !CurrentGame.IsGameOver)
            {
                ToggleTurn();
            }
            return moveSuccessful;
        }

        private void ToggleTurn()
        {
            CurrentPlayer = CurrentPlayer == Player1 ? Player2! : Player1;
        }
    }
}
