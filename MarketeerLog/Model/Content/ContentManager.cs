using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace MarketeerLog.Model.Content
{
    public enum ContentActionState { OPEN, SAVE };
    public static class ContentManager
    {

        public static event EventHandler<ContentActionEventArgs> ContentAction;

        public static void Save(ContentState state)
        {
            Stream stream;
            SaveFileDialog saveDialog = new SaveFileDialog();
            
            if(App.Instance.AppConfigData.LastFileLocation != null)
            {
                saveDialog.InitialDirectory = App.Instance.AppConfigData.LastFileLocation;
            }
            saveDialog.Filter = "mldb files(*mldb)|";
            saveDialog.FilterIndex = 1;
            saveDialog.RestoreDirectory = true;
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                if ((stream = saveDialog.OpenFile()) != null)
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, state);
                    ContentAction?.Invoke(null, new ContentActionEventArgs(ContentActionState.SAVE, state));
                    stream.Flush();


                }
                stream.Close();
                stream.Dispose();
            }
     
        }
        public static bool Open(ref ContentState state)
        {
            Stream stream;
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "mldb files(*mldb)|";
            openDialog.FilterIndex = 1;
            openDialog.RestoreDirectory = true;
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                if ((stream = openDialog.OpenFile()) != null)
                {
                    App.Instance.AppConfigData.LastFileLocation = Path.GetDirectoryName(openDialog.FileName);
                    BinaryFormatter formatter = new BinaryFormatter();
    
                    
                    ContentState content = (ContentState)formatter.Deserialize(stream);
                    ContentAction?.Invoke(null, new ContentActionEventArgs(ContentActionState.OPEN, content));
                    state = content;
                    DisposeStream(ref stream);
                    return true;

                }

                DisposeStream(ref stream);
            }
            return false;
        }

        private static void DisposeStream(ref Stream stream)
        {
            stream.Flush();
            stream.Dispose();
            stream.Close();
        }
    }
}
