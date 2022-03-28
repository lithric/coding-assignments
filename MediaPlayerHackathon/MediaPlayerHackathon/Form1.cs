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
            string text = File.ReadAllText(@"../../browser.html");
            Console.WriteLine("hello");
            base.OnLoad(e);
            var embed = @"
<html>
    <head>
        <meta http-equiv='X-UA-Compatible' content='IE=Edge'/>
    </head>
    <body>
        <iframe width='300' src='{0}' frameborder='0' allow='autoplay; encrypted-media' allowfullscreen>
        </iframe>
    </body>
</html>";
            var url = "https://www.youtube.com/embed/L6ZgzJKfERM";
            webBrowser1.DocumentText = string.Format(embed, url);
        }
    }
}
