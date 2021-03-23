/*
 * Assignment 3
 * 
 * programmer: Dong Yan
 * 
 * Revision History: 2020-10-20: created
 *                   2020-11-25: revised
 * 
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DYanAssignment3
{/// <summary>
 /// Q-Puzzle Game Main Form class
 /// </summary>
    public partial class QGameMainForm : Form
    {
        /// <summary>
        /// Constructor of Q-puzzle QGameMainForm
        /// </summary>
        public QGameMainForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Click on Design button, the display pannel will
        /// show QGameDesignForm and hidde current QGameMainForm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDesign_Click(object sender, EventArgs e)
        {
            //Hide current QGameMainForm
            this.Hide();
            //Declare variable for QGameDesignForm 
            QGameDesignForm design = new QGameDesignForm();
            //Display QGameDesignForm
            design.ShowDialog();
        }
        /// <summary>
        /// Exit button will close current program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            //Close QGameMainForm and running program
            System.Windows.Forms.Application.ExitThread();
        }
        /// <summary>
        /// Click cancel on the form will close current program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancel_Click(object sender, FormClosingEventArgs e)
        {
            //Close QGameMainForm and running program
            System.Windows.Forms.Application.ExitThread();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            //Hide current QGameMainForm
            this.Hide();
            //Declare variable for QGameDesignForm 
            QGamePlayForm play = new QGamePlayForm();
            //Display QGameDesignForm
            play.ShowDialog();
        }
    }
}
