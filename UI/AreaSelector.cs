namespace Simple_Screen_Recorder.UI
{
    public partial class AreaSelector : Form
    {
        public Rectangle SelectedArea { get; private set; }
        private Rectangle virtualScreenBounds;

        public AreaSelector()
        {
            virtualScreenBounds = GetVirtualScreenBounds();

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(virtualScreenBounds.Left, virtualScreenBounds.Top);
            this.Size = new Size(virtualScreenBounds.Width, virtualScreenBounds.Height);

            this.Opacity = 0.7;
            this.FormBorderStyle = FormBorderStyle.None;
            this.DoubleBuffered = true;
            this.Cursor = Cursors.Cross;
            this.BackColor = Color.Black;

            this.MouseDown += AreaSelector_MouseDown;
            this.MouseMove += AreaSelector_MouseMove;
            this.MouseUp += AreaSelector_MouseUp;
            this.Paint += AreaSelector_Paint;
            this.KeyDown += AreaSelector_KeyDown;

            Label guideLabel = new Label
            {
                Text = "Click and drag to select an area. Then start video recording",
                ForeColor = Color.Red,
                BackColor = Color.Black,
                AutoSize = false,
                Size = new Size(100, 100),
                Location = new Point(5, 5),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(guideLabel);

            this.KeyPreview = true;
        }

        private Rectangle GetVirtualScreenBounds()
        {
            int left = Screen.AllScreens.Min(screen => screen.Bounds.Left);
            int top = Screen.AllScreens.Min(screen => screen.Bounds.Top);
            int right = Screen.AllScreens.Max(screen => screen.Bounds.Right);
            int bottom = Screen.AllScreens.Max(screen => screen.Bounds.Bottom);

            return new Rectangle(left, top, right - left, bottom - top);
        }

        private Point startPoint;
        private Point endPoint;
        private bool isDrawing = false;

        private void AreaSelector_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                startPoint = this.PointToScreen(e.Location);
                startPoint = new Point(
                    startPoint.X - virtualScreenBounds.Left,
                    startPoint.Y - virtualScreenBounds.Top
                );
                endPoint = startPoint;
                isDrawing = true;
            }
        }

        private void AreaSelector_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                Point screenPoint = this.PointToScreen(e.Location);
                endPoint = new Point(
                    screenPoint.X - virtualScreenBounds.Left,
                    screenPoint.Y - virtualScreenBounds.Top
                );
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
                    Math.Min(startPoint.X, endPoint.X) + virtualScreenBounds.Left,
                    Math.Min(startPoint.Y, endPoint.Y) + virtualScreenBounds.Top,
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