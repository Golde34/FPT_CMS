using Server.Entity.Enum;
using System.ComponentModel.DataAnnotations;

namespace Server.Entity
{
    public class Grade
    {
        public int GradeId { get; set; }
        public GradeStatus Status { get; set; }
        public decimal Value { get; set; }
    }
}
