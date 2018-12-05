/*
 * Name: Isaac Bennett
 * Class: Cit265
 * Professor: Davide Mauro
 * Assignment: #7
 * 
 * 
 * 
 * */


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

namespace CIT265_Bennett_I_A7
{
    public partial class Form1 : Form
    {

        FolderBrowserDialog folderDialogue;
        public bool subfolderView = false;
        int selectionIndex = 0;

        public Form1()
        {

            InitializeComponent();

        }


        //Main call for adding a new folder or file
        private async void listBox1_DoubleClick(object sender, EventArgs e)
        {

            string subDirectory = listBox1.SelectedItem.ToString();
            selectionIndex = listBox1.SelectedIndex;

            //Checks if it has to be recursive or not;
            if(subfolderView == false)
            {
                //Add this sub folder's files to the listbox
                string[] fileList = Directory.GetFiles(subDirectory);
                foreach (var file in fileList)
                {
                    listBox1.Items.Insert(selectionIndex, file);
                    selectionIndex++;
                }
                //The listbox1 handler for some reason becomes dominated by the above function, performing refresh fixes this.
                listBox1.Refresh();

                //Add this sub folder's files to the listbox
                string[] directoryList = Directory.GetDirectories(subDirectory);

                
                foreach (var directory in directoryList)
                {
                    //This function call inserts a new string into the listbox's item list. This is used multiple times
                    listBox1.Items.Insert(selectionIndex, directory);
                }
                listBox1.Refresh();
            }
            else
            {
                //Calls the start of the recursive display
                await GetSubFolder(subDirectory);
            }
        }

        //Copy of the submit function to allow for better UX
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                //Pull specified directory
                string fileName = textBox1.Text;
                //Resets listbox
                listBox1.Items.Clear();

                //If the directory exists at all
                if (Directory.Exists(fileName))
                {

                    //Generate the list of items.
                    string[] directoryList = Directory.GetDirectories(fileName);

                    //Add the starting folders to the listBox
                    foreach (var directory in directoryList)
                    {
                        listBox1.Items.Add(directory);
                    }

                }
                //If directory does not exist

                else
                {
                    MessageBox.Show($"{textBox1.Text} does not exist", "File error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //Recursive file and folder display function, largest function weight wise.
        private async Task GetSubFolder(string inputFolder)
        {
            //Add this sub folder's files to the listbox
            string[] fileList = Directory.GetFiles(inputFolder);
            foreach (var file in fileList)
            {
                listBox1.Items.Insert(selectionIndex, file);
                selectionIndex++;
            }
            //The listbox1 handler for some reason becomes dominated by the above function, performing refresh fixes this.
            listBox1.Refresh();

            //Adds the subfolders to the list
            //This line gets a list of the directories
            string[] directoryList = Directory.GetDirectories(inputFolder);
            foreach (var directory in directoryList)
            {
                
                listBox1.Items.Insert(selectionIndex, directory);
                selectionIndex++;

                //Calls the next sub folder asynchronously, allowing for the recursive feature
                
                await Task.Run(()=> GetSubFolder(directory));
                
                
            }
            

        }

        //Switches on and off the boolean for subfolderview (recursive display)
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(subfolderView == false)
            {
                subfolderView = true;
            }
            else
            {
                subfolderView = false;
            }
        }

        //Enter button for submitting the directory name
        private void button1_Click(object sender, EventArgs e)
        {
            //Pull specified directory
            string fileName = textBox1.Text;
            //Resets listbox
            listBox1.Items.Clear();

            //If the directory exists at all
            if (Directory.Exists(fileName))
            {
                
                //Generate the list of items.
                string[] directoryList = Directory.GetDirectories(fileName);
                
                //Add the starting folders to the listBox
                foreach (var directory in directoryList)
                {
                    listBox1.Items.Add(directory);
                }

            }
            //If directory does not exist

            else
            {
                MessageBox.Show($"{textBox1.Text} does not exist", "File error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
