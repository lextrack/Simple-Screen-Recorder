namespace Simple_Screen_Recorder.UI
{
    public partial class AreaSelector : Form
    {
        public Rectangle SelectedArea { get; private set; }

        public AreaSelector()
        {
            this.Opacity = 0.5;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.DoubleBuffered = true;
            this.Cursor = Cursors.Cross;

            this.MouseDown += AreaSelector_MouseDown;
            this.MouseMove += AreaSelector_MouseMove;
            this.MouseUp += AreaSelector_MouseUp;
            this.Paint += AreaSelector_Paint;
            this.KeyDown += AreaSelector_KeyDown;

            Label guideLabel = new Label
            {
                Text = "Click and drag to select an area. Release the button to finish and then press the start recording button.",
                ForeColor = Color.White,
                BackColor = Color.Black,
                AutoSize = false,
                Size = new Size(300, 100),
                Location = new Point(10, 10),
                Font = new Font("Segoe UI", 15, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(guideLabel);

            this.KeyPreview = true;
        }

        private Point startPoint;
        private Point endPoint;
        private bool isDrawing = false;

        private void AreaSelector_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                startPoint = e.Location;
                endPoint = e.Location;
                isDrawing = true;
            }
        }

        private void AreaSelector_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                endPoint = e.Location;
                this.Invalidate();
            }
        }

        private void AreaSelector_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                isDrawing = false;
                int width = Math.Abs(startPoint.X - endPoint.X);
                int height = Math.Abs(startPoint.Y - endPoint.Y);

                if (width < 10 || height < 10)
                {
                    MessageBox.Show("This selection is invalid, please try again.", "Error");
                    startPoint = Point.Empty;
                    endPoint = Point.Empty;
                    this.Invalidate();
                    return;
                }

                width = width % 2 == 0 ? width : width + 1;
                height = height % 2 == 0 ? height : height + 1;

                SelectedArea = new Rectangle(
                    Math.Min(startPoint.X, endPoint.X),
                    Math.Min(startPoint.Y, endPoint.Y),
                    width,
                    height
                );

                DialogResult result = MessageBox.Show("Is this the correct area?", "Confirm Selection", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    startPoint = Point.Empty;
                    endPoint = Point.Empty;
                    isDrawing = false;
                    this.Invalidate();
                }
            }
        }

        private void AreaSelector_Paint(object sender, PaintEventArgs e)
        {
            if (isDrawing)
            {
                using (Pen pen = new Pen(Color.Red, 2))
                {
                    Rectangle rect = new Rectangle(
                        Math.Min(startPoint.X, endPoint.X),
                        Math.Min(startPoint.Y, endPoint.Y),
                        Math.Abs(startPoint.X - endPoint.X),
                        Math.Abs(startPoint.Y - endPoint.Y)
                    );
                    e.Graphics.DrawRectangle(pen, rect);
                }
            }
        }

        private void AreaSelector_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
    }
}