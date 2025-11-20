using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TT425_Lotus_Monorail
{
    public partial class loadingScreen : Form
    {
        public loadingScreen(Point whereToPutThisWindow)
        {
            InitializeComponent();
            whereToPutThisWindow.X = (whereToPutThisWindow.X + 52);
            whereToPutThisWindow.Y = (whereToPutThisWindow.Y + 151);
            this.Location = whereToPutThisWindow;
        }
    }
}
