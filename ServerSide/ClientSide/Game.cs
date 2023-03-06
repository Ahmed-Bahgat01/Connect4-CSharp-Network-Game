using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MessageLib;

namespace ClientSide
{
    public partial class Game : Form
    {
        private Rectangle[] boardColumns;

        private int[,] board; //contains row and column number
        int[] ColNum = new int[7] { 5, 7, 6, 8, 9, 10, 8 };
        int[] RowNum = new int[7] { 4, 6, 5, 7, 7, 7, 8 };
        int boardSize;

        int ColWidth = 48;
        int ColHeight = 48;


        int turn;

        Color player1Color;
        Color player2Color;

        SolidBrush Player1Brush;
        SolidBrush Player2Brush;
        public Game()
        {
            InitializeComponent();

            this.boardSize = 0; //0->6
            this.boardColumns = new Rectangle[ColNum[boardSize]];
            this.board = new int[RowNum[boardSize], ColNum[boardSize]];
            this.turn = 1;
            this.player1Color = Color.Red;
            this.player2Color = Color.Yellow;
            Player1Brush = new SolidBrush(player1Color);
            Player2Brush = new SolidBrush(player2Color);
        }

        public Game(int size, Color player1)
        {
            InitializeComponent();
            this.boardSize = size;
            this.boardColumns = new Rectangle[ColNum[boardSize]];
            this.board = new int[RowNum[boardSize], ColNum[boardSize]];
            if(player1 == Color.Red)
            {
                this.turn = 1;
            }
            else
            {
                this.turn = 2;
            }
            this.player1Color = player1;
            //this.player2Color = player2;
            if(player1 == Color.Red)
            {
                this.player2Color = Color.Yellow;
            }
            else
            {
                this.player2Color = Color.Red;
            }
            Player1Brush = new SolidBrush(player1Color);
            Player2Brush = new SolidBrush(player2Color);

            Client.OtherPlayerMoveEvent += OtherPlayerMoveHandler;
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        { 
            int colIndex = GetColClicked(e.Location);
            if(turn == 1)
                play(colIndex);
        }

        private int GetColClicked(Point mouse) //take the point of the clicked position and return the index of the clicked column
        {
            for (int i = 0; i < this.boardColumns.Length; i++)
            {
                if (mouse.X > this.boardColumns[i].X &&
                    mouse.X < this.boardColumns[i].X + this.boardColumns[i].Width &&
                    mouse.Y > this.boardColumns[i].Y &&
                    mouse.Y < this.boardColumns[i].Y + this.boardColumns[i].Height)
                { return i; }
            }
            return -1;
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.Blue, 24, 24, ColWidth * ColNum[boardSize], ColHeight * RowNum[boardSize]);
            for (int i = 0; i < RowNum[boardSize]; i++) //4
            {
                for (int j = 0; j < ColNum[boardSize]; j++) //4 (0->3)
                {
                    if (i == 0)
                    {
                        this.boardColumns[j] = new Rectangle(32 + 48 * j, 24, ColWidth, ColHeight * RowNum[boardSize]);
                        //e.Graphics.FillRectangle(Brushes.Black,32 + 48 * j, 24, ColWidth, ColHeight * RowNum[boardSize]);
                    }

                    e.Graphics.FillEllipse(Brushes.White, 32 + 48 * j, 32 + 48 * i, 32, 32);
                }
            }
        }
        private int EmptyRow(int colindex) //take the colum index and return witch row is empty inside it
        {
            for (int rowIndex = this.RowNum[boardSize] - 1; rowIndex >= 0; rowIndex--)
            {
                if (this.board[rowIndex, colindex] == 0) //column not full yet
                {
                    return rowIndex;
                }
            }
            return -1;
        }
        private bool AllNumbersEquals(int toCheck, params int[] numbers)
        {
            foreach (int i in numbers)
            {
                if (i != toCheck)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        ///     function to decied if player is winner
        /// </summary>
        /// <param name="playerToCheck"></param>
        /// <returns></returns>
        private int WinnerPlayer(int playerToCheck)
        {
            //vertical winner check
            for (int row = 0; row < this.board.GetLength(0) - 3; row++)
            {
                for (int col = 0; col < this.board.GetLength(1); col++)
                {

                    if (this.AllNumbersEquals(playerToCheck, board[row, col], board[row + 1, col], board[row + 2, col], board[row + 3, col]))
                    {
                        return playerToCheck;

                    }
                }
            }

            //Horizontal winner check
            for (int col = 0; col < this.board.GetLength(1) - 3; col++) //the start searching column
            {
                for (int row = 0; row < this.board.GetLength(0); row++)
                {
                    if (this.AllNumbersEquals(playerToCheck, board[row, col], board[row, col + 1], board[row, col + 2], board[row, col + 3]))
                    {
                        return playerToCheck;

                    }
                }

            }

            ////diagonal(\) winner check
            for (int row = 0; row < this.board.GetLength(0) - 3; row++)
            {
                for (int col = 0; col < this.board.GetLength(1) - 3; col++)
                {
                    if (this.AllNumbersEquals(playerToCheck, board[row, col], board[row + 1, col + 1], board[row + 2, col + 2], board[row + 3, col + 3]))
                    {
                        return playerToCheck;

                    }
                }
            }

            ////diagonal(/) winner check
            for (int row = 0; row < this.board.GetLength(0) - 3; row++)
            {
                for (int col = 3; col < this.board.GetLength(1); col++)
                {
                    if (this.AllNumbersEquals(playerToCheck, board[row + 1, col - 1], board[row + 2, col - 2], board[row + 3, col - 3], board[row, col]))
                    {
                        return playerToCheck;

                    }
                }
            }

            return -1;
        }
        private void checkedFullBoard(int[,] board)
        {
            int col;
            for (col = 0; col < board.GetLength(1) - 1; col++)
            {
                if (board[0, col] > 0)
                {

                    continue;
                }

                else
                    break;
            }
            if (col == (board.GetLength(1) - 1))
            {
                MessageBox.Show("End Game");
                Application.Restart();
            }
        }

        private void Game_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        ///     responsible for playing disk on specified col (for this player)
        /// </summary>
        /// <param name="colIndex"></param>
        private void play(int colIndex)
        {
            if (colIndex != -1)
            {
                int rowIndex = EmptyRow(colIndex); //index of the empty row in the column

                try
                {
                    this.board[rowIndex, colIndex] = this.turn;
                }
                catch
                {
                    MessageBox.Show("Full Column");
                }

                if (rowIndex != -1) //entier collum not full yet
                {


                    if (this.turn == 1)
                    {
                        OtherPlayerMoveMessageContainer message = new OtherPlayerMoveMessageContainer(colIndex, false);
                        Graphics g = this.CreateGraphics();
                        //for (int i = 32; i<= 32 + 48 * rowIndex; i++)
                        //{ 
                        g.FillEllipse(Player1Brush, 32 + 48 * colIndex, 32 + 48 * rowIndex, 32, 32);
                        //}
                        checkedFullBoard(this.board);

                        int winner = this.WinnerPlayer(this.board[rowIndex, colIndex]);
                        if (winner != -1) //there if a winner player
                        {
                            // send winning message to server
                            message = new OtherPlayerMoveMessageContainer(colIndex, true);
                            Client.SendMsg(message);
                            EndGame(winner);
                        }
                        else
                        {
                            Client.SendMsg(message);

                        }

                        this.turn = 2;

                    }

                }
            }

        }
        private void EndGame(int winner)
        {
            if (winner == 1)
            {
                MessageBox.Show($"YOU WIN");
            }
            else
            {
                MessageBox.Show($"YOU LOOSE");
            }
            //Application.Restart();
            // TODO:  START PLAYAGAIN FORM
        }
        private void OtherPlayerMoveHandler(OtherPlayerMoveMessageContainer obj)
        {
            if (obj.IsWinningMove)
            {
                otherSideMove(obj.ColNum);
                // TODO: SHOW MESSAGEBOX THAT OTHER PLAYER WON
                // TODO:  OPEN PALY AGAIN FORM
            }
            else
            {
                otherSideMove(obj.ColNum);
            }
        }


        /// <summary>
        ///     this function shows other side move on the board
        /// </summary>
        /// <param name="colIndex"></param>
        private void otherSideMove(int colIndex)
        {
            //Random rnd = new Random();
            //int colIndex = rnd.Next(0, ColNum[boardSize]);
            if (colIndex != -1)
            {
                int rowIndex = EmptyRow(colIndex); //index of the empty row in the column

                try
                {
                    this.board[rowIndex, colIndex] = this.turn;
                }
                catch
                {
                    MessageBox.Show("empty column");
                }

                if (rowIndex != -1) //entier collum not full yet
                {

                    if (this.turn == 2)
                    {
                        Graphics g = this.CreateGraphics();
                        //for (int i = 32; i<= 32 + 48 * rowIndex; i++)
                        //{
                        g.FillEllipse(Player2Brush, 32 + 48 * colIndex, 32 + 48 * rowIndex, 32, 32);
                        checkedFullBoard(this.board);

                        //}
                        int winner = this.WinnerPlayer(this.board[rowIndex, colIndex]);
                        if (winner != -1) //there if a winner player
                        {
                            EndGame(winner);
                            //if(winner == 1)
                            //{
                            //    MessageBox.Show($"YOU WIN");
                            //}
                            //else
                            //{
                            //    MessageBox.Show($"YOU LOOSE");
                            //}
                            //Application.Restart();
                        }
                        this.turn = 1;
                    }
                }

            }


        }
    }

}
