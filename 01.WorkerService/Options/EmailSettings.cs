using System;
using System.Collections.Generic;
using System.Text;

namespace Notification.WorkerService.Models
{
    public class EmailSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseHtml { get; set; }

    }
}
