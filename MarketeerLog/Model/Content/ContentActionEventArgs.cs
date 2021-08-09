using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketeerLog.Model.Content
{
    public class ContentActionEventArgs : EventArgs
    {

        public ContentActionState actionState { get; set; }

        public ContentState contentState { get; set; }

        public ContentActionEventArgs(ContentActionState actionState, ContentState contentState)
        {
            this.actionState = actionState;
            this.contentState = contentState;
        }
    }
}
