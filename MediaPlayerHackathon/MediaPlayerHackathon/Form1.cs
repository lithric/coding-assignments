using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace MediaPlayerHackathon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
        protected override void OnLoad(EventArgs e)
        {
            string url = "https://www.youtube.com/embed/L6ZgzJKfERM";
            string libjs = "*/" + File.ReadAllText(@"C:/$HTOP/_/GlobalEnvironment/libs/GitHub/WebGazer/www/webgazer.js") + "/*";
            string css = "*/"+File.ReadAllText(@"../../browser.css")+"/*";
            string js = "*/" + File.ReadAllText(@"../../browser.js") + "/*";
            string html = string.Format(File.ReadAllText(@"../../browser.html"),url,css,js,libjs);
            base.OnLoad(e);
            webBrowser1.DocumentText = html;
        }
    }
}
