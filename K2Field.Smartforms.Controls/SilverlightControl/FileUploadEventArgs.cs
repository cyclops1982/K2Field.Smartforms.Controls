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
            string[] chunks = args.Split(';');
            for (int i = 0; i < chunks.Length; i++)
            {
                string[] chunkSplit = chunks[i].Split('=');
                if (chunkSplit[0] == "oldFileName")
                {
                    this.OldFileName = chunkSplit[1];
                }
                if (chunkSplit[0] == "newFileName")
                {
                    this.NewFileName = chunkSplit[1];
                }
                if (chunkSplit[0] == "fileContents")
                {
                    this.FileContents = chunkSplit[1];
                }
            }
        }
    }
}
