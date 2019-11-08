using System;

namespace DOMAIN.Abtracts
{
    public interface IAuditable
    {
        string CreateBy { get; set; }

        DateTime? CreateDate { get; set; }

        string UpdateBy { get; set; }

        DateTime? UpdateDate { get; set; }

        bool Status { get; set; }
    }
}