using FluentAssertions;
using Logic;

namespace Tests
{
    public class GameTests
    {
        [Fact]
        public void StartNewGame()
        {
            Game game = new Game();
            game.Reset();
        }
        [Fact]
        public void InitializeBoard()
        {
            Game game = new Game();
            game.Reset();

            game.IsGameOver.Should().BeFalse();
            game.GameOverMessage.Should().BeEmpty();
            game.Board.Should().BeEquivalentTo(new int[6, 7]);
        }

        [Fact]
        public void ShouldNotAllowMoveAfterGameOver()
        {
            Game game = new Game();
            game.MakeMove(1, 0);
            game.MakeMove(2, 1);
            game.MakeMove(1, 0);
            game.MakeMove(2, 1);
            game.MakeMove(1, 0);
            game.MakeMove(2, 1);
            game.MakeMove(1, 0);

            game.GameOverMessage.Should().Be("Player 1 wins!");
            var moveResult = game.MakeMove(2, 2);

            moveResult.Should().BeFalse();
            game.Board[5, 2].Should().Be(0);
        }
        [Fact]
        public void ShouldDetectHorizontalWin()
        {
            Game game = new Game();

            game.MakeMove(1, 0);
            game.MakeMove(2, 0);
            game.MakeMove(1, 1);
            game.MakeMove(2, 1);
            game.MakeMove(1, 2);
            game.MakeMove(2, 2);
            game.MakeMove(1, 3);

            game.GameOverMessage.Should().Be("Player 1 wins!");
        }
        [Fact]
        public void ShouldNotDeclareWinPrematurely()
        {
            Game game = new Game();

            game.MakeMove(1, 0);
            game.MakeMove(2, 0);
            game.MakeMove(1, 0);
            game.MakeMove(2, 0);

            game.GameOverMessage.Should().BeEmpty();
            game.IsGameOver.Should().BeFalse();
        }
        [Fact]
        public void ShouldHaveCorrectBoardSize()
        {
            Game game = new Game();

            game.Board.GetLength(0).Should().Be(6);
            game.Board.GetLength(1).Should().Be(7);
        }
        [Fact]
        public void ShouldAlternatePlayerTurns()
        {
            Game game = new Game();

            game.MakeMove(1, 0); // Player 1
            game.MakeMove(2, 1); // Player 2
            game.MakeMove(1, 0); // Player 1
            game.MakeMove(2, 1); // Player 2

            game.Board[5, 0].Should().Be(1); 
            game.Board[5, 1].Should().Be(2); 
            game.Board[4, 0].Should().Be(1); 
            game.Board[4, 1].Should().Be(2); 
        }


    }
}
