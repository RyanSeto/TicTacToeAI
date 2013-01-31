using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tic_Tac_Toe
{
    public partial class Form1 : Form
    {
        Check checkClass;
        SideClass sideCheck;
        Random rand;
        Rectangle[,] board;
        Pen blaPen;
        Brush blaBrush, rBrush, bluBrush, boardBrush;
        Font font;
        int size, turn, temp, temp2, x, y, count, sidesCount;
        string name;
        Boolean playerTurn, playerStart, done, initialPiece, strat1, strat1A, strat1B, strat2, over, playerWin, tie;
        Boolean[,] filled, players;
        int[] cornersX, cornersY, sidesX, sidesY, pos, sideSend1, sideSend2;

        public Form1()
        {
            InitializeComponent();

            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);

            rand = new Random();

            board = new Rectangle[3, 3];
            filled = new Boolean[3, 3];
            players = new Boolean[3, 3];
            cornersX = new int[4];
            cornersY = new int[4];
            sidesX = new int[4];
            sidesY = new int[4];
            sideSend1 = new int[2];
            sideSend2 = new int[2];

            cornersX[0] = 0;
            cornersX[1] = 2;
            cornersX[2] = 2;
            cornersX[3] = 0;

            cornersY[0] = 0;
            cornersY[1] = 0;
            cornersY[2] = 2;
            cornersY[3] = 2;

            sidesX[0] = 1;
            sidesX[1] = 2;
            sidesX[2] = 1;
            sidesX[3] = 0;

            sidesY[0] = 0;
            sidesY[1] = 1;
            sidesY[2] = 2;
            sidesY[3] = 1;

            font = new Font("Times New Roman", 90);
            blaPen = new Pen(Color.Black, 2);
            blaBrush = new SolidBrush(Color.Black);
            rBrush = new SolidBrush(Color.Red);
            bluBrush = new SolidBrush(Color.Blue);
            boardBrush = new SolidBrush(Color.PaleGreen);

            size = 140;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = new Rectangle((i * size) + 25, (j * size) + 100, size, size);
                }
            }

            name = textBoxName.Text;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    g.FillRectangle(boardBrush, board[i, j]);
                    g.DrawRectangle(blaPen, board[i, j]);

                    if (filled[i, j] == true)
                    {
                        if (players[i, j] == true)
                        {
                            g.DrawString("X", font, rBrush, board[i, j]);
                        }

                        else
                        {
                            g.DrawString("O", font, bluBrush, board[i, j]);
                        }
                    }
                }
            }

            if (over == true)
            {
                if (playerWin == true)
                {
            //        MessageBox.Show("Hey how did you beat the computer?!");
                    labelDisplay.Text = "Hey how did you beat the computer?!";
                }

                else if (tie == true)
                {
                    labelDisplay.Text = "Tie";
                }

                else
                {
                    //         MessageBox.Show("The computer beat you.");
                    labelDisplay.Text = "The computer beat you.";
                }
            }
        //    g.DrawString("X", font, blaBrush, board[0, 0]);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            labelDisplay.Text = name + ", it is your turn.";
            labelDisplay.Visible = true;
            playerTurn = true;
            playerStart = true;
            turn = 1;

            over = false;
            done = false;
            tie = false;
            playerWin = false;

            filled = new Boolean[3, 3];
            players = new Boolean[3, 3];

            strat1 = false;
            strat1A = false;
            strat1B = false;
            strat2 = false;

            Invalidate();
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            name = textBoxName.Text;

            labelDisplay.Text = name + ", it is your turn.";
            labelPlayer.Text = name;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (over == false)
            {
                done = false;

                if (playerTurn == true)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (board[i, j].Contains(e.X, e.Y) && filled[i, j] == false)
                            {
                                filled[i, j] = true;
                                players[i, j] = true;

                                playerTurn = false;

                                if (turn > 2)
                                {
                                    checkWinner(sender, e);
                                }
                            }
                        }
                    }

                    if (playerTurn == false && playerStart == true && over == false)
                    {
                        if (turn == 1)
                        {
                            if (filled[1, 1] == true && players[1, 1] == true)
                            {
                                temp = rand.Next(0, 4);

                                filled[cornersX[temp], cornersY[temp]] = true;
                                players[cornersX[temp], cornersY[temp]] = false;

                                strat2 = true;
                            }

                            else
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    if (filled[cornersX[i], cornersY[i]] == true && players[cornersX[i], cornersY[i]] == true)
                                    {
                                        filled[1, 1] = true;
                                        players[1, 1] = false;

                                        done = true;

                                        strat1 = true;
                                        //              strat1B = true;
                                        break;
                                    }
                                }

                                if (done == false)
                                {
                                    filled[1, 1] = true;
                                    players[1, 1] = false;
                                  //  compRandom(sender, e);
                                }
                            }
                        }

                        else if (turn == 2)
                        {
                            checkTwo(sender, e);
                            checkClass = new Check(filled, players);
                            sidesCount = 0;

                            if (done == false)
                            {
                           //     checkClass = new Check(filled, players);

                               // sidesCount = 0;

                                for (int i = 0; i < 4; i++)
                                {
                                    if (filled[sidesX[i], sidesY[i]] == true && players[sidesX[i], sidesY[i]] == true)
                                    {
                                        if (sidesCount == 0)
                                        {
                                            sideSend1[sidesCount] = sidesX[i];
                                            sideSend1[sidesCount + 1] = sidesY[i];
                                        }

                                        else if (sidesCount == 1)
                                        {
                                            sideSend2[sidesCount - 1] = sidesX[i];
                                            sideSend2[sidesCount] = sidesY[i];
                                        }

                                        sidesCount++;
                                    }
                                }
                            }

                            if (checkClass.Fork())
                            {
                                pos = checkClass.blockFork();

                            //    MessageBox.Show(pos[0].ToString() + " " + pos[1].ToString());

                                filled[pos[0], pos[1]] = true;
                                players[pos[0], pos[1]] = false;

                                done = true;
                            }

                            else if (sidesCount == 2)
                            {
                                sideCheck = new SideClass(filled, players, sideSend1, sideSend2, cornersX, cornersY);

                                pos = sideCheck.TwoSides();

                                filled[pos[0], pos[1]] = true;
                                players[pos[0], pos[1]] = false;

                                done = true;
                            }

                            else if (strat1 == true && done == false)
                            {
                                temp = rand.Next(0, 4);

                                while (filled[sidesX[temp], sidesY[temp]] == true)
                                {
                                    temp = rand.Next(0, 4);
                                }

                                filled[sidesX[temp], sidesY[temp]] = true;
                                players[sidesX[temp], sidesY[temp]] = false;
                            }

                            else if (strat2 == true && done == false)
                            {
                                temp = rand.Next(0, 4);

                                while (filled[cornersX[temp], cornersY[temp]] == true)
                                {
                                    temp = rand.Next(0, 4);
                                }

                                filled[cornersX[temp], cornersY[temp]] = true;
                                players[cornersX[temp], cornersY[temp]] = false;
                            }

                            else if (done == false)
                            {
                                if (filled[1, 1] == false)
                                {
                                    filled[1, 1] = true;
                                    players[1, 1] = false;
                                }

                                else if (filled[cornersX[0], cornersY[0]] == false)
                                {
                                    filled[cornersX[0], cornersY[0]] = true;
                                    players[cornersX[0], cornersY[0]] = false;
                                }

                                else if (filled[cornersX[1], cornersY[1]] == false)
                                {
                                    filled[cornersX[1], cornersY[1]] = true;
                                    players[cornersX[1], cornersY[1]] = false;
                                }

                                else if (filled[cornersX[2], cornersY[2]] == false)
                                {
                                    filled[cornersX[2], cornersY[2]] = true;
                                    players[cornersX[2], cornersY[2]] = false;
                                }

                                else if (filled[cornersX[3], cornersY[3]] == false)
                                {
                                    filled[cornersX[3], cornersY[3]] = true;
                                    players[cornersX[3], cornersY[3]] = false;
                                }

                                else
                                {
                                    compRandom(sender, e);
                                }
                            }
                        }

                        else if (turn == 3)
                        {
                            checkTwoToWin(sender, e);

                            if (done == false)
                            {
                                checkTwo(sender, e);
                            }

                            if (done == false)
                            {
                                compRandom(sender, e);
                            }
                        }

                        else if (turn == 4)
                        {
                            checkTwoToWin(sender, e);

                            if (done == false)
                            {
                                checkTwo(sender, e);
                            }

                            if (done == false)
                            {
                                compRandom(sender, e);
                            }
                        }

                        if (turn > 2)
                        {
                            checkWinner(sender, e);
                        }

                        turn++;
                        playerTurn = true;
                    }

                    if (turn == 6 && over == false)
                    {
                        over = true;
                        tie = true;
                        listBoxPlayer.Items.Add("Tie");
                        listBoxAI.Items.Add("Tie");
                    }

                    Invalidate();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            labelDisplay.Text = name + ", it is your turn.";
            labelDisplay.Visible = true;
            playerTurn = true;
            playerStart = true;
            turn = 1;
        }

        private void compRandom(object sender, EventArgs e)
        {
            temp = rand.Next(0, 3);
            temp2 = rand.Next(0, 3);

            while (filled[temp, temp2] == true)
            {
                temp = rand.Next(0, 3);
                temp2 = rand.Next(0, 3);
            }

            filled[temp, temp2] = true;
            players[temp, temp2] = false;
        }

        private void checkTwo(object sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (filled[i, j] == true && players[i, j] == true)
                    {
                        while (done == false)
                        {
                            //Top Left
                            x = i - 1;
                            y = j - 1;
                            count = 1;

                            while (x >= 0 && y >= 0)
                            {
                                if (filled[x, y] == true && players[x, y] == true)
                                {
                                    count++;
                                }

                                x--;
                                y--;
                            }

                            if (count == 2)
                            {
                                x = i - 1;
                                y = j - 1;

                                while (x >= 0 && y >= 0)
                                {
                                    if (filled[x, y] == false)
                                    {
                                        filled[x, y] = true;
                                        players[x, y] = false;
                                        done = true;
                                    }

                                    x--;
                                    y--;
                                }
                            }

                            if (done == true)
                            {
                                break;
                            }

                            //Top Right
                            x = i + 1;
                            y = j - 1;
                            count = 1;

                            while (x <= 2 && y >= 0)
                            {
                                if (filled[x, y] == true && players[x, y] == true)
                                {
                                    count++;                            
                                }

                                x++;
                                y--;
                            }

                            if (count == 2)
                            {
                                x = i + 1;
                                y = j - 1;

                                while (x <= 2 && y >= 0)
                                {
                                    if (filled[x, y] == false)
                                    {
                                        filled[x, y] = true;
                                        players[x, y] = false;
                                        done = true;
                                    }

                                    x++;
                                    y--;
                                }
                            }

                            if (done == true)
                            {
                                break;
                            }

                            //Bottom Right
                            x = i + 1;
                            y = j + 1;
                            count = 1;

                            while (x <= 2 && y <= 2)
                            {
                                if (filled[x, y] == true && players[x, y] == true)
                                {
                                    count++; 
                                }

                                x++;
                                y++;
                            }

                            if (count == 2)
                            {
                                x = i + 1;
                                y = j + 1;

                                while (x <= 2 && y <= 2)
                                {
                                    if (filled[x, y] == false)
                                    {
                                        filled[x, y] = true;
                                        players[x, y] = false;
                                        done = true;
                                    }

                                    x++;
                                    y++;
                                }
                            }

                            if (done == true)
                            {
                                break;
                            }

                            //Bottom Left
                            x = i - 1;
                            y = j + 1;
                            count = 1;

                            while (x >= 0 && y <= 2)
                            {
                                if (filled[x, y] == true && players[x, y] == true)
                                {
                                    count++;
                                }

                                x--;
                                y++;
                            }

                            if (count == 2)
                            {
                                x = i - 1;
                                y = j + 1;

                                while (x >= 0 && y <= 2)
                                {
                                    if (filled[x, y] == false)
                                    {
                                        filled[x, y] = true;
                                        players[x, y] = false;
                                        done = true;
                                    }

                                    x--;
                                    y++;
                                }
                            }

                            if (done == true)
                            {
                                break;
                            }

                            //Up
                            x = i;
                            y = j - 1;
                            count = 1;

                            while (y >= 0)
                            {
                                if (filled[x, y] == true && players[x, y] == true)
                                {
                                    count++;
                                }

                                y--;
                            }

                            if (count == 2)
                            {
                                y = j - 1;

                                while (y >= 0)
                                {
                                    if (filled[x, y] == false)
                                    {
                                        filled[x, y] = true;
                                        players[x, y] = false;
                                        done = true;
                                    }

                                    y--;
                                }
                            }

                            if (done == true)
                            {
                                break;
                            }

                            //Down
                            x = i;
                            y = j + 1;
                            count = 1;

                            while (y <= 2)
                            {
                                if (filled[x, y] == true && players[x, y] == true)
                                {
                                    count++;
                                }
                                
                                y++;
                            }

                            if (count == 2)
                            {
                                y = j + 1;

                                while (y <= 2)
                                {
                                    if (filled[x, y] == false)
                                    {
                                        filled[x, y] = true;
                                        players[x, y] = false;
                                        done = true;
                                    }

                                    y++;
                                }
                            }

                            if (done == true)
                            {
                                break;
                            }

                            //Right
                            x = i + 1;
                            y = j;
                            count = 1;

                            while (x <= 2)
                            {
                                if (filled[x, y] == true && players[x, y] == true)
                                {
                                    count++;
                                }

                                x++;
                            }

                            if (count == 2)
                            {
                                x = i + 1;

                                while (x <= 2)
                                {
                                    if (filled[x, y] == false)
                                    {
                                        filled[x, y] = true;
                                        players[x, y] = false;
                                        done = true;
                                    }

                                    x++;
                                }
                            }

                            if (done == true)
                            {
                                break;
                            }

                            //Left
                            x = i - 1;
                            y = j;
                            count = 1;

                            while (x >= 0)
                            {
                                if (filled[x, y] == true && players[x, y] == true)
                                {
                                    count++;
                                }

                                x--;
                            }

                            if (count == 2)
                            {
                                x = i - 1;

                                while (x >= 0)
                                {
                                    if (filled[x, y] == false)
                                    {
                                        filled[x, y] = true;
                                        players[x, y] = false;
                                        done = true;
                                    }

                                    x--;
                                }
                            }

                            break;
                        }
                    }

                    if (done == true)
                    {
                        break;
                    }
                }

                if (done == true)
                {
                    break;
                }
            }
        }

        private void checkWinner(object sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (filled[i, j] == true)
                    {
                        if (players[i, j] == true)
                        {
                            initialPiece = true;
                        }

                        else
                        {
                            initialPiece = false;
                        }

                        while (over == false)
                        {
                            //Top Left
                            x = i - 1;
                            y = j - 1;
                            count = 1;

                            while (x >= 0 && y >= 0 && filled[x, y] == true && players[x, y] == initialPiece)
                            {
                                count++;

                                if (count == 3)
                                {
                                    over = true;
                                    playerWin = initialPiece;

                                    if (playerWin == true)
                                    {
                                        listBoxPlayer.Items.Add("Win");
                                        listBoxAI.Items.Add("Loss");
                                    }

                                    else
                                    {
                                        listBoxPlayer.Items.Add("Loss");
                                        listBoxAI.Items.Add("Win");
                                    }
                                }

                                x--;
                                y--;
                            }

                            //Top Right
                            x = i + 1;
                            y = j - 1;
                            count = 1;

                            while (x <= 2 && y >= 0 && filled[x, y] == true && players[x, y] == initialPiece)
                            {
                                count++;

                                if (count == 3)
                                {
                                    over = true;
                                    playerWin = initialPiece;

                                    if (playerWin == true)
                                    {
                                        listBoxPlayer.Items.Add("Win");
                                        listBoxAI.Items.Add("Loss");
                                    }

                                    else
                                    {
                                        listBoxPlayer.Items.Add("Loss");
                                        listBoxAI.Items.Add("Win");
                                    }
                                }

                                x++;
                                y--;
                            }

                            //Bottom Right
                            x = i + 1;
                            y = j + 1;
                            count = 1;

                            while (x <= 2 && y <= 2 && filled[x, y] == true && players[x, y] == initialPiece)
                            {
                                count++;

                                if (count == 3)
                                {
                                    over = true;
                                    playerWin = initialPiece;

                                    if (playerWin == true)
                                    {
                                        listBoxPlayer.Items.Add("Win");
                                        listBoxAI.Items.Add("Loss");
                                    }

                                    else
                                    {
                                        listBoxPlayer.Items.Add("Loss");
                                        listBoxAI.Items.Add("Win");
                                    }
                                }

                                x++;
                                y++;
                            }

                            //Bottom Left
                            x = i - 1;
                            y = j + 1;
                            count = 1;

                            while (x >= 0 && y <= 2 && filled[x, y] == true && players[x, y] == initialPiece)
                            {
                                count++;

                                if (count == 3)
                                {
                                    over = true;
                                    playerWin = initialPiece;

                                    if (playerWin == true)
                                    {
                                        listBoxPlayer.Items.Add("Win");
                                        listBoxAI.Items.Add("Loss");
                                    }

                                    else
                                    {
                                        listBoxPlayer.Items.Add("Loss");
                                        listBoxAI.Items.Add("Win");
                                    }
                                }

                                x--;
                                y++;
                            }

                            //Up
                            x = i;
                            y = j - 1;
                            count = 1;

                            while (y >= 0 && filled[x, y] == true && players[x, y] == initialPiece)
                            {
                                count++;

                                if (count == 3)
                                {
                                    over = true;
                                    playerWin = initialPiece;

                                    if (playerWin == true)
                                    {
                                        listBoxPlayer.Items.Add("Win");
                                        listBoxAI.Items.Add("Loss");
                                    }

                                    else
                                    {
                                        listBoxPlayer.Items.Add("Loss");
                                        listBoxAI.Items.Add("Win");
                                    }
                                }

                                y--;
                            }

                            //Down
                            x = i;
                            y = j + 1;
                            count = 1;

                            while (y <= 2 && filled[x, y] == true && players[x, y] == initialPiece)
                            {
                                count++;

                                if (count == 3)
                                {
                                    over = true;
                                    playerWin = initialPiece;

                                    if (playerWin == true)
                                    {
                                        listBoxPlayer.Items.Add("Win");
                                        listBoxAI.Items.Add("Loss");
                                    }

                                    else
                                    {
                                        listBoxPlayer.Items.Add("Loss");
                                        listBoxAI.Items.Add("Win");
                                    }
                                }

                                y++;
                            }

                            //Right
                            x = i + 1;
                            y = j;
                            count = 1;

                            while (x <= 2 && filled[x, y] == true && players[x, y] == initialPiece)
                            {
                                count++;

                                if (count == 3)
                                {
                                    over = true;
                                    playerWin = initialPiece;

                                    if (playerWin == true)
                                    {
                                        listBoxPlayer.Items.Add("Win");
                                        listBoxAI.Items.Add("Loss");
                                    }

                                    else
                                    {
                                        listBoxPlayer.Items.Add("Loss");
                                        listBoxAI.Items.Add("Win");
                                    }
                                }

                                x++;
                            }

                            //Left
                            x = i - 1;
                            y = j;
                            count = 1;

                            while (x >= 0 && filled[x, y] == true && players[x, y] == initialPiece)
                            {
                                count++;

                                if (count == 3)
                                {
                                    over = true;
                                    playerWin = initialPiece;

                                    if (playerWin == true)
                                    {
                                        listBoxPlayer.Items.Add("Win");
                                        listBoxAI.Items.Add("Loss");
                                    }

                                    else
                                    {
                                        listBoxPlayer.Items.Add("Loss");
                                        listBoxAI.Items.Add("Win");
                                    }
                                }

                                x--;
                            }

                            break;
                        }
                    }

                    if (over == true)
                    {
                        break;
                    }
                }

                if (over == true)
                {
                    break;
                }
            }
        }

        private void checkTwoToWin(object sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (filled[i, j] == true && players[i, j] == false)
                    {
                        while (done == false)
                        {
                            //Top Left
                            x = i - 1;
                            y = j - 1;
                            count = 1;

                            while (x >= 0 && y >= 0)
                            {
                                if (filled[x, y] == true && players[x, y] == false)
                                {
                                    count++;
                                }

                                x--;
                                y--;
                            }

                            if (count == 2)
                            {
                                x = i - 1;
                                y = j - 1;

                                while (x >= 0 && y >= 0)
                                {
                                    if (filled[x, y] == false)
                                    {
                                        filled[x, y] = true;
                                        players[x, y] = false;
                                        done = true;
                                    }

                                    x--;
                                    y--;
                                }
                            }

                            //Top Right
                            x = i + 1;
                            y = j - 1;
                            count = 1;

                            while (x <= 2 && y >= 0)
                            {
                                if (filled[x, y] == true && players[x, y] == false)
                                {
                                    count++;
                                }

                                x++;
                                y--;
                            }

                            if (count == 2)
                            {
                                x = i + 1;
                                y = j - 1;

                                while (x <= 2 && y >= 0)
                                {
                                    if (filled[x, y] == false)
                                    {
                                        filled[x, y] = true;
                                        players[x, y] = false;
                                        done = true;
                                    }

                                    x++;
                                    y--;
                                }
                            }

                            //Bottom Right
                            x = i + 1;
                            y = j + 1;
                            count = 1;

                            while (x <= 2 && y <= 2)
                            {
                                if (filled[x, y] == true && players[x, y] == false)
                                {
                                    count++;
                                }

                                x++;
                                y++;
                            }

                            if (count == 2)
                            {
                                x = i + 1;
                                y = j + 1;

                                while (x <= 2 && y <= 2)
                                {
                                    if (filled[x, y] == false)
                                    {
                                        filled[x, y] = true;
                                        players[x, y] = false;
                                        done = true;
                                    }

                                    x++;
                                    y++;
                                }
                            }

                            //Bottom Left
                            x = i - 1;
                            y = j + 1;
                            count = 1;

                            while (x >= 0 && y <= 2)
                            {
                                if (filled[x, y] == true && players[x, y] == false)
                                {
                                    count++;
                                }

                                x--;
                                y++;
                            }

                            if (count == 2)
                            {
                                x = i - 1;
                                y = j + 1;

                                while (x >= 0 && y <= 2)
                                {
                                    if (filled[x, y] == false)
                                    {
                                        filled[x, y] = true;
                                        players[x, y] = false;
                                        done = true;
                                    }

                                    x--;
                                    y++;
                                }
                            }

                            //Up
                            x = i;
                            y = j - 1;
                            count = 1;

                            while (y >= 0)
                            {
                                if (filled[x, y] == true && players[x, y] == false)
                                {
                                    count++;
                                }
                                
                                y--;
                            }

                            if (count == 2)
                            {
                                y = j - 1;

                                while (y >= 0)
                                {
                                    if (filled[x, y] == false)
                                    {
                                        filled[x, y] = true;
                                        players[x, y] = false;
                                        done = true;
                                    }

                                    y--;
                                }
                            }

                            //Down
                            x = i;
                            y = j + 1;
                            count = 1;

                            while (y <= 2)
                            {
                                if (filled[x, y] == true && players[x, y] == false)
                                {
                                    count++;
                                }

                                y++;
                            }

                            if (count == 2)
                            {
                                y = j + 1;

                                while (y <= 2)
                                {
                                    if (filled[x, y] == false)
                                    {
                                        filled[x, y] = true;
                                        players[x, y] = false;
                                        done = true;
                                    }

                                    y++;
                                }
                            }

                            //Right
                            x = i + 1;
                            y = j;
                            count = 1;

                            while (x <= 2)
                            {
                                if (filled[x, y] == true && players[x, y] == false)
                                {
                                    count++;
                                }

                                x++;
                            }

                            if (count == 2)
                            {
                                x = i + 1;

                                while (x <= 2)
                                {
                                    if (filled[x, y] == false)
                                    {
                                        filled[x, y] = true;
                                        players[x, y] = false;
                                        done = true;
                                    }

                                    x++;
                                }
                            }

                            //Left
                            x = i - 1;
                            y = j;
                            count = 1;

                            while (x >= 0)
                            {
                                if (filled[x, y] == true && players[x, y] == false)
                                {
                                    count++;
                                }

                                x--;
                            }

                            if (count == 2)
                            {
                                x = i - 1;

                                while (x >= 0)
                                {
                                    if (filled[x, y] == false)
                                    {
                                        filled[x, y] = true;
                                        players[x, y] = false;
                                        done = true;
                                    }

                                    x--;
                                }
                            }

                            break;
                        }
                    }

                    if (done == true)
                    {
                        break;
                    }
                }

                if (done == true)
                {
                    break;
                }
            }
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buttonStart_Click(sender, e);
        }

        private void giveUpAndExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
