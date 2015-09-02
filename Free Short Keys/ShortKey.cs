﻿using System;
using System.ComponentModel;

namespace Free_Short_Keys
{
    public class ShortKey
    {
        [DisplayName("Id")]
        public Guid Id { get; set; } = Guid.NewGuid();
        [DisplayName("Key")]
        public string Key { get; set; } = string.Empty;
        [DisplayName("Replacement Key")]
        public string ReplacementKey { get; set; } = string.Empty;
        [DisplayName("Suffix")]
        public string CustomSuffix { get; set; } = string.Empty;
        [DisplayName("Notes")]
        public string Notes { get; set; } = string.Empty;
        [DisplayName("Created")]
        public DateTime Created { get; set; } = DateTime.Now;
        [DisplayName("Updated")]
        public DateTime Updated { get; set; } = DateTime.Now;
        [DisplayName("Use Clipboard")]
        public bool UseClipboard { get; set; } = false;
    }
}