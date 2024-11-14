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
        public void MakeMove()
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
        }
    }
}
