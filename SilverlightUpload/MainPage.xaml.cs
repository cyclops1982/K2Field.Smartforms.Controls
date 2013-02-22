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

namespace SilverlightUpload
{
    [ScriptableType]
    public partial class MainPage : UserControl
    {

        private string _objectID;
        private Stream _reader;
        private FileInfo _info;

        public MainPage()
        {
            InitializeComponent();
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
            else
            {
                return true;
            }
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
                string empty;
                string name;
                try
                {
                    if (this._info != null)
                    {
                        name = this._info.Name;
                    }
                    else
                    {
                        name = string.Empty;
                    }
                    empty = name;
                }
                catch (Exception exception)
                {
                    empty = string.Empty;
                }
                return empty;
            }
        }



        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
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
                    object[] objectID = new object[1];
                    objectID[0] = this.ObjectID;
                    HtmlPage.Window.Invoke("doSilverlightUpload", objectID);
                }
                fileStream.Close();
            }
        }
    }
}
