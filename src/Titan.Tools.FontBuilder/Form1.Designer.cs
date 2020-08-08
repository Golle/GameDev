namespace Titan.Tools.FontBuilder
{
    partial class Form1
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
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label Rendering;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Panel panel1;
            this.PaddingBottom = new System.Windows.Forms.NumericUpDown();
            this.PaddingRight = new System.Windows.Forms.NumericUpDown();
            this.PaddingLeft = new System.Windows.Forms.NumericUpDown();
            this.PaddingTop = new System.Windows.Forms.NumericUpDown();
            this.ShowBorders = new System.Windows.Forms.CheckBox();
            this.Fonts = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.FontSize = new System.Windows.Forms.NumericUpDown();
            this.FontSpriteSheet = new System.Windows.Forms.PictureBox();
            this.SpriteSheetWidth = new System.Windows.Forms.NumericUpDown();
            this.SpriteSheetHeight = new System.Windows.Forms.NumericUpDown();
            this.FontStyles = new System.Windows.Forms.ComboBox();
            this.SpriteText = new System.Windows.Forms.TextBox();
            this.TextRendering = new System.Windows.Forms.ComboBox();
            this.ExportButton = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            Rendering = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            panel1 = new System.Windows.Forms.Panel();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PaddingBottom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PaddingRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PaddingLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PaddingTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FontSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FontSpriteSheet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpriteSheetWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpriteSheetHeight)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(343, 12);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(53, 15);
            label1.TabIndex = 2;
            label1.Text = "Font size";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(343, 44);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(53, 15);
            label4.TabIndex = 2;
            label4.Text = "Font size";
            // 
            // Rendering
            // 
            Rendering.AutoSize = true;
            Rendering.Location = new System.Drawing.Point(343, 73);
            Rendering.Name = "Rendering";
            Rendering.Size = new System.Drawing.Size(82, 15);
            Rendering.TabIndex = 2;
            Rendering.Text = "Text rendering";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(343, 132);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(39, 15);
            label2.TabIndex = 2;
            label2.Text = "Width";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(343, 161);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(43, 15);
            label3.TabIndex = 2;
            label3.Text = "Height";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(1, 1);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(51, 15);
            label5.TabIndex = 2;
            label5.Text = "Padding";
            // 
            // panel1
            // 
            panel1.Controls.Add(this.PaddingBottom);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(this.PaddingRight);
            panel1.Controls.Add(this.PaddingLeft);
            panel1.Controls.Add(this.PaddingTop);
            panel1.Location = new System.Drawing.Point(575, 141);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(200, 94);
            panel1.TabIndex = 7;
            // 
            // PaddingBottom
            // 
            this.PaddingBottom.Location = new System.Drawing.Point(69, 62);
            this.PaddingBottom.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.PaddingBottom.Name = "PaddingBottom";
            this.PaddingBottom.Size = new System.Drawing.Size(51, 23);
            this.PaddingBottom.TabIndex = 3;
            this.PaddingBottom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // PaddingRight
            // 
            this.PaddingRight.Location = new System.Drawing.Point(123, 35);
            this.PaddingRight.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.PaddingRight.Name = "PaddingRight";
            this.PaddingRight.Size = new System.Drawing.Size(51, 23);
            this.PaddingRight.TabIndex = 3;
            this.PaddingRight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // PaddingLeft
            // 
            this.PaddingLeft.Location = new System.Drawing.Point(15, 35);
            this.PaddingLeft.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.PaddingLeft.Name = "PaddingLeft";
            this.PaddingLeft.Size = new System.Drawing.Size(51, 23);
            this.PaddingLeft.TabIndex = 3;
            this.PaddingLeft.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // PaddingTop
            // 
            this.PaddingTop.Location = new System.Drawing.Point(69, 8);
            this.PaddingTop.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.PaddingTop.Name = "PaddingTop";
            this.PaddingTop.Size = new System.Drawing.Size(51, 23);
            this.PaddingTop.TabIndex = 3;
            this.PaddingTop.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ShowBorders
            // 
            this.ShowBorders.AutoSize = true;
            this.ShowBorders.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ShowBorders.Location = new System.Drawing.Point(426, 188);
            this.ShowBorders.Name = "ShowBorders";
            this.ShowBorders.Size = new System.Drawing.Size(98, 19);
            this.ShowBorders.TabIndex = 8;
            this.ShowBorders.Text = "Show borders";
            this.ShowBorders.UseVisualStyleBackColor = true;
            // 
            // Fonts
            // 
            this.Fonts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.Fonts.FullRowSelect = true;
            this.Fonts.HideSelection = false;
            this.Fonts.Location = new System.Drawing.Point(12, 12);
            this.Fonts.MultiSelect = false;
            this.Fonts.Name = "Fonts";
            this.Fonts.Size = new System.Drawing.Size(306, 204);
            this.Fonts.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.Fonts.TabIndex = 0;
            this.Fonts.UseCompatibleStateImageBehavior = false;
            this.Fonts.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Name = "columnHeader1";
            this.columnHeader1.Text = "";
            // 
            // FontSize
            // 
            this.FontSize.Location = new System.Drawing.Point(473, 12);
            this.FontSize.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.FontSize.Name = "FontSize";
            this.FontSize.Size = new System.Drawing.Size(51, 23);
            this.FontSize.TabIndex = 3;
            this.FontSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.FontSize.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // FontSpriteSheet
            // 
            this.FontSpriteSheet.BackColor = System.Drawing.SystemColors.Window;
            this.FontSpriteSheet.Location = new System.Drawing.Point(12, 237);
            this.FontSpriteSheet.Name = "FontSpriteSheet";
            this.FontSpriteSheet.Size = new System.Drawing.Size(512, 512);
            this.FontSpriteSheet.TabIndex = 4;
            this.FontSpriteSheet.TabStop = false;
            // 
            // SpriteSheetWidth
            // 
            this.SpriteSheetWidth.Location = new System.Drawing.Point(473, 130);
            this.SpriteSheetWidth.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.SpriteSheetWidth.Minimum = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.SpriteSheetWidth.Name = "SpriteSheetWidth";
            this.SpriteSheetWidth.Size = new System.Drawing.Size(51, 23);
            this.SpriteSheetWidth.TabIndex = 3;
            this.SpriteSheetWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.SpriteSheetWidth.Value = new decimal(new int[] {
            512,
            0,
            0,
            0});
            // 
            // SpriteSheetHeight
            // 
            this.SpriteSheetHeight.Location = new System.Drawing.Point(473, 159);
            this.SpriteSheetHeight.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.SpriteSheetHeight.Minimum = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.SpriteSheetHeight.Name = "SpriteSheetHeight";
            this.SpriteSheetHeight.Size = new System.Drawing.Size(51, 23);
            this.SpriteSheetHeight.TabIndex = 3;
            this.SpriteSheetHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.SpriteSheetHeight.Value = new decimal(new int[] {
            512,
            0,
            0,
            0});
            // 
            // FontStyles
            // 
            this.FontStyles.FormattingEnabled = true;
            this.FontStyles.Location = new System.Drawing.Point(433, 41);
            this.FontStyles.Name = "FontStyles";
            this.FontStyles.Size = new System.Drawing.Size(91, 23);
            this.FontStyles.TabIndex = 5;
            // 
            // SpriteText
            // 
            this.SpriteText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SpriteText.Location = new System.Drawing.Point(574, 12);
            this.SpriteText.Multiline = true;
            this.SpriteText.Name = "SpriteText";
            this.SpriteText.Size = new System.Drawing.Size(512, 121);
            this.SpriteText.TabIndex = 6;
            // 
            // TextRendering
            // 
            this.TextRendering.FormattingEnabled = true;
            this.TextRendering.Location = new System.Drawing.Point(433, 70);
            this.TextRendering.Name = "TextRendering";
            this.TextRendering.Size = new System.Drawing.Size(91, 23);
            this.TextRendering.TabIndex = 5;
            // 
            // ExportButton
            // 
            this.ExportButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ExportButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ExportButton.Location = new System.Drawing.Point(974, 188);
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.Size = new System.Drawing.Size(112, 47);
            this.ExportButton.TabIndex = 9;
            this.ExportButton.Text = "Export Font";
            this.ExportButton.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1184, 761);
            this.Controls.Add(this.ExportButton);
            this.Controls.Add(Rendering);
            this.Controls.Add(this.TextRendering);
            this.Controls.Add(this.ShowBorders);
            this.Controls.Add(panel1);
            this.Controls.Add(this.SpriteText);
            this.Controls.Add(label4);
            this.Controls.Add(this.FontStyles);
            this.Controls.Add(this.SpriteSheetHeight);
            this.Controls.Add(label3);
            this.Controls.Add(this.SpriteSheetWidth);
            this.Controls.Add(label2);
            this.Controls.Add(this.FontSpriteSheet);
            this.Controls.Add(this.FontSize);
            this.Controls.Add(label1);
            this.Controls.Add(this.Fonts);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = " ";
            this.Load += new System.EventHandler(this.Form1_Load);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PaddingBottom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PaddingRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PaddingLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PaddingTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FontSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FontSpriteSheet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpriteSheetWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpriteSheetHeight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView Fonts;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader dummyHeader;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.NumericUpDown FontSize;
        private System.Windows.Forms.PictureBox FontSpriteSheet;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown SpriteSheetWidth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown SpriteSheetHeight;
        private System.Windows.Forms.ComboBox FontStyles;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox SpriteText;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown PaddingTop;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown p;
        private System.Windows.Forms.NumericUpDown PaddingBottom;
        private System.Windows.Forms.NumericUpDown PaddingRight;
        private System.Windows.Forms.NumericUpDown PaddingLeft;
        private System.Windows.Forms.CheckBox ShowBorders;
        private System.Windows.Forms.ComboBox TextRendering;
        private System.Windows.Forms.Button ExportButton;
    }
}

