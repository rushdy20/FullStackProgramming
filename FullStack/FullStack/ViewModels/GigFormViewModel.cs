﻿using FullStack.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FullStack.ViewModels
{
    public class GigFormViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Venue { get; set; }

        [Required]
        [FutureDate]
        public string Date { get; set; }

        [Required]
        [ValidTime]
        public string Time { get; set; }

        [Required]
        public byte Genre { get; set; }

        public IEnumerable<Genre> Genres { get; set; }
        public string Heading { get; set; }

        public string Action => (Id != 0 ? "Update" : "Create");

        public DateTime GetDateTime() { return DateTime.Parse(string.Format("{0} {1}", this.Date, this.Time)); }
    }
}