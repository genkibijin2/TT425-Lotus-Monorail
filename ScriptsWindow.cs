using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLua;
using DynamicLua;

namespace TT425_Lotus_Monorail
{
    public partial class ScriptsWindow : Form
    {
        public ScriptsWindow(Point whereIsThisWindowAppearing)
        {
            InitializeComponent();
        }

        private string runSelectedScript(string script)
        {
            dynamic lua = new DynamicLua.DynamicLua();
            string resultOfScript = lua(script);
            return resultOfScript;
        }

        private void runScriptButton_Click_1(object sender, EventArgs e)
        {
            string script2send = ScriptLogBox.Text;
            ScriptLogBox.Text = runSelectedScript(script2send);
        }
    }
}
