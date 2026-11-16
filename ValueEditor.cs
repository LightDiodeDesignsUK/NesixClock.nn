using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clockv2.nn
{
    public partial class ValueEditor : Form
    {
        public delegate void OnOKEvent(string newValue);
        public ValueEditor(string initialValue)
        {
            InitializeComponent();
            this.initValue = initialValue;
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string LabelValue
        { set { label1.Text = value; } }
        string initValue = "";
        
        public event OnOKEvent OnOK;
        private void ValueEditor_Load(object sender, EventArgs e)
        {
            ValueTBox.Text = initValue;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OnOK?.Invoke(ValueTBox.Text);
            this.Close();
        }
    }
}
