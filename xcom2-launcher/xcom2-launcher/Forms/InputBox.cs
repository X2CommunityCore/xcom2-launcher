using System.Drawing;
using System.Windows.Forms;

namespace XCOM2Launcher.Forms
{
    //
    // Native InputBox implementation for Windows Forms
    //
    public static class InputBox
    {
        public static string Show(string message, string title, string defaultValue = "Please enter text")
        {
            // Controls
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            // Margins and sizes
            int marginX = 16;
            int controlWidth = 368; // 400 width - 32 adjusted for margins

            // Label with dynamic height based on text content and word wrapping
            label.AutoSize = true;
            label.MaximumSize = new Size(controlWidth, 0);
            label.Text = message;
            label.Location = new Point(marginX, 16);

            // Label's height will be calculated based on the text content and word wrapping
            var actualLabelSize = label.GetPreferredSize(new Size(controlWidth, 0));
            int labelHeight = actualLabelSize.Height;

            // TextBox positioned below the label with a margin of 12 pixels
            int positionYTextBox = label.Top + labelHeight + 12;
            textBox.Location = new Point(marginX, positionYTextBox);
            textBox.Size = new Size(controlWidth, 26);
            textBox.Text = defaultValue;

            // Buttons below the TextBox with a margin of 20 pixels
            int positionYButtons = textBox.Bottom + 20;

            buttonOk.Text = "OK";
            buttonOk.DialogResult = DialogResult.OK;
            buttonOk.Size = new Size(100, 32);
            buttonOk.Location = new Point(176, positionYButtons);

            buttonCancel.Text = "Cancelar";
            buttonCancel.DialogResult = DialogResult.Cancel;
            buttonCancel.Size = new Size(100, 32);
            buttonCancel.Location = new Point(284, positionYButtons);

            // InputBox final height calculation: TextBox + Buttons + margins
            int totalFormHeight = buttonCancel.Bottom + 16;

            // Form properties
            form.Text = title;
            form.ClientSize = new Size(400, totalFormHeight);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterParent;
            form.MaximizeBox = false;
            form.MinimizeBox = false;

            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            // Dialog result handling
            DialogResult dialogResult = form.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                return textBox.Text;
            }
            return string.Empty;

        }
    }
}

