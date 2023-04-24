

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Disply_Evo
{

    public partial class Form1 : Form
    {
        private TextBox arraySizeTextBox;
        private TextBox densiteTextBox;
        private bool playing = false;


        private TableService tableService = new TableService();
        private Population[,] values;
        private Dictionary<int, Color> colorMap = new Dictionary<int, Color>()
        {
            { 1, Color.White },
            { 2, Color.Black },
            { 3, Color.LightBlue },
            { 4, Color.Yellow },
            { 5, Color.Orange },
            { 6, Color.Purple },
            { 7, Color.Pink },
            { 8, Color.Brown },
            { 9, Color.Gray },
            { 10, Color.Black }
        };
        private int cellSize = 15;



        public Form1()
        {

            InitializeComponent();
            //this.Load += new EventHandler(Form1_Load);
        }

        private void Form1_Load(object sender, EventArgs e)
        {


            // Bouton pour créer une nouvelle simulation
            Button btnRefresh = new Button();
            btnRefresh.Text = "Refresh";
            btnRefresh.Width = 200;
            btnRefresh.Height = 30;
            btnRefresh.Top = 10;
            btnRefresh.Left = 600;
            btnRefresh.Click += new EventHandler(btnRefresh_Click);
            this.panelArray.Controls.Add(btnRefresh);

            // Bouton pour simuler 10 interactions
            Button btnInteract10 = new Button();
            btnInteract10.Text = "next step";
            btnInteract10.Width = 200;
            btnInteract10.Height = 30;
            btnInteract10.Top = 10;
            btnInteract10.Left = btnRefresh.Right + 10;
            btnInteract10.Click += new EventHandler(btnInteract10_Click);
            this.panelArray.Controls.Add(btnInteract10);

            Button btnPlay = new Button();
            btnPlay.Text = "Play/Pause";
            btnPlay.Width = 200;
            btnPlay.Height = 30;
            btnPlay.Top = 10;
            btnPlay.Left = btnInteract10.Right + 10;
            btnPlay.Click += new EventHandler(btnPlay_Click);
            this.panelArray.Controls.Add(btnPlay);


            // Conserver les meilleurs éléments et les fait se reproduire


            //Paramètres de simulation

            //taille de la simulation (défaut 10 par 10)s
            Label arraySizeLabel = new Label();
            arraySizeLabel.Text = "Array Size:";
            arraySizeLabel.Width = 200;
            arraySizeLabel.Top = 10;
            arraySizeLabel.Left = 10;
            this.panelArray.Controls.Add(arraySizeLabel);

            arraySizeTextBox = new TextBox();
            arraySizeTextBox.Text = "10";
            arraySizeTextBox.Top = 10;
            arraySizeTextBox.Left = 400;
            arraySizeTextBox.Name = "arraySizeTextBox";
            this.panelArray.Controls.Add(arraySizeTextBox);

            Label densiteLabel = new Label();
            densiteLabel.Text = "densite 1-100";
            densiteLabel.Width = 200;
            densiteLabel.Top = 40;
            densiteLabel.Left = 10;
            this.panelArray.Controls.Add(densiteLabel);

            densiteTextBox = new TextBox();
            densiteTextBox.Text = "50";
            densiteTextBox.Top = 40;
            densiteTextBox.Left = 400;
            densiteTextBox.Name = "densiteTextBox";
            this.panelArray.Controls.Add(densiteTextBox);

            GenerateValues();
            AddLabels();
            ResizePanel();
        }




        // Fonction génératrice
        private void GenerateValues()
        {
            int arraySize = int.Parse(this.arraySizeTextBox.Text);
            int densite = int.Parse(this.densiteTextBox.Text);



            Random random = new Random();
            values = new Population[arraySize, arraySize];
            Parallel.For(0, arraySize, i =>
            {
                for (int j = 0; j < arraySize; j++)
                {
                    int colorValue = 0;
                    int choix = random.Next(1, 101);
                    if (choix <= densite)
                    {
                        colorValue = 1;
                    }
                    else
                    {
                        colorValue = 2;
                    }


                    values[i, j] = new Population(colorValue);
                }
            });
        }

        private Label[,] labels;
        private Panel panelArray;

        // affichage des valeurs
        public void AddLabels()
        {
            // Crée les labels si ils ne sont pas déja créés
            int arraySize = int.Parse(arraySizeTextBox.Text);
            if (labels == null)
            {
                labels = new Label[arraySize, arraySize];

                for (int i = 0; i < arraySize; i++)
                {
                    for (int j = 0; j < arraySize; j++)
                    {
                        Label label = new Label();
                        label.Name = string.Format("label{0:D2}{1:D2}", i, j);
                        label.AutoSize = false;
                        label.Width = cellSize;
                        label.Height = cellSize;
                        label.TextAlign = ContentAlignment.MiddleCenter;
                        label.Top = i * cellSize + 200;
                        label.Left = j * cellSize;
                        label.Click += new EventHandler(labelclick);
                        labels[i, j] = label;

                        // Update the label with the initial population value
                        UpdateLabel(i, j, label, values[i, j]);
                    }
                }

                // Add all labels to the panel at once to improve performance
                panelArray.Controls.AddRange(labels.Cast<Control>().ToArray());
            }
        }


        // mise a jour des labels
        private void UpdateLabel(int i, int j, Label label, Population value)
        {


            int colorValue = value.type;
            if (colorMap.ContainsKey(colorValue))
            {
                label.BackColor = colorMap[colorValue];
            }
            else
            {
                label.BackColor = Color.White;
            }
        }

        //Efface les labels
        private void DisposeLabels()
        {
            if (labels != null)
            {
                foreach (Label label in labels)
                {
                    label.Dispose();

                }

                labels = null;


            }


        }

        // rend l'esthétique de la page plus plaisant
        private void ResizePanel()
        {
            int arraySize = int.Parse(arraySizeTextBox.Text);
            int panelWidth = (cellSize + 2) * arraySize + 2;
            int panelHeight = (cellSize + 2) * arraySize + 2;
            panelArray.Width = panelWidth;
            panelArray.Height = panelHeight;
        }

        // Nouvelle simulation
        private void btnRefresh_Click(object sender, EventArgs e)
        {

            DisposeLabels();
            GenerateValues();
            AddLabels();
            ResizePanel();

        }

        private void labelclick(object sender, EventArgs e)
        {

            Label clickedLabel = sender as Label;

            int i = int.Parse(clickedLabel.Name.Substring(5, 2));
            int j = int.Parse(clickedLabel.Name.Substring(7, 2));
            if (clickedLabel != null)
            {
                if (values[i, j].type == 1)
                {
                    values[i, j].type = 2;
                    labels[i, j].BackColor = colorMap[2];

                }
                else
                {
                    values[i, j].type = 1;
                    labels[i, j].BackColor = colorMap[1];
                }

            }

        }


        private void btnInteract10_Click(object sender, EventArgs e)
        {
            Population[,] oldvalues = DeepCloneHelper.DeepClone(values);
            values = tableService.tableinteraction(values);

            for (int i = 0; i < values.GetLength(0); i++)
            {
                for (int j = 0; j < values.GetLength(1); j++)
                {
                    Population oldValue = oldvalues[i, j];
                    Population newValue = values[i, j];
                    if (!newValue.Equals(oldValue))
                    {
                        changedCells.Add(new Point(i, j));
                    }
                }
            }
            foreach (Point point in changedCells)
            {
                int i = point.X;
                int j = point.Y;
                UpdateLabel(i, j, labels[i, j], values[i, j]);
            }
            changedCells.Clear();


        }

        private HashSet<Point> changedCells = new HashSet<Point>();

        // itere automatiquement
        private void Play()
        {
            playing = true;
            while (playing)
            {

                Population[,] oldvalues = DeepCloneHelper.DeepClone(values);
                values = tableService.tableinteraction(values);

                for (int i = 0; i < values.GetLength(0); i++)
                {
                    for (int j = 0; j < values.GetLength(1); j++)
                    {
                        Population oldValue = oldvalues[i, j];
                        Population newValue = values[i, j];
                        if (!newValue.Equals(oldValue))
                        {
                            changedCells.Add(new Point(i, j));
                        }
                    }
                }
                foreach (Point point in changedCells)
                {
                    int i = point.X;
                    int j = point.Y;
                    UpdateLabel(i, j, labels[i, j], values[i, j]);
                }
                changedCells.Clear();


                Application.DoEvents(); // Maj UI
                System.Threading.Thread.Sleep(300); // Attend X secondes
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (!playing)
            {
                Play();
            }
            else
            {
                playing = false;
            }
        }

        

    }
}