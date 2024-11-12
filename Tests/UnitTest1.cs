using FluentAssertions;
using Logic;

namespace Tests
{
    public class GameTests
    {
        [Fact]
        public void StartNewGame_ShouldInitializeBoard()
        {
            var game = new Game();
            game.Reset();

            game.IsGameOver.Should().BeFalse();
            game.GameOverMessage.Should().BeEmpty();
            game.Board.Should().BeEquivalentTo(new int[6, 7], options => options.WithStrictOrdering());
        }

        [Fact]
        public void MakeMove_ShouldAlternateTurnsAndDetectWin()
        {
            var game = new Game();
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
