﻿using Emgu.CV.Structure;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognition
{
    internal class FaceData
    {
        public string PersonName { get; set; }
        public Image<Gray, byte> FaceImage { get; set; }
    }
}
