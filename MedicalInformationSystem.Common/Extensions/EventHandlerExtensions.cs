using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalInformationSystem.Common.Extensions
{
    public static class EventHandlerExtensions
    {
        public static void RaiseEvent(this EventHandler eventHandler, object sender)
        {
            var handler = eventHandler;

            handler?.Invoke(sender, EventArgs.Empty);
        }

        public static void RaiseEvent<T>(this EventHandler<T> eventHandler, object sender, T e) where T : EventArgs
        {
            var handler = eventHandler;

            handler?.Invoke(sender, e);
        }
    }
}
