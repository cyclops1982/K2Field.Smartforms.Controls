using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Browser;
using System.Text;
using System.IO;
using System.Windows.Media.Imaging;
using System.Reflection;

namespace SilverlightUpload
{
    [ScriptableType]
    public partial class MainPage : UserControl
    {

        private string _objectID;
        private Stream _reader;
        private MemoryStream _writer;
        private FileInfo _info;

        public MainPage()
        {
            InitializeComponent();
            AssemblyName assemblyName = new AssemblyName(Assembly.GetExecutingAssembly().FullName);
            version.Text = assemblyName.Version.ToString();
            BitmapImage bmp = new BitmapImage();
            bmp.UriSource = new Uri("http://k2.denallix.com/Runtime/Files/K2logo.png");
            imgThumb.Source = bmp;
        }

        public string ObjectID
        {
            get
            {
                return this._objectID;
            }
            set
            {
                this._objectID = value;
            }
        }


        [ScriptableMember(ScriptAlias = "info")]
        public FileInfo Info
        {
            get
            {
                return this._info;
            }
        }

        [ScriptableMember(ScriptAlias = "completedReading")]
        public bool CompletedReading()
        {
            if (this._reader != null)
            {
                return false;
            }
            return true;
        }

        [ScriptableMember(ScriptAlias = "byteArrayChunk")]
        public string ByteArrayChunk(string l)
        {
            int length = int.Parse(l);

            StringBuilder stringBuilder = new StringBuilder();
            int num = this._reader.ReadByte();
            for (int i = 0; num != -1 && i < length; i++)
            {
                stringBuilder.Append(",");
                stringBuilder.Append(num);
                num = this._reader.ReadByte();
            }
            if (num != -1)
            {
                stringBuilder.Append(",");
                stringBuilder.Append(num);
            }
            else
            {
                this._reader.Close();
                this._reader = null;
            }
            return stringBuilder.ToString();
        }

        [ScriptableMember(ScriptAlias = "fileName")]
        public string FileName
        {
            get
            {
                if (this._info != null)
                    return this._info.Name;
                return String.Empty;
            }
        }

        [ScriptableMember(ScriptAlias="loadImageFromUrl")]
        public void LoadImageFromUrl(string url)
        {
            ///Runtime/Runtime/File.ashx?_path=C:\Program Files (x86)\K2 blackpearl\K2 smartforms Runtime\Files\81ecdf95-c95b-4012-a526-ce77e77629da_K2_background_01g(1400x900).jpg&_controltype=image
            string lastPart = url.Substring(url.LastIndexOf('\\') + 1);
            string filename = lastPart.Substring(0, lastPart.IndexOf('&'));


            string fullUrl = App.Current.Host.Source.OriginalString;
            string imgUrl = fullUrl.Substring(0, fullUrl.IndexOf(App.Current.Host.Source.AbsolutePath)) + "/Runtime/Files/" + filename;


            BitmapImage bmp = new BitmapImage();
            bmp.ImageFailed += new EventHandler<ExceptionRoutedEventArgs>(bmp_ImageFailed);
            bmp.DownloadProgress += new EventHandler<DownloadProgressEventArgs>(bmp_DownloadProgress);
            bmp.UriSource = new Uri(imgUrl, UriKind.Absolute);
            imgThumb.BindingValidationError += new EventHandler<ValidationErrorEventArgs>(imgThumb_BindingValidationError);
            imgThumb.ImageFailed += new EventHandler<ExceptionRoutedEventArgs>(imgThumb_ImageFailed);
            imgThumb.Source = bmp;
        }

        void imgThumb_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            version.Text = "imgThumb_ImageFailed";
        }

        void imgThumb_BindingValidationError(object sender, ValidationErrorEventArgs e)
        {
            version.Text = "imgThumb_BindingValidationError";
        }

        void bmp_DownloadProgress(object sender, DownloadProgressEventArgs e)
        {
            version.Text += ".";
        }

        void bmp_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            version.Text += "bmp_ImageFailed";
        }

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Filter = "Text Files (.txt)|*.txt|JPEG Images|*.jpg|All Files (*.*)|*.*";
            dialog.FilterIndex = 1;

            // Call the ShowDialog method to show the dialog box.
            bool? userClickedOK = dialog.ShowDialog();

            // Process input if the user clicked OK.
            if (userClickedOK == true)
            {
                System.IO.Stream fileStream = dialog.File.OpenRead();

                using (System.IO.StreamReader reader = new System.IO.StreamReader(fileStream))
                {
                    _reader = dialog.File.OpenRead();
                    _info = dialog.File;
                    BitmapImage bmp = new BitmapImage();
                    bmp.SetSource(_reader);
                    imgThumb.Source = bmp;
                    _reader.Seek(0, SeekOrigin.Begin);

                    object[] objectID = new object[1];
                    objectID[0] = this.ObjectID;
                    HtmlPage.Window.Invoke("doSilverlightUpload", objectID);
                }
                fileStream.Close();
            }
        }
    }
}
