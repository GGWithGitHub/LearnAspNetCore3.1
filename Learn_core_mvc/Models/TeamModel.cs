using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.Models
{
    public class TeamModel
    {
        public Guid Id { get; set; }
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public string Country { get; set; }
        public int Player100Count { get; set; }
    }
}
