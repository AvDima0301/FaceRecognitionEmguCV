namespace FaceRecognition
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
            mainPhotoBox = new PictureBox();
            btn_add_face = new Button();
            faceBox = new PictureBox();
            label1 = new Label();
            l_faces_name = new Label();
            ((System.ComponentModel.ISupportInitialize)mainPhotoBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)faceBox).BeginInit();
            SuspendLayout();
            // 
            // mainPhotoBox
            // 
            mainPhotoBox.BorderStyle = BorderStyle.Fixed3D;
            mainPhotoBox.Location = new Point(26, 32);
            mainPhotoBox.Name = "mainPhotoBox";
            mainPhotoBox.Size = new Size(560, 424);
            mainPhotoBox.TabIndex = 0;
            mainPhotoBox.TabStop = false;
            mainPhotoBox.Click += mainPhotoBox_Click;
            // 
            // btn_add_face
            // 
            btn_add_face.Location = new Point(592, 195);
            btn_add_face.Name = "btn_add_face";
            btn_add_face.Size = new Size(160, 36);
            btn_add_face.TabIndex = 1;
            btn_add_face.Text = "Add new face";
            btn_add_face.UseVisualStyleBackColor = true;
            btn_add_face.Click += btn_add_face_Click;
            // 
            // faceBox
            // 
            faceBox.BorderStyle = BorderStyle.Fixed3D;
            faceBox.Location = new Point(592, 32);
            faceBox.Name = "faceBox";
            faceBox.Size = new Size(160, 157);
            faceBox.TabIndex = 2;
            faceBox.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.Location = new Point(26, 459);
            label1.Name = "label1";
            label1.Size = new Size(182, 25);
            label1.TabIndex = 4;
            label1.Text = "Presented on photo:";
            // 
            // l_faces_name
            // 
            l_faces_name.AutoSize = true;
            l_faces_name.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            l_faces_name.ForeColor = Color.Blue;
            l_faces_name.Location = new Point(26, 484);
            l_faces_name.Name = "l_faces_name";
            l_faces_name.Size = new Size(152, 25);
            l_faces_name.TabIndex = 5;
            l_faces_name.Text = "Please Add Face";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(780, 530);
            Controls.Add(l_faces_name);
            Controls.Add(label1);
            Controls.Add(faceBox);
            Controls.Add(btn_add_face);
            Controls.Add(mainPhotoBox);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)mainPhotoBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)faceBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox mainPhotoBox;
        private Button btn_add_face;
        private PictureBox faceBox;
        private Label label1;
        private Label l_faces_name;
    }
}
