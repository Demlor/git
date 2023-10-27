using System.Diagnostics.Eventing.Reader;

namespace WinFormsApp3
{
    public partial class Form1 : Form
    {
        #region Inittalize 
        ChessboardM Chessboard;

        #endregion

        public Form1()
        {

            InitializeComponent();
            Chessboard = new ChessboardM(panel1, textBox1, pictureBox2);
            Chessboard.EndedGame += Chessboard_EndedGame;
            Chessboard.PlayerMarked += Chessboard_PlayerMarked;
            Chessboard.PlayerVsCOM += Chessboard_PlayerVsCOM;



            progressBar1.Step = FileName.CDStep;
            progressBar1.Maximum = FileName.CDTime;
            progressBar1.Value = 0;

            timer1.Interval = FileName.CDInterval;


            Chessboard.DrawChessBoard();
        }

        public void Chessboard_PlayerVsCOM(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void EndGame()
        {
            timer1.Stop();
            panel1.Enabled = false;
            MessageBox.Show("Kết Thúc!");
        }
        private void Chessboard_PlayerMarked(object? sender, EventArgs e)
        {
            timer1.Start();
            progressBar1.Value = 0;
        }

        private void Chessboard_EndedGame(object? sender, EventArgs e)
        {
            EndGame();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Player1 Go First!");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.PerformStep();


            if (progressBar1.Value >= progressBar1.Maximum)
            {
                EndGame();
            }

        }

        private void playerVsCOMToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }
    }
}
