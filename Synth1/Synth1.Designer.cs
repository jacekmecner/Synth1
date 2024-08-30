namespace Synth1
{
    partial class Synth1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            oscillator1 = new Oscillator();
            SuspendLayout();
            // 
            // oscillator1
            // 
            oscillator1.Location = new Point(305, 123);
            oscillator1.Name = "oscillator1";
            oscillator1.Size = new Size(200, 100);
            oscillator1.TabIndex = 0;
            oscillator1.TabStop = false;
            oscillator1.Text = "oscillator1";
            // 
            // Synth1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(oscillator1);
            KeyPreview = true;
            Name = "Synth1";
            Text = "Synth1";
            KeyDown += Synth1_KeyDown;
            ResumeLayout(false);
        }

        #endregion

        private Oscillator oscillator1;
    }
}
