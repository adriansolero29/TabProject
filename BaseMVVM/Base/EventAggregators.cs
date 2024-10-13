using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base
{
    public static class EventAggregators
    {
        public class SendExceptionNotification : PubSubEvent<string> { }

        public class PassData<T> : PubSubEvent<Payload<T>> where T : class 
        {

        }

        public class Payload<T> : PubSubEvent<T> where T : class
        {
            public T? Data { get; set; }
            public string? Command { get; set; }
        }
    }
}
