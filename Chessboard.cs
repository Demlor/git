using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp3
{
    public class ChessboardM
    {
        #region properties
        private Panel chessboard;

        public Panel ChessBoard
        {
            get { return chessboard; }
            set { chessboard = value; }
        }

        private List<Player> player;

        public List<Player> Player
        {
            get { return player; }
            set { player = value; }
        }

        private int currentPlayer;

        public int CurrentPlayer
        {
            get => currentPlayer;
            set => currentPlayer = value;
        }

        private TextBox playerName;

        public TextBox PlayerName
        {
            get { return playerName; }
            set { playerName = value; }
        }

        private PictureBox playerMark;

        public PictureBox PlayerMark
        {
            get { return playerMark; }
            set { playerMark = value; }
        }
        private List<List<Button>> matrix;

        public List<List<Button>> Matrix
        {
            get { return matrix; }
            set { matrix = value; }
        }

        private event EventHandler playerMarked;

        public event EventHandler PlayerMarked
        {
            add
            {
                playerMarked += value;
            }
            remove
            {
                playerMarked -= value;
            }
        }

        private event EventHandler endedGame;

        public event EventHandler EndedGame
        {
            add
            {
                endedGame += value;
            }
            remove
            {
                endedGame -= value;
            }
        }

        private event EventHandler PlayerVscom;

        public event EventHandler PlayerVsCOM
        {
            add
            {
               PlayerVscom += value;
            }
            remove
            { PlayerVsCOM -= value;}
        }

        #endregion

        #region Initialize
        private bool VsCOM;
        
        public ChessboardM(Panel chessboard, TextBox playerName, PictureBox mark)
        {
            
            this.ChessBoard = chessboard;
            this.PlayerName = playerName;
            this.PlayerMark = mark;
            

            this.Player = new List<Player>()
            {
              new Player("Nguoi choi 1", Image.FromFile("C:\\Users\\84792\\source\\repos\\WinFormsApp3\\WinFormsApp3\\Resources\\Untitled.png")),
              new Player("Nguoi choi 2", Image.FromFile("C:\\Users\\84792\\source\\repos\\WinFormsApp3\\WinFormsApp3\\Resources\\123.png"))
            };

            currentPlayer = 0;

            ChangePlayer();
        }
        #endregion

        #region Mehthod
        public void DrawChessBoard()
        {
            ChessBoard.Enabled = true;


            Matrix = new List<List<Button>>();


            Button oldbtn = new Button() { Width = 0, Location = new Point(0, 0) };
            for (int i = 0; i < FileName.ChessBoardWidth; i++)
            {

                Matrix.Add(new List<Button>());
                for (int j = 0; j < FileName.ChessBoardHeight; j++)
                {
                    Button btn = new Button()
                    {
                        Width = FileName.ChessWidth,
                        Height = FileName.ChessHeight,
                        Location = new Point(oldbtn.Location.X + FileName.ChessWidth, oldbtn.Location.Y),
                        BackgroundImageLayout = ImageLayout.Stretch,
                        Tag = i.ToString()
                    };
                    btn.Click += Btn_click;
                    chessboard.Controls.Add(btn);
                    Matrix[i].Add(btn);
                    oldbtn = btn;
                }
                oldbtn.Location = new Point(0, oldbtn.Location.Y + FileName.ChessHeight);
                oldbtn.Width = 0;
                oldbtn.Height = 0;
            }
        }
        void Btn_click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.BackgroundImage != null)
                return;

            ChangePlayer();

            Mark(btn);

            if(playerMarked != null)
                playerMarked(this, new EventArgs());

            if (endgame(btn))
            {
                Endgame();
            }
        }

        private void PlayerVSCOM(Button btn)
        {
            VsCOM = true;

            Mark(btn);
            ChangePlayer();
            DrawChessBoard();
        }
        
        public void Endgame()
        {
            if (endedGame != null)
                endedGame(this, new EventArgs());          
        }

        private Point GetChessPoint(Button btn)
        {

            int vertical = Convert.ToInt32(btn.Tag);
            int Horizontal = Matrix[vertical].IndexOf(btn);
            Point point = new Point(Horizontal, vertical);

            return point;
        }

        private bool endgame(Button btn)
        {
            return endgameHorizontal(btn) || endgameVertical(btn) || endgamerightdiagonal(btn) || endgameleftdiagonal(btn);
        }
        private bool endgameHorizontal(Button btn)
        {
            Point point = GetChessPoint(btn);

            int countLeft = 0;
            for (int i = point.X; i >= 0; i--)
            {
                if (Matrix[point.Y][i].BackgroundImage == btn.BackgroundImage)
                {
                    countLeft++;
                }
                else
                    break;
            }
            int countRight = 0;
            for (int i = point.X + 1; i < FileName.ChessBoardWidth; i++)
            {
                if (Matrix[point.Y][i].BackgroundImage == btn.BackgroundImage)
                {
                    countRight++;
                }
                else
                    break;
            }

            return countLeft + countRight == 5;
        }
        private bool endgameVertical(Button btn)
        {
            Point point = GetChessPoint(btn);

            int countTop = 0;
            for (int i = point.Y; i >= 0; i--)
            {
                if (Matrix[i][point.X].BackgroundImage == btn.BackgroundImage)
                {
                    countTop++;
                }
                else
                    break;
            }
            int countBottom = 0;
            for (int i = point.Y + 1; i < FileName.ChessHeight; i++)
            {
                if (Matrix[i][point.X].BackgroundImage == btn.BackgroundImage)
                {
                    countBottom++;
                }
                else
                    break;
            }

            return countTop + countBottom == 5;
        }
        private bool endgamerightdiagonal(Button btn)
        {
            Point point = GetChessPoint(btn);

            int countTop = 0;
            for (int i = 0; i <= point.X; i++)
            {
                if (point.X - i < 0 || point.Y - i < 0)

                    break;

                if (Matrix[point.Y - i][point.X - i].BackgroundImage == btn.BackgroundImage)
                {
                    countTop++;
                }
                else
                    break;
            }
            int countBottom = 0;
            for (int i = 1; i <= FileName.ChessBoardWidth - point.X; i++)
            {
                if (point.Y + i >= FileName.ChessBoardHeight || point.X + i >= FileName.ChessBoardWidth)
                    break;
                if (Matrix[point.Y + i][point.X + i].BackgroundImage == btn.BackgroundImage)
                {
                    countBottom++;
                }
                else
                    break;
            }

            return countTop + countBottom == 5;
        }
        private bool endgameleftdiagonal(Button btn)
        {
            Point point = GetChessPoint(btn);

            int countTop = 0;
            for (int i = 0; i <= point.X; i++)
            {
                if (point.X + i > FileName.ChessBoardWidth || point.Y - i < 0)

                    break;

                if (Matrix[point.Y - i][point.X + i].BackgroundImage == btn.BackgroundImage)
                {
                    countTop++;
                }
                else
                    break;
            }
            int countBottom = 0;
            for (int i = 1; i <= FileName.ChessBoardWidth - point.X; i++)
            {
                if (point.Y + i >= FileName.ChessBoardHeight || point.X - i < 0)
                    break;

                if (Matrix[point.Y + i][point.X - i].BackgroundImage == btn.BackgroundImage)
                {
                    countBottom++;
                }
                else
                    break;
            }

            return countTop + countBottom == 5;
        }
    
     
        private void ChangePlayer()
        {
            PlayerName.Text = Player[CurrentPlayer].Name;

            PlayerMark.Image = Player[CurrentPlayer].Mark;
        }

        private void Mark(Button btn)
        {
            btn.BackgroundImage = Player[CurrentPlayer].Mark;

            CurrentPlayer = CurrentPlayer == 1 ? 0 : 1;
        }
    }
}
#endregion



