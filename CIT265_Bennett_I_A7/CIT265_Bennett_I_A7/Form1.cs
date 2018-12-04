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
        //public int indexInMain=0;
        int selectionIndex = 0;
        public Form1()
        {

            InitializeComponent();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                string fileName = textBox1.Text;

                /*if (File.Exists(fileName)){

                    GetInformation(fileName);

                    try
                    {
                        using(var stream = new StreamReader(fileName))
                        {
                            outputTextBox.AppendText(stream.ReadToEnd());
                        }
                    }
                    catch (IOException)
                    {
                        MessageBox.Show("Error reading from file", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }*/
                if (Directory.Exists(fileName)){
                    //GetInformation(fileName);

                    string[] directoryList = Directory.GetDirectories(fileName);
                    //outputTextBox.AppendText("Directory contents:\n");

                    foreach (var directory in directoryList)
                    {
                        //outputTextBox.AppendText($"{directory}\n");
                        listBox1.Items.Add(directory);
                    }

                }
                else
                {
                    MessageBox.Show($"{textBox1.Text} does not exist", "File error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            
        }

       /* private void GetInformation(string filename)
        {
            outputTextBox.Clear();

            outputTextBox.AppendText($"{filename} exists\n" + Environment.NewLine);

            outputTextBox.AppendText($"Created {File.GetCreationTime(filename)}\n" + Environment.NewLine);

            outputTextBox.AppendText($"Last modified: {File.GetLastWriteTime(filename)}\n" + Environment.NewLine);
            outputTextBox.AppendText($"Last accessed: {File.GetLastAccessTime(filename)}\n" + Environment.NewLine);
        }
        */
        private async void listBox1_DoubleClick(object sender, EventArgs e)
        {
            string subDirectory = listBox1.SelectedItem.ToString();
            selectionIndex = listBox1.SelectedIndex;
            if(subfolderView == false)
            {

                
                //outputTextBox.Clear();
                
                //outputTextBox.AppendText($"{subDirectory}");
                
                string[] directoryList = Directory.GetDirectories(subDirectory);
                //outputTextBox.AppendText("Directory contents:\n");
                
                foreach (var directory in directoryList)
                {
                
                    //outputTextBox.AppendText($"{directory}\n");
                
                    listBox1.Items.Insert(selectionIndex, directory);
                }
            }
            else
            {
                await GetSubFolder(subDirectory);
                
            }
        }

        /*private void GetSubFolder(string inputFolder, ref int indexInMain)
        {
            

            string[] directoryList = Directory.GetDirectories(inputFolder);
            //outputTextBox.AppendText("Directory contents:\n");
            foreach (var directory in directoryList)
            {
                indexInMain++;
                //outputTextBox.AppendText($"{directory}\n");
                listBox1.Items.Insert(indexInMain, directory);
                GetSubFolder(directory, ref indexInMain);
            }
        }*/

        private async Task GetSubFolder(string inputFolder)
        {
           
              
            string[] directoryList = Directory.GetDirectories(inputFolder);
            //outputTextBox.AppendText("Directory contents:\n");
            foreach (var directory in directoryList)
            {
                //outputTextBox.AppendText($"{directory}\n");
                listBox1.Items.Insert(selectionIndex, directory);
                selectionIndex++;
                
                await GetSubFolder(directory);
                
                
            }
            
        }

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
    }
}
