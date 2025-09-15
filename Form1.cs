using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace ToDoListWinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            //InitializeComponent();

            // Form settings
            this.Text = "📋 To-Do List App";
            this.Width = 650;
            this.Height = 500;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;

            // Fonts
            Font defaultFont = new Font("Segoe UI", 10, FontStyle.Regular);
            Font buttonFont = new Font("Segoe UI", 10, FontStyle.Bold);

            // Input controls
            Label lblTask = new Label() { Text = "Enter Task:", Left = 20, Top = 25, Width = 100, Font = defaultFont };
            TextBox txtTask = new TextBox() { Name = "txtTask", Left = 120, Top = 20, Width = 400, Font = defaultFont };
            Button btnAdd = new Button() { Text = "Add", Left = 530, Top = 18, Width = 80, Height = 30, BackColor = Color.LightGreen, Font = buttonFont };

            // Tasks list
            GroupBox grpTasks = new GroupBox() { Text = "Tasks", Left = 20, Top = 70, Width = 590, Height = 280, Font = defaultFont };
            ListBox lstTasks = new ListBox() { Name = "lstTasks", Left = 15, Top = 25, Width = 550, Height = 230, Font = new Font("Segoe UI", 11, FontStyle.Regular) };
            grpTasks.Controls.Add(lstTasks);

            // Action buttons
            Button btnExport = new Button() { Text = "Export", Left = 20, Top = 370, Width = 120, Height = 40, BackColor = Color.LightSkyBlue, Font = buttonFont };
            Button btnImport = new Button() { Text = "Import", Left = 160, Top = 370, Width = 120, Height = 40, BackColor = Color.MediumPurple, ForeColor = Color.White, Font = buttonFont };
            Button btnClear = new Button() { Text = "Clear", Left = 300, Top = 370, Width = 120, Height = 40, BackColor = Color.IndianRed, ForeColor = Color.White, Font = buttonFont };
            Button btnRemove = new Button() { Text = "Remove", Left = 440, Top = 370, Width = 120, Height = 40, BackColor = Color.OrangeRed, ForeColor = Color.White, Font = buttonFont };

            // Add controls
            this.Controls.Add(lblTask);
            this.Controls.Add(txtTask);
            this.Controls.Add(btnAdd);
            this.Controls.Add(grpTasks);
            this.Controls.Add(btnExport);
            this.Controls.Add(btnImport);
            this.Controls.Add(btnClear);
            this.Controls.Add(btnRemove);

            // Button events
            btnAdd.Click += (s, e) =>
            {
                if (!string.IsNullOrWhiteSpace(txtTask.Text))
                {
                    lstTasks.Items.Add(txtTask.Text);
                    txtTask.Clear();
                }
                else
                {
                    MessageBox.Show("⚠️ Please enter a task.");
                }
            };

            btnExport.Click += (s, e) =>
            {
                try
                {
                    string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tasks.txt");
                    File.WriteAllLines(filePath, GetTasksFromListBox(lstTasks));
                    MessageBox.Show($"✅ Tasks exported to {filePath}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"❌ Error while exporting: {ex.Message}");
                }
            };

            btnImport.Click += (s, e) =>
            {
                try
                {
                    string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tasks.txt");

                    if (File.Exists(filePath))
                    {
                        lstTasks.Items.Clear();

                        var lines = File.ReadAllLines(filePath);

                        if (lines.Length == 0)
                        {
                            MessageBox.Show("⚠️ File exists but it's empty.");
                            return;
                        }

                        foreach (var line in lines)
                        {
                            lstTasks.Items.Add(line);
                        }

                        MessageBox.Show($"✅ {lines.Length} tasks imported successfully!");
                    }
                    else
                    {
                        MessageBox.Show($"❌ File not found.\nLooked in: {filePath}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"❌ Error while importing: {ex.Message}");
                }
            };

            btnClear.Click += (s, e) =>
            {
                lstTasks.Items.Clear();
            };

            btnRemove.Click += (s, e) =>
            {
                if (lstTasks.SelectedIndex != -1)
                {
                    string removedTask = lstTasks.SelectedItem.ToString();
                    lstTasks.Items.RemoveAt(lstTasks.SelectedIndex);
                    MessageBox.Show($"🗑️ Removed task: {removedTask}");
                }
                else
                {
                    MessageBox.Show("⚠️ Please select a task to remove.");
                }
            };
        }

        private string[] GetTasksFromListBox(ListBox listBox)
        {
            string[] tasks = new string[listBox.Items.Count];
            for (int i = 0; i < listBox.Items.Count; i++)
            {
                tasks[i] = listBox.Items[i].ToString();
            }
            return tasks;
        }
    }
}
