using FluentAssertions;
using Logic;

namespace Tests
{
    public class GameTests
    {
        // REQ#1.1.1 - Validate placing a token in a valid column
        [Fact]
        public void MakeMove_ValidMove_ShouldPlaceToken()
        {
            var game = new Game();
            bool result = game.MakeMove(1, 3);
            result.Should().BeTrue();
            game.Board[5, 3].Should().Be(1);
        }

        // REQ#1.1.2 - Validate move rejection for an invalid column
        [Fact]
        public void MakeMove_InvalidColumn_ShouldReturnFalse()
        {
            var game = new Game();
            bool result = game.MakeMove(1, 10); // Invalid column
            result.Should().BeFalse();
        }

        // REQ#1.1.2 - Validate move rejection when column is full
        [Fact]
        public void MakeMove_FullColumn_ShouldReturnFalse()
        {
            var game = new Game();
            for (int i = 0; i < 6; i++) game.MakeMove(1, 3);
            bool result = game.MakeMove(2, 3); // Column is full
            result.Should().BeFalse();
        }

        // REQ#1.2.1 - Validate horizontal win condition
        [Fact]
        public void CheckWinCondition_HorizontalWin_ShouldReturnTrue()
        {
            var game = new Game();
            game.MakeMove(1, 0);
            game.MakeMove(1, 1);
            game.MakeMove(1, 2);
            game.MakeMove(1, 3);
            game.IsGameOver.Should().BeTrue();
            game.GameOverMessage.Should().Be("Player 1 wins!");
        }

        // REQ#1.2.1 - Validate vertical win condition
        [Fact]
        public void CheckWinCondition_VerticalWin_ShouldReturnTrue()
        {
            var game = new Game();
            for (int i = 0; i < 4; i++) game.MakeMove(1, 3);
            game.IsGameOver.Should().BeTrue();
            game.GameOverMessage.Should().Be("Player 1 wins!");
        }

        // REQ#1.2.1 - Validate diagonal win condition
        [Fact]
        public void CheckWinCondition_DiagonalWin_ShouldReturnTrue()
        {
            var game = new Game();
            game.MakeMove(1, 0);
            game.MakeMove(2, 1);
            game.MakeMove(1, 1);
            game.MakeMove(2, 2);
            game.MakeMove(1, 2);
            game.MakeMove(2, 3);
            game.MakeMove(1, 2);
            game.MakeMove(1, 3);
            game.MakeMove(1, 3);
            game.MakeMove(1, 3);
            game.IsGameOver.Should().BeTrue();
            game.GameOverMessage.Should().Be("Player 1 wins!");
        }

        // REQ#1.1.3 - Validate resetting the game board
        [Fact]
        public void Reset_ShouldClearBoardAndResetState()
        {
            var game = new Game();
            game.MakeMove(1, 3);
            game.Reset();
            game.Board.Cast<int>().All(cell => cell == 0).Should().BeTrue();
            game.IsGameOver.Should().BeFalse();
        }

        // REQ#1.3.1 - Validate game session creation
        [Fact]
        public void CreateGameSession_ShouldInitializeSession()
        {
            var player1 = new Player("1", "Player1");
            var session = new GameSession(player1, null);
            session.Player1.Should().Be(player1);
            session.Player2.Should().BeNull();
        }

        // REQ#1.3.2 - Validate player joining a session
        [Fact]
        public void JoinGameSession_ShouldAddSecondPlayer()
        {
            var player1 = new Player("1", "Player1");
            var session = new GameSession(player1, null);
            session.Player2 = new Player("2", "Player2");
            session.Player2.Should().NotBeNull();
        }

        // REQ#1.1.3 - Validate turn toggling between players
        [Fact]
        public void MakeMove_ShouldToggleTurn()
        {
            var player1 = new Player("1", "Player1");
            var player2 = new Player("2", "Player2");
            var session = new GameSession(player1, player2);
            session.MakeMove(3);
            session.CurrentPlayer.Should().Be(player2);
        }

        // REQ#1.2.2 - Validate game does not end if opponent blocks win
        [Fact]
        public void MakeMove_BlockHorizontalWin_ShouldNotWin()
        {
            var game = new Game();
            game.MakeMove(1, 0);
            game.MakeMove(1, 1);
            game.MakeMove(1, 2);
            game.MakeMove(2, 3); // Player 2 blocks Player 1's win
            game.IsGameOver.Should().BeFalse();
            game.GameOverMessage.Should().BeEmpty();
        }

        // REQ#1.2.3 - Validate game ends in a draw when the board is full
        [Fact]
        public void CheckWinCondition_NoMovesLeft_ShouldBeGameOver()
        {
            var game = new Game();
            int player = 1;
            for (int col = 0; col < 7; col++)
            {
                for (int row = 0; row < 6; row++)
                {
                    game.MakeMove(player, col);
                    player = player == 1 ? 2 : 1;
                }
            }
            game.IsGameOver.Should().BeTrue();
            game.GameOverMessage.Should().Be("Player 1 wins!");
        }

        // REQ#1.1.2 - Validate move rejection for negative column index
        [Fact]
        public void MakeMove_NegativeColumn_ShouldReturnFalse()
        {
            var game = new Game();
            bool result = game.MakeMove(1, -1); // Invalid negative column
            result.Should().BeFalse();
        }

    }
}
