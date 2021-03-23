
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DYanAssignment3
{
    /// <summary>
    /// Q-Puzzle Game Design Form class
    /// </summary>
    public partial class QGameDesignForm : Form
    {
        /// <summary>
        /// Constructor for Q-Puzzle QGameDesignForm
        /// </summary>
        public QGameDesignForm()
        {
            InitializeComponent();
        }

        //Declare vaiables  
        private Image imgNone = DYanAssignment3.Properties.Resources.none;
        private Image imgWall = DYanAssignment3.Properties.Resources.wall;
        private Image imgRedDoor = DYanAssignment3.Properties.Resources.red_door;
        private Image imgGreenDoor = DYanAssignment3.Properties.Resources.green_door;
        private Image imgRedBox = DYanAssignment3.Properties.Resources.red_box;
        private Image imgGreenBox = DYanAssignment3.Properties.Resources.green_box;

        private PictureBox toolImg;
        private PictureBox[,] pBox;

        private int rows = 0;
        private int columns = 0;
        private int countWall = 0;
        private int countDoor = 0;
        private int countBox = 0;

        //To story Tool:None is 0, Wall is 1, RedDoor is 2
        //              GreenDoor is 3, RedBox is 4, GreenBox is 5
        private enum Tool { None, Wall, RedDoor, GreenDoor, RedBox, GreenBox };

        /// <summary>
        /// The method will generate a 2D picturebox array playboard,
        /// each picture box has an initial image --imgNone,
        /// will also create picturebox click event.
        /// </summary>
        /// <param name="r">The rows of 2D picturebox array</param>
        /// <param name="c">The columns of 2D picturebox array</param>
        private void generatePlayboard(int r, int c)
        {
            //Declare local variables on picture boxs size and location
            const int P_WIDTH= 50;
            const int P_HEIGHT = 50;
            const int P_XLOCATION = 50;
            const int P_YLOCATION = 50;
            const int P_XLOCATIONEXT = 180;
            const int P_YLOCATIONEXT = 80;

            pBox = new PictureBox[rows, columns];
            for (int i = 0; i < pBox.GetLength(0); i++)
            {
                for (int j = 0; j < pBox.GetLength(1); j++)
                {
                    pBox[i, j] = new PictureBox();
                    pBox[i, j].Image = imgNone;
                    pBox[i, j].Location = new Point(j * P_XLOCATION + P_XLOCATIONEXT, i * P_YLOCATION + P_YLOCATIONEXT);
                    pBox[i, j].Width = P_WIDTH;
                    pBox[i, j].Height = P_HEIGHT;
                    pBox[i, j].Visible = true;
                    pBox[i, j].BorderStyle = BorderStyle.Fixed3D;
                    pBox[i, j].BringToFront();

                    //Define pictureBox click event
                    pBox[i, j].Click += new EventHandler(pictureBox_Click);
                    //Add pictureBox to the panel
                    this.Controls.Add(pBox[i, j]);
                }
            }

        }
        /// <summary>
        /// When click on "Generate" button, a 2D picture box array playboard will display on the pannel
        /// The picture box array's rows and columns depend on user's inputs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                //Validate the inputs
                rows = Convert.ToInt32(txtRows.Text);
                columns = Convert.ToInt32(txtColumns.Text);
            }
            catch (Exception)
            {

                MessageBox.Show("Please provides valid data for rows and columns" +
                    "[Both must be integers]", "QGame", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Generate pictureBox playboard on pannel with initial image imgNone
            generatePlayboard(rows, columns);
            
        }
        /// <summary>
        /// Each time picture box on the pannel is clicked, the corresponding image from previously 
        /// clicked the button will display. The image from button click was stored in a picture box--toolImg.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox_Click(object sender, EventArgs e)
        {
            PictureBox picture = (PictureBox)sender;
            //Make the image fit into picture box size
            picture.SizeMode = PictureBoxSizeMode.StretchImage;
            //Store image to picture boxes on the pannel from picture box toolImg
            picture.Image = toolImg.Image;
        }
        /// <summary>
        /// Each time click on a specific button, the corresponding image will store
        /// in the picuture box-- toolImg. Each image index represents a tool.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolButton_Click(object sender, EventArgs e)
        {
            toolImg = new PictureBox();
            Button tool = (Button)sender;
            switch (tool.ImageIndex)
            {
                case (int)Tool.None:
                    toolImg.Image = imgNone;
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
                    break;
                case (int)Tool.GreenBox:
                    toolImg.Image = imgGreenBox;
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// The method will record the playboard (picture box matrix) rows and columns,
        /// total number of walls,
        /// total number of doors,
        /// total number of boxes,
        /// and each cell's row, column and content will save to the specific file.
        /// </summary>
        /// <param name="fileName">The file name to save the playboard and cells data</param>
        private void save(string fileName)
        {
            StreamWriter writer = new StreamWriter(fileName);
            writer.WriteLine(rows);
            writer.WriteLine(columns);
            for (int i = 0; i < pBox.GetLength(0); i++)
            {
                for (int j = 0; j < pBox.GetLength(1); j++)
                {
                    if (pBox[i, j].Image == imgNone)
                    {
                        writer.WriteLine(i);
                        writer.WriteLine(j);
                        writer.WriteLine((int)Tool.None);
                    }
                    if (pBox[i, j].Image == imgWall)
                    {
                        countWall++;
                        writer.WriteLine(i);
                        writer.WriteLine(j);
                        writer.WriteLine((int)Tool.Wall);

                    }
                    if (pBox[i, j].Image == imgRedDoor)
                    {
                        countDoor++;
                        writer.WriteLine(i);
                        writer.WriteLine(j);
                        writer.WriteLine((int)Tool.RedDoor);
                    }
                    if (pBox[i, j].Image == imgGreenDoor)
                    {
                        countDoor++;
                        writer.WriteLine(i);
                        writer.WriteLine(j);
                        writer.WriteLine((int)Tool.GreenDoor);

                    }
                    if (pBox[i, j].Image == imgRedBox)
                    {
                        countBox++;
                        writer.WriteLine(i);
                        writer.WriteLine(j);
                        writer.WriteLine((int)Tool.RedBox);
                    }
                    if (pBox[i, j].Image == imgGreenBox)
                    {
                        countBox++;
                        writer.WriteLine(i);
                        writer.WriteLine(j);
                        writer.WriteLine((int)Tool.GreenBox);
                    }
                }
            }
            writer.Close();

        }
        /// <summary>
        /// Save click event, will save playboard and cell information to specific file location with specific name
        /// which are both identified by user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult r = dlgSave.ShowDialog();
            switch (r)
            {
                case DialogResult.None:
                    break;
                case DialogResult.OK:
                    try
                    {
                        string fileName = dlgSave.FileName;
                        save(fileName);
                        MessageBox.Show("File is saved successfully." + Environment.NewLine
                                       + "Total number of walls: " + countWall + Environment.NewLine
                                       + "Total number of doors: " + countDoor + Environment.NewLine
                                       + "Total number of boxes: " + countBox, "QGame",
                                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error in file save");
                    }
                    break;
                case DialogResult.Cancel:
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// When click "Close" on menu, the QGameDesignForm will be closed
        /// and return to QGameMainForm.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Hide current QGameDesignForm
            this.Hide();
            //Declare variable for QGameMainForm 
            QGameMainForm main = new QGameMainForm();
            //Display QGameDesignForm
            main.ShowDialog();
        }
        /// <summary>
        /// When click cancel on the form, the QGameDesignForm will be closed
        /// and return to QGameMainForm.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancel_Click(object sender, FormClosingEventArgs e)
        {
            //Hide current QGameDesignForm
            this.Hide();
            //Declare variable for QGameMainForm
            QGameMainForm main = new QGameMainForm();
            //Display QGameDesignForm
            main.ShowDialog();
        }
    }
}
