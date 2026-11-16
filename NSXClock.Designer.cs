namespace Clockv2.nn
{
    partial class NESixClock
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        //components = new System.ComponentModel.Container();
        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NESixClock));
            spiralOptionsMenu = new ContextMenuStrip(components);
            isHandsOn = new ToolStripMenuItem();
            isEnds = new ToolStripMenuItem();
            isSeconds = new ToolStripMenuItem();
            isOrdinals = new ToolStripMenuItem();
            isTextOn = new ToolStripMenuItem();
            is7segment = new ToolStripMenuItem();
            isfullSevenSeg = new ToolStripMenuItem();
            isQSevenSeg = new ToolStripMenuItem();
            isHalfSevenSeg = new ToolStripMenuItem();
            isTransparentMenu = new ToolStripMenuItem();
            isBorderedMenu = new ToolStripMenuItem();
            isAnimated = new ToolStripMenuItem();
            isNumbers = new ToolStripMenuItem();
            isNumerals = new ToolStripMenuItem();
            rotateToolStripMenuItem = new ToolStripMenuItem();
            webserverCheck = new ToolStripMenuItem();
            showLogToolStripMenuItem = new ToolStripMenuItem();
            isDateShown = new ToolStripMenuItem();
            IsInverse = new ToolStripMenuItem();
            SchemePick = new ToolStripComboBox();
            timeSizelMenu = new ToolStripMenuItem();
            smallMenu = new ToolStripMenuItem();
            medTextMenu = new ToolStripMenuItem();
            largeTextMenu = new ToolStripMenuItem();
            largerTextMenu = new ToolStripMenuItem();
            autoTextSize = new ToolStripMenuItem();
            isCentreDome = new ToolStripMenuItem();
            isFixedDome = new ToolStripMenuItem();
            isPulsedDome = new ToolStripMenuItem();
            filledText = new ToolStripMenuItem();
            isAnalog = new ToolStripMenuItem();
            makeTestsMenu = new ToolStripMenuItem();
            Calendar = new ToolStripMenuItem();
            dailyWeeklyToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            ClockPBox = new PictureBox();
            
            spiralOptionsMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ClockPBox).BeginInit();
            SuspendLayout();
            // 
            // spiralOptionsMenu
            // 
            spiralOptionsMenu.Items.AddRange(new ToolStripItem[] { isHandsOn, isEnds, isSeconds, isOrdinals, isTextOn, is7segment, isAnimated, isNumbers, isNumerals, rotateToolStripMenuItem, webserverCheck, showLogToolStripMenuItem, isDateShown, IsInverse, SchemePick, timeSizelMenu, isCentreDome, filledText, isAnalog, makeTestsMenu, exitToolStripMenuItem });
            spiralOptionsMenu.Name = "spiralOptionsMenu";
            spiralOptionsMenu.Size = new Size(182, 557);
            spiralOptionsMenu.Opening += spiralOptionsMenu_Opening;
            spiralOptionsMenu.Click += spiralOptionsMenu_Click;
            // 
            // isHandsOn
            // 
            isHandsOn.Checked = true;
            isHandsOn.CheckOnClick = true;
            isHandsOn.CheckState = CheckState.Checked;
            isHandsOn.Name = "isHandsOn";
            isHandsOn.Size = new Size(181, 26);
            isHandsOn.Text = "&Hands";
            isHandsOn.Click += isHandsOn_Click;
            // 
            // isEnds
            // 
            isEnds.Checked = true;
            isEnds.CheckOnClick = true;
            isEnds.CheckState = CheckState.Checked;
            isEnds.Name = "isEnds";
            isEnds.Size = new Size(181, 26);
            isEnds.Text = "&Ends?";
            isEnds.Click += endsMenu_Click;
            // 
            // isSeconds
            // 
            isSeconds.Checked = true;
            isSeconds.CheckOnClick = true;
            isSeconds.CheckState = CheckState.Checked;
            isSeconds.Name = "isSeconds";
            isSeconds.Size = new Size(181, 26);
            isSeconds.Text = "&Seconds";
            isSeconds.Click += isSeconds_Click;
            // 
            // isOrdinals
            // 
            isOrdinals.Checked = true;
            isOrdinals.CheckOnClick = true;
            isOrdinals.CheckState = CheckState.Checked;
            isOrdinals.Name = "isOrdinals";
            isOrdinals.Size = new Size(181, 26);
            isOrdinals.Text = "&Ordinals";
            isOrdinals.Click += isOrdinals_Click;
            // 
            // isTextOn
            // 
            isTextOn.CheckOnClick = true;
            isTextOn.Name = "isTextOn";
            isTextOn.Size = new Size(181, 26);
            isTextOn.Text = "Text?";
            isTextOn.Click += isTextOn_Click;
            // 
            // is7segment
            // 
            is7segment.Checked = true;
            is7segment.CheckOnClick = true;
            is7segment.CheckState = CheckState.Checked;
            is7segment.DropDownItems.AddRange(new ToolStripItem[] { isfullSevenSeg, isQSevenSeg, isHalfSevenSeg, isTransparentMenu, isBorderedMenu });
            is7segment.Name = "is7segment";
            is7segment.Size = new Size(181, 26);
            is7segment.Text = "&7 Segment";
            is7segment.Click += is7segment_Click;
            // 
            // isfullSevenSeg
            // 
            isfullSevenSeg.CheckOnClick = true;
            isfullSevenSeg.Name = "isfullSevenSeg";
            isfullSevenSeg.Size = new Size(192, 26);
            isfullSevenSeg.Text = "&Full";
            isfullSevenSeg.Click += isfullSevenSeg_Click;
            // 
            // isQSevenSeg
            // 
            isQSevenSeg.CheckOnClick = true;
            isQSevenSeg.Name = "isQSevenSeg";
            isQSevenSeg.Size = new Size(192, 26);
            isQSevenSeg.Text = "Top Left Quarter";
            isQSevenSeg.Click += TopLeft7SegMenu_Click;
            // 
            // isHalfSevenSeg
            // 
            isHalfSevenSeg.Checked = true;
            isHalfSevenSeg.CheckOnClick = true;
            isHalfSevenSeg.CheckState = CheckState.Checked;
            isHalfSevenSeg.Name = "isHalfSevenSeg";
            isHalfSevenSeg.Size = new Size(192, 26);
            isHalfSevenSeg.Text = "&Half";
            isHalfSevenSeg.Click += isHalfSevenSeg_Click;
            // 
            // isTransparentMenu
            // 
            isTransparentMenu.Checked = true;
            isTransparentMenu.CheckOnClick = true;
            isTransparentMenu.CheckState = CheckState.Checked;
            isTransparentMenu.Name = "isTransparentMenu";
            isTransparentMenu.Size = new Size(192, 26);
            isTransparentMenu.Text = "Transparent";
            isTransparentMenu.Click += isTransparentMenu_Click;
            // 
            // isBorderedMenu
            // 
            isBorderedMenu.Checked = true;
            isBorderedMenu.CheckOnClick = true;
            isBorderedMenu.CheckState = CheckState.Checked;
            isBorderedMenu.Name = "isBorderedMenu";
            isBorderedMenu.Size = new Size(192, 26);
            isBorderedMenu.Text = "Border";
            // 
            // isAnimated
            // 
            isAnimated.BackColor = SystemColors.ButtonFace;
            isAnimated.CheckOnClick = true;
            isAnimated.Name = "isAnimated";
            isAnimated.Size = new Size(181, 26);
            isAnimated.Text = "&Animate?";
            isAnimated.Click += isAnimated_Click;
            // 
            // isNumbers
            // 
            isNumbers.Checked = true;
            isNumbers.CheckOnClick = true;
            isNumbers.CheckState = CheckState.Checked;
            isNumbers.DisplayStyle = ToolStripItemDisplayStyle.Text;
            isNumbers.Name = "isNumbers";
            isNumbers.Size = new Size(181, 26);
            isNumbers.Text = "Nu&mbers";
            isNumbers.CheckedChanged += isNumbers_CheckedChanged;
            isNumbers.Click += isNumbers_Click;
            // 
            // isNumerals
            // 
            isNumerals.CheckOnClick = true;
            isNumerals.Name = "isNumerals";
            isNumerals.Size = new Size(181, 26);
            isNumerals.Text = "&Numerals?";
            isNumerals.Click += isNumerals_Click;
            // 
            // rotateToolStripMenuItem
            // 
            rotateToolStripMenuItem.CheckOnClick = true;
            rotateToolStripMenuItem.Name = "rotateToolStripMenuItem";
            rotateToolStripMenuItem.Size = new Size(181, 26);
            rotateToolStripMenuItem.Text = "&Rotate?";
            rotateToolStripMenuItem.Click += rotateToolStripMenuItem_Click;
            // 
            // webserverCheck
            // 
            webserverCheck.CheckOnClick = true;
            webserverCheck.Name = "webserverCheck";
            webserverCheck.Size = new Size(181, 26);
            webserverCheck.Text = "Webserver";
            webserverCheck.CheckedChanged += webserverToolStripMenuItem_CheckedChanged;
            webserverCheck.Click += webserverToolStripMenuItem_Click;
            // 
            // showLogToolStripMenuItem
            // 
            showLogToolStripMenuItem.CheckOnClick = true;
            showLogToolStripMenuItem.Name = "showLogToolStripMenuItem";
            showLogToolStripMenuItem.Size = new Size(181, 26);
            showLogToolStripMenuItem.Text = "&Show Log";
            showLogToolStripMenuItem.Click += showLogToolStripMenuItem_Click;
            // 
            // isDateShown
            // 
            isDateShown.Checked = true;
            isDateShown.CheckOnClick = true;
            isDateShown.CheckState = CheckState.Checked;
            isDateShown.Name = "isDateShown";
            isDateShown.Size = new Size(181, 26);
            isDateShown.Text = "Date title?";
            isDateShown.Click += isDateShown_Click;
            // 
            // IsInverse
            // 
            IsInverse.Checked = true;
            IsInverse.CheckOnClick = true;
            IsInverse.CheckState = CheckState.Checked;
            IsInverse.Name = "IsInverse";
            IsInverse.Size = new Size(181, 26);
            IsInverse.Text = "&Inverse";
            IsInverse.Click += isInverse_Click;
            // 
            // SchemePick
            // 
            SchemePick.Items.AddRange(new object[] { "B/W", "W/B", "Grey", "B/W2", "Colorful" });
            SchemePick.Name = "SchemePick";
            SchemePick.Size = new Size(121, 29);
            SchemePick.Text = "Color Scheme";
            SchemePick.SelectedIndexChanged += schemePick_SelectedIndexChanged;
            // 
            // timeSizelMenu
            // 
            timeSizelMenu.DropDownItems.AddRange(new ToolStripItem[] { smallMenu, medTextMenu, largeTextMenu, largerTextMenu, autoTextSize });
            timeSizelMenu.Name = "timeSizelMenu";
            timeSizelMenu.Size = new Size(181, 26);
            timeSizelMenu.Text = "Time Si&ze";
            // 
            // smallMenu
            // 
            smallMenu.Name = "smallMenu";
            smallMenu.Size = new Size(121, 26);
            smallMenu.Text = "&Small";
            smallMenu.Click += smallFont_Click;
            // 
            // medTextMenu
            // 
            medTextMenu.Name = "medTextMenu";
            medTextMenu.Size = new Size(121, 26);
            medTextMenu.Text = "&Med";
            medTextMenu.Click += medFont_Click;
            // 
            // largeTextMenu
            // 
            largeTextMenu.Name = "largeTextMenu";
            largeTextMenu.Size = new Size(121, 26);
            largeTextMenu.Text = "&Large";
            largeTextMenu.Click += largeFont_Click;
            // 
            // largerTextMenu
            // 
            largerTextMenu.Name = "largerTextMenu";
            largerTextMenu.Size = new Size(121, 26);
            largerTextMenu.Text = "lar&ger";
            largerTextMenu.Click += largerFont_Click;
            // 
            // autoTextSize
            // 
            autoTextSize.Name = "autoTextSize";
            autoTextSize.Size = new Size(121, 26);
            autoTextSize.Text = "&Auto";
            autoTextSize.Click += autoFontSize_Click;
            // 
            // isCentreDome
            // 
            isCentreDome.DropDownItems.AddRange(new ToolStripItem[] { isFixedDome, isPulsedDome });
            isCentreDome.Name = "isCentreDome";
            isCentreDome.Size = new Size(181, 26);
            isCentreDome.Text = "&Centre Dome";
            isCentreDome.Click += isCentreDome_Click;
            // 
            // isFixedDome
            // 
            isFixedDome.Checked = true;
            isFixedDome.CheckOnClick = true;
            isFixedDome.CheckState = CheckState.Checked;
            isFixedDome.Name = "isFixedDome";
            isFixedDome.Size = new Size(117, 26);
            isFixedDome.Text = "&Fixed";
            isFixedDome.Click += isFixedDome_Click;
            // 
            // isPulsedDome
            // 
            isPulsedDome.CheckOnClick = true;
            isPulsedDome.Name = "isPulsedDome";
            isPulsedDome.Size = new Size(117, 26);
            isPulsedDome.Text = "&Pulse";
            isPulsedDome.Click += isPulsedDome_Click;
            // 
            // filledText
            // 
            filledText.Checked = true;
            filledText.CheckOnClick = true;
            filledText.CheckState = CheckState.Checked;
            filledText.Name = "filledText";
            filledText.Size = new Size(181, 26);
            filledText.Text = "Filled Text?";
            filledText.Click += filledText_Click;
            // 
            // isAnalog
            // 
            isAnalog.Checked = true;
            isAnalog.CheckOnClick = true;
            isAnalog.CheckState = CheckState.Checked;
            isAnalog.Name = "isAnalog";
            isAnalog.Size = new Size(181, 26);
            isAnalog.Text = "&Analogue";
            isAnalog.Click += isAnalog_Click;
            // 
            // makeTestsMenu
            // 
            makeTestsMenu.DropDownItems.AddRange(new ToolStripItem[] { Calendar, dailyWeeklyToolStripMenuItem });
            makeTestsMenu.Name = "makeTestsMenu";
            makeTestsMenu.Size = new Size(181, 26);
            makeTestsMenu.Text = "&Alarms";
            makeTestsMenu.Click += makeTests_Click;
            // 
            // Calendar
            // 
            Calendar.Name = "Calendar";
            Calendar.Size = new Size(171, 26);
            Calendar.Text = "&Schedule";
            Calendar.Click += Calendar_Click;
            // 
            // dailyWeeklyToolStripMenuItem
            // 
            dailyWeeklyToolStripMenuItem.Name = "dailyWeeklyToolStripMenuItem";
            dailyWeeklyToolStripMenuItem.Size = new Size(171, 26);
            dailyWeeklyToolStripMenuItem.Text = "Daily/Weekly";
            dailyWeeklyToolStripMenuItem.Click += dailyWeeklyToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(181, 26);
            exitToolStripMenuItem.Text = "E&xit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // ClockPBox
            // 
            ClockPBox.ContextMenuStrip = spiralOptionsMenu;
            ClockPBox.Dock = DockStyle.Fill;
            ClockPBox.Location = new Point(0, 0);
            ClockPBox.Margin = new Padding(10, 6, 10, 6);
            ClockPBox.MinimumSize = new Size(96, 96);
            ClockPBox.Name = "ClockPBox";
            ClockPBox.Padding = new Padding(2);
            ClockPBox.Size = new Size(910, 621);
            ClockPBox.TabIndex = 1;
            ClockPBox.TabStop = false;
            ClockPBox.Click += pictureBox1_Click;
            ClockPBox.Paint += NSXClockPicbox_Paint;
            ClockPBox.DoubleClick += pictureBox1_DoubleClick;
            ClockPBox.MouseHover += ClockPBox_MouseHover;
            ClockPBox.MouseMove += ClockPBox_MouseMove;
            // 
            // 
            // NESixClock
            // 
            AutoScaleDimensions = new SizeF(17F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(910, 621);
            Controls.Add(ClockPBox);
            Font = new Font("Courier New", 21.75F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(10, 6, 10, 6);
            MinimumSize = new Size(180, 180);
            Name = "NESixClock";
            Text = "...";
            Load += NSXClock_Load;
            DoubleClick += NSXClock_DoubleClick;
            MouseDown += NESixClock_MouseDown;
            MouseHover += NESixClock_MouseHover;
            MouseUp += NESixClock_MouseUp;
            Move += NSXClock_Move;
            Resize += NSXClock_Resize;
            spiralOptionsMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)ClockPBox).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private ToolStripMenuItem isNumerals;
        private ToolStripMenuItem rotateToolStripMenuItem;
        private ToolStripMenuItem showLogToolStripMenuItem;
        public ToolStripMenuItem webserverCheck;
        private ToolStripMenuItem isAnimated;
        private ToolStripMenuItem IsInverse;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripComboBox SchemePick;
        private ToolStripMenuItem isHandsOn;
        private ToolStripMenuItem isDateShown;
        private ToolStripMenuItem isTextOn;
        private ToolStripMenuItem filledText;
        private ToolStripMenuItem isAnalog;
        private ToolStripMenuItem isNumbers;
        private ToolStripMenuItem isOrdinals;
        private ToolStripMenuItem isSeconds;
        private ToolStripMenuItem isEnds;
        private ToolStripMenuItem timeSizelMenu;
        private ToolStripMenuItem smallMenu;
        private ToolStripMenuItem medTextMenu;
        private ToolStripMenuItem largeTextMenu;
        private ToolStripMenuItem isCentreDome;
        private ToolStripMenuItem largerTextMenu;
        private ToolStripMenuItem isFixedDome;
        private ToolStripMenuItem isPulsedDome;
        public PictureBox ClockPBox;
        private ToolStripMenuItem autoTextSize;
        private ToolStripMenuItem makeTestsMenu;
        private ToolStripMenuItem Calendar;
        private ToolStripMenuItem dailyWeeklyToolStripMenuItem;
        private ToolStripMenuItem is7segment;
        public ContextMenuStrip spiralOptionsMenu;
        private ToolStripMenuItem isfullSevenSeg;
        private ToolStripMenuItem isQSevenSeg;
        private ToolStripMenuItem isHalfSevenSeg;
        private ToolStripMenuItem isTransparentMenu;
        private ToolStripMenuItem isBorderedMenu;
        private NotifyIcon notificationIcon;
    }
}