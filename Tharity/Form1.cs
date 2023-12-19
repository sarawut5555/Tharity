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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.Remoting.Contexts;
using System.Diagnostics;

namespace Tharity
{
    public partial class Form1 : Form
    {
        String currentFilePath = "";
        public Form1()
        {
            InitializeComponent();
        }
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = "C:\\"; // ตำแหน่งเริ่มต้น
            fileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"; 
            fileDialog.FilterIndex = 1; 
            fileDialog.RestoreDirectory = true;
            DialogResult result = fileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string selectedFilePath = fileDialog.FileName;
                string fileContent = File.ReadAllText(selectedFilePath);
                textBox1.Text = fileContent;
            }
        }
        private void sToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveDocument();
        }
        private void SaveDocument()
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {
                SaveAsDocument();
            }
            else
            {
                File.WriteAllText(currentFilePath, textBox1.Text);
            }
        }

        private void SaveAsDocument()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files|*.txt";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                currentFilePath = saveFileDialog.FileName;
                File.WriteAllText(currentFilePath, textBox1.Text);
            }
        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAsDocument();
        }
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Undo();
        }
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Cut();
        }
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Copy();
        }
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Paste();
        }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectionLength > 0)
            {
                textBox1.SelectedText = "";
            }
            else
            {
                int currentSelectionStart = textBox1.SelectionStart;
                if (currentSelectionStart < textBox1.Text.Length)
                {
                    textBox1.Text = textBox1.Text.Remove(currentSelectionStart, 1);
                }
            }
        }
        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Font = new Font(textBox1.Font.FontFamily, textBox1.Font.Size + 10);
        }
        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.Font.Size > 10) 
            {
                textBox1.Font = new Font(textBox1.Font.FontFamily, textBox1.Font.Size - 5);
            }
        }
        private void restoreDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Font = new Font("Microsoft Sans Serif", 8.25f);
        }
        private void helpTharityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Sarawut kaikaew\n" +
                "พัฒนาโดย\n" +
                "นายศราวุฒิ ข่ายแก้ว\n" +
                "เวอร์ชั่น 1.0\n" +
                "ข้อมูล\n" +
                "นักศึกษาระดับชั้น : ประกาศนียบัตรวิชาชีพชั้นสูง\n" +
                "กำลังศึกษา : วิทยาลัยเทคนิคเชียงใหม่\n" +
                "สาขา : เทคโนโลยีสารสนเทศ\n" +
                "แผนก: นักพัฒนาซอฟต์แวร์",
                "About Thariity",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                int digitCount = 0;
                int letterCount = 0;

                // วนลูปผ่านทุกตัวอักษรใน TextBox
                foreach (char character in textBox1.Text)
                {
                    // ตรวจสอบว่าเป็นตัวเลขหรือไม่
                    if (char.IsDigit(character))
                    {
                        digitCount++;
                    }
                    // ตรวจสอบว่าเป็นตัวอักษรหรือไม่
                    else if (char.IsLetter(character))
                    {
                        letterCount++;
                    }
                }
                toolStripStatusLabel1.Text = $"Digits: {digitCount}, Letters: {letterCount}";
            }
            else
            {
                toolStripStatusLabel1.Text = "No input";
            }
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofileDialog = new OpenFileDialog();
            ofileDialog.InitialDirectory = "C:\\"; // ตำแหน่งเริ่มต้น
            ofileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            ofileDialog.FilterIndex = 1;
            ofileDialog.RestoreDirectory = true;
            DialogResult result = ofileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string selectedFilePath = ofileDialog.FileName;
                string fileContent = File.ReadAllText(selectedFilePath);
                textBox1.Text = fileContent;
            }
        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            PrintDialog pDialog = new PrintDialog();
            if(pDialog.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void editToFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            DialogResult result = fontDialog.ShowDialog();

            // ตรวจสอบว่าผู้ใช้เลือก Font และกด OK หรือไม่
            if (result == DialogResult.OK)
            {
                // ดึง Font ที่ผู้ใช้เลือก
                Font selectedFont = fontDialog.Font;

                // ตั้งค่า Font ของ TextBox เป็น Font ที่เลือก
                textBox1.Font = selectedFont;
            }
        }
    }
}
