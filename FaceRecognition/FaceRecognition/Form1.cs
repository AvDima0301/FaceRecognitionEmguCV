using Emgu.CV;
using System.Windows.Forms;
using Emgu.CV.CvEnum;
using Emgu.CV.UI;
using Emgu.CV.Structure;
using System.Drawing.Imaging;
using Emgu.CV.Face;
using Emgu.CV.Util;
using Emgu.CV.Ocl;
using System.Windows;

namespace FaceRecognition
{
    public partial class Form1 : Form
    {
        static readonly CascadeClassifier haarCascade = new CascadeClassifier("haarcascade_frontalface_default.xml");
        public static string FacePhotosPath = Environment.CurrentDirectory + "\\Faces\\";
        public static string FaceListTextFile = Environment.CurrentDirectory + "\\Faces\\FaceList.txt";
        public static string ImageFileExtension = ".bmp";

        private List<FaceData> faceList = new List<FaceData>();
        private VectorOfMat imageList = new VectorOfMat();
        private List<string> nameList = new List<string>();
        private VectorOfInt labelList = new VectorOfInt();
        private Image<Gray, Byte> detectedFace = null;
        private List<Image<Gray, Byte>> detectedFaces = new List<Image<Gray, byte>>();

        private EigenFaceRecognizer recognizer;

        public Form1()
        {
            InitializeComponent();
            mainPhotoBox.SizeMode = PictureBoxSizeMode.Zoom;
            faceBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void mainPhotoBox_Click(object sender, EventArgs e)
        {
            getFacesList();
            using (OpenFileDialog ofd = new OpenFileDialog() { Multiselect = false, Filter = "JPEG|*.jpg" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        mainPhotoBox.Image = Image.FromFile(ofd.FileName);
                        detectFace(Image.FromFile(ofd.FileName));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
            }

        }

        private void getFacesList()
        {
            faceList.Clear();
            FaceData faceInstance = null;
            string line;
            if (!Directory.Exists(FacePhotosPath))
            {
                Directory.CreateDirectory(FacePhotosPath);
            }
            if (!File.Exists(FaceListTextFile))
            {
                string text = "Cannot find face data file:\n\n";
                text += FaceListTextFile + "\n\n";
                text += "If this is your first time running the app, an empty file will be created for you.";

                DialogResult result = MessageBox.Show(text, "Warning", MessageBoxButtons.OK);
                switch (result)
                {
                    case DialogResult.OK:
                        String dirName = Path.GetDirectoryName(FaceListTextFile);
                        Directory.CreateDirectory(dirName);
                        File.Create(FaceListTextFile).Close();
                        break;
                }
            }
            StreamReader reader = new StreamReader(FaceListTextFile);
            int i = 0;
            while ((line = reader.ReadLine()) != null)
            {
                string[] lineParts = line.Split(':');
                faceInstance = new FaceData();
                faceInstance.FaceImage = new Image<Gray, byte>(FacePhotosPath + lineParts[0] + ImageFileExtension);
                faceInstance.PersonName = lineParts[1];
                faceList.Add(faceInstance);
            }
            foreach (var face in faceList)
            {
                imageList.Push(face.FaceImage.Mat);
                nameList.Add(face.PersonName);
                labelList.Push(new[] { i++ });
            }
            reader.Close();

            if (imageList.Size > 0)
            {
                recognizer = new EigenFaceRecognizer(imageList.Size);
                recognizer.Train(imageList, labelList);
            }

        }

        private void detectFace(Image img)
        {
            getFacesList();
            detectedFaces = new List<Image<Gray, byte>>();
            Image<Gray, byte> face = null;
            Bitmap bm = new Bitmap(img);
            Image<Bgr, Byte> imgBrg = bm.ToImage<Bgr, byte>();
            Image<Gray, byte> grayframe = imgBrg.Convert<Gray, byte>();

            Rectangle[] faces = haarCascade.DetectMultiScale(grayframe, 1.3, 3);

            Bitmap bitmap = imgBrg.ToBitmap();
            if (faces.Length != 0)
            {
                foreach (Rectangle rectangle in faces)
                {
                    using (Graphics graphics = Graphics.FromImage(bitmap))
                    {
                        using (Pen pen = new Pen(Brushes.Red))
                        {
                            graphics.DrawRectangle(pen, rectangle);
                        }
                    }
                    grayframe.ROI = rectangle;
                    face = grayframe.CopyBlank();
                    grayframe.CopyTo(face);
                    grayframe.ROI = Rectangle.Empty;
                    detectedFaces.Add(face);
                }
                mainPhotoBox.Image = bitmap;

                grayframe.ROI = faces[0];
                face = grayframe.CopyBlank();
                grayframe.CopyTo(face);
                grayframe.ROI = Rectangle.Empty;
                detectedFace = face;
                faceBox.Image = face.ToBitmap();
            }
            FaceRecognition();
        }

        private void btn_add_face_Click(object sender, EventArgs e)
        {
            getFacesList();
            if (detectedFace == null)
            {
                MessageBox.Show("No face detected.");
                return;
            }
            //Save detected face
            detectedFace = detectedFace.Resize(100, 100, Inter.Cubic);
            detectedFace.Save(FacePhotosPath + "face" + (faceList.Count + 1) + ImageFileExtension);
            StreamWriter writer = new StreamWriter(FaceListTextFile, true);
            string personName = Microsoft.VisualBasic.Interaction.InputBox("Your Name");
            writer.WriteLine(String.Format("face{0}:{1}", (faceList.Count + 1), personName));
            writer.Close();
            getFacesList();
            MessageBox.Show("Successful.");
            Application.Restart();
            Environment.Exit(0);
        }

        private void FaceRecognition()
        {
            getFacesList();
            string names = "";
            if (imageList.Size != 0)
            {
                //Eigen Face Algorithm
                foreach (var detectFace in detectedFaces)
                {
                    FaceRecognizer.PredictionResult result = recognizer.Predict(detectFace.Resize(100, 100, Inter.Cubic));


                    //if (result.Distance <=  faceList.Count*3300)
                    names += nameList[result.Label] + "(" + result.Distance + ")" + ", ";
                    //else
                    //    names += "unknown, ";
                }
                if (names.Length != 0)
                {
                    names = names.Remove(names.Length - 1);
                    names = names.Remove(names.Length - 1);
                }
            }
            else
            {
                names = "Please Add Face";
            }
            l_faces_name.Text = names;
        }

    }
}
