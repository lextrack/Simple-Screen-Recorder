using Simple_Screen_Recorder.Langs;
using Simple_Screen_Recorder.Properties;

namespace Simple_Screen_Recorder.ScreenRecorderWin
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            this.Text = StringsEN.AboutForm;
            LbAbout1.Text = StringsEN.LbAbout1;
            LbAbout2.Text = StringsEN.LbAbout2;
            labelCopyright.Text = StringsEN.labelCopyright;
        }

        private void AboutForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Default.Save();
        }
    }
}
