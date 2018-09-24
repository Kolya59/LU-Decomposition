namespace Test
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    /// <inheritdoc />
    /// <summary>TODO The nuber text box.</summary>
    public partial class NumberTextBox : TextBox
    {
        public NumberTextBox()
        {
            InitializeComponent();
        }

        public NumberTextBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            ForeColor = int.TryParse(Text, out _) ? Color.Red : ForeColor;
        }
    }
}
