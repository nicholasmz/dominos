using System;

namespace Logic
{
    public class GameSession
    {
        public string SessionId { get; private set; }
        public Player Player1 { get; }
        public Player? Player2 { get; set; }
        public Player CurrentPlayer { get; private set; }
        public Game CurrentGame { get; }

        public event Action? OnChange;

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

            if (moveSuccessful)
            {
                if (CurrentGame.IsGameOver)
                {
                    RegisterWin(playerNumber);
                }
                else
                {
                    ToggleTurn();
                }
                NotifyStateChanged();
            }

            return moveSuccessful;
        }

        private void RegisterWin(int playerNumber)
        {
            var winningPlayer = playerNumber == 1 ? Player1 : Player2!;
            GameService.RegisterWin(winningPlayer);
        }

        private void ToggleTurn()
        {
            CurrentPlayer = CurrentPlayer == Player1 ? Player2 : Player1!;
        }

        public void NotifyStateChanged()
        {
            OnChange?.Invoke();
        }
    }
}
