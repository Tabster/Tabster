#region

using System;

#endregion

namespace Tabster.Update
{
    public class UpdateResponseEventArgs : EventArgs
    {
        public UpdateResponseEventArgs(UpdateResponse updateResponse, Exception exception, object userState)
        {
            Response = updateResponse;
            Error = exception;
            UserState = userState;
        }

        public UpdateResponse Response { get; private set; }
        public object UserState { get; private set; }
        public Exception Error { get; private set; }
    }
}