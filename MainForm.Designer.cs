namespace ElevatorApp
{
    partial class MainForm
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
            this.elevatorViewA = new ElevatorApp.ElevatorView();
            this.elevatorViewB = new ElevatorApp.ElevatorView();
            this.hallPanel = new System.Windows.Forms.Panel();
            this.hallTitleLabel = new System.Windows.Forms.Label();
            this.callButtonA2 = new System.Windows.Forms.Button();
            this.callButtonA1 = new System.Windows.Forms.Button();
            this.callButtonB2 = new System.Windows.Forms.Button();
            this.callButtonB1 = new System.Windows.Forms.Button();
            this.showDatabaseButton = new System.Windows.Forms.Button();
            this.statusLabel = new System.Windows.Forms.Label();
            this.hallPanel.SuspendLayout();
            this.SuspendLayout();
            //
            // elevatorViewA
            //
            this.elevatorViewA.Location = new System.Drawing.Point(10, 10);
            this.elevatorViewA.Name = "elevatorViewA";
            this.elevatorViewA.Size = new System.Drawing.Size(270, 530);
            this.elevatorViewA.TabIndex = 0;
            //
            // elevatorViewB
            //
            this.elevatorViewB.Location = new System.Drawing.Point(480, 10);
            this.elevatorViewB.Name = "elevatorViewB";
            this.elevatorViewB.Size = new System.Drawing.Size(270, 530);
            this.elevatorViewB.TabIndex = 1;
            //
            // hallPanel
            //
            this.hallPanel.Controls.Add(this.hallTitleLabel);
            this.hallPanel.Controls.Add(this.callButtonA2);
            this.hallPanel.Controls.Add(this.callButtonA1);
            this.hallPanel.Controls.Add(this.callButtonB2);
            this.hallPanel.Controls.Add(this.callButtonB1);
            this.hallPanel.Controls.Add(this.showDatabaseButton);
            this.hallPanel.Location = new System.Drawing.Point(290, 10);
            this.hallPanel.Name = "hallPanel";
            this.hallPanel.Size = new System.Drawing.Size(180, 530);
            this.hallPanel.TabIndex = 2;
            //
            // hallTitleLabel
            //
            this.hallTitleLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.hallTitleLabel.Location = new System.Drawing.Point(0, 2);
            this.hallTitleLabel.Name = "hallTitleLabel";
            this.hallTitleLabel.Size = new System.Drawing.Size(180, 24);
            this.hallTitleLabel.TabIndex = 0;
            this.hallTitleLabel.Text = "Call buttons (outside)";
            this.hallTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // callButtonA2
            //
            this.callButtonA2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.callButtonA2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.callButtonA2.Location = new System.Drawing.Point(5, 38);
            this.callButtonA2.Name = "callButtonA2";
            this.callButtonA2.Size = new System.Drawing.Size(80, 60);
            this.callButtonA2.TabIndex = 1;
            this.callButtonA2.Text = "A  ▼\r\nFloor 2";
            this.callButtonA2.UseVisualStyleBackColor = false;
            this.callButtonA2.Click += new System.EventHandler(this.callButtonA2_Click);
            //
            // callButtonA1
            //
            this.callButtonA1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.callButtonA1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.callButtonA1.Location = new System.Drawing.Point(5, 188);
            this.callButtonA1.Name = "callButtonA1";
            this.callButtonA1.Size = new System.Drawing.Size(80, 60);
            this.callButtonA1.TabIndex = 2;
            this.callButtonA1.Text = "A  ▲\r\nFloor 1";
            this.callButtonA1.UseVisualStyleBackColor = false;
            this.callButtonA1.Click += new System.EventHandler(this.callButtonA1_Click);
            //
            // callButtonB2
            //
            this.callButtonB2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.callButtonB2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.callButtonB2.Location = new System.Drawing.Point(95, 38);
            this.callButtonB2.Name = "callButtonB2";
            this.callButtonB2.Size = new System.Drawing.Size(80, 60);
            this.callButtonB2.TabIndex = 3;
            this.callButtonB2.Text = "B  ▼\r\nFloor 2";
            this.callButtonB2.UseVisualStyleBackColor = false;
            this.callButtonB2.Click += new System.EventHandler(this.callButtonB2_Click);
            //
            // callButtonB1
            //
            this.callButtonB1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.callButtonB1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.callButtonB1.Location = new System.Drawing.Point(95, 188);
            this.callButtonB1.Name = "callButtonB1";
            this.callButtonB1.Size = new System.Drawing.Size(80, 60);
            this.callButtonB1.TabIndex = 4;
            this.callButtonB1.Text = "B  ▲\r\nFloor 1";
            this.callButtonB1.UseVisualStyleBackColor = false;
            this.callButtonB1.Click += new System.EventHandler(this.callButtonB1_Click);
            //
            // showDatabaseButton
            //
            this.showDatabaseButton.Location = new System.Drawing.Point(10, 300);
            this.showDatabaseButton.Name = "showDatabaseButton";
            this.showDatabaseButton.Size = new System.Drawing.Size(160, 45);
            this.showDatabaseButton.TabIndex = 5;
            this.showDatabaseButton.Text = "Show database";
            this.showDatabaseButton.UseVisualStyleBackColor = true;
            this.showDatabaseButton.Click += new System.EventHandler(this.showDatabaseButton_Click);
            //
            // statusLabel
            //
            this.statusLabel.Location = new System.Drawing.Point(10, 550);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(740, 25);
            this.statusLabel.TabIndex = 3;
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // MainForm
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(760, 585);
            this.Controls.Add(this.elevatorViewA);
            this.Controls.Add(this.hallPanel);
            this.Controls.Add(this.elevatorViewB);
            this.Controls.Add(this.statusLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Two-Elevator Control";
            this.hallPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private ElevatorApp.ElevatorView elevatorViewA;
        private ElevatorApp.ElevatorView elevatorViewB;
        private System.Windows.Forms.Panel hallPanel;
        private System.Windows.Forms.Label hallTitleLabel;
        private System.Windows.Forms.Button callButtonA2;
        private System.Windows.Forms.Button callButtonA1;
        private System.Windows.Forms.Button callButtonB2;
        private System.Windows.Forms.Button callButtonB1;
        private System.Windows.Forms.Button showDatabaseButton;
        private System.Windows.Forms.Label statusLabel;
    }
}
