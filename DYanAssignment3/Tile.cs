
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DYanAssignment3
{
    /// <summary>
    /// Tile class inheritant from PictureBox class
    /// </summary>
    public class Tile:PictureBox
    {
        //declare fields
        private int row;
        private int col;
        private int toolType;
        /// <summary>
        /// Tile Class contructor
        /// </summary>
        public Tile() { }
     
        //properties
        public int Row { get => row; set => row = value; }
        public int Col { get => col; set => col = value; }
        public int ToolType { get => toolType; set => toolType = value; } 
    }
}
