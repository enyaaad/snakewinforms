using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace snakewinforms
{
    public partial class Form1 : Form
    {

        private PictureBox fruit;
        private int x, y;
        private int moveX, moveY ;
        private int _width = 900;
        private int _height = 800;
        private int _sizeOfCell = 40;
        private int score;
        private PictureBox[] snake = new PictureBox[50];
        public Form1()
        {
            
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(OKP);
            _generateMap();
            _PrintSnake();
            _generateFruit();

        }
        private void _generateFruit()
        {
            fruit = new PictureBox();
            fruit.BackColor = Color.Green;
            fruit.Size = new Size(_sizeOfCell, _sizeOfCell);

            Random r = new Random();
                x = r.Next(0, _height -_sizeOfCell);
                int tempX = x % _sizeOfCell;
                x -= tempX;
                y = r.Next(0, _height - _sizeOfCell);
                int tempY = y % _sizeOfCell;
                y -= tempY;
                fruit.Location = new Point(x, y);
                this.Controls.Add(fruit);
        }
        private void _moveSnake()
        {
            for(int i = score; i >= 1; i--)
            {
                snake[i].Location = snake[i - 1].Location;
            }
            snake[0].Location = new Point(snake[0].Location.X + moveX * _sizeOfCell, snake[0].Location.Y + moveY* _sizeOfCell );
        }
        private void _eat()
        {
            if (snake[0].Location.X == x && snake[0].Location.Y == y)
            {
                ScoreLabel.Text = "Score" + ++score;
                snake[score] = new PictureBox();
                snake[score].Location = new Point(snake[score-1].Location.X+20*moveX, snake[score - 1].Location.Y - 20 * moveY);
                snake[score].Size = new Size(_sizeOfCell, _sizeOfCell);
                snake[score].BackColor = Color.Red;
                this.Controls.Add(snake[score]);
                this.Controls.Remove(fruit);
                _generateFruit();
            }
        }
        private void _eatitself()
        {
            for(int i=1; i<score; i++)
            {
                if (snake[0].Location == snake[i].Location)
                {
                    MessageBox.Show("game is over, your score:" + score);
                    break;
                }
            }
        }
        private void _generateMap()
        {
            for(int i=0; i <= _width  / _sizeOfCell; i++)
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.BackColor = Color.Black;
                pictureBox.Location = new Point(0, _sizeOfCell*i);
                pictureBox.Size = new Size(_width-100, 1);
                this.Controls.Add(pictureBox);
            }
            for (int i = 0; i <= _height / _sizeOfCell; i++)
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.BackColor = Color.Black;
                pictureBox.Location = new Point( _sizeOfCell * i,0);
                pictureBox.Size = new Size(1,_width);
                this.Controls.Add(pictureBox);
            }
        }
        private void OKP(object sender, KeyEventArgs e)
        {

            switch (e.KeyCode.ToString())
            {
                case "Right":
                    moveX = 1;
                    moveY = 0;
                    break;
                case "Left":
                    moveX =  -1;
                    moveY = 0;
                    break;
                case "Up":
                    moveY = -1;
                    moveX = 0;
                    break;
                case "Down":
                    moveY = 1;
                    moveX = 0;
                    break;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = 100;
            timer1.Start();
            moveX = 1;
            moveY = 0;
            this.Width = _width;
            this.Height = _height;

        }
        private void _PrintSnake()
        {
            snake[0] = new PictureBox();
            snake[0].Location = new Point(200, 200);
            snake[0].Size = new Size(_sizeOfCell, _sizeOfCell);
            snake[0].BackColor = Color.Blue;
            this.Controls.Add(snake[0]);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            _moveSnake();
            _eat();
            _eatitself();
        }
    }
}
