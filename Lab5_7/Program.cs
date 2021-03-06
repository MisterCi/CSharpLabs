using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace Lab5_7    
{
    static class Program
    {
        static public Form mainWin;

        [STAThread]
        static void Main(string[] args)
        {
            mainWin = new MainWindow();
            mainWin.IsMdiContainer = true;
            Application.Run(mainWin);
        }
    }

    public class MainWindow : Form
    {
        private StatusStrip statStrip;

        public MainWindow()
        {
            MenuStrip Menu = new MenuStrip();

            ToolStripMenuItem FileIt = new ToolStripMenuItem("File");
            ToolStripMenuItem DataIt = new ToolStripMenuItem("Data");
            ToolStripMenuItem AboutIt = new ToolStripMenuItem("About");
            ToolStripMenuItem ExitIt = new ToolStripMenuItem("Exit");
            ToolStripMenuItem SupplierIt = new ToolStripMenuItem("Supplier");
            ToolStripMenuItem PartIt = new ToolStripMenuItem("Part");
            ToolStripMenuItem ProjectIt = new ToolStripMenuItem("Project");
            ToolStripMenuItem DeliveryIt = new ToolStripMenuItem("Delivery");
            ToolStripMenuItem WinIt = new ToolStripMenuItem("Windows");

            FileIt.DropDownItems.Add(ExitIt);

            AboutIt.Click += aboutF;
            ExitIt.Click += exitF;
            SupplierIt.Click += SupplierF;
            PartIt.Click += PartF;
            ProjectIt.Click += ProjectF;
            DeliveryIt.Click += DeliveryF;

            DataIt.DropDownItems.Add(SupplierIt);
            DataIt.DropDownItems.Add(PartIt);
            DataIt.DropDownItems.Add(ProjectIt);
            DataIt.DropDownItems.Add(DeliveryIt);

            Menu.Items.Add(FileIt);
            Menu.Items.Add(DataIt);
            Menu.Items.Add(WinIt);
            Menu.Items.Add(AboutIt);

            statStrip = new StatusStrip();
            ToolStripStatusLabel
            statLabel = new ToolStripStatusLabel();
            statStrip.Items.Add(statLabel);
            statLabel = new ToolStripStatusLabel();
            statStrip.Items.Add(statLabel);
            Controls.Add(statStrip);
            Controls.Add(Menu);
            statStrip.Items[0].Text = "³��� ������";
            statStrip.Items[1].Text = "ĳ������ ���� ����";
        }

        private bool shouldIOpen(string text)
        {
            for (int i = 0; i < MdiChildren.Length; i++)
            {
                if (this.MdiChildren[i].Name == text)
                {
                    MdiChildren[i].Activate();
                    return false;
                }
            }
            return true;
        }
        private void SupplierF(object sender, EventArgs a)
        {
            Console.WriteLine("����������");
            string id = "supplier";

            if (shouldIOpen(id))
            {
                statStrip.Items[1].Text = "³��� ���������� �� ������";
                Form z = new Window2(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/s.csv", Encoding.UTF8);
                z.MdiParent = Program.mainWin;
                z.Text = "����������";
                z.Name = id;
                z.Show();
            }
            statStrip.Items[1].Text = "���������� ���������";
        }
        private void PartF(object sender, EventArgs a)
        {
            Console.WriteLine("�����");
            string id = "part";

            if (shouldIOpen(id))
            {
                statStrip.Items[1].Text = "³��� ������� �� ������";
                Form z = new Window2(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/p.csv", Encoding.UTF8);
                z.MdiParent = Program.mainWin;
                z.Text = "������";
                z.Name = id;
                z.Show();
            }
            statStrip.Items[1].Text = "����� ����������";
        }
        private void ProjectF(object sender, EventArgs a)
        {
            Console.WriteLine("�������");
            string id = "project";
            
            if (shouldIOpen(id))
            {
                statStrip.Items[1].Text = "³��� ������� �� ������";
                Form z = new Window2(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/j.csv", Encoding.UTF8);
                z.MdiParent = Program.mainWin;
                z.Text = "�������";
                z.Name = id;
                z.Show();
            }

            statStrip.Items[1].Text = "������� ����������";
        }
        private void DeliveryF(object sender, EventArgs a)
        {
            Console.WriteLine("��������");
            statStrip.Items[1].Text = "³��� �������� �� ������";
            string id = "delivery";

            if (shouldIOpen(id))
            {
                Form z = new Window2(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/spj.csv", Encoding.UTF8);
                z.MdiParent = Program.mainWin;
                z.Text = "��������";
                z.Name = id;
                z.Show();
            }

            statStrip.Items[1].Text = "�������� �����������";
        }
        private void aboutF(object sender, EventArgs a)
        {
            Console.WriteLine("About");
            statStrip.Items[1].Text = "³������ ���� About";
            
            MessageBox.Show("This programm was made by ImiaRek, Kiev 201?.");
            statStrip.Items[1].Text = "�������� ���� ����";
        }
        private void exitF(object sender, EventArgs x)
        {
            Console.WriteLine("FormClosing");
            MessageBox.Show("exit");
            Close();
        }
    }

    public class Field
    {
        public string name;
        public string def;
        public string value;

        public Field(string name, string def)
        {
            this.name = name;
            this.def = def;
            value = string.Empty;
        }
    }

    public class WindowFields : OkCancel
    {
        Dictionary<TextBox, Field> dictionaryFields = null;

        public WindowFields(string Title, Field[] fields) : base(Title)
        {
            dictionaryFields = new Dictionary<TextBox, Field>(); // ������� ������ ���

            for (int i = fields.Length - 1; i >= 0; i--)
            {
                dictionaryFields.Add(addFld(i, fields[i]), fields[i]);
            }

            okButton.Click += _KeyDown;
        }
        public TextBox addFld(int n, Field par) // ����� ��������� � ���� ����� ���� �����
        {
            int h = 32;

            Panel p = new Panel(); // ������ �������� ����� � ���� �����
            p.Name = string.Format("p{0}", n);
            p.BorderStyle = BorderStyle.FixedSingle;
            p.Size = new Size(1, h);
            p.AutoSize = true;
            p.Dock = DockStyle.Top;
            p.Padding = new Padding(0, 0, 0, 8);

            Panel p1 = new Panel(); // ������ ��� ������
            p1.BorderStyle = BorderStyle.FixedSingle;
            p1.Size = new Size(1, h);
            p1.Dock = DockStyle.Top; /// <--------
            p1.BackColor = Color.Blue;
            p.Controls.Add(p1);

            Label l1;
            l1 = new Label();
            l1.Name = string.Format("l{0}", n);
            l1.Size = new Size(172, h); ;
            l1.Text = par.name; // ������� ����� ������
            l1.Dock = DockStyle.Right;
            // Console.Error.WriteLine ("new field: �{0}�/�{1}�/�{2}�"
            // , par.name, l1.Text, par.value);
            l1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            p.Controls.Add(l1);

            TextBox t1;
            t1 = new TextBox();
            t1.TabIndex = n;
            t1.Name = string.Format("t{0}", n); ;
            t1.Size = new Size(162, h);
            t1.Text = par.def;
            t1.Dock = DockStyle.Right;
            p.Controls.Add(t1);

            Controls.Add(p); /// ������� ���� �����

            return t1;
        }
        void _KeyDown(object sender, EventArgs e)
        {
            TextBox tb;
            Field f;
            foreach (KeyValuePair<TextBox, Field> Item in dictionaryFields)
            {
                tb = Item.Key;
                f = Item.Value;
                f.value = tb.Text;
                tb.Text = f.def; /// � ������ ����������������� �������� �� ���������.
                Console.Error.WriteLine("keyDown: �{0}�:�{1}� ", f.name, f.value);
            }
        }

    }

    public class OkCancel : Form
    {
        protected Button okButton;
        protected Button cancleButton;

        public OkCancel(string text)
        {
            Text = text;
            int p = 10;
            Padding = new Padding(p, p, p, p);

            FormBorderStyle = FormBorderStyle.FixedDialog;
            MinimizeBox = false;
            MaximizeBox = false;
            ControlBox = false;
            AutoScroll = false;
            AutoSize = true;
            Size = new Size(370, 200);

            Panel p0 = new Panel(); // ��� ������ ������ ������ ������
            p0.Size = new Size(1, 32);
            p0.BorderStyle = BorderStyle.Fixed3D;
            p0.BackColor = Color.Green;
            Controls.Add(p0);

            CreateOkButton();

            p0.Size = new Size(112, 32);
            p0.Dock = DockStyle.Right;
            p0.BorderStyle = BorderStyle.Fixed3D;
            p0.BackColor = Color.Red;
            Controls.Add(p0);

            CreateCancleButton();

            DialogResult = DialogResult.Cancel;
            AcceptButton = okButton; // ������� ������� Enter ��� �� ��
            CancelButton = cancleButton; // ������� ������� Esc ��� �� Cancel
            StartPosition = FormStartPosition.CenterScreen;
        }

        public void CreateOkButton()
        {
            okButton = new Button();
            okButton.Size = new Size(112, 32);
            okButton.Dock = DockStyle.Bottom;
            okButton.Text = "Ok";
            okButton.DialogResult = DialogResult.OK;
            Controls.Add(okButton);
        }

        public void CreateCancleButton()
        {
            cancleButton = new Button();
            cancleButton.Size = new Size(112, 32);
            cancleButton.Dock = DockStyle.Bottom;
            cancleButton.Text = "Cancel";
            cancleButton.DialogResult = DialogResult.Cancel;
            Controls.Add(cancleButton);
        }
    }

    public class Window : Form
    {
        protected Encoding encoding;
        protected string fileName;
        protected DataGridView dataGridView = new DataGridView();

        public Window(string fileName, Encoding encoding)
        {
            this.fileName = fileName;
            this.encoding = encoding;

            DataGridViewColumn dataGridViewColumn = new DataGridViewColumn();
            dataGridViewColumn.Name = "Col 1";

            dataGridView.ReadOnly = true;
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.BackgroundColor = Color.White;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            Load += fLoad;

            Controls.Add(dataGridView);
        }

        private void fLoad(object sender, EventArgs a)
        {
            Console.WriteLine("Load");
            ReadFile(fileName);
        }

        public void ReadFile(string path)
        {
            string str;
            string[] fields;
            bool firsttime = true;

            using (StreamReader fileStream = new StreamReader(path, encoding))
            {
                while ((str = fileStream.ReadLine()) != null)
                {
                    fields = str.Split(';');

                    if (fields.Length <= 0)
                    {
                        continue;
                    }

                    if (firsttime)
                    {
                        dataGridView.ColumnCount = fields.Length;

                        for (int i = 0; i < fields.Length; i++)
                        {
                            dataGridView.Columns[i].Name = String.Format("Pole{0}", i + 1);
                        }

                        firsttime = false;
                    }

                    dataGridView.Rows.Add(fields);
                }
            }
        }
    }

    public class Window2 : Window
    {
        protected ToolStrip toolStrip = new ToolStrip();
        protected StatusStrip statStrip;

        public Window2(string fileName, Encoding encoding) : base(fileName, encoding)
        {
            statStrip = new StatusStrip();

            ToolStripStatusLabel statLabel = new ToolStripStatusLabel();
            statLabel.Text = "³��� ������";
            statStrip.Items.Add(statLabel);
            Controls.Add(statStrip);

            toolStrip.Size = new Size((int)(200 / 3), (int)(40));

            ToolStripButton tlbExit = new ToolStripButton("Exit");
            tlbExit.ToolTipText = "������� ���� �������";

            ToolStripButton tIns = new ToolStripButton("Insert");
            tIns.ToolTipText = "������ �����";

            ToolStripButton tEdit = new ToolStripButton("Edit");
            tEdit.ToolTipText = "���������� �����";

            ToolStripButton tDel = new ToolStripButton("Delete");
            tDel.ToolTipText = "�������� �����";

            ToolStripButton tSave = new ToolStripButton("Save");
            tSave.ToolTipText = "�������� ����";

            ToolStripButton tExp = new ToolStripButton("Export");
            tExp.ToolTipText = "������������ �������";

            Padding = new Padding(2);
            toolStrip.Items.AddRange(new ToolStripButton[] {
                tIns,
                tEdit,
                tDel,
                tSave,
                tExp,
                tlbExit
            });

            toolStrip.ItemClicked += ToolStripButtonClick;
            toolStrip.Dock = DockStyle.Top;
            Controls.Add(toolStrip);
            Load += fLoad;
        }
        protected void fLoad(object sender, EventArgs args)
        {
            statStrip.Items[0].Text = string.Format("{0} records has been red", dataGridView.Rows.Count);
        }
        protected void ToolStripButtonClick(object sender, ToolStripItemClickedEventArgs args)
        {
            string buttonText = args.ClickedItem.Text;
            statStrip.Items[0].Text = string.Format("You've pressed {0} button", buttonText);

            if (buttonText == "Exit")
            {
                Close();
            }
            else if (buttonText == "Open")
            {
                ;
            }
            else
            {
                MessageBox.Show("Action not ready!", "Warning");
            }
        }
    }

    public class Window3 : Window2
    {
        public Window3(string fnm, Encoding encoding) : base(fnm, encoding)
        {
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.RowHeadersVisible = false;
            toolStrip.ItemClicked -= base.ToolStripButtonClick;
            toolStrip.ItemClicked += ToolBarButtonClick3;
            dataGridView.CellDoubleClick += dc2;
            dataGridView.PreviewKeyDown += _PreviewKeyDown;
            dataGridView.KeyDown += _KeyDown;
        }

        protected void _PreviewKeyDown(object sender, PreviewKeyDownEventArgs args)
        {
            if (args.KeyCode == Keys.Enter)
            {
                Console.WriteLine("CellDoubleClick: selrow / row: {0} ", dataGridView.CurrentRow.Index);
                doEdit();
            }
        }

        protected void _KeyDown(object sender, KeyEventArgs args)
        {
            if (args.KeyCode == Keys.Enter)
            {
                args.SuppressKeyPress = true;
            }
        }

        protected void dc2(object sender, DataGridViewCellEventArgs args)
        {
            Console.WriteLine("CellDoubleClick: selrow / row: {0}/{1} ", dataGridView.CurrentRow.Index, args.RowIndex);

            if (args.RowIndex >= 0)
            {
                doEdit();
                dataGridView.CurrentCell = dataGridView.Rows[args.RowIndex].Cells[0];
            }
        }

        protected void ToolBarButtonClick3(object sender, ToolStripItemClickedEventArgs args)
        {
            string buttonName = args.ClickedItem.Text;

            statStrip.Items[0].Text = string.Format("�� ��������� {0} ������", buttonName);

            if (buttonName == "Exit")
            {
                Close();
            }
            else if (buttonName == "Insert")
            {
                doInsert();
            }
            else if (buttonName == "Edit")
            {
                doEdit();
            }
            else if (buttonName == "Delete")
            {
                doDelete();
            }
            else if (buttonName == "Save")
            {
                doSave();
            }
            else
            {
                base.ToolStripButtonClick(sender, args);
            }
        }

        public void doEdit()
        {
            string answer = "";

            if (dataGridView.RowCount > 0)
            {
                DataGridViewRow dataGridViewRow = dataGridView.Rows[dataGridView.CurrentRow.Index];
                DataGridViewCellCollection row = dataGridViewRow.Cells;

                List<Field> fields = new List<Field>();
                Field field;

                for (int i = 0; i < dataGridView.ColumnCount; i++)
                {
                    field = new Field(dataGridView.Columns[i].Name, (row[i]).Value.ToString());
                    fields.Add(field);
                }

                if (fields.Count > 0)
                {
                    WindowFields w = new WindowFields("������ �������� �����", fields.ToArray());

                    DialogResult rc = w.ShowDialog();

                    if (rc == DialogResult.OK)
                    {
                        for (int i = 0; i < fields.Count; i++)
                        {
                            dataGridViewRow.Cells[i].Value = fields[i].value;
                        }
                    }
                }
            }
            else
            {
                answer = "ͳ���� ��������!";
            }

            Console.WriteLine("������: '{0}'", answer);
            statStrip.Items[0].Text = answer;
        }

        public void doDelete()
        {
            string answer = "";

            if (dataGridView.RowCount > 0)
            {
                foreach (DataGridViewRow item in dataGridView.SelectedRows)
                {
                    dataGridView.Rows.RemoveAt(item.Index);
                }
            }
            else
            {
                answer = "ͳ���� ��������!";
            }

            Console.WriteLine("��������: '{0}'", answer);
            statStrip.Items[0].Text = answer;
        }

        public void doSave()
        {
            saveIntoFile(fileName);
            Console.WriteLine("Data saved!");
            statStrip.Items[0].Text = "Data saved!";
        }

        public void doInsert()
        {
            Field field;
            List<Field> fields = new List<Field>();

            for (int i = 0; i < dataGridView.ColumnCount; i++)
            {
                field = new Field(dataGridView.Columns[i].Name, "");
                fields.Add(field);
            }

            if (fields.Count > 0)
            {
                Form w = new WindowFields("������ ����� �����", fields.ToArray());
                DialogResult dialogResult = w.ShowDialog();

                if (dialogResult == DialogResult.OK)
                {
                    Console.Error.WriteLine("doIns: ������� ����: '{0}' ", fields.Count);
                    statStrip.Items[0].Text = string.Format("�������� ���� {0} ������", dataGridView.CurrentRow.Index);
                    string[] fs = new string[fields.Count];

                    for (int j = 0; j < fields.Count; j++)
                    {
                        fs[j] = fields[j].value;
                    }

                    dataGridView.Rows.Insert(dataGridView.CurrentRow.Index, fs);
                }
            }
            else
            {
                statStrip.Items[1].Text = string.Format("ͳ���� ���������!");
            }
        }

        public void doExport()
        {
            if (dataGridView.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "CSV (*.csv)|*.csv";
                sfd.FileName = "Output.csv";

                bool fileError = false;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("It wasn't possible to write the data to the disk." + ex.Message);
                        }
                    }
                    if (!fileError)
                    {
                        try
                        {
                            saveIntoFile(sfd.FileName);
                            MessageBox.Show("Data Exported Successfully !!!", "Info");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error :" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No Record To Export !!!", "Info");
            }
        }

        private void saveIntoFile(string fileName)
        {
            int columnCount = dataGridView.Columns.Count;
            string columnNames = "";
            string[] outputCsv = new string[dataGridView.Rows.Count + 1];

            for (int i = 0; i < columnCount; i++)
            {
                columnNames += dataGridView.Columns[i].HeaderText.ToString() + ((i + 1 < columnCount) ? ";" : "");
            }

            outputCsv[0] += columnNames;

            for (int i = 1; (i - 1) < dataGridView.Rows.Count; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    outputCsv[i] += dataGridView.Rows[i - 1].Cells[j].Value.ToString() + ((j + 1 < columnCount) ? ";" : "");
                }
            }

            File.WriteAllLines(fileName, outputCsv, Encoding.UTF8);
        }
    }
}