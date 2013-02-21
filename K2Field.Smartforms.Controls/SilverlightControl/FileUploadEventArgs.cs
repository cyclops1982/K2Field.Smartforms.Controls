using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace K2Field.Smartforms.Controls.SilverlightControl
{
    public class FileUploadEventArgs : EventArgs
    {
        public string OldFileName
        {
            get;
            private set;
        }
        public string NewFileName
        {
            get;
            private set;
        }
        public string FileContents
        {
            get;
            private set;
        }
        public string CallbackResult
        {
            get;
            set;
        }
        public FileUploadEventArgs(string args)
        {
            if (args == null)
            {
                throw new ArgumentNullException("args");
            }
            string[] array = args.Split(';');
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].Split('=')[0] == "oldFileName")
                {
                    this.OldFileName = array[i].Split('=')[1];
                }
                else
                {
                    if (array[i].Split('=')[0] == "newFileName")
                    {
                        this.NewFileName = array[i].Split('=')[1];
                    }
                    else
                    {
                        if (array[i].Split('=')[0] == "fileContents")
                        {
                            this.FileContents = array[i].Split('=')[1];
                        }
                    }
                }
            }
        }
    }
}
