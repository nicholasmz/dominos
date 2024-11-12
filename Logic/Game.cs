namespace Logic
{
    public class Game
    {
        public int[,] Board { get; private set; } = new int[6, 7];
        public string GameOverMessage { get; private set; } = string.Empty;
        public bool IsGameOver => !string.IsNullOrEmpty(GameOverMessage);

        public event Action OnChange;

        public Game()
        {
            Reset();
        }

        public void Reset()
        {
            Board = new int[6, 7];
            GameOverMessage = string.Empty;
            NotifyStateChanged();
        }

        public bool MakeMove(int player, int column)
        {
            if (IsGameOver || column < 0 || column >= 7)
                return false;

            for (int row = 5; row >= 0; row--)
            {
                if (Board[row, column] == 0)
                {
                    Board[row, column] = player;
                    if (CheckWinCondition(row, column, player))
                    {
                        GameOverMessage = $"Player {player} wins!";
                    }
                    NotifyStateChanged();
                    return true;
                }
            }

            return false; 
        }

        private bool CheckWinCondition(int row, int column, int player)
        {
            return CheckDirection(row, column, player, 1, 0) || 
                   CheckDirection(row, column, player, 0, 1) || 
                   CheckDirection(row, column, player, 1, 1) || 
                   CheckDirection(row, column, player, 1, -1); 
        }

        private bool CheckDirection(int row, int column, int player, int rowDelta, int colDelta)
        {
            int count = 1;
            count += CountConsecutive(row, column, player, rowDelta, colDelta);
            count += CountConsecutive(row, column, player, -rowDelta, -colDelta);
            return count >= 4;
        }

        private int CountConsecutive(int row, int col, int player, int rowDelta, int colDelta)
        {
            int count = 0;

            row += rowDelta;
            col += colDelta;

            while (row >= 0 && row < 6 && col >= 0 && col < 7 && Board[row, col] == player)
            {
                count++;
                row += rowDelta;
                col += colDelta;
            }

            return count;
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
