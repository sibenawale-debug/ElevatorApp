namespace ElevatorApp
{
    partial class LogForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblFrom = new System.Windows.Forms.Label();
            this.lblTo = new System.Windows.Forms.Label();
            this.lblElevator = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.fromPicker = new System.Windows.Forms.DateTimePicker();
            this.toPicker = new System.Windows.Forms.DateTimePicker();
            this.elevatorCombo = new System.Windows.Forms.ComboBox();
            this.typeCombo = new System.Windows.Forms.ComboBox();
            this.applyButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.logGrid = new System.Windows.Forms.DataGridView();
            this.statusLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.logGrid)).BeginInit();
            this.SuspendLayout();
            //
            // lblFrom
            //
            this.lblFrom.AutoSize = true;
            this.lblFrom.Location = new System.Drawing.Point(10, 8);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(60, 15);
            this.lblFrom.TabIndex = 0;
            this.lblFrom.Text = "From date";
            //
            // lblTo
            //
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(135, 8);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(45, 15);
            this.lblTo.TabIndex = 1;
            this.lblTo.Text = "To date";
            //
            // lblElevator
            //
            this.lblElevator.AutoSize = true;
            this.lblElevator.Location = new System.Drawing.Point(260, 8);
            this.lblElevator.Name = "lblElevator";
            this.lblElevator.Size = new System.Drawing.Size(51, 15);
            this.lblElevator.TabIndex = 2;
            this.lblElevator.Text = "Elevator";
            //
            // lblType
            //
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(365, 8);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(66, 15);
            this.lblType.TabIndex = 3;
            this.lblType.Text = "Event type";
            //
            // fromPicker
            //
            this.fromPicker.Checked = false;
            this.fromPicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.fromPicker.Location = new System.Drawing.Point(10, 26);
            this.fromPicker.Name = "fromPicker";
            this.fromPicker.ShowCheckBox = true;
            this.fromPicker.Size = new System.Drawing.Size(115, 23);
            this.fromPicker.TabIndex = 4;
            //
            // toPicker
            //
            this.toPicker.Checked = false;
            this.toPicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.toPicker.Location = new System.Drawing.Point(135, 26);
            this.toPicker.Name = "toPicker";
            this.toPicker.ShowCheckBox = true;
            this.toPicker.Size = new System.Drawing.Size(115, 23);
            this.toPicker.TabIndex = 5;
            //
            // elevatorCombo
            //
            this.elevatorCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.elevatorCombo.FormattingEnabled = true;
            this.elevatorCombo.Location = new System.Drawing.Point(260, 26);
            this.elevatorCombo.Name = "elevatorCombo";
            this.elevatorCombo.Size = new System.Drawing.Size(95, 23);
            this.elevatorCombo.TabIndex = 6;
            //
            // typeCombo
            //
            this.typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeCombo.FormattingEnabled = true;
            this.typeCombo.Location = new System.Drawing.Point(365, 26);
            this.typeCombo.Name = "typeCombo";
            this.typeCombo.Size = new System.Drawing.Size(140, 23);
            this.typeCombo.TabIndex = 7;
            //
            // applyButton
            //
            this.applyButton.Location = new System.Drawing.Point(515, 25);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(75, 25);
            this.applyButton.TabIndex = 8;
            this.applyButton.Text = "Apply";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            //
            // clearButton
            //
            this.clearButton.Location = new System.Drawing.Point(600, 25);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(80, 25);
            this.clearButton.TabIndex = 9;
            this.clearButton.Text = "Clear log";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            //
            // logGrid
            //
            this.logGrid.AllowUserToAddRows = false;
            this.logGrid.AllowUserToDeleteRows = false;
            this.logGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.logGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.logGrid.Location = new System.Drawing.Point(10, 60);
            this.logGrid.Name = "logGrid";
            this.logGrid.ReadOnly = true;
            this.logGrid.RowHeadersVisible = false;
            this.logGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.logGrid.Size = new System.Drawing.Size(740, 380);
            this.logGrid.TabIndex = 10;
            //
            // statusLabel
            //
            this.statusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.statusLabel.Location = new System.Drawing.Point(10, 448);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(740, 22);
            this.statusLabel.TabIndex = 11;
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // LogForm
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(760, 480);
            this.Controls.Add(this.lblFrom);
            this.Controls.Add(this.lblTo);
            this.Controls.Add(this.lblElevator);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.fromPicker);
            this.Controls.Add(this.toPicker);
            this.Controls.Add(this.elevatorCombo);
            this.Controls.Add(this.typeCombo);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.logGrid);
            this.Controls.Add(this.statusLabel);
            this.MinimumSize = new System.Drawing.Size(720, 360);
            this.Name = "LogForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Elevator database (log)";
            this.Load += new System.EventHandler(this.LogForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.logGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Label lblElevator;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.DateTimePicker fromPicker;
        private System.Windows.Forms.DateTimePicker toPicker;
        private System.Windows.Forms.ComboBox elevatorCombo;
        private System.Windows.Forms.ComboBox typeCombo;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.DataGridView logGrid;
        private System.Windows.Forms.Label statusLabel;
    }
}
