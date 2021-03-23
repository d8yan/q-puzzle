
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DYanAssignment3
{
    /// <summary>
    /// Q-puzzle Game Play Form class
    /// </summary>
    public partial class QGamePlayForm : Form

    {
        //Declare vaiables  
        private Image imgNone = DYanAssignment3.Properties.Resources.none;
        private Image imgWall = DYanAssignment3.Properties.Resources.wall;
        private Image imgRedDoor = DYanAssignment3.Properties.Resources.red_door;
        private Image imgGreenDoor = DYanAssignment3.Properties.Resources.green_door;
        private Image imgRedBox = DYanAssignment3.Properties.Resources.red_box;
        private Image imgGreenBox = DYanAssignment3.Properties.Resources.green_box;

        private Tile selectedBox;
        private Tile[,] pTile;
        private int[,] tileReference;
        private Tile tile;

        private int countBox;
        private int moves;

        //declare constant variables on picture boxs size and initial location
        const int WIDTH = 50;
        const int HEIGHT = 50;
        const int INIT_LEFT = 50;
        const int INIT_TOP = 50;
        //declare constant button direction.
        const string LEFT = "Left";
        const string RIGHT = "Right";
        const string UP = "Up";
        const string DOWN = "Down";
        //declare constant moving factors
        const int POSITIVE_FACTOR = 1;
        const int NEGATIVE_FACTOR = -1;
        //declare constant door factors
        const int DOOR_FACTOR = 2;
        //To store Tool:None is 0, Wall is 1, RedDoor is 2
        //              GreenDoor is 3, RedBox is 4, GreenBox is 5
        private enum Tool { None, Wall, RedDoor, GreenDoor, RedBox, GreenBox };


        /// <summary>
        /// Constructor for QGame Play Form
        /// </summary>
        public QGamePlayForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Method to initialize playboard: tile border style, slot size and position
        /// add tiles to control pannel,
        /// choose tiles with greenbox image and redbox image to be clickable
        /// </summary>
        private void initializePlayBoard()
        {
            for (int i = 0; i < tileReference.GetLength(0); i++)
            {
                for (int j = 0; j < tileReference.GetLength(1); j++)
                {
                    if (tileReference[i, j] != (int)Tool.None)
                    {
                        tile = new Tile();
                        tile.Row = i;
                        tile.Col = j;
                        tile.ToolType = tileReference[i, j];
                        tile.Image = tool(tile.ToolType);
                        tile.Location = new Point(j * WIDTH + INIT_LEFT, i * HEIGHT + INIT_TOP);
                        tile.Width = WIDTH;
                        tile.Height = HEIGHT;
                        tile.Visible = true;
                        tile.BorderStyle = BorderStyle.None;
                        tile.SizeMode = PictureBoxSizeMode.StretchImage;
                        pTile[i, j] = tile;
                        if (tile.Image == imgGreenBox || tile.Image == imgRedBox)
                        {
                            //    Define pictureBox click event
                            tile.Click += new EventHandler(tileBox_Click);
                        }
                        //Add pictureBox to the panel
                        this.Controls.Add(tile);
                        tile.BringToFront();
                    }
                }
            }
        }
        /// <summary>
        /// Method to read the file, get row number and column number and reference of tile position and tool type
        /// </summary>
        /// <param name="fileName">file name chosed by user</param>
        private void load(string fileName)
        {
            //declare local varialbe
            int rows;
            int columns;
            int tool;

            try
            {
                StreamReader reader = new StreamReader(fileName);
                rows = int.Parse(reader.ReadLine());
                columns = int.Parse(reader.ReadLine());
                pTile = new Tile[rows, columns];
                tileReference = new int[rows, columns];
                for (int i = 0; i < pTile.GetLength(0); i++)
                {
                    for (int j = 0; j < pTile.GetLength(1); j++)
                    {
                        i = int.Parse(reader.ReadLine());
                        j = int.Parse(reader.ReadLine());
                        tool = int.Parse(reader.ReadLine());
                        tileReference[i, j] = tool;
                    }
                }

                reader.Close();
                btnUp.Enabled = true;
                btnDown.Enabled = true;
                btnLeft.Enabled = true;
                btnRight.Enabled = true;

                initializePlayBoard();
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("File not found", "QGame", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception)
            {

                MessageBox.Show("Reading file error", "QGame", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        /// <summary>
        /// Method to generate picture box image correspondent to input number which respresent tile content
        /// </summary>
        /// <param name="content">the number represent the tile content</param>
        /// <returns>return a picturebox image that correspondent with the tile content</returns>
        private Image tool(int content)
        {
            PictureBox toolImg = new PictureBox();
            switch (content)
            {
                case (int)Tool.None:
                    toolImg.Image = null;
                    break;
                case (int)Tool.Wall:
                    toolImg.Image = imgWall;
                    break;
                case (int)Tool.RedDoor:
                    toolImg.Image = imgRedDoor;
                    break;
                case (int)Tool.GreenDoor:
                    toolImg.Image = imgGreenDoor;
                    break;
                case (int)Tool.RedBox:
                    toolImg.Image = imgRedBox;
                    countBox++;
                    break;
                case (int)Tool.GreenBox:
                    toolImg.Image = imgGreenBox;
                    countBox++;
                    break;
                default:
                    break;
            }
            return toolImg.Image;
        }
        /// <summary>
        /// Tile click event to store clicked box into selectedBox
        /// show user the selectedbox can be switched
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tileBox_Click(object sender, EventArgs e)
        {
            if (selectedBox != null)
            {
                selectedBox.BorderStyle = BorderStyle.None;
            }

            Tile box = (Tile)sender;
            selectedBox = box;
            selectedBox.BorderStyle = BorderStyle.FixedSingle;

        }

        /// <summary>
        /// click load game on menu will load the game from selected file to the playboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult r = dlgLoad.ShowDialog();
            switch (r)
            {
                case DialogResult.OK:
                    try
                    {
                        if (pTile != null)
                        {
                            newGame();
                        }
                        string fileName = dlgLoad.FileName;
                        load(fileName);
                        txtBoxes.Text = countBox.ToString();
                        txtMove.Text = moves.ToString();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error in file load");
                    }
                    break;
                case DialogResult.Cancel:
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// click close on menu to close QGamePlayForm and back to QGameMainForm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Hide current QGamePlayForm
            this.Hide();
            //Declare variable for QGameMainForm 
            QGameMainForm main = new QGameMainForm();
            //Display QGameMainForm
            main.ShowDialog();
        }
        /// <summary>
        /// When click cancel on the form, the QGamePlayForm will be closed
        /// and return to QGameMainForm.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancel_Click(object sender, FormClosingEventArgs e)
        {
            //Hide current QGamePlayForm
            this.Hide();
            //Declare variable for QGameMainForm
            QGameMainForm main = new QGameMainForm();
            //Display QGameDesignForm
            main.ShowDialog();
        }

        /// <summary>
        /// Direction button clicked event will check if any box is selected and move the selected box,
        /// and count on moving steps.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDirection_Click(object sender, EventArgs e)
        {
            Button moveDirection = (Button)sender;
            if (selectedBox == null)
            {
                MessageBox.Show("You need to select a box first", "QGame", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            moves++;
            txtMove.Text = moves.ToString();
            switch (moveDirection.Text)
            {
                case LEFT:
                    moveHorizontally(NEGATIVE_FACTOR);
                    break;
                case RIGHT:
                    moveHorizontally(POSITIVE_FACTOR);
                    break;
                case UP:
                    moveVertically(NEGATIVE_FACTOR);
                    break;
                case DOWN:
                    moveVertically(POSITIVE_FACTOR);
                    break;
                default:
                    break;
            }
            checkIfWin();
        }
        /// <summary>
        /// Method will transfer a playboard slot to a tile with specified row and column
        /// </summary>
        /// <param name="r">row</param>
        /// <param name="c">column</param>
        /// <returns>return a tile or null if the slot is empty</returns>
        private Tile getTile(int r, int c)
        {
            Tile tileBox = pTile[r, c];
            if (tileBox != null)
            {
                return tileBox;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Method will check horizontally the next slot the box will move to,
        /// if it empty, box will move and box old position will be clear;
        /// if it is door of the same color, the box will be delete from the playboard;
        /// if it is others tiles, box will not move;
        /// count on boxes on playboard.
        /// </summary>
        /// <param name="movingFactor">integer that specifies the direction of horizontal movement
        /// Up movement: -1 & Down movement:1
        /// </param>
        private void moveHorizontally(int movingFactor)
        {
            try
            {
                Tile newPosition = getTile(selectedBox.Row, selectedBox.Col + movingFactor);

                while (newPosition == null)
                {
                    pTile[selectedBox.Row, selectedBox.Col] = null;
                    selectedBox.Col = selectedBox.Col + movingFactor;
                    selectedBox.Left = INIT_LEFT + (selectedBox.Width * selectedBox.Col);
                    pTile[selectedBox.Row, selectedBox.Col] = selectedBox;
                    newPosition = getTile(selectedBox.Row, selectedBox.Col + movingFactor);
                }
                if (newPosition != null)
                {
                    if (newPosition.ToolType == selectedBox.ToolType - DOOR_FACTOR)
                    {
                        pTile[selectedBox.Row, selectedBox.Col] = null;
                        this.Controls.Remove(selectedBox);
                        selectedBox = null;
                        countBox--;
                        txtBoxes.Text = countBox.ToString();
                    }

                }
            }
            catch (Exception)
            {

                MessageBox.Show("Can not move any more", "QGame", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        /// <summary>
        /// Method will check veritically the next slot the box will move to,
        /// if it empty, box will move and box old position will be clear;
        /// if it is door of the same color, the box will be delete from the playboard;
        /// if it is others content, box will not move;
        /// count on boxes on playboard.
        /// </summary>
        /// <param name="movingFactor">integer that specifies the direction of horizontal movement
        /// Left movement: -1 & Right movement:1</param>
        private void moveVertically(int movingFactor)
        {
            try
            {
                Tile newPosition = getTile(selectedBox.Row + movingFactor, selectedBox.Col);

                while (newPosition == null)
                {
                    pTile[selectedBox.Row, selectedBox.Col] = null;
                    selectedBox.Row = selectedBox.Row + movingFactor;
                    selectedBox.Top = INIT_TOP + (selectedBox.Height * selectedBox.Row);
                    pTile[selectedBox.Row, selectedBox.Col] = selectedBox;
                    newPosition = getTile(selectedBox.Row + movingFactor, selectedBox.Col);

                }

                if (newPosition != null)
                {
                    if (newPosition.ToolType == selectedBox.ToolType - DOOR_FACTOR)
                    {
                        pTile[selectedBox.Row, selectedBox.Col] = null;
                        this.Controls.Remove(selectedBox);
                        selectedBox = null;
                        countBox--;
                        txtBoxes.Text = countBox.ToString();
                    }
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Can not move any more", "QGame", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Method to clear playboard by removing all the tiles and number of moves and number of boxes
        /// make buttons unclickable
        /// </summary>
        private void newGame()
        {
            for (int i = 0; i < pTile.GetLength(0); i++)
            {
                for (int j = 0; j < pTile.GetLength(1); j++)
                {
                    this.Controls.Remove(pTile[i, j]);
                }
            }
            btnUp.Enabled = false;
            btnDown.Enabled = false;
            btnLeft.Enabled = false;
            btnRight.Enabled = false;
            moves = 0;
            countBox = 0;
            txtMove.Text = moves.ToString();
            txtBoxes.Text = countBox.ToString();
        }
        /// <summary>
        /// Method will check if user clear all the box on the playboard, if yes, user win.
        /// Message will be displayed and playboard will be cleared.
        /// </summary>
        private void checkIfWin()
        {
            if (countBox == 0)
            {
                DialogResult result = MessageBox.Show("Congraduation\nGame End", "QGame", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                if (result == DialogResult.OK)
                {
                    newGame();
                }
            }
        }

    }

}


