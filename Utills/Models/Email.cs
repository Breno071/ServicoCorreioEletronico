﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utills.Models
{
    public class Email
    {
        public string From { get; set; } = string.Empty;
        public string FromName { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
        public string ToName { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string FromPassword { get; set; } = "vcxi lfqj hkll icst";
        public List<string> Attachments { get; set; } = [];
        public bool Processed { get; set; } = false;
    }
}