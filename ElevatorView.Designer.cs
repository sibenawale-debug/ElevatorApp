namespace ElevatorApp
{
    partial class ElevatorView
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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.titleLabel = new System.Windows.Forms.Label();
            this.shaftPanel = new System.Windows.Forms.Panel();
            this.carPanel = new System.Windows.Forms.Panel();
            this.doorLeftPanel = new System.Windows.Forms.Panel();
            this.doorRightPanel = new System.Windows.Forms.Panel();
            this.floorLinePanel2 = new System.Windows.Forms.Panel();
            this.floorLinePanel1 = new System.Windows.Forms.Panel();
            this.floorDoor2 = new System.Windows.Forms.Panel();
            this.floorDoor1 = new System.Windows.Forms.Panel();
            this.floorDisplay2 = new System.Windows.Forms.Label();
            this.floorDisplay1 = new System.Windows.Forms.Label();
            this.floorName2 = new System.Windows.Forms.Label();
            this.floorName1 = new System.Windows.Forms.Label();
            this.controlGroup = new System.Windows.Forms.GroupBox();
            this.panelDisplayLabel = new System.Windows.Forms.Label();
            this.panelButton2 = new System.Windows.Forms.Button();
            this.panelButton1 = new System.Windows.Forms.Button();
            this.openDoorButton = new System.Windows.Forms.Button();
            this.closeDoorButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.alarmButton = new System.Windows.Forms.Button();
            this.bannerLabel = new System.Windows.Forms.Label();
            this.shaftPanel.SuspendLayout();
            this.carPanel.SuspendLayout();
            this.controlGroup.SuspendLayout();
            this.SuspendLayout();
            //
            // titleLabel
            //
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.titleLabel.Location = new System.Drawing.Point(10, 2);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(250, 24);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Elevator";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // shaftPanel
            //
            this.shaftPanel.BackColor = System.Drawing.Color.FromArgb(45, 48, 55);
            this.shaftPanel.Controls.Add(this.carPanel);
            this.shaftPanel.Controls.Add(this.floorLinePanel2);
            this.shaftPanel.Controls.Add(this.floorLinePanel1);
            this.shaftPanel.Location = new System.Drawing.Point(10, 28);
            this.shaftPanel.Name = "shaftPanel";
            this.shaftPanel.Size = new System.Drawing.Size(140, 300);
            this.shaftPanel.TabIndex = 1;
            //
            // carPanel
            //
            this.carPanel.BackColor = System.Drawing.Color.Wheat;
            this.carPanel.Controls.Add(this.doorLeftPanel);
            this.carPanel.Controls.Add(this.doorRightPanel);
            this.carPanel.Location = new System.Drawing.Point(15, 160);
            this.carPanel.Name = "carPanel";
            this.carPanel.Size = new System.Drawing.Size(110, 130);
            this.carPanel.TabIndex = 0;
            //
            // doorLeftPanel
            //
            this.doorLeftPanel.BackColor = System.Drawing.Color.SlateGray;
            this.doorLeftPanel.Location = new System.Drawing.Point(0, 0);
            this.doorLeftPanel.Name = "doorLeftPanel";
            this.doorLeftPanel.Size = new System.Drawing.Size(55, 130);
            this.doorLeftPanel.TabIndex = 0;
            //
            // doorRightPanel
            //
            this.doorRightPanel.BackColor = System.Drawing.Color.SlateGray;
            this.doorRightPanel.Location = new System.Drawing.Point(55, 0);
            this.doorRightPanel.Name = "doorRightPanel";
            this.doorRightPanel.Size = new System.Drawing.Size(55, 130);
            this.doorRightPanel.TabIndex = 1;
            //
            // floorLinePanel2
            //
            this.floorLinePanel2.BackColor = System.Drawing.Color.DimGray;
            this.floorLinePanel2.Location = new System.Drawing.Point(0, 148);
            this.floorLinePanel2.Name = "floorLinePanel2";
            this.floorLinePanel2.Size = new System.Drawing.Size(140, 2);
            this.floorLinePanel2.TabIndex = 1;
            //
            // floorLinePanel1
            //
            this.floorLinePanel1.BackColor = System.Drawing.Color.DimGray;
            this.floorLinePanel1.Location = new System.Drawing.Point(0, 298);
            this.floorLinePanel1.Name = "floorLinePanel1";
            this.floorLinePanel1.Size = new System.Drawing.Size(140, 2);
            this.floorLinePanel1.TabIndex = 2;
            //
            // floorDoor2
            //
            this.floorDoor2.BackColor = System.Drawing.Color.Firebrick;
            this.floorDoor2.Location = new System.Drawing.Point(154, 38);
            this.floorDoor2.Name = "floorDoor2";
            this.floorDoor2.Size = new System.Drawing.Size(12, 130);
            this.floorDoor2.TabIndex = 2;
            //
            // floorDoor1
            //
            this.floorDoor1.BackColor = System.Drawing.Color.Firebrick;
            this.floorDoor1.Location = new System.Drawing.Point(154, 188);
            this.floorDoor1.Name = "floorDoor1";
            this.floorDoor1.Size = new System.Drawing.Size(12, 130);
            this.floorDoor1.TabIndex = 3;
            //
            // floorDisplay2
            //
            this.floorDisplay2.BackColor = System.Drawing.Color.Black;
            this.floorDisplay2.Font = new System.Drawing.Font("Consolas", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.floorDisplay2.ForeColor = System.Drawing.Color.Red;
            this.floorDisplay2.Location = new System.Drawing.Point(172, 38);
            this.floorDisplay2.Name = "floorDisplay2";
            this.floorDisplay2.Size = new System.Drawing.Size(80, 40);
            this.floorDisplay2.TabIndex = 4;
            this.floorDisplay2.Text = "1";
            this.floorDisplay2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // floorDisplay1
            //
            this.floorDisplay1.BackColor = System.Drawing.Color.Black;
            this.floorDisplay1.Font = new System.Drawing.Font("Consolas", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.floorDisplay1.ForeColor = System.Drawing.Color.Red;
            this.floorDisplay1.Location = new System.Drawing.Point(172, 188);
            this.floorDisplay1.Name = "floorDisplay1";
            this.floorDisplay1.Size = new System.Drawing.Size(80, 40);
            this.floorDisplay1.TabIndex = 5;
            this.floorDisplay1.Text = "1";
            this.floorDisplay1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // floorName2
            //
            this.floorName2.Location = new System.Drawing.Point(172, 80);
            this.floorName2.Name = "floorName2";
            this.floorName2.Size = new System.Drawing.Size(80, 18);
            this.floorName2.TabIndex = 6;
            this.floorName2.Text = "Floor 2";
            this.floorName2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // floorName1
            //
            this.floorName1.Location = new System.Drawing.Point(172, 230);
            this.floorName1.Name = "floorName1";
            this.floorName1.Size = new System.Drawing.Size(80, 18);
            this.floorName1.TabIndex = 7;
            this.floorName1.Text = "Floor 1";
            this.floorName1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // controlGroup
            //
            this.controlGroup.Controls.Add(this.panelDisplayLabel);
            this.controlGroup.Controls.Add(this.panelButton2);
            this.controlGroup.Controls.Add(this.panelButton1);
            this.controlGroup.Controls.Add(this.openDoorButton);
            this.controlGroup.Controls.Add(this.closeDoorButton);
            this.controlGroup.Controls.Add(this.stopButton);
            this.controlGroup.Controls.Add(this.alarmButton);
            this.controlGroup.Location = new System.Drawing.Point(10, 336);
            this.controlGroup.Name = "controlGroup";
            this.controlGroup.Size = new System.Drawing.Size(250, 150);
            this.controlGroup.TabIndex = 8;
            this.controlGroup.TabStop = false;
            this.controlGroup.Text = "Control panel (inside)";
            //
            // panelDisplayLabel
            //
            this.panelDisplayLabel.BackColor = System.Drawing.Color.Black;
            this.panelDisplayLabel.Font = new System.Drawing.Font("Consolas", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.panelDisplayLabel.ForeColor = System.Drawing.Color.Red;
            this.panelDisplayLabel.Location = new System.Drawing.Point(12, 22);
            this.panelDisplayLabel.Name = "panelDisplayLabel";
            this.panelDisplayLabel.Size = new System.Drawing.Size(70, 66);
            this.panelDisplayLabel.TabIndex = 0;
            this.panelDisplayLabel.Text = "1";
            this.panelDisplayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // panelButton2
            //
            this.panelButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.panelButton2.Location = new System.Drawing.Point(92, 22);
            this.panelButton2.Name = "panelButton2";
            this.panelButton2.Size = new System.Drawing.Size(44, 30);
            this.panelButton2.TabIndex = 1;
            this.panelButton2.Text = "2";
            this.panelButton2.UseVisualStyleBackColor = false;
            this.panelButton2.Click += new System.EventHandler(this.panelButton2_Click);
            //
            // panelButton1
            //
            this.panelButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.panelButton1.Location = new System.Drawing.Point(92, 58);
            this.panelButton1.Name = "panelButton1";
            this.panelButton1.Size = new System.Drawing.Size(44, 30);
            this.panelButton1.TabIndex = 2;
            this.panelButton1.Text = "1";
            this.panelButton1.UseVisualStyleBackColor = false;
            this.panelButton1.Click += new System.EventHandler(this.panelButton1_Click);
            //
            // openDoorButton
            //
            this.openDoorButton.Location = new System.Drawing.Point(146, 22);
            this.openDoorButton.Name = "openDoorButton";
            this.openDoorButton.Size = new System.Drawing.Size(92, 30);
            this.openDoorButton.TabIndex = 3;
            this.openDoorButton.Text = "Open door";
            this.openDoorButton.UseVisualStyleBackColor = true;
            this.openDoorButton.Click += new System.EventHandler(this.openDoorButton_Click);
            //
            // closeDoorButton
            //
            this.closeDoorButton.Location = new System.Drawing.Point(146, 58);
            this.closeDoorButton.Name = "closeDoorButton";
            this.closeDoorButton.Size = new System.Drawing.Size(92, 30);
            this.closeDoorButton.TabIndex = 4;
            this.closeDoorButton.Text = "Close door";
            this.closeDoorButton.UseVisualStyleBackColor = true;
            this.closeDoorButton.Click += new System.EventHandler(this.closeDoorButton_Click);
            //
            // stopButton
            //
            this.stopButton.BackColor = System.Drawing.Color.Firebrick;
            this.stopButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stopButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.stopButton.ForeColor = System.Drawing.Color.White;
            this.stopButton.Location = new System.Drawing.Point(12, 100);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(110, 38);
            this.stopButton.TabIndex = 5;
            this.stopButton.Text = "STOP";
            this.stopButton.UseVisualStyleBackColor = false;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            //
            // alarmButton
            //
            this.alarmButton.BackColor = System.Drawing.Color.DarkOrange;
            this.alarmButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.alarmButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.alarmButton.Location = new System.Drawing.Point(128, 100);
            this.alarmButton.Name = "alarmButton";
            this.alarmButton.Size = new System.Drawing.Size(110, 38);
            this.alarmButton.TabIndex = 6;
            this.alarmButton.Text = "ALARM";
            this.alarmButton.UseVisualStyleBackColor = false;
            this.alarmButton.Click += new System.EventHandler(this.alarmButton_Click);
            //
            // bannerLabel
            //
            this.bannerLabel.BackColor = System.Drawing.Color.Firebrick;
            this.bannerLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bannerLabel.ForeColor = System.Drawing.Color.White;
            this.bannerLabel.Location = new System.Drawing.Point(10, 492);
            this.bannerLabel.Name = "bannerLabel";
            this.bannerLabel.Size = new System.Drawing.Size(250, 36);
            this.bannerLabel.TabIndex = 9;
            this.bannerLabel.Text = "ALARM";
            this.bannerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // ElevatorView
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.shaftPanel);
            this.Controls.Add(this.floorDoor2);
            this.Controls.Add(this.floorDoor1);
            this.Controls.Add(this.floorDisplay2);
            this.Controls.Add(this.floorDisplay1);
            this.Controls.Add(this.floorName2);
            this.Controls.Add(this.floorName1);
            this.Controls.Add(this.controlGroup);
            this.Controls.Add(this.bannerLabel);
            this.Name = "ElevatorView";
            this.Size = new System.Drawing.Size(270, 530);
            this.shaftPanel.ResumeLayout(false);
            this.carPanel.ResumeLayout(false);
            this.controlGroup.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Panel shaftPanel;
        private System.Windows.Forms.Panel carPanel;
        private System.Windows.Forms.Panel doorLeftPanel;
        private System.Windows.Forms.Panel doorRightPanel;
        private System.Windows.Forms.Panel floorLinePanel2;
        private System.Windows.Forms.Panel floorLinePanel1;
        private System.Windows.Forms.Panel floorDoor2;
        private System.Windows.Forms.Panel floorDoor1;
        private System.Windows.Forms.Label floorDisplay2;
        private System.Windows.Forms.Label floorDisplay1;
        private System.Windows.Forms.Label floorName2;
        private System.Windows.Forms.Label floorName1;
        private System.Windows.Forms.GroupBox controlGroup;
        private System.Windows.Forms.Label panelDisplayLabel;
        private System.Windows.Forms.Button panelButton2;
        private System.Windows.Forms.Button panelButton1;
        private System.Windows.Forms.Button openDoorButton;
        private System.Windows.Forms.Button closeDoorButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button alarmButton;
        private System.Windows.Forms.Label bannerLabel;
    }
}
