using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

namespace XCOM2Launcher.PropertyGrid
{
    class CheckboxEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {

            return UITypeEditorEditStyle.Modal;
        }


        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {

            return true;
        }


        public override void PaintValue(PaintValueEventArgs e)
        {

            ButtonState State;

            bool res = Convert.ToBoolean((e.Value));

            if (res)
                State = ButtonState.Checked;

            else
                State = ButtonState.Normal;

            ControlPaint.DrawCheckBox(e.Graphics, e.Bounds, State);
            e.Graphics.ExcludeClip(e.Bounds);
        }

    }
}
