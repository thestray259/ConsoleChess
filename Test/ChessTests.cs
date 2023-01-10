using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessConsole;
using System.Linq;
using System.Text.RegularExpressions;

namespace Test
{
    [TestClass]
    public class ChessTests
    {
        ChessGame game = new ChessGame(true);
        //ChessBoard board = new ChessBoard(true); 

        [TestMethod]
        public void PiecesSpawn()
        {
            // checks board to see if all necessary pieces are placed
            var cBoard = game.GetChessBoard();
            ChessBoard.Cell[,] cells = cBoard.GetCells();

            int expectedPieces = 32;
            int pieces = 0;

            foreach (ChessBoard.Cell cell in cells)
            {
                if (!(cell.Piece == null)) pieces++;  
            }

            Assert.AreEqual(expectedPieces, pieces); 
        }

        [TestMethod]
        public void KingBetweenRooks()
        {
            // checks to make sure the king is placed between the 2 rooks
            var cBoard = game.GetChessBoard();
            ChessBoard.Cell[,] cells = cBoard.GetCells();

            string expected = "RKR"; 
            string whitePieces = "";
            string blackPieces = "";

            // iterate through first row and add Char letters to string 
            // then take out Chars that are not R or K 
            // check that the string = "RKR"

            for (int i = 0; i < 8; i++)
            {
                var whitePiece = cells[i, 0].Piece.Char;
                //System.Console.WriteLine(whitePiece);
                whitePieces += whitePiece; 

                var blackPiece = cells[i, 7].Piece.Char;
                //System.Console.WriteLine(blackPiece);
                blackPieces += blackPiece; 
            }

            whitePieces = Regex.Replace(whitePieces, "[^RK]", "");
            blackPieces = Regex.Replace(blackPieces, "[^RK]", "");

            Assert.AreEqual(expected, whitePieces); 
            Assert.AreEqual(expected, blackPieces); 
        }

        [TestMethod]
        public void C960BlackPiecesSetCorrectly()
        {
            // black pieces should be equal and opposite of white pieces
            var cBoard = game.GetChessBoard();
            ChessBoard.Cell[,] cells = cBoard.GetCells();

            string whitePieces = "";
            string blackPieces = "";

            for (int i = 0; i < 8; i++)
            {
                var whitePiece = cells[i, 0].Piece.Char;
                //System.Console.WriteLine(whitePiece);
                whitePieces += whitePiece;

                var blackPiece = cells[i, 7].Piece.Char;
                //System.Console.WriteLine(blackPiece);
                blackPieces += blackPiece;
            }

            char[] whiteArray = whitePieces.ToCharArray();
            System.Array.Reverse(whiteArray);
            string expectedBlack = new string(whiteArray);

            Assert.AreEqual(expectedBlack, blackPieces); 
        }

        [TestMethod]
        public void BishopsPlacedCorrectly()
        {
            // bishops should be on opposite colors
            var cBoard = game.GetChessBoard();
            ChessBoard.Cell[,] cells = cBoard.GetCells();
            int[] whiteBishops = new int[2] { -1, -1 }; 
            int[] blackBishops = new int[2] { -1, -1 }; 

            for (int i = 0; i < 8; i++)
            {
                if (cells[i, 0].Piece.Char == 'B')
                {
                    if (whiteBishops[0] == -1) whiteBishops[0] = i % 2;
                    else whiteBishops[1] = i % 2; 
                }

                if (cells[i, 7].Piece.Char == 'B')
                {
                    if (blackBishops[0] == -1) blackBishops[0] = i % 2;
                    else blackBishops[1] = i % 2; 
                }
            }

            Assert.AreEqual(1, whiteBishops.Sum()); 
            Assert.AreEqual(1, blackBishops.Sum()); 

        }

        [TestMethod]
        public void RegGamePlacedCorrectly()
        {
            // checks to make sure the regular game board is as it should be at start
            var cBoard = new ChessGame(false).GetChessBoard();
            ChessBoard.Cell[,] cells = cBoard.GetCells();

            string expected = "RHBQKBHR";
            string whitePieces = "";
            string blackPieces = "";

            for (int i = 0; i < 8; i++)
            {
                var whitePiece = cells[i, 0].Piece.Char;
                //System.Console.WriteLine(whitePiece);
                whitePieces += whitePiece;

                var blackPiece = cells[i, 7].Piece.Char;
                //System.Console.WriteLine(blackPiece);
                blackPieces += blackPiece;
            }

            Assert.AreEqual(expected, whitePieces); 
            Assert.AreEqual(expected, blackPieces); 
        }
    }
}
