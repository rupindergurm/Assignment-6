﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CareerCloud.Pocos
{
    [Table("Applicant_Job_Applications")]
    public class ApplicantJobApplicationPoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }
        public Guid Applicant { get; set; }
        public Guid Job { get; set; }
        [Column("Application_Date")]
        public DateTime ApplicationDate { get; set; }
        [Column("Time_Stamp")]
        [NotMapped]
        public Byte[] TimeStamp { get; set; }
        //public DateTime Application_Date { get; set; }
        public virtual ApplicantProfilePoco ApplicantProfiles { get; set; }

        public virtual CompanyJobPoco CompanyJobs { get; set; }
    }
}


